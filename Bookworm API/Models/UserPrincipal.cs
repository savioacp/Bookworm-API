using Bookworm_API.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bookworm_API.Models
{
	public enum UserKind
	{
		Leitor, Funcionario
	}
	public class UserPrincipal : IPrincipal
	{
		public UserKind UserKind { get; set; }
		public IIdentity Identity { get; }

		public UserPrincipal(tblLeitor leitor)
		{
			UserKind = UserKind.Leitor;
			Identity = new LeitorIdentity(leitor);
		}

		public bool IsInRole(string role)
		{
			role = role.ToLowerInvariant();
			if (UserKind == UserKind.Leitor)
				return ((LeitorIdentity)Identity).tblTipoLeitor.TipoLeitor.ToLowerInvariant() == role;
			else
				throw new InvalidEnumArgumentException();
		}
	}

	public class LeitorIdentity : tblLeitor, IIdentity
	{
		public string Name => Nome;

		public string AuthenticationType { get; set; } = "normal";

		public bool IsAuthenticated => true;

		public LeitorIdentity(tblLeitor leitor)
		{
			IDLeitor = leitor.IDLeitor;
			Nome = leitor.Nome;
			RG = leitor.RG;
			DataNasc = leitor.DataNasc;
			Endereco = leitor.Endereco;
			Telefone = leitor.Telefone;
			tblTipoLeitor = leitor.tblTipoLeitor;
			Email = leitor.Email;
			DataCadastro = leitor.DataCadastro;
		}
	}
}