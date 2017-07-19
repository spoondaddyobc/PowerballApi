namespace PowerballApi.Api.Helpers.HttpHandler
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public interface IHttpHandler
	{
		HttpResponseMessage Get(string uri);
		Task<HttpResponseMessage> GetAsync(string uri);
	}
}