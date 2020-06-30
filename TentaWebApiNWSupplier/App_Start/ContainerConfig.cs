using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System.Reflection;
using System.Web.Http;
using TentaWebApiNWSupplier.Data;
using TentaWebApiNWSupplier.Models;

namespace TentaWebApiNWSupplier.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(builder);
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();
            var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            /* 
             RegisterServices(builder);
             builder.RegisterWebApiModelBinderProvider();
            */
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SupplierMappingProfile());
            });

            builder.RegisterInstance(config.CreateMapper())
              .As<IMapper>()
              .SingleInstance();

            builder.RegisterType<NorthWindDataBaseContext>()
                .InstancePerRequest();

            builder.RegisterType<SupplierData>()
                .As<ISupplierData>()
                .InstancePerRequest();
        }
    }
}