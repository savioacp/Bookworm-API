using Bookworm_API.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bookworm_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new JWTMiddleware());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
