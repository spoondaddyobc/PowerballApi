namespace PowerballApi.Api.Helper
{
	public interface ICacher
	{
		object Get(string name);
		void Set(string name, object data, int daysUntilStale);
	}
}
