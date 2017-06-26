namespace PowerballApi.Api.Helpers.PowerballParser
{
	using System.Collections.Generic;

	public interface IFileParser<T> where T : class 
	{
		List<T> Parse(string file);
	}
}