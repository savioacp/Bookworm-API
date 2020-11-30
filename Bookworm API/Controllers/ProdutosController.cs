using Bookworm_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bookworm_API.Controllers
{
    public class ProdutosController : ApiController
    {
        public JsonResult<object> Get(int page = 1, int results = 20)
        {
            using (var db = new TccSettings())
            {
                int prodCount = db.tblProduto.Count();
                if ((page - 1) * results > prodCount)
                    return Json(new
                    {
                        total_count = prodCount,
                        count = 0,
                        produtos = new object[] { }
                    } as object);

                var SeteDiasAtrás = DateTime.Now.AddDays(-7);

                var produtos = db.tblProduto
                    .Include("tblGeneroProduto")
                    .Include("tblReserva")
                    .Include("tblLeitor")
                    .Include("tblGenero")
                    .OrderByDescending(p => p.IDProduto)
                    .Skip((page - 1) * results)
                    .Take(results)
                    .Select(p => new
                    {
                        p.AnoEdicao,
                        p.AutoresLivro,
                        p.DescricaoProd,
                        p.Editora,
                        p.Fileira,
                        p.IDProduto,
                        p.ISBN,
                        p.ImagemProd,
                        p.NomeLivro,
                        p.Prateleira,
                        p.Setor,
                        Generos = p.tblGeneroProduto
                            .Select(gp => gp.tblGenero.NomeGenero)
                            .ToList(),

                        Reservas = p.tblReserva
                            .Where(r => r.DataReserva > SeteDiasAtrás)
                            .OrderByDescending(r => r.IDReserva)
                            .Select(
                            r => new
                            {
                                r.IDReserva,
                                r.DataReserva,
                            }).ToList(),
                        p.TipoAcervo,
                        p.TipoProduto,
                    })
                    .ToList();

                return Json(new
                {
                    total_count = prodCount,
                    count = produtos.Count,
                    produtos,
                } as object);
            }
        }

        [Route("produtos/count")]
        public IHttpActionResult Get([FromUri] string isbn)
        {
            using (var db = new TccSettings())
            {
                var count = db.tblProduto.Count(p => p.ISBN == isbn);
                return Json(count);
            }
        }

        [Route("produtos/count")]
        public IHttpActionResult Get([FromUri] string isbn, string reservas)
        {
            using (var db = new TccSettings())
            {
                var count = 
                    from p in db.tblProduto
                    where p.ISBN == isbn
                    select p.tblReserva.Count;

                return Json(count.Sum());
            }
        }

        public JsonResult<object> Get(string query, int page = 1, int results = 20)
        {
            using (var db = new TccSettings())
            {
                int prodCount = db.tblProduto.Count();
                if ((page - 1) * results > prodCount)
                    return Json(new
                    {
                        total_count = prodCount,
                        count = 0,
                        produtos = new object[] { }
                    } as object);

                var SeteDiasAtrás = DateTime.Now.AddDays(-7);

                var produtos = db.tblProduto
                    .Include("tblGeneroProduto")
                    .Include("tblReserva")
                    .Include("tblLeitor")
                    .Include("tblGenero")
                    .Where(p => p.Editora.ToLower().Contains(query) || p.NomeLivro.ToLower().Contains(query) || p.AutoresLivro.ToLower().Contains(query))
                    .OrderByDescending(p => p.IDProduto)
                    .Skip((page - 1) * results)
                    .Take(results)
                    .Select(p => new
                    {
                        p.AnoEdicao,
                        p.AutoresLivro,
                        p.DescricaoProd,
                        p.Editora,
                        p.Fileira,
                        p.IDProduto,
                        p.ISBN,
                        p.ImagemProd,
                        p.NomeLivro,
                        p.Prateleira,
                        p.Setor,
                        Generos = p.tblGeneroProduto
                            .Select(gp => gp.tblGenero.NomeGenero)
                            .ToList(),

                        Reservas = p.tblReserva
                            .Where(r => r.DataReserva > SeteDiasAtrás)
                            .OrderByDescending(r => r.IDReserva)
                            .Select(
                                r => new
                                {
                                    r.IDReserva,
                                    r.DataReserva,
                                }).ToList(),
                        p.TipoAcervo,
                        p.TipoProduto,
                    })
                    .ToList();

                return Json(new
                {
                    total_count = prodCount,
                    count = produtos.Count,
                    produtos,
                } as object);
            }
        }
    }
}
