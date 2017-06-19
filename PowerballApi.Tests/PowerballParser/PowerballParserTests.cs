namespace PowerballApi.UnitTests.PowerballParser
{
	using System.Collections.Generic;
	using Api.Helper;
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
	}
}
