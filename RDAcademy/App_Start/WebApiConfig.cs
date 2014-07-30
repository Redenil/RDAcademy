using System.Net.Http.Formatting;
using System.Web.Http;

namespace RDAcademy
{
    using System.Web.Http.OData.Builder;

    using RDAcademy.Models;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Adding formatters
            //config.Formatters.Clear();
            //config.Formatters.Add(new XmlMediaTypeFormatter());
            //config.Formatters.Add(new JsonMediaTypeFormatter());
            //config.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());

            // New code:
            // Odata
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Individual>("Individuals");
            builder.EntitySet<Contract>("Contracts");
            ODataHttpRouteCollectionExtensions.MapODataRoute(config.Routes, routeName: "ODataRoute", routePrefix: "odata", model: builder.GetEdmModel());
        }
    }
}
