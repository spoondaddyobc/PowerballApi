namespace PowerballApi.Api.Helper
{
	using System.Threading.Tasks;

	public interface IHttpHandler
	{
		Task<string> GetStringAsync(string requestUri);
	}
}
