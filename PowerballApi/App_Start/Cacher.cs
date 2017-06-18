using System.Collections.Generic;
using PowerballApi.Api.Models;

namespace PowerballApi.Api
{
	using System;
	using System.Runtime.Caching;

	public class Cacher
    {
        public void CacheData(List<PowerballSet> powerballSetList)
        {
            // plain object for now.
            

            var expiration = DateTime.Today.AddDays(1);

            MemoryCache.Default.Set("PowerballData", powerballSetList, expiration);
        }
    }
}