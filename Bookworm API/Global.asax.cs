using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bookworm_API.Models;

namespace Bookworm_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public const string JWTSecret = "nTXA/UO8iDjy3Mlkk1Q1+pjfppDH0Cf1x5JM+6p4x9M=";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            

            //TODO: Atualizar models para o novo banco
            //TODO: Authorization
        }

        
    }
}
