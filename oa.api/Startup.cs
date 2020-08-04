using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using oa.api.Providers;
using oa.db.Context;
using oa.services.Business;
using oa.services.Personnel;
using oa.services.Schedule;
using oa.services.Service;
using oa.services.User;
using Owin;
using System;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(oa.api.Startup))]

namespace oa.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Autofac
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<PersonnelService>().As<IPersonnelService>();
            builder.RegisterType<BusinessService>().As<IBusinessService>();
            builder.RegisterType<ServicesService>().As<IServicesService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<OAContext>().As<IOAContext>();
            builder.RegisterType<ScheduleService>().As<IScheduleService>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthorizationServerProvider>()
                      .PropertiesAutowired() 
                      .SingleInstance();
            var container = builder.Build();

            // Autofac - Run
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            WebApiConfig.Register(config);
            ConfigureOAuth(app, container);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling 
                = ReferenceLoopHandling.Ignore;

            app.UseWebApi(config);


        }


        public void ConfigureOAuth(IAppBuilder app, IContainer container)
        {

            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = container.Resolve<AuthorizationServerProvider>()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}
