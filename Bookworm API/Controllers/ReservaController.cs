using Antlr.Runtime.Tree;
using Bookworm_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Bookworm_API.Controllers
{
    public class ReservaController : ApiController
    {
        public IHttpActionResult Post(int idLivro)
        {
            if (RequestContext.Principal?.Identity == null)
                return StatusCode(HttpStatusCode.Unauthorized);

            using (var db = new TccSettings())
            {
                if (db.tblReserva.Count(r => r.IDProduto == idLivro && DbFunctions.AddDays(r.DataReserva, 7) > DateTime.Now) > 1)
                    return Json(new { 
                        Code = 400,
                        Message = "Reserva indisponível"
                    });

                db.tblReserva.Add(new tblReserva() { 
                    IDLeitor = (RequestContext.Principal.Identity as LeitorIdentity).IDLeitor,
                    DataReserva = DateTime.Now,
                    IDProduto = idLivro
                });
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
