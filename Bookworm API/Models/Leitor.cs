using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookworm_API.Services;
using Dapper;

namespace Bookworm_API.Models
{
    public class Leitor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Endereço { get; set; }
        public string Telefone { get; set; }
        public int? TipoLeitor { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

        public Leitor Add()
        {
            var _params = new
            {
                Nome,
                RG,
                DataNascimento,
                Endereço,
                Telefone,
                TipoLeitor,
                Email,
                DataCadastro = DateTime.UtcNow,
                Imagem = (byte)0x00
            };
            
            //TODO: Authentication and Authorization

            Id = DbManager.Connection.QueryFirst<int>("insert tblLeitor output INSERTED.idLeitor values (@Nome, @RG, @DataNascimento, @Endereço, @Telefone, @TipoLeitor, @Email, @DataCadastro, '', '', @Imagem)", _params);
            return this;
        }

        public Leitor Commit()
        {
            var _params = new
            {
                IdLeitor = Id,
                Nome,
                RG,
                Endereço,
                Telefone,
                TipoLeitor,
                Email
            };
            DbManager.Connection.Execute(
                "update tblLeitor set Nome=@Nome, RG=@RG, Endereco=@Endereço, Telefone=@Telefone, TipoLeitor=@TipoLeitor, Email=@Email where IdLeitor=@IdLeitor", _params);

            return this;
        }

        public void Delete()
        {
            DbManager.Connection.Execute("delete from tblLeitor where IdLeitor=@IdLeitor", new { IdLeitor = Id });
        }

        public static Leitor[] GetLeitores()
        {
            return DbManager.Connection.Query<Leitor>("select * from tblLeitor").ToArray();    
        }


        public static Leitor GetLeitor(int id)
        {
            return DbManager.Connection.QueryFirst<Leitor>("select * from tblLeitor where idLeitor=@Id", new { Id = id });
        }

        public static Leitor GetLeitor(string email)
        {
            return DbManager.Connection.QueryFirst<Leitor>("select * from tblLeitor where Email=@Email", new { Email = email });
        }




    }
}