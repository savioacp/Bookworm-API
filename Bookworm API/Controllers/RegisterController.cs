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
    public class LoginByEmail
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
    public class RegisterController : ApiController
    {
        public JsonResult<object> Post(LoginByEmail value)
        {
            //if(User.Identity == null)
            //    return Json(new
            //    {
            //        Code = 403,
            //        Message = "Não autorizado"
            //    } as object);
            //if (((FuncionarioIdentity)User.Identity).PermissionLevel > 1)
            //    return Json(new
            //    {
            //        Code = 403,
            //        Message = "Não autorizado"
            //    } as object);
            using(var db = new TccSettings())
                if (Authentication.RegisterUser(db.tblLeitor.First(l => l.Email == value.Email), value.Senha))
                    return Json(new
                    {
                        Code = 200,
                        Message = "Usuário registrado com sucesso"
                    } as object);
                else
                    return Json(new
                    {
                        Code = 500,
                        Message = "Ocorreu um erro ao registrar o usuário."
                    } as object);
        }
    }
}
