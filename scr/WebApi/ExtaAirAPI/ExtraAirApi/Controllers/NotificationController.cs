using System.Linq;
using System.Web.Http;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Models.EFContex;
using Rest.Helpers;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/notification")]
	[Authorize]
	public class NotificationController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();
		[Route("")]
		[HttpPost]
		public object SendMessage([FromBody]HTMLDto html)
		{
			var currentUser = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

			EMailHelper.SendNotification(new EmailInputDto
			{
				Email = currentUser.Email,
				Body = html.HTML,
				Subject = "Підтвердження замовлення",
				UserName = currentUser.LastName + " " + currentUser.FirstName
			});
			return Ok();
		}
	}
}