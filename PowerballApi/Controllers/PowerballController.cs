namespace PowerballApi.Api.Controllers
{
	using System.Collections.Generic;
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
		    List<PowerballSet> results;
		    if (MemoryCache.Default.Get("PowerballData") !=  null)
		    {
		        results = (List<PowerballSet>) MemoryCache.Default.Get("PowerballData");
		    }
		    else
		    {
                var file = GetPowerballFile();
                var parser = new PowerballParser();
		        results = parser.Parse(file);
                var cacher = new Cacher();
                cacher.CacheData(results);
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
