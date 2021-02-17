using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLK.Mvvm
{
    public sealed partial class Binding : IDisposable
    {
        // Fields
        private readonly BindingMode _bindingMode = BindingMode.Default;

        private readonly IValueConverter _valueConverter = null;

        private Expression<Func<object>> _sourceExpression = null;

        private Expression<Func<object>> _targetExpression = null;

        private PropertyInfo _sourcePropertyInfo = null;        

        private PropertyInfo _targetPropertyInfo = null;

        private object _sourceInstance = null;

        private object _targetInstance = null;

        private bool _isDisposed = false;


        // Constructors
        internal Binding(Expression<Func<object>> source, Expression<Func<object>> target, BindingMode bindingMode = BindingMode.Default, IValueConverter valueConverter = null)
        {
            #region Contracts

            if (source == null) throw new ArgumentNullException();
            if (target == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _bindingMode = bindingMode;
            _valueConverter = valueConverter;

            // Initialize
            this.InitializeSource(source);
            this.InitializeTarget(target);

            // Update
            this.TargetPropertyValue = this.SourcePropertyValue;
        }

        ~Binding()
        {
            // Dispose
            this.Dispose();
        }

        public void Dispose()
        {
            // Require
            if (_isDisposed == true) return;
            _isDisposed = true;

            // SourceInterface
            try
            {
                var sourceInterface = _sourceInstance as INotifyPropertyChanged;
                if (sourceInterface != null)
                {
                    sourceInterface.PropertyChanged -= this.SourceInstance_PropertyChanged;
                }
            }
            catch { }

            // TargetInterface
            try
            {
                var targetInterface = _targetInstance as INotifyPropertyChanged;
                if (targetInterface != null)
                {
                    targetInterface.PropertyChanged -= this.TargetInstance_PropertyChanged;
                }
            }
            catch { }

            // Notify
            try
            {
                this.OnBindingDisposed();
            }
            catch { }
        }


        // Properties
        private object SourcePropertyValue
        {
            get
            {
                return _sourceExpression.Compile()();
            }
            set
            {
                this.SetValue(_sourceInstance, _sourcePropertyInfo, value);
            }
        }

        private object TargetPropertyValue
        {
            get
            {
                return _targetExpression.Compile()();
            }
            set
            {
                this.SetValue(_targetInstance, _targetPropertyInfo, value);
            }
        }
        

        // Methods
        public void UpdateToSource()
        {
            // Require
            switch(_bindingMode)
            {
                case BindingMode.Default: return;
                case BindingMode.OneTime: return;
                case BindingMode.OneWay: return;
                case BindingMode.TwoWay: break;                    
            }
            if (_isDisposed == true) return;

            // Update
            object value = this.TargetPropertyValue;
            if (_valueConverter == null)
            {
                this.SourcePropertyValue = value;
            }
            else
            {
                this.SourcePropertyValue = _valueConverter.ToSource(value);
            }  
        }

        public void UpdateToTarget()
        {
            // Require
            switch (_bindingMode)
            {
                case BindingMode.Default: break;
                case BindingMode.OneTime: return;
                case BindingMode.OneWay: break;
                case BindingMode.TwoWay: break;
            }
            if (_isDisposed == true) return;

            // Update
            object value = this.SourcePropertyValue;
            if (_valueConverter == null)
            {
                this.TargetPropertyValue = value;
            }
            else
            {
                this.TargetPropertyValue = _valueConverter.ToTarget(value);
            }            
        }


        private void InitializeSource(Expression<Func<object>> sourceExpression)
        {
            #region Contracts

            if (sourceExpression == null) throw new ArgumentNullException();

            #endregion

            // SourceExpression
            _sourceExpression = sourceExpression;

            // SourceMemberExpression
            var sourceMemberExpression = sourceExpression.Body as MemberExpression;
            if (sourceMemberExpression == null) throw new InvalidOperationException();

            // SourcePropertyInfo
            _sourcePropertyInfo = sourceMemberExpression.Member as PropertyInfo;
            if (_sourcePropertyInfo == null) throw new InvalidOperationException();

            // SourceInstance
            _sourceInstance = this.GetInstance(sourceMemberExpression.Expression);
            if (_sourceInstance == null) throw new InvalidOperationException();

            // SourceInterface
            var sourceInterface = _sourceInstance as INotifyPropertyChanged;
            if (sourceInterface != null)
            {
                sourceInterface.PropertyChanged += this.SourceInstance_PropertyChanged;
            }
        }

        private void InitializeTarget(Expression<Func<object>> targetExpression)
        {
            #region Contracts

            if (targetExpression == null) throw new ArgumentNullException();

            #endregion

            // TargetExpression
            _targetExpression = targetExpression;

            // TargetMemberExpression
            var targetMemberExpression = targetExpression.Body as MemberExpression;
            if (targetMemberExpression == null) throw new InvalidOperationException();

            // TargetPropertyInfo
            _targetPropertyInfo = targetMemberExpression.Member as PropertyInfo;
            if (_targetPropertyInfo == null) throw new InvalidOperationException();

            // TargetInstance
            _targetInstance = this.GetInstance(targetMemberExpression.Expression);
            if (_targetInstance == null) throw new InvalidOperationException();

            // TargetInterface
            var targetInterface = _targetInstance as INotifyPropertyChanged;
            if (targetInterface != null)
            {
                targetInterface.PropertyChanged += this.TargetInstance_PropertyChanged;
            }
        }

        
        private object GetInstance(Expression expression)
        {
            #region Contracts

            if (expression == null) throw new ArgumentNullException();

            #endregion

            // ConstantExpression
            var constantExpression = expression as ConstantExpression;
            if (constantExpression != null)
            {
                // Return
                return constantExpression.Value;
            }

            // MemberExpression
            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                // Instance
                var instance = this.GetInstance(memberExpression.Expression);
                if (instance == null) throw new InvalidOperationException();

                // Return
                return this.GetValue(instance, memberExpression.Member);
            }

            // Other
            throw new InvalidOperationException();
        }

        private object GetValue(object instance, MemberInfo memberInfo)
        {
            #region Contracts

            if (instance == null) throw new ArgumentNullException();
            if (memberInfo == null) throw new ArgumentNullException();

            #endregion

            // PropertyInfo
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(instance, null);
            }

            // FieldInfo
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(instance);
            }

            // Other
            throw new InvalidOperationException();
        }

        private void SetValue(object instance, MemberInfo memberInfo, object value)
        {
            #region Contracts

            if (instance == null) throw new ArgumentNullException();
            if (memberInfo == null) throw new ArgumentNullException();

            #endregion

            // PropertyInfo
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo!=null)
            {
                propertyInfo.SetValue(instance, value, null);
                return;
            }

            // FieldInfo
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
                return;
            }

            // Other
            throw new InvalidOperationException();
        }


        // Handlers
        private void SourceInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region Contracts

            if (sender == null) throw new ArgumentNullException();
            if (e == null) throw new ArgumentNullException();

            #endregion

            // Require
            if (e.PropertyName != _sourcePropertyInfo.Name) return;

            // Update
            this.UpdateToTarget();
        }

        private void TargetInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            #region Contracts

            if (sender == null) throw new ArgumentNullException();
            if (e == null) throw new ArgumentNullException();

            #endregion

            // Require
            if (e.PropertyName != _targetPropertyInfo.Name) return;

            // Update
            this.UpdateToSource();
        }


        // Events
        internal delegate void BindingDisposedEventHandler(Binding binding);
        internal event BindingDisposedEventHandler BindingDisposed;
        private void OnBindingDisposed()
        {
            var handler = this.BindingDisposed;
            if (handler != null)
            {
                handler(this);
            }
        }
    }

    public sealed partial class Binding : IDisposable
    {
        // UpdateToSource
        public void UpdateToSource(object t1) { this.UpdateToSource(); }

        public void UpdateToSource(object t1, object t2) { this.UpdateToSource(); }

        public void UpdateToSource(object t1, object t2, object t3) { this.UpdateToSource(); }


        // UpdateToTarget
        public void UpdateToTarget(object t1) { this.UpdateToTarget(); }

        public void UpdateToTarget(object t1, object t2) { this.UpdateToTarget(); }

        public void UpdateToTarget(object t1, object t2, object t3) { this.UpdateToTarget(); }
    }
}
