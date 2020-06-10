using Bookworm_API.Models;
using Bookworm_API.Services;
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
        public JsonResult<object> Post([FromBody]Login value)
        {
            if (Authentication.LogUserIn(Leitor.GetLeitor(value.Email), value.Senha))
                return Json(new
                {
                    Code = 200
                } as object);
            return Json(new 
            { 
                Code = 402
            } as object);
        }
    }
}
