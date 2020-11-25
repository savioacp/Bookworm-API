using Bookworm_API.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Bookworm_API.Middlewares
{
	public class JWTMiddleware : DelegatingHandler
	{
		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var _ = (request, cancellationToken);

			var authHeader = request.Headers.Authorization;

			if (authHeader == null)
				return Next(_);

			if (authHeader.Scheme != "Bearer")
				return Next(_);

			if(string.IsNullOrWhiteSpace(authHeader.Parameter))
				return Next(_);

			var tokenHandler = new JwtSecurityTokenHandler();

			if (!tokenHandler.CanReadToken(authHeader.Parameter))
				return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Malformed Token");

			var claimsIdentity = tokenHandler.ValidateToken(authHeader.Parameter, new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
				ValidateAudience = false,
				ValidateIssuer = false,
				RequireExpirationTime = false,
				IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(WebApiApplication.JWTSecret))
			}, out var token).Identity as ClaimsIdentity;
			
			if(claimsIdentity == null)
				return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty Claims");

			var userId = int.Parse(claimsIdentity.FindFirst("UserId").Value);

			using(var db = new TccSettings())
				SetPrincipal(new UserPrincipal(db.tblLeitor.Include("tblTipoLeitor").First(l => l.IDLeitor == userId)));


			return Next(_);
		}


		private void SetPrincipal(IPrincipal principal)
		{
			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
		}

		private HttpResponseMessage Next((HttpRequestMessage request, CancellationToken cancellationToken) _)
		{
			return base.SendAsync(_.request, _.cancellationToken).GetAwaiter().GetResult();
		}
	}

}