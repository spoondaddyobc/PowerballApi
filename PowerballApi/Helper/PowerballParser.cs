using System;
using System.IO;

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
			using (StringReader sr = new StringReader(file))
			{
			    string line;
			    bool isFirstLine = true;
			    while ((line = sr.ReadLine()) != null)
			    {
			        if (isFirstLine)
			        {
			            isFirstLine = false;
                        continue;
                    }

			        var dataLine = line.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);

                    var set = new PowerballSet
					{
						Date = dataLine[0].ToString(),
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
