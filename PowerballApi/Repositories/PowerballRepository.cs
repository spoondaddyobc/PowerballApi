namespace PowerballApi.Api.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Helpers.Cacher;
	using Helpers.Parser;
	using Models;
	using Services;

	public class PowerballRepository : IRepository<PowerballSet>
	{
		public string CacheName { get; set; } = @"PowerballData";
		public int DaysUntilStale { get; set; } = 1;

		private readonly ICacher _cacher;
		private readonly IParser<PowerballSet, string> _parser;
		private readonly IService _service;

		public PowerballRepository(ICacher cacher, IParser<PowerballSet, string> parser, IService service)
		{
			_cacher = cacher;
			_parser = parser;
			_service = service;
		}

		public List<PowerballSet> Get()
		{
			var cache = _cacher.Get(CacheName);
			if (cache != null)
				return (List<PowerballSet>)cache;

			var file = _service.Get();
			var results = _parser.Parse(file);
			_cacher.Set(CacheName, results, DaysUntilStale);
			return results;
		}

		public PowerballSet Get(string id)
		{
			DateTime date;
			if (!DateTime.TryParse(id, out date))
				throw new ArgumentException("The date input was invalid");

			var data = Get();

			try
			{
				return data.SingleOrDefault(p => p.Date.Equals(date));
			}
			catch (Exception ex)
			{
				throw new Exception("Could not process the request.", ex);
			}
		}

		public List<PowerballSet> GetByRange(string after, string before)
		{
			DateTime afterDate;
			DateTime beforeDate;
			if (!DateTime.TryParse(after, out afterDate) ||
			    !DateTime.TryParse(before, out beforeDate) ||
			    DateTime.Compare(afterDate, beforeDate) > 0)
				throw new ArgumentException("One or both date inputs were invalid.");

			var data = Get();

			try
			{
				return data.FindAll(p => afterDate <= p.Date && p.Date <= beforeDate);
			}
			catch (Exception ex)
			{
				throw new Exception("Could not process the request", ex);
			}
		}
	}
}