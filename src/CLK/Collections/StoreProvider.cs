using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLK.Collections
{
    public interface IStoreProvider<TKey, TValue>
    {
        // Methods  
        void Add(TKey key, TValue value);

        void Remove(TKey key);

        bool ContainsKey(TKey key);

        TValue GetValue(TKey key);

        IEnumerable<TKey> GetAllKey();        
    }
}
