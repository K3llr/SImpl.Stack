using System.Collections.Generic;

namespace SImpl.Storage.KeyValue.Services
{
    public class InMemoryKeyValueStorage<TKey, TModel> : IKeyValueStorage<TKey, TModel>
    {
        private readonly IDictionary<TKey, TModel> _store = new Dictionary<TKey, TModel>();
        
        public TModel Fetch(TKey key)
        {
            if (_store.ContainsKey(key))
            {
                return _store[key];
            }

            throw new KeyNotFoundException($"No such key: {key}");
        }

        public TModel[] Fetch(TKey[] keys)
        {
            var list = new List<TModel>();
            foreach (var key in keys)
            {
                var model = Fetch(key);
                list.Add(model);
            }

            return list.ToArray();
        }

        public void Store(TKey key, TModel model)
        {
            _store[key] = model;
        }

        public TModel Remove(TKey key)
        {
            var model = Fetch(key);
            _store.Remove(key);
            return model;
        }
    }
}