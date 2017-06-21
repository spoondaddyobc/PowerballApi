namespace PowerballApi.Api.Repositories
{
	using System.Collections.Generic;
	using Helpers.Cacher;
	using Helpers.HttpHandler;
	using Helpers.PowerballParser;
	using Models;

	public class PowerballRepository : IPowerballRepository
	{
		public string CacheName { get; set; } = @"PowerballData";
		public int DaysUntilStale { get; set; } = 1;
		public string PowerballUrl { get; set; } = @"http://www.powerball.com/powerball/winnums-text.txt";

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
			var cache = _cacher.Get(CacheName);
			if (cache != null)
				return (List<PowerballSet>)cache;

			return GetFreshDrawings();
		}

		private List<PowerballSet> GetFreshDrawings()
		{
			var file = _httpHandler.GetStringAsync(PowerballUrl).Result;
			var results = _parser.Parse(file);
			_cacher.Set(CacheName, results, DaysUntilStale);
			return results;
		}
	}
}