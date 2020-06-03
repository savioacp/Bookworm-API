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
    public class RegisterController : ApiController
    {
        public JsonResult<object> Post(Leitor leitor, [FromUri]string password)
        {
            if (Authentication.RegisterUser(leitor, password))
                return Json(new
                {
                    Code = 200,
                    Message = "sucesso, mensagem vai mudar"
                } as object);
            else
                return Json(new
                {
                    Code = 403,
                    Message = "deu ero parsa"
                } as object);
        }
    }
}
