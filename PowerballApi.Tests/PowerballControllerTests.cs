namespace PowerballApi.UnitTests
{
	using System.Collections.Generic;
	using System.Web.Http.Results;
	using Api.Controllers;
	using Api.Models;
	using Api.Repositories;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NSubstitute;

	[TestClass]
	public class PowerballControllerTests
	{
		private IRepository<PowerballSet> _repository;
		private PowerballController _sut;

		[TestInitialize]
		public void MyTestInitialize()
		{
			_repository = Substitute.For<IRepository<PowerballSet>>();
			_sut = new PowerballController(_repository);
		}

		[TestMethod]
		public void PowerballResults_WhenNullRepositoryResponse_ReturnNotFound()
		{
			_repository.Get().Returns((List<PowerballSet>) null);

			var result = _sut.PowerballResults();

			Assert.IsInstanceOfType(result, typeof(NotFoundResult));
		}

		[TestMethod]
		public void PowerballResults_WhenEmptyRepositoryResponse_ReturnNotFound()
		{
			_repository.Get().Returns(new List<PowerballSet>());

			var result = _sut.PowerballResults();

			Assert.IsInstanceOfType(result, typeof(NotFoundResult));
		}

		[TestMethod]
		public void PowerballResults_WhenPowerballResultsReturned_ReturnOk()
		{
			var expected = new List<PowerballSet> { new PowerballSet() };
			_repository.Get().Returns(expected);

			var actionResult = _sut.PowerballResults();
			var contentResult = actionResult as OkNegotiatedContentResult<List<PowerballSet>>;

			Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<PowerballSet>>));
			CollectionAssert.AreEqual(contentResult?.Content, expected);
		}
	}
}