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
		static int ITERATIONS = 10000;


		
		public static bool RegisterUser(Leitor user, string plaintextPassword)
		{
			Rfc2898DeriveBytes hash;
			var generatedSaltBytes = new byte[16];

			rng.GetBytes(generatedSaltBytes);

			hash = new Rfc2898DeriveBytes(plaintextPassword, generatedSaltBytes, ITERATIONS);

			var digestedHashedPassword = BitConverter.ToString(hash.GetBytes(16)).Replace("-","").ToLower();

			hash.Dispose();
			var _params = new 
			{
				id = user.Id,
				senha = digestedHashedPassword,
				salt = BitConverter.ToString(generatedSaltBytes).Replace("-", "").ToLower()
			};

			if (DbManager.Connection.Execute("update tblLeitor set Salt=@salt, Senha=@senha where IDLeitor=@id", _params) < 1) return false;

			return true;
		}

		public static bool LogUserIn(Leitor user, string plaintextPassword)
		{
			//sif (DbManager.Connection.ExecuteScalar<int>("select count(*) from tblLeitor where IdLeitor=@IdLeitor", new { IdLeitor = user.Id }) < 1) return false;

			Rfc2898DeriveBytes hash;
			(string digestedSalt, string digestedHash) = DbManager.Connection.QueryFirst<(string, string)>("select Salt, Senha from tblLeitor where IdLeitor = @IdLeitor", new { IdLeitor = user.Id });

			var saltBytes = StringToByteArray(digestedSalt);

			hash = new Rfc2898DeriveBytes(plaintextPassword, saltBytes, ITERATIONS);

			var digestedHashedPassword = BitConverter.ToString(hash.GetBytes(16)).Replace("-", "").ToLower();

			hash.Dispose();
			if (digestedHashedPassword != digestedHash)
				return false;


			return true;

		}
















		public static byte[] StringToByteArray(string hex)
		{
			if (hex.Length % 2 == 1)
				throw new Exception("The binary key cannot have an odd number of digits");

			byte[] arr = new byte[hex.Length >> 1];

			for (int i = 0; i < hex.Length >> 1; ++i)
			{
				arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
			}

			return arr;
		}

		public static int GetHexVal(char hex)
		{
			int val = hex;
			return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
		}
	}
}