using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	public class ToursController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Tours
		public object GetTours()
		{
			return IoC.Get<IGetTours>().GetAllTours();
		}

		// GET: api/Tours/5
		[ResponseType(typeof(Tour))]
		public IHttpActionResult GetTour(int id)
		{
			Tour tour = db.Tours.Find(id);
			if (tour == null)
			{
				return NotFound();
			}

			return Ok(tour);
		}

		// PUT: api/Tours/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutTour(int id, Tour tour)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != tour.TourId)
			{
				return BadRequest();
			}

			db.Entry(tour).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TourExists(id))
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

		// POST: api/Tours
		[ResponseType(typeof(Tour))]
		public IHttpActionResult PostTour(Tour tour)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Tours.Add(tour);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = tour.TourId }, tour);
		}

		// DELETE: api/Tours/5
		[ResponseType(typeof(Tour))]
		public IHttpActionResult DeleteTour(int id)
		{
			Tour tour = db.Tours.Find(id);
			if (tour == null)
			{
				return NotFound();
			}

			db.Tours.Remove(tour);
			db.SaveChanges();

			return Ok(tour);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TourExists(int id)
		{
			return db.Tours.Count(e => e.TourId == id) > 0;
		}
	}
}