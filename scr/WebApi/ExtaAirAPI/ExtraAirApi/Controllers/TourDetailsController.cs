using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.TourDetails;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	public class TourDetailsController : ApiController
	{
		private ExtraAirContext db = new ExtraAirContext();

		// GET: api/TourDetails
		public IQueryable<TourDetails> GetTourDetailses()
		{
			return db.TourDetailses;
		}

		// GET: api/TourDetails/5
		[ResponseType(typeof(TourDetails))]
		public IHttpActionResult GetTourDetails(int id, [FromUri] ComfortType comfort, [FromUri] DateTime DateStart,
			[FromUri] DateTime DateFinish)
		{
			var tourDetail = IoC.Get<IGetTourDetails>().Get(new TourDetailsHelper
			{
				TourId = id,
				ComfortType = comfort,
				DateStart = DateStart,
				DateFinish = DateFinish
			});

			return Ok(tourDetail);
		}

		// PUT: api/TourDetails/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutTourDetails(int id, TourDetails tourDetails)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != tourDetails.TourDetailsId)
			{
				return BadRequest();
			}

			db.Entry(tourDetails).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TourDetailsExists(id))
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

		// POST: api/TourDetails
		[ResponseType(typeof(TourDetails))]
		public IHttpActionResult PostTourDetails(TourDetails tourDetails)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var list = db.TourDetailses.Where(x => x.TourId == tourDetails.TourId && x.DateStart == tourDetails.DateStart &&
			                            x.DateFinish == tourDetails.DateFinish).ToList();
			if (list.Count != 0)
			{
				tourDetails.CurrentCountOfBusinessPassenger +=
					list.OrderBy(x => x.CurrentCountOfBusinessPassenger).First().CurrentCountOfBusinessPassenger;
				tourDetails.CurrentCountOfEconomyPassenger +=
					list.OrderBy(x => x.CurrentCountOfEconomyPassenger).First().CurrentCountOfEconomyPassenger;
			}

			tourDetails.BookedPlaces = null;
			tourDetails.DatePushed = DateTime.Now;

			db.TourDetailses.Add(tourDetails);
			db.SaveChanges();


			return Ok(tourDetails);
		}

		// DELETE: api/TourDetails/5
		[ResponseType(typeof(TourDetails))]
		public IHttpActionResult DeleteTourDetails(int id)
		{
			TourDetails tourDetails = db.TourDetailses.Find(id);
			if (tourDetails == null)
			{
				return NotFound();
			}

			db.TourDetailses.Remove(tourDetails);
			db.SaveChanges();

			return Ok(tourDetails);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TourDetailsExists(int id)
		{
			return db.TourDetailses.Count(e => e.TourDetailsId == id) > 0;
		}
	}
}