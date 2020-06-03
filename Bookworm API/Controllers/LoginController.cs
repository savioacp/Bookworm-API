using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bookworm_API.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login/5
        public JsonResult<string> Get(int id)
        {
            return Json("value");
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {

        }
    }
}
