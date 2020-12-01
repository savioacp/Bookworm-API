using Bookworm_API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                var leitores = db.tblLeitor
                    .Include("tbReserva")
                    .Include("tblFavoritos").OrderBy(l => l.IDLeitor).Skip((page - 1) * results).Take(results)
                    .Select(l => new
                    {
                        l.IDLeitor,
                        l.RG,
                        l.CPF,
                        l.DataCadastro,
                        l.DataNasc,
                        l.Email,
                        l.Endereco,
                        l.ImagemLeitor,
                        l.tblTipoLeitor.TipoLeitor,
                        l.Nome,
                        l.Telefone,
                        Reservas = l.tblReserva.Select(r => new
                        {
                            r.IDReserva,
                            r.IDProduto
                        }).ToList(),
                        Favoritos = l.tblFavoritos.Select(f => f.IDProduto).ToList()
                    })
                    .ToList();

                return Json(new
                {
                    total_count = leitoresCount,
                    count = leitores.Count(),
                    leitores
                });
            }
        }


        public IHttpActionResult Post(dynamic _leitor)
        {
            var leitor = (_leitor as Newtonsoft.Json.Linq.JObject).ToObject<tblLeitor>();
            using (var db = new TccSettings())
            {
                leitor.IDLeitor = (db.tblLeitor.OrderByDescending(l => l.IDLeitor).FirstOrDefault()?.IDLeitor ?? 0) + 1;
                leitor.Senha = "";
                leitor.Salt = "";
                leitor.DataCadastro = DateTime.Now;
                var addedLeitor = db.tblLeitor.Add(leitor);
                db.SaveChanges();
                return Json(addedLeitor);
            }
        }


        public IHttpActionResult Get(int id)
        {

            using (var db = new TccSettings())
            {
                var leitor = db.tblLeitor
                    .Include("tbReserva")
                    .Include("tblFavoritos")
                    .Select(l => new
                    {
                        l.IDLeitor,
                        l.RG,
                        l.CPF,
                        l.DataCadastro,
                        l.DataNasc,
                        l.Email,
                        l.Endereco,
                        l.ImagemLeitor,
                        l.tblTipoLeitor.TipoLeitor,
                        l.Nome,
                        l.Telefone,
                        Reservas = l.tblReserva.Select(r => new
                        {
                            r.IDReserva,
                            r.IDProduto
                        }).ToList(),
                        Favoritos = l.tblFavoritos.Select(f => f.IDProduto).ToList()
                    }).First(l => l.IDLeitor == id);
                return Json(leitor);
            }
        }

        public IHttpActionResult Put(int id, tblLeitor leitor)
        {
            using (var db = new TccSettings())
                try
                {
                    tblLeitor _leitor = db.tblLeitor.Include("tblEmprestimo").Include("tblFavoritos").First(l => l.IDLeitor == id);
                    _leitor.Nome = leitor.Nome ?? _leitor.Nome;
                    _leitor.CPF = leitor.CPF ?? _leitor.CPF;
                    _leitor.RG = leitor.RG ?? _leitor.RG;
                    _leitor.DataNasc = leitor.DataNasc ?? _leitor.DataNasc;
                    _leitor.IDTipoLeitor = leitor.IDTipoLeitor ?? _leitor.IDTipoLeitor;
                    _leitor.Email = leitor.Email ?? _leitor.Email;
                    _leitor.Telefone = leitor.Telefone ?? _leitor.Telefone;
                    _leitor.Endereco = leitor.Endereco ?? _leitor.Endereco;
                    db.SaveChanges();
                    return Ok();
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

        [Route("leitor/{id:int}/favoritos")]
        public IHttpActionResult Put(int id, int[] favs)
        {
            using (var db = new TccSettings())
            {
                try
                {
                    var livros = (from l in db.tblProduto
                                  where favs.Contains(l.IDProduto)
                                  select l).ToList();

                    var amiguinho = (from l in db.tblLeitor
                                     where l.IDLeitor == id
                                     select l).First();

                    amiguinho.tblFavoritos = livros.Select(l => new tblFavoritos()
                    {
                        IDProduto = l.IDProduto
                    }).ToList();

                    db.SaveChanges();

                    return Ok();
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
        }

        public IHttpActionResult Delete(int id)
        {
            using (var db = new TccSettings())
            {
                db.tblLeitor.Remove(db.tblLeitor.First(l => l.IDLeitor == id));
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
