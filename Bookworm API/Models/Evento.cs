using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
            SqlCommand command = new SqlCommand()
            {
                CommandText = "insert tblEvento output INSERTED.IDEvento values (@Titulo, @Descricao, @Responsavel, @Email)"
            };

            command.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = Titulo;
            command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = Descrição;
            command.Parameters.Add("@Responsavel", SqlDbType.VarChar).Value = Responsável;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;

            Id = (int) Data.DbManager.CurrentInstance.ExecuteScalar(command);

            return this;
        }

        public Evento Commit()
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "update tblEvento set Titulo=@Titulo, Descricao=@Descricao, Responsavel=@Responsavel, Email=@Email where IdEvento=@IdEvento"
            };

            command.Parameters.Add("@IdEvento", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = Titulo;
            command.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = Descrição;
            command.Parameters.Add("@Responsavel", SqlDbType.VarChar).Value = Responsável;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;

            Data.DbManager.CurrentInstance.ExecuteNonQuery(command);

            return this;
        }

        public void Delete()
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "delete from tblEvento where IdEvento=@IdEvento"
            };

            command.Parameters.Add("@IdEvento", SqlDbType.Int).Value = Id;

            Data.DbManager.CurrentInstance.ExecuteNonQuery(command);
        }

        public static Evento[] GetEventos()
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblEvento"
            };

            List<Evento> eventos = new List<Evento>();

            DataTable dt = Data.DbManager.CurrentInstance.Execute(command);

            foreach (DataRow row in dt.Rows)
            {
                eventos.Add(new Evento()
                {
                    Id = (int)row["IDEvento"],
                    Titulo = row["Titulo"].ToString(),
                    Descrição = row["Descricao"].ToString(),
                    Responsável = row["Responsavel"].ToString(),
                    Email = row["Email"].ToString()
                });
            }

            return eventos.ToArray();
        }
        public static Evento[] GetEventos(string query)
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblEvento where Titulo like @Query"
            };

            command.Parameters.Add("@Query", SqlDbType.VarChar).Value = $"%{query}%";

            List<Evento> eventos = new List<Evento>();

            DataTable dt = Data.DbManager.CurrentInstance.Execute(command);

            foreach (DataRow row in dt.Rows)
            {
                eventos.Add(new Evento()
                {
                    Id = (int)row["IDEvento"],
                    Titulo = row["Titulo"].ToString(),
                    Descrição = row["Descricao"].ToString(),
                    Responsável = row["Responsavel"].ToString(),
                    Email = row["Email"].ToString()
                });
            }

            return eventos.ToArray();
        }

        public static Evento GetEvento(int id)
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblEvento where IdEvento=@Id"
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            DataTable dt = Data.DbManager.CurrentInstance.Execute(command);

            return new Evento()
            {
                Id = (int)dt.Rows[0]["IDEvento"],
                Titulo = dt.Rows[0]["Titulo"].ToString(),
                Descrição = dt.Rows[0]["Descricao"].ToString(),
                Responsável = dt.Rows[0]["Responsavel"].ToString(),
                Email = dt.Rows[0]["Email"].ToString()
            };
        }

    }
}