namespace PowerballApi.UnitTests
{
	using System;
	using System.Collections.Generic;
	using Api.Helpers.Cacher;
	using Api.Helpers.Parser;
	using Api.Models;
	using Api.Repositories;
	using Api.Services;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NSubstitute;

	[TestClass]
	public class PowerballRepositoryTests
	{
		private ICacher _cacher;
		private IParser<PowerballSet, string> _parser;
		private IService _service;
		private PowerballRepository _sut;

		[TestInitialize]
		public void Initialize()
		{
			_cacher = Substitute.For<ICacher>();
			_parser = Substitute.For<IParser<PowerballSet, string>>();
			_service = Substitute.For<IService>();
			_sut = new PowerballRepository(_cacher, _parser, _service);
		}

		[TestMethod]
		public void Get_WhenCacheExists_ReturnCache()
		{
			const string cacheName = "name";
			_sut.CacheName = cacheName;
			_cacher.Get(cacheName).Returns(new List<PowerballSet>());

			var result = _sut.Get();

			CollectionAssert.AreEqual(result, new List<PowerballSet>());
			_service.DidNotReceive().Get();
		}

		[TestMethod]
		public void Get_WhenCacheIsNull_GetPowerballDrawings()
		{
			const string cacheName = "name";
			const int daysUntilStale = 1;
			const string file = "file";
			var expected = new List<PowerballSet>();

			_sut.CacheName = cacheName;
			_sut.DaysUntilStale = daysUntilStale;
			_cacher.Get(Arg.Any<string>()).Returns(null);
			_service.Get().Returns(file);
			_parser.Parse(file).Returns(expected);

			var actual = _sut.Get();

			_cacher.Received().Set(cacheName, expected, daysUntilStale);
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void GetById_WhenInvalidRequest_ThrowArgumentException()
		{
			const string invalidDate = "12/50/2000";

			try
			{
				_sut.Get(invalidDate);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(ArgumentException));
			}
		}

		[TestMethod]
		public void GetById_WhenOneMatchingDrawing_ReturnDrawing()
		{
			const string date = "12/12/2000";
			var expected = new PowerballSet
			{
				Date = DateTime.Parse(date),
				PowerPlay = null,
				WinNumbers = new [] {1,2,3,4,5,6 }
			};
			_cacher.Get(Arg.Any<string>()).Returns(new List<PowerballSet> { expected });

			var result = _sut.Get(date);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Date, expected.Date);
		}

		[TestMethod]
		public void GetById_WhenNoMatchingDrawing_ReturnNull()
		{
			const string requestDate = "12/12/2000";
			const string drawingDate = "11/11/2000";
			var drawing = new PowerballSet
			{
				Date = DateTime.Parse(drawingDate),
				PowerPlay = null,
				WinNumbers = new[] { 1, 2, 3, 4, 5, 6 }
			};
			_cacher.Get(Arg.Any<string>()).Returns(new List<PowerballSet> { drawing });

			var result = _sut.Get(requestDate);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void GetById_WhenMultipleMatchingDrawings_ThrowException()
		{
			const string date = "12/12/2000";
			var drawing = new PowerballSet
			{
				Date = DateTime.Parse(date),
				PowerPlay = null,
				WinNumbers = new[] { 1, 2, 3, 4, 5, 6 }
			};
			_cacher.Get(Arg.Any<string>()).Returns(new List<PowerballSet>
			{
				drawing,
				drawing
			});

			try
			{
				_sut.Get(date);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(Exception));
			}
		}
	}
}