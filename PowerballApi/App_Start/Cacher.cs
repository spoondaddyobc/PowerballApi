namespace PowerballApi.Api
{
	using System;
	using System.Runtime.Caching;

	public class Cacher
    {
        public void CacheData()
        {
            var results = new object();

            var cache = new MemoryCache("DataCache");
            var cacheItem = new CacheItem("PowerballData", results);

            var expiration = DateTime.Today.AddDays(1);

            cache.AddOrGetExisting("PowerballData",cacheItem, expiration);
        }
    }
}