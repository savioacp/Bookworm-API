using Bookworm_API.Models;
using Bookworm_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bookworm_API.Controllers
{
    public class LoginController : ApiController
    {
        public struct LoginData
		{
            public string Email;
            public string Senha;
		}

        public JsonResult<object> PostUser([FromBody] LoginData login)
        {
            using (var db = new TccSettings()) {
                /*Leitor currentLeitor = Leitor.GetLeitor(login.Email);*/
                var leitor = db.tblLeitor.FirstOrDefault(l => l.Email == login.Email);
                if (Authentication.LogUserIn(leitor, login.Senha))
                    return Json(new
                    {
                        Code = 200,
                        Token = Authorization.GenerateJWT(leitor)
                    } as object);
                return Json(new
                {
                    Code = 402
                } as object);

            }
        }
    }
}
