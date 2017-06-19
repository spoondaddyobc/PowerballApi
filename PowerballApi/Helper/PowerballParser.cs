namespace PowerballApi.Api.Helper
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using Models;

	public class PowerballParser : IPowerballParser
	{
		public List<PowerballSet> Parse(string file)
		{
			if (string.IsNullOrWhiteSpace(file))
				return new List<PowerballSet>();

			var powerballResults = new List<PowerballSet>();
			using (var reader = new StringReader(file))
			{
				string line;
				var isFirstLine = true;
				while ((line = reader.ReadLine()) != null)
				{
					if (isFirstLine)
					{
						isFirstLine = false;
						continue;
					}

					var dataLine = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
					if (dataLine.Length < 8)
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
						},
						PowerPlay = int.Parse(dataLine[7])
					};

					powerballResults.Add(set);
				}
			}

			return powerballResults;
		}
	}
}
