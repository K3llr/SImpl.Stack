namespace SImpl.Storage.KeyValue.Redis
{
    public interface IRedisKeyValueStorage<in TKey, TModel> : IKeyValueStorage<TKey, TModel>
    {
        
    }
}