namespace PowerballApi.Api.Repositories
{
	using System.Collections.Generic;
	using Models;

	public interface IRepository <T> where T : class 
	{
		List<T> Get();

	    T GetById();

	    List<T> GetByRange();
	}
}