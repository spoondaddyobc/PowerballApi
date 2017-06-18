namespace PowerballApi.Api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Web.Http;
	using Helper;
	using Models;

	public class PowerballController : ApiController
	{
		[HttpGet]
		public IEnumerable<PowerballSet> GetPowerballNumbers()
		{
			var file = GetPowerballFile();
			var parser = new PowerballParser();
			var results = parser.Parse(file);
			return results;
		}

		private static string GetPowerballFile()
		{
			var path = GetPowerballFilePath();

			if (File.Exists(path))
				return path;

			using (var client = new WebClient())
			{
				const string url = @"http://www.powerball.com/powerball/winnums-text.txt";

				// TODO: Store in memory so we're not collecting stale files
				client.DownloadFile(url, path);

				return path;
			}
		}

		private static string GetPowerballFilePath()
		{
			var date = DateTime.Today.ToString("yyyymmdd");
			var name = "numberfile" + date + ".txt";

			// TODO: Relocate so project is workstation agnostic
			const string location = @"C:\Users\jaurand\Desktop";
			var path = location + "\\NumbersFile\\" + name;

			return path;
		}
	}
}
