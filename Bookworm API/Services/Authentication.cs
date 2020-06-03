using Bookworm_API.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using Dapper;

namespace Bookworm_API.Services
{
	public class Authentication
	{
		static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		static Rfc2898DeriveBytes hash;
		static int ITERATIONS = 10000;


		
		public static bool RegisterUser(Leitor user, string plaintextPassword)
		{

			if(DbManager.Connection.QueryFirst<int>("select count(*) from tblLeitor where IDLeitor=@id", new { id = user.Id }) < 1) return false;

			var generatedSaltBytes = new byte[16];

			rng.GetBytes(generatedSaltBytes);

			hash = new Rfc2898DeriveBytes(plaintextPassword, generatedSaltBytes, ITERATIONS);

			var digestedHashedPassword = BitConverter.ToString(hash.GetBytes(16)).Replace("-","");

			var _params = new {
				id = user.Id,
				senha = digestedHashedPassword,
				salt = generatedSaltBytes
			};
			if (DbManager.Connection.Execute("update tblLeitor set Salt=@salt, Senha=@senha where IDLeitor=@id", _params) < 1) return false;

			return true;
		}

		public static bool LogUserIn(Leitor user, string plaintextPassword)
		{

			return true;

		}
	}
}