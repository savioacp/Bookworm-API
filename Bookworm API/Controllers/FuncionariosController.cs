using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Bookworm_API.Models;

namespace Bookworm_API.Controllers
{
    public class FuncionariosController : ApiController
    {
        // GET /funcionarios
        public JsonResult<Funcionario[]> Get()
        {
            return Json(Funcionario.GetFuncionarios());
        }
    }
}
