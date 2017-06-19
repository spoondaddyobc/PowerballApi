namespace PowerballApi.Api.Repositories
{
	using System.Collections.Generic;
	using Helper;
	using Models;

	public class PowerballRepository : IPowerballRepository
	{
		private const string _cacheName = @"PowerballData";
		private const int _daysUntilStale = 1;
		private const string _powerballUrl = @"http://www.powerball.com/powerball/winnums-text.txt";
		private readonly ICacher _cacher;
		private readonly IHttpHandler _httpHandler;
		private readonly IPowerballParser _parser;

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

			var file = GetPowerballFile();
			var results = _parser.Parse(file);
			_cacher.Set(_cacheName, results, _daysUntilStale);
			return results;
		}

		private string GetPowerballFile()
		{
			return _httpHandler.GetStringAsync(_powerballUrl).Result;
		}
	}
}
