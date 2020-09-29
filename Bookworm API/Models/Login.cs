using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookworm_API.Models
{
	public class LoginByEmail
	{
		public string Email { get; set; }
		public string Senha { get; set; }
	}

	public class LoginById
	{
		public int Id { get; set; }
		public string Senha { get; set; }
	}

	public class Login
	{
		public object suga { get; set; }
		public string senha { get; set; }
	}
}