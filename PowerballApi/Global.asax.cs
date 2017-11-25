using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using PowerballApi.Api.Helpers.Cacher;


namespace PowerballApi.Api
{
	using System.Web.Http;

	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			
            Bootstrapper.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            

		}
	}
}
