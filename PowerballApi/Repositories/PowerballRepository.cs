using System;
using System.Linq;

namespace PowerballApi.Api.Repositories
{
    using System.Collections.Generic;
    using Helpers.Cacher;
    using Helpers.HttpHandler;
    using Helpers.Parser;
    using Models;

    public class PowerballRepository : IRepository<PowerballSet>
    {
        public string CacheName { get; set; } = @"PowerballData";
        public int DaysUntilStale { get; set; } = 1;
        public string PowerballUrl { get; set; } = @"http://www.powerball.com/powerball/winnums-text.txt";

        private readonly ICacher _cacher;
        private readonly IHttpHandler _httpHandler;
        private readonly IParser<PowerballSet, string> _parser;

        public PowerballRepository()
        {
            _cacher = new Cacher();
            _httpHandler = new HttpHandler();
            _parser = new PowerballParser();
        }

        public PowerballRepository(ICacher cacher, IHttpHandler httpHandler, IParser<PowerballSet, string> parser)
        {
            _cacher = cacher;
            _httpHandler = httpHandler;
            _parser = parser;
        }

        public List<PowerballSet> Get()
        {
            return GetDrawings();
        }

        public PowerballSet GetById(string id)
        {
            var data = GetDrawings();

            var result = data.FirstOrDefault(p => p.Date == id);
            return result;
        }

        public List<PowerballSet> GetByRange(string idBegin, string idEnd)
        {
            var data = GetDrawings();

            var upperLimit = data.FindAll(p => DateTime.Parse(p.Date) >= DateTime.Parse(idBegin));
            var results = upperLimit.FindAll(p => DateTime.Parse(p.Date) <= DateTime.Parse(idEnd));

            return results.ToList();
        }

        private List<PowerballSet> GetDrawings()
        {
            var cache = _cacher.Get(CacheName);
            List<PowerballSet> results;
            if (cache != null)
            {
                results = (List<PowerballSet>)cache;
            }
            else
            {
                var file = _httpHandler.GetStringAsync(PowerballUrl).Result;
                results = _parser.Parse(file);
                _cacher.Set(CacheName, results, DaysUntilStale);
            }
            return results;
        }
    }
}