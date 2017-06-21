namespace PowerballApi.Api.Helpers.HttpHandler
{
	using System.Threading.Tasks;

	public interface IHttpHandler
	{
		Task<string> GetStringAsync(string requestUri);
	}
}