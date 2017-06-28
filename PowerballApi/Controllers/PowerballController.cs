using System.Runtime.InteropServices.ComTypes;


namespace PowerballApi.Api.Controllers
{
	using System.Linq;
	using System.Web.Http;
	using Repositories;
    using Models;
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
		public IHttpActionResult PowerballResults()
		{
			var response = _repository.Get();

			if (response == null || !response.Any())
				return NotFound();

			return Ok(response);
		}
	}
}