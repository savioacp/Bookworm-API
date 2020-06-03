using Bookworm_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bookworm_API.Controllers
{
    public class LeitorController : ApiController
    {
        public JsonResult<Leitor[]> Get()
        {
            return Json(Leitor.GetLeitores());
        }

        public JsonResult<Leitor> Post(Leitor leitor)
        {
            return Json(leitor.Add());
        }


        public JsonResult<object> Get(int id)
        {
            try
            {
                return Json(Leitor.GetLeitor(id) as object);
            }
            catch (Exception)
            {
                return Json(new
                {
                    Code = 404,
                    Message = "Leitor não encontrado"
                } as object);
            }
        }

        public JsonResult<object> Put(int id, Leitor leitor)
        {
            try
            {
                Leitor _leitor = Leitor.GetLeitor(id);
                _leitor.Nome = leitor.Nome ?? _leitor.Nome;
                _leitor.RG = leitor.RG ?? _leitor.RG;
                _leitor.DataNascimento = leitor.DataNascimento == null ? _leitor.DataNascimento : leitor.DataNascimento;
                _leitor.TipoLeitor = leitor.TipoLeitor ?? _leitor.TipoLeitor;
                _leitor.Email = leitor.Email ?? _leitor.Email;
                _leitor.Telefone = leitor.Telefone ?? _leitor.Telefone;
                _leitor.Endereço = leitor.Endereço ?? _leitor.Endereço;

                return Json(_leitor.Commit() as object);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Code = 404,
                    Message = e.Message
                } as object);
            }
        }
    }
}
