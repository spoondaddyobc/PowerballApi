namespace PowerballApi.Api.Controllers
{
	using System.Linq;
	using System.Web.Http;
	using Repositories;
	using Models;

	[RoutePrefix("api/powerball")]
	public class PowerballController : ApiController
	{
		private readonly IRepository<PowerballSet> _repository;

		public PowerballController()
		{
			_repository = new PowerballRepository();
		}

		public PowerballController(IRepository<PowerballSet> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("drawings")]
		public IHttpActionResult PowerballResults()
		{
			var response = _repository.Get();

			if (response == null || !response.Any())
				return NotFound();

			return Ok(response);
		}

		[HttpGet]
		[Route("drawings/{id}")]
		public IHttpActionResult PowerballResult(string id)
		{
			var response = _repository.Get(id);

			if (response == null)
				return NotFound();

			return Ok(response);
		}
		[HttpGet]
		[Route("drawings")]
		public IHttpActionResult PowerballResults(string after, string before)
		{
			var response = _repository.GetByRange(after, before);

			if (response == null)
				return NotFound();

			return Ok(response);
		}
	}
}