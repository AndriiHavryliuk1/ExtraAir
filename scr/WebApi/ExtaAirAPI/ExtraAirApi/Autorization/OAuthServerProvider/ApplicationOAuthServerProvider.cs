using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using ExtraAirCore.Models.EFContex;
using Microsoft.Owin.Security.OAuth;

namespace ExtraAirApi.Autorization.OAuthServerProvider
{
	public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
	{
		public ApplicationOAuthServerProvider()
		{
			new MediaTypeWithQualityHeaderValue("application/json");
		}


		public override async Task ValidateClientAuthentication(
			OAuthValidateClientAuthenticationContext context)
		{
			
			var userName = context.Parameters.Get("username");
			var pass = context.Parameters.Get("password");
			var userProvider = new UserProvider(new ExtraAirContext());
			var user = await userProvider.FindByEmailAsync(userName);
			try
			{
				if (context.Parameters.Get("client") == null)
					throw new Exception();
			}
			catch (Exception)
			{
				context.SetError(
					"invalid_grant",
					"type of client is undefined"
					);
				context.Rejected();
				return;
			}

			if (user != null && user.Password == pass)
			{
				await Task.FromResult(context.Validated());
				return;
			}

			context.SetError(
				"invalid_grant",
				"The authentification code is invalid"
			);
			context.Rejected();
			return;
		}


		public override async Task GrantResourceOwnerCredentials(
			OAuthGrantResourceOwnerCredentialsContext context)
		{
			var userProvider = new UserProvider(new ExtraAirContext());
			var user = await userProvider.FindByEmailAsync(context.UserName);
			if (user == null || user.Password != context.Password || user.Deleted)
			{
				context.SetError(
					"invalid_grant",
					"The user name or password is incorrect or user account is inactive."
					);
				context.Rejected();
				return;
			}

			var identity = new ClaimsIdentity("JWT");
			identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
			identity.AddClaim(new Claim("id", user.UserId.ToString()));
			identity.AddClaim(new Claim("sub", context.UserName));
			identity.AddClaim(new Claim(ClaimTypes.Role, user.UserClaim.ClaimValue));

			context.Validated(identity);
		}
	}
}