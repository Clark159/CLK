using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Activities
{
    public partial class ActivityContext : IDisposable
    {
        // Singleton 
        private static ActivityContext _current;

        internal static ActivityContext Current
        {
            get
            {
                // Require
                if (_current == null) throw new InvalidOperationException("_current=null");

                // Return
                return _current;
            }
        }

        public static ActivityContext Initialize(ActivityContext viewContext)
        {
            #region Contracts

            if (viewContext == null) throw new ArgumentException();

            #endregion

            // Default
            _current = viewContext;

            // Return
            return _current;
        }
    }

    public partial class ActivityContext : IDisposable
    {
        // Fields
        private readonly List<ViewFactory> _viewFactoryList = null;

        private readonly List<ViewModelLauncher> _viewModelLauncherList = null;
        
        private readonly Dictionary<Type, ViewFactory> _viewFactoryDictionary = new Dictionary<Type, ViewFactory>();
        

        // Constructors
        public ActivityContext(List<ViewFactory> viewFactoryList, List<ViewModelLauncher> viewModelLauncherList)
        {
            #region Contracts

            if (viewFactoryList == null) throw new ArgumentException();
            if (viewModelLauncherList == null) throw new ArgumentException();

            #endregion

            // Default
            _viewFactoryList = viewFactoryList;
            _viewModelLauncherList = viewModelLauncherList;

            // ViewFactoryDictionary
            foreach (var viewFactory in viewFactoryList)
            {
                // Add
                _viewFactoryDictionary.Add(viewFactory.ModelType, viewFactory);
            }
        }

        public void Start()
        {

        }

        public void Dispose()
        {

        }


        // Properties
        public IEnumerable<ViewFactory> ViewFactoryList
        {
            get { return _viewFactoryList; }
        }

        public IEnumerable<ViewModelLauncher> ViewModelLauncherList
        {
            get { return _viewModelLauncherList; }
        }


        // Methods
        public View CreateView(object model = null)
        {
            // Model
            if (model == null)
            {
                return this.CreateView
                (
                    new NotFoundView(),
                    new NotFoundViewModel("null")
                );
            }

            // ViewModel
            var viewModel = model as ViewModel;
            if (viewModel == null)
            {
                return this.CreateView
                (
                    new NotFoundView(),
                    new NotFoundViewModel(model.GetType().FullName)
                );
            }

            // ViewFactory
            if (_viewFactoryDictionary.ContainsKey(viewModel.GetType()) == false)
            {
                return this.CreateView
                (
                    new NotFoundView(),
                    new NotFoundViewModel(model.GetType().FullName)
                );
            }
            var viewFactory = _viewFactoryDictionary[viewModel.GetType()];

            // View
            var view = viewFactory.Create();
            if (view == null)
            {
                return this.CreateView
                (
                    new NotFoundView(),
                    new NotFoundViewModel(model.GetType().FullName)
                );
            }

            // Return
            return this.CreateView
            (
                view,
                viewModel
            );
        }

        private View CreateView(View view, ViewModel viewModel)
        {
            #region Contracts

            if (view == null) throw new ArgumentException();
            if (viewModel == null) throw new ArgumentException();

            #endregion

            // Initailize
            view.DataContext = viewModel;

            // Return
            return view;
        }


        public ViewModel CreateViewModel(Uri uri, Dictionary<string, object> bundle = null)
        {
            #region Contracts

            if (uri == null) throw new ArgumentException();

            #endregion

            // Result
            ViewModel viewModel = null;

            // ViewModelLauncher
            foreach (var viewModelLauncher in _viewModelLauncherList)
            {
                // ViewModel
                viewModel = this.CreateViewModel(viewModelLauncher, uri, bundle);
                if (viewModel != null) break;
            }

            // Return
            return viewModel;
        }

        private ViewModel CreateViewModel(ViewModelLauncher viewModelLauncher, Uri uri, Dictionary<string, object> bundle = null)
        {
            #region Contracts

            if (uri == null) throw new ArgumentException();

            #endregion

            // Scheme
            if (Uri.Compare(uri, viewModelLauncher.ViewUri, UriComponents.Scheme, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) != 0) return null;

            // HostAndPort
            if (string.IsNullOrEmpty(viewModelLauncher.ViewUri.Host) == false)
            {
                if (Uri.Compare(uri, viewModelLauncher.ViewUri, UriComponents.HostAndPort, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) != 0) return null;
            }

            // Path
            if (string.IsNullOrEmpty(viewModelLauncher.ViewUri.LocalPath) == false && viewModelLauncher.ViewUri.LocalPath != @"/" && viewModelLauncher.ViewUri.LocalPath != @"\")
            {
                if (uri.LocalPath.StartsWith(viewModelLauncher.ViewUri.LocalPath, StringComparison.OrdinalIgnoreCase) == false) return null;
            }

            // CreateModel
            return viewModelLauncher.Create(uri, bundle);
        }
    }
}
