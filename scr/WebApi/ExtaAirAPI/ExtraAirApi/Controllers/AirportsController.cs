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
using ExtraAirCore.API_DTOs;
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
		[Route("")]
		[HttpPut]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutAirport(AirportDto airportDto)
		{
			var airport = db.Airports.Find(airportDto.AirportId);

			if (airport == null)
			{
				return BadRequest();
			}

			airport.Name = airport.Name != airportDto.Name ? airportDto.Name : airport.Name;

			airport.Address.Country = airport.Address.Country != airportDto.Country ? airportDto.Country : airport.Address.Country;
			airport.Address.Street = airport.Address.Street != airportDto.Street ? airportDto.Street : airport.Address.Street;
			airport.Address.StreetNumber = airport.Address.StreetNumber != Convert.ToInt32(airportDto.StreetNumber) ? Convert.ToInt32(airportDto.StreetNumber) : airport.Address.StreetNumber;
			airport.Address.City = airport.Address.City != airportDto.City ? airportDto.City : airport.Address.City;

			db.Entry(airport).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AirportExists(airportDto.AirportId))
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
		[Route("")]
		[HttpPost]
		public IHttpActionResult PostAirport(Airport airport)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Airports.Add(airport);
			db.SaveChanges();

			return Ok();
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