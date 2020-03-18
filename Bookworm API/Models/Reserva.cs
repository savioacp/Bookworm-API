using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookworm_API.Models
{
    public class Reserva
    {
        public int IdProduto { get; set; }
        public int IdLeitor { get; set; }
        public DateTime DataReserva { get; set; }

    }
}