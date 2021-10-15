using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AYMWebDevelopment.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.EnableCors();
            var corsAttr = new EnableCorsAttribute("http://41.38.247.175/AYMWebDevelopment/", "*", "*");
            config.EnableCors(corsAttr);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{controller}/{id}",
                routeTemplate: "AllPRJapis/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
