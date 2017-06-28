namespace PowerballApi.Api.Helpers.Parser
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using Models;

	public class PowerballParser : IParser<PowerballSet, string>
	{
		public List<PowerballSet> Parse(string file)
		{
			if (file == null)
				throw new ArgumentNullException();

			if (string.IsNullOrWhiteSpace(file))
				throw new FormatException();

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
						PowerPlay = int.Parse(dataLine[7]),
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
	}
}