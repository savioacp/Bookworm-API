using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookworm_API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int TipoAcervo { get; set; }
        public string Nome { get; set; }
        public string Autores { get; set; }
        public int Ano { get; set; }
        public int Setor { get; set; }
        public int Fileira { get; set; }
        public int Prateleira { get; set; }
        public string TipoProduto { get; set; }
        public string Editora { get; set; }
    }
}