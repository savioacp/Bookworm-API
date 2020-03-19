using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookworm_API;

namespace Bookworm_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("<a href=\"/eventos\">/eventos</a>");
        }
    }
}
