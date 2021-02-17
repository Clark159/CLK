using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLK.Collections;

namespace CLK.Reflection
{
    internal interface IReflectGroupRepository
    {
        // Methods
        void Add(string groupName);

        void Remove(string groupName);

        bool ContainsKey(string groupName);

        IEnumerable<string> GetAllKey();

        ReflectGroup GetValue(string groupName);     
    }

    internal sealed class ReflectGroupRepository : IReflectGroupRepository
    {
        // Fields        
        private readonly IReflectRepository _repository = null;    


        // Constructors
        public ReflectGroupRepository(IReflectRepository repository)
        {
            #region Contracts

            if (repository == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _repository = repository;
        }
        

        // Methods
        public void Add(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion
                        
            // Repository
            _repository.AddGroup(groupName);
        }

        public void Remove(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            _repository.RemoveGroup(groupName);
        }

        public bool ContainsKey(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            return _repository.ContainsGroup(groupName);
        }
        
        public IEnumerable<string> GetAllKey()
        {
            // Repository
            return _repository.GetAllGroupName();
        }

        public ReflectGroup GetValue(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            if (_repository.ContainsGroup(groupName) == true)
            {
                return new ReflectGroup(_repository, groupName);
            }
            return null;
        }
    }

    internal sealed class CacheReflectGroupRepository : IReflectGroupRepository
    {
        // Fields    
        private readonly object _syncRoot = new object();

        private readonly IReflectGroupRepository _repository = null;

        private string _cacheGroupName = null;

        private ReflectGroup _cacheGroupInstance = null;


        // Constructors
        public CacheReflectGroupRepository(IReflectGroupRepository repository)
        {
            #region Contracts

            if (repository == null) throw new ArgumentNullException();

            #endregion

            // Arguments
            _repository = repository;
        }


        // Methods
        public void Add(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion
                        
            lock (_syncRoot)
            {
                // Cache
                _cacheGroupName = null;
                _cacheGroupInstance = null;

                // Repository
                _repository.Add(groupName);
            }            
        }

        public void Remove(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion
                        
            lock (_syncRoot)
            {
                // Cache
                _cacheGroupName = null;
                _cacheGroupInstance = null;

                // Repository
                _repository.Remove(groupName);
            }
        }

        public bool ContainsKey(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion

            // Repository
            return _repository.ContainsKey(groupName);
        }

        public IEnumerable<string> GetAllKey()
        {
            // Repository
            return _repository.GetAllKey();
        }

        public ReflectGroup GetValue(string groupName)
        {
            #region Contracts

            if (string.IsNullOrEmpty(groupName) == true) throw new ArgumentNullException();

            #endregion
                        
            lock (_syncRoot)
            {
                // Cache
                if (_cacheGroupName == groupName) return _cacheGroupInstance;
                _cacheGroupName = groupName;
                _cacheGroupInstance = _repository.GetValue(groupName);

                // Return
                return _cacheGroupInstance;
            }
        }
    }
}
