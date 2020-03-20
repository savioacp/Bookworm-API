using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Bookworm_API.Services;
using Newtonsoft.Json;

namespace Bookworm_API.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereço { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }

        public Funcionario Add()
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "insert tblFuncionario output INSERTED.IDFuncionario values (@Nome, @CPF, @Endereco, @Telefone, @Cargo, @Email, 0, '')" //TODO: Authentication and Authorization
            };

            command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
            command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = CPF;
            command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = Endereço;
            command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = Telefone;
            command.Parameters.Add("@Cargo", SqlDbType.VarChar).Value = Cargo;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;

            Id = (int)DbManager.CurrentInstance.ExecuteScalar(command);

            return this;
        }

        public Funcionario Commit()
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "update tblFuncionario set Nome=@Nome, CPF=@CPF, Endereco=@Endereco, Telefone=@Telefone, Cargo=@Cargo, Email=@Email where IDFuncionario=@IDFuncionario"
            };

            command.Parameters.Add("@IDFuncionario", SqlDbType.VarChar).Value = Id;
            command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
            command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = CPF;
            command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = Endereço;
            command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = Telefone;
            command.Parameters.Add("@Cargo", SqlDbType.VarChar).Value = Cargo;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;

            DbManager.CurrentInstance.ExecuteNonQuery(command);

            return this;
        }

        public static Funcionario[] GetFuncionarios()
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblFuncionario"
            };

            List<Funcionario> funcionarios = new List<Funcionario>();

            DataTable dt = DbManager.CurrentInstance.Execute(command);

            foreach (DataRow row in dt.Rows)
            {
                funcionarios.Add(new Funcionario()
                {
                    Id = (int)row["IDFuncionario"],
                    Nome = row["Nome"] as string,
                    CPF = row["CPF"] as string,
                    Endereço = row["Endereco"] as string,
                    Telefone = row["Telefone"] as string,
                    Cargo = row["Cargo"] as string,
                    Email = row["Email"] as string
                });
            }

            return funcionarios.ToArray();
        }
        public static Funcionario GetFuncionario(int id)
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblFuncionario where IDFuncionario=@Id"
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            var row = DbManager.CurrentInstance.Execute(command).Rows[0];


            return new Funcionario()
            {
                Id = (int) row["IDFuncionario"],
                Nome = row["Nome"] as string,
                CPF = row["CPF"] as string,
                Endereço = row["Endereco"] as string,
                Telefone = row["Telefone"] as string,
                Cargo = row["Cargo"] as string,
                Email = row["Email"] as string
            };
        }

    }
}