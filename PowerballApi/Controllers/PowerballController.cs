namespace PowerballApi.Api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Web.Http;
	using Helper;
	using Models;
    using System.Runtime.Caching;

	public class PowerballController : ApiController
	{
		[HttpGet]
		public IEnumerable<PowerballSet> GetPowerballNumbers()
		{
		    IEnumerable<PowerballSet> results;
		    if (MemoryCache.Default.Get("PowerballData") !=  null)
		    {
		        results = (IEnumerable<PowerballSet>) MemoryCache.Default.Get("PowerballData");
		    }
		    else
		    {
                var file = GetPowerballFile();
                var parser = new PowerballParser();
		        results = parser.Parse(file);
                var listings = new PowerballSetList();
		        listings.PowerballSets = (List<PowerballSet>) results;
                var cacher = new Cacher();
                cacher.CacheData(listings);
                
                
            }
		    return results;
		}

		private static string GetPowerballFile()
		{
			using (var client = new WebClient())
			{
				const string url = @"http://www.powerball.com/powerball/winnums-text.txt";

				var result = client.DownloadString(url);

				return result;
			}
		}
	}
}
