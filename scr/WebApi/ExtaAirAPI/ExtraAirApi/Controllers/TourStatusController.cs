using System;
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
	public class TourStatusController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/TourStatus
		public IHttpActionResult GetTourStatuses([FromUri]int? tourId = null, [FromUri]DateTime? dateStart = null, [FromUri]DateTime? dateFinish = null, [FromUri]int? airportFromId = null, [FromUri]int? airportToId = null)
		{
			return Ok(IoC.Get<IGetTourStatus>().GetTourWithStatus(tourId, dateStart, dateFinish, airportFromId, airportToId));
		}

		// GET: api/TourStatus/5
		[ResponseType(typeof(TourStatus))]
		public IHttpActionResult GetTourStatus(int id)
		{
			TourStatus tourStatus = db.TourStatuses.Find(id);
			if (tourStatus == null)
			{
				return NotFound();
			}

			return Ok(tourStatus);
		}

		// PUT: api/TourStatus/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutTourStatus(int id, TourStatus tourStatus)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != tourStatus.TourStatusId)
			{
				return BadRequest();
			}

			db.Entry(tourStatus).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TourStatusExists(id))
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

		// POST: api/TourStatus
		[ResponseType(typeof(TourStatus))]
		public IHttpActionResult PostTourStatus(TourStatus tourStatus)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.TourStatuses.Add(tourStatus);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = tourStatus.TourStatusId }, tourStatus);
		}

		// DELETE: api/TourStatus/5
		[ResponseType(typeof(TourStatus))]
		public IHttpActionResult DeleteTourStatus(int id)
		{
			TourStatus tourStatus = db.TourStatuses.Find(id);
			if (tourStatus == null)
			{
				return NotFound();
			}

			db.TourStatuses.Remove(tourStatus);
			db.SaveChanges();

			return Ok(tourStatus);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TourStatusExists(int id)
		{
			return db.TourStatuses.Count(e => e.TourStatusId == id) > 0;
		}
	}
}