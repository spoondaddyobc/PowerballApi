namespace PowerballApi.Api.Helper
{
	using System.Collections.Generic;
	using Models;
	using Microsoft.VisualBasic.FileIO;

	public class PowerballParser
	{
		public List<PowerballSet> Parse(string file)
		{
			if (string.IsNullOrWhiteSpace(file))
				return new List<PowerballSet>();

			var powerballResults = new List<PowerballSet>();
			using (var parser = new TextFieldParser(file))
			{
				parser.SetDelimiters("  ");

				while (!parser.EndOfData)
				{
					if (parser.LineNumber == 1)
					{
						// This is necessary to incremenet the line number
						parser.ReadLine();
						continue;
					}

					var dataLine = parser.ReadFields();
					if (dataLine == null || dataLine.Length < 7)
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
	}
}
