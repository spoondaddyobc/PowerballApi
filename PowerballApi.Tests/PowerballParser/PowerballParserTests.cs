namespace PowerballApi.UnitTests.PowerballParser
{
	using System.Collections.Generic;
	using Api.Helpers.PowerballParser;
	using Api.Models;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class PowerballParserTests
	{
		[TestMethod]
		public void WhenFileIsNull_ReturnEmptyList()
		{
			var sot = new PowerballParser();

			var result = sot.Parse(null);

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
		}

		[TestMethod]
		public void WhenFileIsEmpty_ReturnEmptyList()
		{
			var sot = new PowerballParser();

			var result = sot.Parse(string.Empty);

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
		}

		[TestMethod]
		public void WhenDataLineIsTooShort_SkipThatLine()
		{
			var sot = new PowerballParser();
			var file = PowerballDrawings.DataLineTooShort;

			var result = sot.Parse(file);

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
		}

		[TestMethod]
		public void WhenGoodDataSet_ReturnPowerballResults()
		{
			var sot = new PowerballParser();
			var file = PowerballDrawings.GoodFileFormat;

			var result = sot.Parse(file)[0];

			Assert.AreEqual(result.Date, "01/01/2000");
			Assert.AreEqual(result.WinNumbers[0], 1);
			Assert.AreEqual(result.WinNumbers[1], 2);
			Assert.AreEqual(result.WinNumbers[2], 3);
			Assert.AreEqual(result.WinNumbers[3], 4);
			Assert.AreEqual(result.WinNumbers[4], 5);
			Assert.AreEqual(result.WinNumbers[5], 6);
			Assert.AreEqual(result.PowerPlay, 7);
		}
	}
}
