using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Bookworm_API.Services;

namespace Bookworm_API.Controllers
{
    public class RecoverController : ApiController
    {
        public IHttpActionResult Get(string email)
        {
            return Ok();
        }
    }
}
