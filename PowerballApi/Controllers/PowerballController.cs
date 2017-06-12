namespace PowerballApi.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Web.Http;
	using Microsoft.VisualBasic.FileIO;
	using Models;

	public class PowerballController : ApiController
	{
		[HttpGet]
		public IEnumerable<PowerballSet> GetPowerballNumbers()
		{
			var file = GetPowerballFile();
			var powerballResults = new List<PowerballSet>();

			// TODO: Isolate parser for single responsibility
			using (var parser = new TextFieldParser(file))
			{
				parser.SetDelimiters("  ");

				while (!parser.EndOfData)
				{
					if (parser.LineNumber == 1)
						continue;

					var dataLine = parser.ReadFields();
					if (dataLine == null)
						continue;

					var set = new PowerballSet
					{
						Date = dataLine[0],
						WinNumbers =
						{
							[0] = int.Parse(dataLine[1]),
							[1] = int.Parse(dataLine[2]),
							[2] = int.Parse(dataLine[3]),
							[3] = int.Parse(dataLine[4]),
							[4] = int.Parse(dataLine[5]),
							[5] = int.Parse(dataLine[6])
						}
					};

					powerballResults.Add(set);
				}
			}

			return powerballResults;
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
			const string location = @"C:\Users\Tyree Barron\Desktop";
			var path = location + "\\NumbersFile\\" + name;

			return path;
		}
	}
}
