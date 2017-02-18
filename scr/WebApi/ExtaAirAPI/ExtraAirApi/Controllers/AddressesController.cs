using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.Command.Address;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/addresses")]
	public class AddressesController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Addresses
		[Route("")]
		public object GetAddresses()
		{
			return IoC.Get<IGetAllAddresses>().GetAddresses();
		}

		// GET: api/Addresses/5
		[ResponseType(typeof(Address))]
		[Route("{id}")]
		public IHttpActionResult GetAddress(int id)
		{
			Address address = db.Addresses.Find(id);
			if (address == null)
			{
				return NotFound();
			}

			return Ok(address);
		}

		// PUT: api/Addresses/5
		[ResponseType(typeof(void))]
		[Route("{id}")]
		public IHttpActionResult PutAddress(int id, Address address)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != address.AddressId)
			{
				return BadRequest();
			}

			db.Entry(address).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AddressExists(id))
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

		// POST: api/Addresses
		[ResponseType(typeof(Address))]
		[Route("")]
		public IHttpActionResult PostAddress(Address address)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Addresses.Add(address);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = address.AddressId }, address);
		}

		// DELETE: api/Addresses/5
		[ResponseType(typeof(Address))]
		[Route("{id}")]
		public IHttpActionResult DeleteAddress(int id)
		{
			Address address = db.Addresses.Find(id);
			if (address == null)
			{
				return NotFound();
			}

			db.Addresses.Remove(address);
			db.SaveChanges();

			return Ok(address);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool AddressExists(int id)
		{
			return db.Addresses.Count(e => e.AddressId == id) > 0;
		}
	}
}