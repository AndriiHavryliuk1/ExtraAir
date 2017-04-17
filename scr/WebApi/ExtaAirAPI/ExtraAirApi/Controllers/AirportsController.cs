using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ExtraAirApi.Utils.Ninject;
using ExtraAirCore.Command.Airport;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
	[RoutePrefix("api/airports")]
	public class AirportsController : ApiController
    {
        private ExtraAirContext db = new ExtraAirContext();

		// GET: api/Airports
		[Route("")]
		public object GetAirports()
        {
	        return IoC.Get<IGetAirports>().GetAllAirports();
        }

		// GET: api/Airports
		[Route("{id:int}")]
		[HttpGet]
		[ActionName("ById")]
		public object GetAirportsById(int id)
		{
			return IoC.Get<IGetAirports>().GetAirportsById(id);
		}

        // PUT: api/Airports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAirport(int id, Airport airport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airport.AirportId)
            {
                return BadRequest();
            }

            db.Entry(airport).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportExists(id))
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

        // POST: api/Airports
        [ResponseType(typeof(Airport))]
        public IHttpActionResult PostAirport(Airport airport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Airports.Add(airport);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = airport.AirportId }, airport);
        }

        // DELETE: api/Airports/5
        [ResponseType(typeof(Airport))]
        public IHttpActionResult DeleteAirport(int id)
        {
            Airport airport = db.Airports.Find(id);
            if (airport == null)
            {
                return NotFound();
            }

            db.Airports.Remove(airport);
            db.SaveChanges();

            return Ok(airport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AirportExists(int id)
        {
            return db.Airports.Count(e => e.AirportId == id) > 0;
        }
    }
}