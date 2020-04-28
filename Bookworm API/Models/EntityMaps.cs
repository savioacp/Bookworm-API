using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.FluentMap.Mapping;

namespace Bookworm_API.Models
{
    public class EntityMaps
    {
        public class FuncionarioMap : EntityMap<Funcionario>
        {
            internal FuncionarioMap()
            {
                Map(f => f.Id).ToColumn("IDFuncionario");
                Map(f => f.Endereço).ToColumn("Endereco");
            }
        }

        public class EventoMap : EntityMap<Evento>
        {
            internal EventoMap()
            {
                
            }
        }
    }
}