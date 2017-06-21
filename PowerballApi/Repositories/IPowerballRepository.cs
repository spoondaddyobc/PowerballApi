namespace PowerballApi.Api.Repositories
{
	using System.Collections.Generic;
	using Models;

	public interface IPowerballRepository
	{
		List<PowerballSet> Get();
	}
}