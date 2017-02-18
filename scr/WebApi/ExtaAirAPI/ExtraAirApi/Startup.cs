using System;
using System.Configuration;
using ExtraAirApi.Autorization.OAuthServerProvider;
using ExtraAirApi.Autorization.Providers;
using ExtraAirCore.Constants;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(ExtraAirApi.Startup))]
namespace ExtraAirApi
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}


		private void ConfigureAuth(IAppBuilder app)
		{
			var issuer = Path.WebApiPath;
			var secret = TextEncodings.Base64Url.Decode(
				ConfigurationManager.AppSettings["as:AudienceSecret"]);


			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

			var OAuthOptions = new OAuthAuthorizationServerOptions
			{
				TokenEndpointPath = new PathString("/Token"),
				Provider = new ApplicationOAuthServerProvider(),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
				AccessTokenFormat = new CustomJwtFormat(issuer),
				AllowInsecureHttp = true
			};
			app.UseOAuthAuthorizationServer(OAuthOptions);

			app.UseJwtBearerAuthentication(
				new JwtBearerAuthenticationOptions
				{
					AuthenticationMode = AuthenticationMode.Active,
					AllowedAudiences = new[] { ConfigurationManager.AppSettings["as:AudienceId"] },
					IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
					{
						new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
					}
				});
		}
	}
}