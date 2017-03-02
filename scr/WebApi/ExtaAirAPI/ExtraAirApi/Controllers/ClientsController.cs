using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;
using ExtraAirCore.Models.Enumeration;

namespace ExtraAirApi.Controllers
{
	public class ClientsController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Clients
		public IEnumerable<UserForViewDto> GetUsers()
		{
			return IoC.Get<IGetUsers>().GetAllUsers(UserType.Client);
		}

		// GET: api/Clients/5
		[ResponseType(typeof(Client))]
		public IHttpActionResult GetClient(int id)
		{
			return Ok(IoC.Get<IGetUsers>().GetUser<UserForViewDto>(id));
		}

		// PUT: api/Clients/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutClient(int id, Client client)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != client.UserId)
			{
				return BadRequest();
			}

			db.Entry(client).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ClientExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Clients
		[ResponseType(typeof(Client))]
		public IHttpActionResult PostClient(Client client)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IoC.Get<ISaveUser>().Save(client);

			return Ok(client);
		}

		// DELETE: api/Clients/5
		[ResponseType(typeof(Client))]
		public IHttpActionResult DeleteClient(int id)
		{
			var client = db.Clients.Find(id);
			if (client == null)
			{
				return NotFound();
			}

			db.Users.Remove(client);
			db.SaveChanges();

			return Ok(client);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ClientExists(int id)
		{
			return db.Users.Count(e => e.UserId == id) > 0;
		}
	}
}