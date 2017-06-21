namespace PowerballApi.Api.Helpers.Cacher
{
	public interface ICacher
	{
		object Get(string name);
		void Set(string name, object data, int daysUntilStale);
	}
}