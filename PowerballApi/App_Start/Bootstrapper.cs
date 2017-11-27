namespace PowerballApi.Api
{
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Helpers.Cacher;
    using Helpers.HttpHandler;
    using Helpers.Parser;
    using Models;
    using Repositories;
    using Services;

    public class Bootstrapper
    {

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Cacher>().As<ICacher>();
            builder.RegisterType<HttpHandler>().As<IHttpHandler>();
            builder.RegisterType<PowerballParser>().As<IParser<PowerballSet, string>>();
            builder.RegisterType<PowerballRepository>().As<IRepository<PowerballSet>>();
            builder.RegisterType<DrawingsService>().As<IService>();

            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        
    }
}