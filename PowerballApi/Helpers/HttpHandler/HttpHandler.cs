namespace PowerballApi.Api.Helpers.HttpHandler
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public class HttpHandler : IHttpHandler
	{
		private readonly HttpClient _client;

		public HttpHandler()
		{
			_client = new HttpClient();
		}

		public HttpResponseMessage Get(string uri)
		{
			return GetAsync(uri).Result;
		}

		public Task<HttpResponseMessage> GetAsync(string uri)
		{
			return _client.GetAsync(uri);
		}
	}
}