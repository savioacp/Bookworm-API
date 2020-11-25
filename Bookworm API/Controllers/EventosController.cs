using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Bookworm_API.Models;

namespace Bookworm_API.Controllers
{
    public class EventosController : ApiController
    {
        // GET /eventos
        public JsonResult<object> Get(int page = 1, int results = 20)
        {
            using (var db = new TccSettings())
			{
                var eventCount = db.tblEvento.Count();

                if ((page - 1) * results > eventCount)
                    return Json(new
                    {
                        total_count = eventCount,
                        count = 0,
                        eventos = new object[] { }
                    } as object);

                var eventos = db.tblEvento.OrderByDescending(p => p.IDEvento)
                    .Skip(page * results)
                    .Take(results)
                    .ToArray();

                return Json(new 
                { 
                    total_count = eventCount,
                    count = eventos.Count(),
                    eventos
                } as object);

            }
        }

        // POST /eventos
        public JsonResult<object> Post(dynamic e)
        {   
            using (var db = new TccSettings())
			{
                var addedEvent = db.tblEvento.Add(new tblEvento
                {
                    Email = e.Email,
                    Descricao = e.Descricao,
                    Responsavel = e.Responsavel,
                    Titulo = e.Titulo,
                });

                return Json(addedEvent as object);
			}
        }


        // GET /eventos/{id}
        public IHttpActionResult Get(int id)
        {
            using (var db = new TccSettings())
			{
                var evento = db.tblEvento.FirstOrDefault(e => e.IDEvento == id);

                if (evento == null)
                    return NotFound();

                return Json(evento);
            }
        }

        // PUT /eventos/{id}
        public IHttpActionResult Put(int id, tblEvento e)
        {
            try
            {
                using (var db = new TccSettings()) {
                    tblEvento _e = db.tblEvento.First(i => i.IDEvento == id);
                    _e.Titulo = e.Titulo ?? _e.Titulo;
                    _e.Descricao = e.Descricao ?? _e.Descricao;
                    _e.Email = e.Email ?? _e.Email;
                    _e.Responsavel = e.Responsavel ?? _e.Responsavel;

                    db.SaveChanges();
                    return Json(_e as object);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // DELETE /eventos/{id}
        public StatusCodeResult Delete(int id)
        {
            try
            {
                using (var db = new TccSettings())
                    db.tblEvento.Remove(db.tblEvento.Find(id));
                return StatusCode(HttpStatusCode.OK);
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET /eventos/search/{q}
        [Route("eventos/search/{q}")]
        public IHttpActionResult Get(string q, int page = 1, int results = 20)
        {
            using(var db = new TccSettings())
			{
                var searchResults = db.tblEvento.Where(p => p.Titulo.Contains(q) || p.IDEvento.ToString().Contains(q));
                int resultCount = searchResults.Count();
                if ((page - 1) * results > searchResults.Count())
                    return Json(new
                    {
                        count = 0,
                        total_count = resultCount,
                        eventos = new object[] { }
                    }) ;

                searchResults = searchResults
                    .Skip(page * results)
                    .Take(results);

                return Json(new
                {
                    count = searchResults.Count(),
                    total_count = resultCount,
                    eventos = searchResults.ToArray()
                }) ;
			}
        }
    }
}
