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
        public IHttpActionResult Get(int page = 1, int results = 20)
        {
            using (var db = new TccSettings())
            {
                var leitoresCount = db.tblLeitor.Count();
                if ((page - 1) * results > leitoresCount)
                    return Json(new
                    {
                        total_count = leitoresCount,
                        count = 0,
                        leitores = new object[] { }
                    });

                var leitores = db.tblLeitor.Skip(page * results).Take(results).ToArray();

                return Json(new
                {
                    total_count = leitoresCount,
                    count = leitores.Count(),
                    leitores
                });
            }
        }


        public IHttpActionResult Post(dynamic leitor)
        {
            using (var db = new TccSettings())
            {
                var addedLeitor = db.tblLeitor.Add(leitor);
                db.SaveChanges();
                return Json(leitor);
            }
        }


        public IHttpActionResult Get(int id)
        {

            using (var db = new TccSettings())
            {
                var leitor = db.tblLeitor.Select(l => new
                {
                    l.IDLeitor,
                    TipoLeitor = l.tblTipoLeitor,
                    l.Nome,
                    l.RG,
                    l.CPF,
                    l.DataCadastro,
                    l.DataNasc,
                    l.Email,
                    l.Endereco,
                    ImagemLeitor = Convert.ToBase64String(l.ImagemLeitor),
                }).First(l => l.IDLeitor == id);
                return Json(leitor);
            }
        }

        public IHttpActionResult Put(int id, tblLeitor leitor)
        {
            using (var db = new TccSettings())
                try
                {
                    tblLeitor _leitor = db.tblLeitor.First(l => l.IDLeitor == id);
                    _leitor.Nome = leitor.Nome ?? _leitor.Nome;
                    _leitor.CPF = leitor.CPF ?? _leitor.CPF;
                    _leitor.RG = leitor.RG ?? _leitor.RG;
                    _leitor.DataNasc = leitor.DataNasc ?? _leitor.DataNasc;
                    _leitor.IDTipoLeitor = leitor.IDTipoLeitor ?? _leitor.IDTipoLeitor;
                    _leitor.Email = leitor.Email ?? _leitor.Email;
                    _leitor.Telefone = leitor.Telefone ?? _leitor.Telefone;
                    _leitor.Endereco = leitor.Endereco ?? _leitor.Endereco;
                    db.SaveChanges();
                    return Json(_leitor);
                }
                catch (Exception e)
                {
                    return Json(new
                    {
                        Code = 404,
                        e.Message
                    });
                }
        }

        public IHttpActionResult Delete(int id)
        {
            using (var db = new TccSettings())
            {
                db.tblLeitor.Remove(db.tblLeitor.First(l => l.IDLeitor == id));
                return StatusCode(HttpStatusCode.OK);
            }
        }
    }
}
