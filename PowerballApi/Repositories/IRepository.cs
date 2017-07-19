namespace PowerballApi.Api.Repositories
{
    using System.Collections.Generic;
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        T Get(string id);
        List<T> GetByRange(string idStart, string idEnd);
    }
}