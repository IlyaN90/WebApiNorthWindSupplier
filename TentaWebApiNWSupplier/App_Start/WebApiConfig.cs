using Newtonsoft.Json.Serialization;
using System.Web.Http;
using TentaWebApiNWSupplier.App_Start;
using TentaWebApiNWSupplier.BasicAuthentication;

namespace TentaWebApiNWSupplier
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //authentication
            config.Filters.Add(new BasicAuthenticationAttribute());

            // Web API configuration and services
            ContainerConfig.RegisterContainer();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
