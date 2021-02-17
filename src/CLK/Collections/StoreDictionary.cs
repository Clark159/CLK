using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Collections
{
    public class StoreDictionary<TKey, TValue>
    {
        // Fields
        private readonly IStoreProvider<TKey, TValue> _provider = null;


        // Constructors
        public StoreDictionary(IStoreProvider<TKey, TValue> provider)
        {
            #region Contracts

            if (provider == null) throw new ArgumentNullException();

            #endregion

            // Provider
            _provider = provider;
        }


        // Properties   
        public virtual IEnumerable<TKey> Keys
        {
            get
            {
                // Provider
                return _provider.GetAllKey();
            }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                // Provider
                return _provider.GetValue(key);
            }
            set
            {
                // Provider
                _provider.Remove(key);
                _provider.Add(key, value);
            }
        }


        // Methods  
        public virtual void Add(TKey key, TValue value)
        {
            // Provider
            _provider.Remove(key);
            _provider.Add(key, value);
        }

        public virtual bool Remove(TKey key)
        {
            // Require
            if (_provider.ContainsKey(key) == false) return false;

            // Provider
            _provider.Remove(key);

            // Return
            return true;
        }

        public virtual bool ContainsKey(TKey key)
        {
            // Provider
            return _provider.ContainsKey(key);
        }
    }
}
