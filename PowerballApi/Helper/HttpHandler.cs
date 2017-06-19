namespace PowerballApi.Api.Helper
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

		public Task<string> GetStringAsync(string requestUri)
		{
			return _client.GetStringAsync(requestUri);
		}
	}
}
