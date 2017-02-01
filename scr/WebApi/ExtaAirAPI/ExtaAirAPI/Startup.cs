using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ExtaAirAPI.Startup))]

namespace ExtaAirAPI
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
