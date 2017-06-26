namespace PowerballApi.UnitTests.PowerballParser
{
	using System;
	using System.Collections.Generic;
	using Api.Helpers.Parser;
	using Api.Models;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class PowerballParserTests
	{
		private IFileParser<PowerballSet> _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new PowerballParser();
		}

		[TestMethod]
		public void Parse_WhenFileIsNull_ReturnArgumentNullException()
		{
			try
			{
				_sut.Parse(null);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
			}
		}

		[TestMethod]
		public void Parse_WhenFileIsEmpty_ReturnFormatException()
		{
			try
			{
				_sut.Parse(string.Empty);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(FormatException));
			}
		}

		[TestMethod]
		public void Parse_WhenFileLineIsTooShort_SkipThatLine()
		{
			var file = PowerballDrawings.DataLineTooShort;

			var result = _sut.Parse(file);

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
		}

		[TestMethod]
		public void Parse_WhenGoodFileFormat_ReturnPowerballResults()
		{
			var file = PowerballDrawings.GoodFileFormat;

			var result = _sut.Parse(file)[0];

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