using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Bookworm_API.Services;
using Dapper;

namespace Bookworm_API.Models
{
    public enum ImageOwnerType
    {
        Leitor, Funcionário, Produto
    }
    public class Icon
    {
        public int ObjectID { get; set; }
        public byte[] RawImage { get; set; }
        public ImageOwnerType ownerType { get; set; }

        public static Icon GetIcon(int id, ImageOwnerType ownerType)
        {
            if (ownerType == ImageOwnerType.Leitor)
                return DbManager.Connection.QueryFirst("select ImagemLeitor from tblLeitor where IDLeitor=@Id",
                    new { Id = id });
            if (ownerType == ImageOwnerType.Funcionário) // www www www www www www www www www www www www
                return DbManager.Connection.QueryFirst("select ImagemFunc from tblFuncionario where IDFuncionario=@Id",
                    new { Id = id });
            if (ownerType == ImageOwnerType.Produto)
                return DbManager.Connection.QueryFirst("select ImagemProd from tblProduto where IDProduto=@Id",
                    new { Id = id });
            throw new InvalidEnumArgumentException();
        }
    }
}