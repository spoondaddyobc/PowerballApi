using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualBasic.FileIO;
using PowerballApi.Models;

namespace PowerballApi.Controllers
{
    public class PowerballController : ApiController
    {
        [HttpGet]
        public IEnumerable<PowerballSet> GetPowerballNumbers()
        {

            var powerballList = new List<PowerballSet>();

            var file = GetNumberFile();

            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new TextFieldParser(file))
            {
                parser.SetDelimiters("  ");
                var lineNumber = 0;
                while (!parser.EndOfData)
                {
                    var dataline = parser.ReadFields();
                    if (lineNumber != 0)
                    {
                        var set = new PowerballSet();
                        set.Date = dataline[0];
                        set.WinNumbers[0] = int.Parse(dataline[1]);
                        set.WinNumbers[1] = int.Parse(dataline[2]);
                        set.WinNumbers[2] = int.Parse(dataline[3]);
                        set.WinNumbers[3] = int.Parse(dataline[4]);
                        set.WinNumbers[4] = int.Parse(dataline[5]);
                        set.WinNumbers[5] = int.Parse(dataline[6]);
                        powerballList.Add(set);
                    }
                    lineNumber++;
                    
                }
            }
            var result = powerballList;
            return result;
        }
        [NonAction]
        public string GetNumberFile()
        {
            var shortDate = DateTime.Today.ToString("yyyymmdd");

            var numberFile = "numberfile" + shortDate + ".txt";
            //var currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectory = @"C:\Users\Tyree Barron\Desktop";
            string path = currentDirectory + "\\NumbersFile\\" + numberFile;

            //string[] files = System.IO.Directory.GetFiles(path);

            var fileExists = File.Exists(path);

            if (!fileExists)
            {
                using (var client = new WebClient())
                {

                    var url = @"http://www.powerball.com/powerball/winnums-text.txt";
                    client.DownloadFile(url, path);
                    return path;

                }
            }
            else
            { return path;}
        }
    }
}
