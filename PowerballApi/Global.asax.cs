namespace PowerballApi
{
	using System.Web.Http;

	public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            var cacher = new Cacher();
            cacher.CacheData();
        }
    }
}
