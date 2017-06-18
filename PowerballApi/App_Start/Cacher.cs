namespace PowerballApi
{
    using System;
    using System.Runtime.Caching;

    public class Cacher
    {
        public void CacheData()
        {
            // plain object for now.
            var results = new object();
            
            var expiration = DateTime.Today.AddDays(1);

            MemoryCache.Default.Set("PowerballData", results, expiration);
        }
    }
}