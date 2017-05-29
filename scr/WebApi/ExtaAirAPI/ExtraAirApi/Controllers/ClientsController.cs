using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;
using ExtraAirCore.Models.Enumeration;
using Rest.Helpers;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/Clients")]
	public class ClientsController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Clients
		[Route("")]
		public IEnumerable<UserForViewDto> GetUsers()
		{
			return IoC.Get<IGetUsers>().GetAllUsers(UserType.Client);
		}

		// GET: api/Clients/5
		[ResponseType(typeof(Client))]
		[Route("{id:int}")]
		public IHttpActionResult GetClient(int id)
		{
			return Ok(IoC.Get<IGetUsers>().GetUser<UserForViewDto>(id, UserType.Client));
		}

		// PUT: api/Clients/5
		[ResponseType(typeof(void))]
		[Route("")]
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
		[Route("")]
		public IHttpActionResult PostClient(Client client)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				IoC.Get<ISaveUser>().Save(client);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(client);
		}

		[HttpPost]
		[Route("{id:int}/confirmRegistration")]
		[ResponseType(typeof(void))]
		public IHttpActionResult SendConfirmRegisterEmail(int id)
		{
			var emailInput = new EmailInputDto();
			var client = db.Clients.Find(id);
			emailInput.UserName = client.FirstName + client.LastName;
			emailInput.Email = client.Email;
			emailInput.Subject = "Підтвердження реєстрації!";

			try
			{
				EMailHelper.SendConfirmRegisterNotification(emailInput, id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[ResponseType(typeof(void))]
		[HttpPut]
		[Route("changeState/{id:int}", Name = "ChangeState")]
		public IHttpActionResult PutUser2(int id, object state)
		{
			var user = db.Users.Find(id);

			if (user == null)
			{
				return NotFound();
			}

			if (user.IsActive == null || !user.IsActive.Value)
			{
				user.IsActive = true;
			}

			db.Entry(user).State = EntityState.Modified;

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

			return Ok();
		}

		[Route("forRegistrationConfirm/{id:int}", Name = "GetUnActiveUser")]
		public IHttpActionResult GetPatientForConfirm(int id)
		{
			var client = db.Users.FirstOrDefault(x => x.UserId == id);

			if (client == null)
			{
				return NotFound();
			}
			return Ok(new
			{
				client.UserId,
				client.Email,
				client.IsActive
			});
		}

		// DELETE: api/Clients/5
		[ResponseType(typeof(Client))]
		[Route("")]
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