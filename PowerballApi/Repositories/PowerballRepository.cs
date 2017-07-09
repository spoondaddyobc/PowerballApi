using System.Net;
using System.Web.Http;

namespace PowerballApi.Api.Repositories
{
    using System.Collections.Generic;
    using Helpers.Cacher;
    using Helpers.HttpHandler;
    using Helpers.Parser;
    using Models;
    using System;
    using System.Linq;

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

        public PowerballSet Get(string id)
        {
            DateTime dateId;
            if (!DateTime.TryParse(id, out dateId))
                throw new ArgumentException("The date input was invalid");

            var data = GetDrawings();

            try
            {
                return data.SingleOrDefault(p => p.Date == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not process the request.", ex);
            }
        }

        public List<PowerballSet> GetByRange(string idBegin, string idEnd)
        {
            DateTime dateBegin;
            DateTime dateEnd;

            if (!DateTime.TryParse(idBegin, out dateBegin) || !DateTime.TryParse(idEnd, out dateEnd))
                throw new ArgumentException("One or both date inputs were invalid.");

            var data = GetDrawings();
            try
            {
                var results =
                    data.FindAll(
                        p =>
                            (dateBegin <= DateTime.Parse(p.Date) &&
                             DateTime.Parse(p.Date) <= dateEnd));
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not process the request", ex);
            }
        }

        private List<PowerballSet> GetDrawings()
        {
            var cache = _cacher.Get(CacheName);
            if (cache != null)
                return (List<PowerballSet>)cache;
            
            var file = _httpHandler.GetStringAsync(PowerballUrl).Result;
            var results = _parser.Parse(file);
            _cacher.Set(CacheName, results, DaysUntilStale);
            return results;
        }
    }
}