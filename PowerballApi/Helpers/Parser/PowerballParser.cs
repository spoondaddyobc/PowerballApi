namespace PowerballApi.Api.Helpers.Parser
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Models;

	public class PowerballParser : IParser<PowerballSet, string>
	{
		public List<PowerballSet> Parse(string file)
		{
			if (file == null)
				throw new ArgumentNullException();

			if (string.IsNullOrWhiteSpace(file))
				throw new FormatException(@"Powerball data file contains no data.");

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
					if (dataLine.Length < 7)
						continue;

					DateTime date;
					if (!DateTime.TryParse(dataLine[0], out date))
						throw new FormatException(@"Powerball data contains a drawing with an invalid date.");
					if (powerballResults.Any(p => p.Date == date))
						throw new FormatException(@"Powerball data contains more than one drawing with the same date.");

					powerballResults.Add(new PowerballSet
					{
						Date = date,
						PowerPlay = dataLine.Length > 7 ? int.Parse(dataLine[7]) : (int?)null,
						WinNumbers =
						{
							[0] = int.Parse(dataLine[1]),
							[1] = int.Parse(dataLine[2]),
							[2] = int.Parse(dataLine[3]),
							[3] = int.Parse(dataLine[4]),
							[4] = int.Parse(dataLine[5]),
							[5] = int.Parse(dataLine[6])
						}
					});
				}
			}

			return powerballResults;
		}
	}
}