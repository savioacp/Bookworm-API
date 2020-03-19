using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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


        public static Funcionario[] GetFuncionarios()
        {

            SqlCommand command = new SqlCommand()
            {
                CommandText = "select * from tblFuncionario"
            };

            List<Funcionario> funcionarios = new List<Funcionario>();

            DataTable dt = Data.DbManager.CurrentInstance.Execute(command);

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
    }
}