﻿using System;
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
    }
}