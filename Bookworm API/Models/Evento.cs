using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Bookworm_API.Services;
using Dapper;

namespace Bookworm_API.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descrição { get; set; }
        public string Responsável { get; set; }
        public string Email { get; set; }

        public Evento Add()
        {
            var _params = new
            {
                Titulo,
                Descrição,
                Responsável,
                Email
            };

            Id = DbManager.Connection.ExecuteScalar<int>("insert tblEvento output INSERTED.IDEvento values (@Titulo, @Descrição, @Responsável, @Email)", _params);

            return this;
        }

        public Evento Commit()
        {
            var _params = new
            {
                IdEvento = Id,
                Titulo,
                Descrição,
                Responsável,
                Email
            };


            DbManager.Connection.Execute("update tblEvento set Titulo=@Titulo, Descricao=@Descrição, Responsavel=@Responsável, Email=@Email where IdEvento=@IdEvento", _params);

            return this;
        }

        public void Delete()
        {
            DbManager.Connection.Execute("delete from tblEvento where IdEvento=@IdEvento", new { IdEvento = Id });
        }

        public static Evento[] GetEventos()
        {
            return DbManager.Connection.Query<Evento>("select * from tblEvento").ToArray();
        }
        public static Evento[] GetEventos(string query)
        {
            return DbManager.Connection.Query<Evento>("select * from tblEvento where Titulo like @Query", new { Query = query }).ToArray();
        }

        public static Evento GetEvento(int id)
        {
            return DbManager.Connection.QueryFirst<Evento>("select * from tblEvento where IdEvento=@Id", new { Id=id });
        }

    }
}