using Bookworm_API.Models;
using System;
using System.Collections.Generic;
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
                if (page * results > prodCount)
                    return Json(new 
                    { 
                        total_count = prodCount,
                        count = 0,
                        produtos = new object[] { }
                    } as object);
                
                var produtos = db.tblProduto.OrderByDescending(p => p.IDProduto)
                    .Skip(page * results)
                    .Take(results)
                    .Select(p => new { 
                        p.AnoEdicao, 
                        p.AutoresLivro, 
                        p.DescricaoProd, 
                        p.Editora, 
                        p.Fileira, 
                        p.IDProduto, 
                        p.ImagemProd, 
                        p.NomeLivro, 
                        p.Prateleira, 
                        p.Setor, 
                        Reservas= p.tblReserva.OrderBy(r => r.DataReserva).ToArray(),
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

        public JsonResult<object> Get(string query, int page = 1, int results = 20)
        {
            using (var db = new TccSettings())
            {
                int prodCount = db.tblProduto.Count();
                if (page * results > prodCount)
                    return Json(new
                    {
                        total_count = prodCount,
                        count = 0,
                        produtos = new object[] { }
                    } as object);

                var produtos = db.tblProduto
                    .Where(p => p.Editora.Contains(query) || p.NomeLivro.Contains(query) || p.AutoresLivro.Contains(query))
                    .OrderByDescending(p => p.IDProduto)
                    .Skip(page * results)
                    .Take(results)
                    .Select(p => new {
                        p.AnoEdicao,
                        p.AutoresLivro,
                        p.DescricaoProd,
                        p.Editora,
                        p.Fileira,
                        p.IDProduto,
                        p.ImagemProd,
                        p.NomeLivro,
                        p.Prateleira,
                        p.Setor,
                        Reservas = p.tblReserva.OrderBy(r => r.DataReserva).ToArray(),
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
