namespace PowerballApi.Api.Repositories
{
	using System.Collections.Generic;
	using Helpers.Cacher;
	using Helpers.HttpHandler;
	using Helpers.PowerballParser;
	using Models;

	public class PowerballRepository : IPowerballRepository
	{
		private const string _cacheName = @"PowerballData";
		private const int _daysUntilStale = 1;
		private const string _powerballUrl = @"http://www.powerball.com/powerball/winnums-text.txt";
		private readonly ICacher _cacher;
		private readonly IHttpHandler _httpHandler;
		private readonly IPowerballParser _parser;

		public PowerballRepository()
		{
			_cacher = new Cacher();
			_httpHandler = new HttpHandler();
			_parser = new PowerballParser();
		}

		public PowerballRepository(ICacher cacher, IHttpHandler httpHandler, IPowerballParser parser)
		{
			_cacher = cacher;
			_httpHandler = httpHandler;
			_parser = parser;
		}

		public List<PowerballSet> Get()
		{
			var cache = _cacher.Get(_cacheName);
			if (cache != null)
				return (List<PowerballSet>)cache;

			return GetPowerballDrawings();
		}

		private List<PowerballSet> GetPowerballDrawings()
		{
			var file = _httpHandler.GetStringAsync(_powerballUrl).Result;
			var results = _parser.Parse(file);
			_cacher.Set(_cacheName, results, _daysUntilStale);
			return results;
		}
	}
}
