namespace PowerballApi.Api.Helpers.Parser
{
	using System.Collections.Generic;

	public interface IFileParser<T> where T : class 
	{
		List<T> Parse(string file);
	}
}