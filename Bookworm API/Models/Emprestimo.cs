using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookworm_API.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int IdFuncionario { get; set; }
        public int IdProduto { get; set; }
        public int IdLeitor { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataEntrega { get; set; }
        public int Renovação { get; set; }
    }
}