using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Constants;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;
using ExtraAirCore.Models.Enumeration;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/users")]
	public class UsersController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Users
		public IQueryable<User> GetUsers()
		{
			return db.Users;
		}

		// GET: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUser(int id)
		{
			return Ok(IoC.Get<IGetUsers>().GetUser<UserForViewDto>(id, UserType.User));
		}

		[Authorize]
		[HttpPut]
		// PUT: api/Users/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutUser(int id, User user)
		{
			if (id != user.UserId)
			{
				return BadRequest();
			}

			var currentUser = db.Users.Find(id);
			currentUser.Birthday = currentUser.Birthday != user.Birthday ? user.Birthday : currentUser.Birthday;
			currentUser.LastName = currentUser.LastName != user.LastName ? user.LastName : currentUser.LastName;
			currentUser.FirstName = currentUser.FirstName != user.FirstName ? user.FirstName : currentUser.FirstName;
			currentUser.Phone = currentUser.Phone != user.Phone ? user.Phone : currentUser.Phone;
			currentUser.Address.City = currentUser.Address.City != user.Address.City ? user.Address.City : currentUser.Address.City;
			currentUser.Address.Country = currentUser.Address.Country != user.Address.Country ? user.Address.Country : currentUser.Address.Country;
			currentUser.Address.Street = currentUser.Address.Street != user.Address.Street ? user.Address.Street : currentUser.Address.Street;
			currentUser.Address.StreetNumber = currentUser.Address.StreetNumber != user.Address.StreetNumber ? user.Address.StreetNumber : currentUser.Address.StreetNumber;
			currentUser.Address.PostIndex = currentUser.Address.PostIndex != user.Address.PostIndex ? user.Address.PostIndex : currentUser.Address.PostIndex;
			currentUser.IdCard = currentUser.IdCard != user.IdCard ? user.IdCard : currentUser.IdCard;

			db.Entry(currentUser).State = EntityState.Modified;
			db.Entry(currentUser).Property(x => x.ImagePath).IsModified = false;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
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

		[Authorize]
		[ResponseType(typeof(void))]
		[HttpPut]
		[Route("changepass/{id:int}", Name = "ChangePass")]
		public IHttpActionResult PutUser(int id, ChangePassDto user)
		{
			var currentUser = db.Users.FirstOrDefault(x => x.Email == this.User.Identity.Name);

			if (currentUser.UserId != id)
			{
				return Content(HttpStatusCode.Forbidden, Messages.AccsesDenied);
			}

			var tmp = db.Users.Find(id);

			if (tmp == null)
			{
				return NotFound();
			}

			if (user.OldPass != tmp.Password)
			{
				return BadRequest();
			}

			tmp.Password = user.NewPass;

			db.Entry(tmp).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
				return Ok();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}


		// POST: api/Users
		[ResponseType(typeof(User))]
		public IHttpActionResult PostUser(User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Users.Add(user);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
		}

		// DELETE: api/Users/5
		[ResponseType(typeof(User))]
		public IHttpActionResult DeleteUser(int id)
		{
			User user = db.Users.Find(id);
			if (user == null)
			{
				return NotFound();
			}

			db.Users.Remove(user);
			db.SaveChanges();

			return Ok(user);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool UserExists(int id)
		{
			return db.Users.Count(e => e.UserId == id) > 0;
		}
	}
}