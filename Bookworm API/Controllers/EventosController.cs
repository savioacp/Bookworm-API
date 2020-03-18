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
        public JsonResult<Evento[]> Get()
        {
            return Json(Evento.GetEventos());
        }

        // POST /eventos
        public JsonResult<Evento> Post(Evento e)
        {   
            return Json(e.Add());
        }


        // GET /eventos/{id}
        public JsonResult<object> Get(int id)
        {
            try
            {
                return Json(Evento.GetEvento(id) as object);
            }
            catch (IndexOutOfRangeException)
            {
                return Json(new
                {
                    Error = 404,
                    Message = "Evento não encontrado"
                } as object);
            }
        }

        // PUT /eventos/{id}
        public JsonResult<object> Put(int id, Evento e)
        {
            try
            {
                Evento _e = Evento.GetEvento(id);
                _e.Titulo = e.Titulo ?? _e.Titulo;
                _e.Descrição = e.Descrição ?? _e.Descrição;
                _e.Email = e.Email ?? _e.Email;
                _e.Responsável = e.Responsável ?? _e.Responsável;

                return Json(_e.Commit() as object);
            }
            catch (IndexOutOfRangeException)
            {
                return Json(new
                {
                    Error=404,
                    Message="Evento não encontrado"
                } as object);
            }
        }

        // DELETE /eventos/{id}
        public StatusCodeResult Delete(int id)
        {
            try
            {
                Evento.GetEvento(id).Delete();
                return StatusCode(HttpStatusCode.OK);
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        // GET /eventos/search/{q}
        [Route("eventos/search/{q}")]
        public JsonResult<Evento[]> Get(string q)
        {
            return Json(Evento.GetEventos(q));
        }
    }
}
