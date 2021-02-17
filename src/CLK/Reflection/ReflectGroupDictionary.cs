using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Reflection
{
    public sealed class ReflectGroupDictionary
    {
        // Fields        
        private readonly IReflectGroupRepository _repository = null;  


        // Constructors
        internal ReflectGroupDictionary(IReflectGroupRepository repository) 
        {
            #region Contracts
                        
            if (repository == null) throw new ArgumentNullException();

            #endregion

            // Arguments            
            _repository = repository;       
        }


        // Properties   
        public IEnumerable<string> Keys
        {
            get
            {
                // Repository
                return _repository.GetAllKey();
            }
        }

        public ReflectGroup this[string key]
        {
            get
            {
                // Repository
                return _repository.GetValue(key);
            }
        }


        // Methods
        public void Add(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            _repository.Add(key);
        }

        public bool Remove(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Require
            if (_repository.ContainsKey(key) == false) return false;

            // Repository
            _repository.Remove(key);

            // Return
            return true;
        }

        public bool ContainsKey(string key)
        {
            #region Contracts

            if (string.IsNullOrEmpty(key) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            return _repository.ContainsKey(key);
        }
    }
}
