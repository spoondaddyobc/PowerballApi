namespace PowerballApi.Api.Controllers
{
	using System.Linq;
	using System.Web.Http;
	using Repositories;

	public class PowerballController : ApiController
	{
		private readonly IPowerballRepository _repository;

		public PowerballController()
		{
			_repository = new PowerballRepository();
		}

		[HttpGet]
		public IHttpActionResult PowerballResults()
		{
			var response = _repository.Get();

			if (response == null || !response.Any())
				return NotFound();

			return Ok(response);
		}
	}
}
