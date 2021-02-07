namespace SImpl.Storage.Redis.Module
{
    public class RedisConnectionConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool OverrideSslEnabled { get; set; } = false;
        public bool IgnoreSslErrors { get; set; } = false;
        public int ConnectionTimeToLive { get; set; } = 3600; // 1 hour
    }
}