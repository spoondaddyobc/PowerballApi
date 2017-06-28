namespace PowerballApi.Api.Helpers.Parser
{
	using System.Collections.Generic;

	public interface IParser<TResponse, in TRequest>
		where TResponse : class
		where TRequest : class 
	{
		List<TResponse> Parse(TRequest request);
	}
}