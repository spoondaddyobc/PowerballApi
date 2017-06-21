namespace PowerballApi.UnitTests
{
	using System.Collections.Generic;
	using Api.Helpers.Cacher;
	using Api.Helpers.HttpHandler;
	using Api.Helpers.PowerballParser;
	using Api.Models;
	using Api.Repositories;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NSubstitute;

	[TestClass]
	public class PowerballRepositoryTests
	{
		private ICacher _cacher;
		private IHttpHandler _httpHandler;
		private IPowerballParser _parser;
		private PowerballRepository _sot;

		[TestInitialize]
		public void Initialize()
		{
			_cacher = Substitute.For<ICacher>();
			_httpHandler = Substitute.For<IHttpHandler>();
			_parser = Substitute.For<IPowerballParser>();
			_sot = new PowerballRepository(_cacher, _httpHandler, _parser);
		}

		[TestMethod]
		public void OnGet_WhenCacheExists_ReturnCache()
		{
			var cacheName = "name";
			_sot.CacheName = cacheName;
			_cacher.Get(cacheName).Returns(new List<PowerballSet>() as object);

			var result = _sot.Get();

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
			_httpHandler.DidNotReceive().GetStringAsync(Arg.Any<string>());
		}

		[TestMethod]
		public void OnGet_WhenCacheIsNull_GetPowerballDrawings()
		{
			var cacheName = "name";
			var daysUntilStale = 1;
			var expected = new List<PowerballSet>();
			var file = "file";
			var url = "url";
			_sot.CacheName = cacheName;
			_sot.DaysUntilStale = daysUntilStale;
			_sot.PowerballUrl = url;
			_cacher.Get(Arg.Any<string>()).Returns(null);
			_httpHandler.GetStringAsync(url).Returns(file);
			_parser.Parse(file).Returns(expected);

			var actual = _sot.Get();

			_cacher.Received().Set(cacheName, expected, daysUntilStale);
			CollectionAssert.AreEqual(actual, expected);
		}
	}
}