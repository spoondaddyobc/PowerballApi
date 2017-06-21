namespace PowerballApi.Api.Helpers.PowerballParser
{
	using System.Collections.Generic;
	using Models;

	public interface IPowerballParser
	{
		List<PowerballSet> Parse(string file);
	}
}