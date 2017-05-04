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
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Controllers
{
    public class BookedPlacesController : ApiController
    {
        private ExtraAirContext db = new ExtraAirContext();

        // GET: api/BookedPlaces
        public IQueryable<BookedPlace> GetBookedPlaces()
        {
            return db.BookedPlaces;
        }

        // GET: api/BookedPlaces/5
        [ResponseType(typeof(BookedPlace))]
        public IHttpActionResult GetBookedPlace(int id)
        {
            BookedPlace bookedPlace = db.BookedPlaces.Find(id);
            if (bookedPlace == null)
            {
                return NotFound();
            }

            return Ok(bookedPlace);
        }

        // PUT: api/BookedPlaces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookedPlace(int id, BookedPlace bookedPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookedPlace.BookedPlaceId)
            {
                return BadRequest();
            }

            db.Entry(bookedPlace).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookedPlaceExists(id))
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

        // POST: api/BookedPlaces
        [ResponseType(typeof(List<BookedPlace>))]
        public IHttpActionResult PostBookedPlace(List<BookedPlace> bookedPlaces)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

	        foreach (var b in bookedPlaces)
	        {
				db.BookedPlaces.Add(b);
				db.SaveChanges();
			}


            return Ok();
        }

        // DELETE: api/BookedPlaces/5
        [ResponseType(typeof(BookedPlace))]
        public IHttpActionResult DeleteBookedPlace(int id)
        {
            BookedPlace bookedPlace = db.BookedPlaces.Find(id);
            if (bookedPlace == null)
            {
                return NotFound();
            }

            db.BookedPlaces.Remove(bookedPlace);
            db.SaveChanges();

            return Ok(bookedPlace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookedPlaceExists(int id)
        {
            return db.BookedPlaces.Count(e => e.BookedPlaceId == id) > 0;
        }
    }
}