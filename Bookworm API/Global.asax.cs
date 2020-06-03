using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bookworm_API.Models;
using Dapper.FluentMap;

namespace Bookworm_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new EntityMaps.LeitorMap());
                config.AddMap(new EntityMaps.EventoMap());
                config.AddMap(new EntityMaps.FuncionarioMap());
            });
            //TODO: Authentication
            //TODO: Authorization
            //TODO: Pagination
        }
    }
}
