using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerballApi;
using PowerballApi.Controllers;
using PowerballApi.Models;

namespace Powerball.ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCall = new PowerballController();

            var numbers = new List<PowerballSet>();
            var download = testCall.GetPowerballNumbers();
            foreach (var item in download)
            {
                numbers.Add(item);
            }

            Console.ReadLine();
        }
    }
}
