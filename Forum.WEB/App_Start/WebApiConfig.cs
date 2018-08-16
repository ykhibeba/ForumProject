using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Forum.WEB
{
    /// <summary>
    /// Configuration class for WebApi application
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register configuration. services and routes
        /// </summary>
        /// <param name="config">Configuration parametr</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/forum/{categoryid}/{postid}",
                defaults: new { categoryid = RouteParameter.Optional, postid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CreatePost",
                routeTemplate: "api/forum/{categoryid}/post",
                defaults: new { categoryid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CreateComment",
                routeTemplate: "api/forum/{categoryid}/{postid}/comments",
                defaults: new { categoryid = RouteParameter.Optional, postid = RouteParameter.Optional }
            );



            //Remove xml format from app, default will be JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Add authorize filter
            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}
