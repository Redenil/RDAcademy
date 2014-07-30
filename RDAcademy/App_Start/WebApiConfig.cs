using System.Net.Http.Formatting;
using System.Web.Http;

namespace RDAcademy
{
    using System.Web.Http.OData.Builder;

    using RDAcademy.Models;
    using System.Net.Http.Headers;
    using RDAcademy.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var newFormater = new FixedWidthTextMediaFormatter();
            config.Formatters.Add(newFormater);

            newFormater.MediaTypeMappings.Add(new QueryStringMapping("frmt", "fwt", new MediaTypeHeaderValue("text/plain")));

            // Serialize les types complexes qui sont rattachés à l'objet
            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            // New code:
            // Odata
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Individual>("Individuals");
            builder.EntitySet<Contract>("Contracts");
            ODataHttpRouteCollectionExtensions.MapODataRoute(config.Routes, routeName: "ODataRoute", routePrefix: "odata", model: builder.GetEdmModel());

            builder.Entity<Individual>().Ignore(item => item.Contracts);
            builder.Entity<Contract>().Ignore(item => item.Individual);
        }
    }
}
