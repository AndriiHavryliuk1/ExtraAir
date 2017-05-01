using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using ExtraAirCore.Constants;
using ExtraAirCore.Models.EFContex;
using Path = System.IO.Path;

namespace ExtraAirApi.Controllers
{
	public class FilenameMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
	{
		public FilenameMultipartFormDataStreamProvider(string path) : base(path)
		{
		}

		public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
		{
			return string.Format(@"{0}.jpg", DateTime.Now.Ticks);
		}
	}

	[Authorize]
	[RoutePrefix("api/image")]
	public class FileUploadController : ApiController
	{
		private static readonly string ServerUploadFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/img");

		private ExtraAirContext db = new ExtraAirContext();
		[Route("{id:int}")]
		[HttpPost]
		public async Task<IHttpActionResult> UploadFile(int id)
		{
			HttpRequestMessage request = Request;
			if (!request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}
			try
			{
				var currentUser = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

				if (currentUser == null)
				{
					return NotFound();
				}

				if (!User.IsInRole(Roles.Client) && id != currentUser.UserId)
				{
					return Content(HttpStatusCode.Forbidden, Messages.AccsesDenied);
				}

				var streamProvider = new FilenameMultipartFormDataStreamProvider(ServerUploadFolder);
				await Request.Content.ReadAsMultipartAsync(streamProvider);


				var fileName = Path.GetFileName(streamProvider.FileData.Select(entry => entry.LocalFileName).First());

				var user = db.Users.Find(id);

				if (user == null)
				{
					return NotFound();
				}

				user.ImagePath = "img/" + fileName;

				db.Entry(user).State = System.Data.Entity.EntityState.Modified;

				db.SaveChanges();
				return Ok(ExtraAirCore.Constants.Path.WebApiPath + "img/" + fileName);
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}
	}
}
