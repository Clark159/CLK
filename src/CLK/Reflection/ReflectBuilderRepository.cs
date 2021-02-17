using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Reflection
{
    internal interface IReflectBuilderRepository : IStoreProvider<string, ReflectBuilder>
    {

    }

    internal sealed class ReflectBuilderRepository : IReflectBuilderRepository
    {
        // Fields        
        private readonly IReflectRepository _repository = null;

        private readonly string _groupName = null;        


        // Constructors
        public ReflectBuilderRepository(IReflectRepository repository, string groupName)
        {
            #region Contracts
                        
            if (repository == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Arguments            
            _repository = repository;
            _groupName = groupName;
        }


        // Methods        
        public void Add(string entityName, ReflectBuilder builder)
        {
            #region Contracts

            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();
            if (builder == null) throw new ArgumentNullException();

            #endregion

            // Setting
            ReflectSetting setting = builder.ToSetting();
            if (setting == null) throw new InvalidOperationException();

            // Repository
            _repository.AddSetting(_groupName, entityName, setting);
        }

        public void Remove(string entityName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            _repository.RemoveSetting(_groupName, entityName);
        }

        public bool ContainsKey(string entityName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            return _repository.ContainsSetting(_groupName, entityName);
        }
                
        public IEnumerable<string> GetAllKey()
        {
            // Repository
            return _repository.GetAllEntityName(_groupName);
        }

        public ReflectBuilder GetValue(string entityName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(entityName) == true) throw new ArgumentNullException();

            #endregion

            // Setting
            ReflectSetting setting = _repository.GetSetting(_groupName, entityName);
            if (setting == null) return null;

            // Builder
            ReflectBuilder builder = setting.ToBuilder();
            if (builder == null) throw new InvalidOperationException();

            // Return
            return builder;
        }
    }
}
