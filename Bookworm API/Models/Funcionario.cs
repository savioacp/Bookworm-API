using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using Bookworm_API.Services;
using Dapper;
using Newtonsoft.Json;

namespace Bookworm_API.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Endereço { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }

        public Funcionario Add()
        {
            var _params = new
            {
                Nome,
                CPF,
                Endereço,
                Telefone,
                Cargo,
                Email
            };
            
            //TODO: Authentication and Authorization
            
            Id = DbManager.Connection.QueryFirst<int>("insert tblFuncionario output INSERTED.IDFuncionario values (@Nome, @CPF, @RG, @Endereço, @Telefone, @Cargo, @Email, 0, '')", _params); 
            return this;
        }

        public Funcionario Commit()
        {
            var _params = new
            {
                IDFuncionario = Id,
                Nome,
                CPF,
                Endereço,
                Telefone,
                Cargo,
                Email
            };
            DbManager.Connection.Execute(
                "update tblFuncionario set Nome=@Nome, CPF=@CPF, RG=@RG, Endereco=@Endereço, Telefone=@Telefone, Cargo=@Cargo, Email=@Email where IDFuncionario=@IDFuncionario", _params);

            return this;
        }

        public void Delete()
        {
            DbManager.Connection.Execute("delete from tblFuncionario where IDFuncionario=@IDFuncionario", new { IDFuncionario = Id });
        }

        public static Funcionario[] GetFuncionarios()
        {
            return DbManager.Connection.Query<Funcionario>("select * from tblFuncionario").ToArray();
        }
        public static Funcionario GetFuncionario(int id)
        {
            return DbManager.Connection.QueryFirst<Funcionario>(
                "select * from tblFuncionario where IDFuncionario=@Id", new { Id = id });
        }

    }
}