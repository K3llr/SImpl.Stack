namespace SImpl.Storage.KeyValue
{
    public interface IKeyValueStorage<in TKey, TModel>
    {
        TModel Fetch(TKey key);
        
        TModel[] Fetch(TKey[] keys);
        void Store(TKey key, TModel spaceViewModel);

        TModel Remove(TKey key);
    }
}