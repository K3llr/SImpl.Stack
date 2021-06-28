using System;

namespace SImpl.Cache.Models
{
    public class CacheEntryInfo
    {
        public static CacheEntryInfo NoCacheInstance => new CacheEntryInfo { NoCache = true };

        public bool NoCache { get; set; }

        public string Key { get; set; }

        public TimeSpan? TimeToLive { get; set; }
    }
}