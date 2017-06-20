namespace PowerballApi.Api.Helpers.Cacher
{
	using System;
	using System.Runtime.Caching;

	public class Cacher : ICacher
	{
		public object Get(string name)
		{
			return MemoryCache.Default.Get(name);
		}

		public void Set(string name, object data, int daysUntilStale)
		{
			var expiration = DateTime.Today.AddDays(daysUntilStale);
			MemoryCache.Default.Set(name, data, expiration);
		}
	}
}