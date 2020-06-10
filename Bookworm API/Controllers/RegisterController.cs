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
        public JsonResult<object> Post(Login value)
        {
            if (Authentication.RegisterUser(Leitor.GetLeitor(value.Email), value.Senha))
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
