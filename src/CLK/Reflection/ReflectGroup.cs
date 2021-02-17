using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Reflection
{
    public sealed class ReflectGroup
    {
        // Fields        
        private readonly IReflectRepository _repository = null;

        private readonly string _groupName = null;


        // Constructors
        internal ReflectGroup(IReflectRepository repository, string groupName)
        {
            #region Contracts
                       
            if (repository == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Arguments            
            _repository = repository;
            _groupName = groupName;

            // ReflectBuilderDictionary        
            IReflectBuilderRepository reflectBuilderRepository = null;
            reflectBuilderRepository = new ReflectBuilderRepository(repository, groupName);
            this.ReflectBuilders = new ReflectBuilderDictionary(reflectBuilderRepository);
        }


        // Properties
        public string DefaultEntityName
        {
            get
            {
                // Require
                if (_repository.ContainsDefaultEntityName(_groupName) == false) return string.Empty;

                // Repository
                return _repository.GetDefaultEntityName(_groupName);
            }
            set
            {
                // Require     
                if (string.IsNullOrEmpty(value) == true)
                {
                    if (_repository.ContainsSetting(_groupName, value) == false)
                    {
                        throw new InvalidOperationException();
                    }
                }                

                // Repository
                if (string.IsNullOrEmpty(value) == false)
                {
                    // Add
                    _repository.RemoveDefaultEntityName(_groupName);
                    _repository.AddDefaultEntityName(_groupName, value);
                }
                else
                {
                    // Remove
                    _repository.RemoveDefaultEntityName(_groupName);                    
                }                              
            }
        }

        public ReflectBuilderDictionary ReflectBuilders { get; private set; }
    }
}
