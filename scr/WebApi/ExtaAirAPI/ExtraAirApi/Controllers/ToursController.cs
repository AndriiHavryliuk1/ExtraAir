using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/tours")]
	public class ToursController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Tours
		[Route("")]
		public object GetTours([FromUri]int page = 1, [FromUri]int itemsPerPage = 15, [FromUri]string search = null, [FromUri]int? airportFromId = null, [FromUri]int? airportToId = null,
			[FromUri]string day = null)
		{
			var list = IoC.Get<IGetTours>().GetAllTours();

			var pagedList = IoC.Get<IGetTours>().GetToursWithPaginFiltering(new PaginFilteringHelper
			{
				Day = day,
				AirportToId = airportToId,
				AirportFromId = airportFromId,
				ItemsPerPage = itemsPerPage,
				Page = page,
				Search = search
			}, list);

			var json = new
			{
				count = list.Count(),
				list = pagedList
			};

			return json;
		}

		[HttpGet]
		[Route("bysearch")]
		public object GetToursBySearch([FromUri]int airportFromId, [FromUri]int airportToId, [FromUri]string dayStart)
		{
			return IoC.Get<IGetTours>().GetToursBySearch(new TourSearchHelperDto
			{
				AirportFormId = airportFromId,
				AirportToId = airportToId,
				DayStart = dayStart
			});
		}

		// GET: api/Tours/5
		[ResponseType(typeof(Tour))]
		public IHttpActionResult GetTour(int id)
		{
			var tour = IoC.Get<IGetTours>().GetTourById(id);
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