using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
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

        public JsonResult<Funcionario> Post(Funcionario func)
        {
            return Json(func.Add());
        }


        public JsonResult<object> Get(int id)
        {
            try
            {
                return Json(Funcionario.GetFuncionario(id) as object);
            }
            catch (Exception)
            {
                return Json(new
                {
                    Code = 404,
                    Message = "Funcionário não encontrado"
                } as object);
            }
        }

        public JsonResult<object> Put(int id, Funcionario func)
        {
            try
            {
                Funcionario _f = Funcionario.GetFuncionario(id);
                _f.Nome = func.Nome ?? _f.Nome;
                _f.CPF = func.CPF ?? _f.CPF;
                _f.Cargo = func.Cargo ?? _f.Cargo;
                _f.Email = func.Email ?? _f.Email;
                _f.Telefone = func.Telefone ?? _f.Telefone;
                _f.Endereço = func.Endereço ?? _f.Endereço;

                return Json(_f.Commit() as object);
            }
            catch (Exception)
            {
                return Json(new
                {
                    Code = 404,
                    Message = "Funcionário não encontrado"
                } as object);
            }
        }
    }
}
