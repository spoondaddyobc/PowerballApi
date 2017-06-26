namespace PowerballApi.UnitTests.PowerballParser
{
	using System;
	using System.Collections.Generic;
	using Api.Helpers.PowerballParser;
	using Api.Models;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class PowerballParserTests
	{
		private IFileParser<PowerballSet> _sot;

		[TestInitialize]
		public void Initialize()
		{
			_sot = new PowerballParser();
		}

		[TestMethod]
		public void WhenFileIsNull_ReturnEmptyList()
		{
			try
			{
				_sot.Parse(null);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
			}
		}

		[TestMethod]
		public void WhenFileIsEmpty_ReturnEmptyList()
		{
			try
			{
				_sot.Parse(string.Empty);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(FormatException));
			}
		}

		[TestMethod]
		public void WhenDataLineIsTooShort_SkipThatLine()
		{
			var file = PowerballDrawings.DataLineTooShort;

			var result = _sot.Parse(file);

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
		}

		[TestMethod]
		public void WhenGoodDataSet_ReturnPowerballResults()
		{
			var file = PowerballDrawings.GoodFileFormat;

			var result = _sot.Parse(file)[0];

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