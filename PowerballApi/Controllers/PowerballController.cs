namespace PowerballApi.Api.Controllers
{
	using System.Linq;
	using System.Web.Http;
	using Helpers.Cacher;
	using Helpers.HttpHandler;
	using Helpers.PowerballParser;
	using Repositories;

	public class PowerballController : ApiController
	{
		private ICacher _cacher;
		private IHttpHandler _httpHandler;
		private IPowerballParser _parser;
		private readonly IPowerballRepository _repository;

		public PowerballController()
		{
			_cacher = new Cacher();
			_httpHandler = new HttpHandler();
			_parser = new PowerballParser();
			_repository = new PowerballRepository(_cacher, _httpHandler, _parser);
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