using System;
using System.Collections.Generic;
using SImpl.Storage.KeyValue.Redis.Services;

namespace SImpl.Storage.KeyValue.Redis.Module
{
    public class RedisKeyValueConfig
    {
        private readonly List<ModelStorageSettingsRegistration> _modelSettings = new();
        
        public IReadOnlyList<ModelStorageSettingsRegistration> RegisteredModelStorageSettings => _modelSettings.AsReadOnly();
        
        public RedisKeyValueConfig AddModelStorageSettings<TModel>(RedisModelStorageStorageSettings<TModel> modelStorageStorageSettings)
        {
            _modelSettings.Add(new ModelStorageSettingsRegistration
            {
                Model = typeof(TModel),
                StorageSettings = modelStorageStorageSettings
            });
            
            return this;
        }
    }
    
    public class ModelStorageSettingsRegistration 
    {
        public Type Model { get; set; }
        public IRedisModelStorageSettings StorageSettings { get; set; }
    }
}