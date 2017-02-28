using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.Tours
{
	public class GetTours : IGetTours
	{
		public IEnumerable<TourDto> GetAllTours()
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Tours.Select(MapHelder).ToList();
			}
		}


		public IEnumerable<TourDto> GetToursBySearch(TourSearchHelperDto addressFrom, TourSearchHelperDto addressTo)
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Tours.Where(x => x.DateStart >= addressFrom.Time && x.DateFinish <= addressTo.Time).Select(MapHelder)
					.Where(a=> a.AirportFrom.AirportId == addressFrom.AirportId || a.AirportTo.AirportId == addressTo.AirportId).ToList();
			}
		}


		private static TourDto MapHelder(Tour tour)
		{
			var airports = tour.TourToAirports.ToList();
			Airport airportFrom = null;
			Airport airportTo = null;
			if (airports.Any())
			{
				airportFrom = airports.Where(x => x.DateStart != null).OrderBy(x => x.DateStart).FirstOrDefault().Airport;
				airports[0].DateStart = null;
				airportTo = airports.Where(x => x.DateFinish != null).OrderByDescending(x => x.DateFinish).FirstOrDefault().Airport;
				airports[0].DateFinish = null;
			}

			var itnerimAirports = airports.Where(x => x.DateStart == null && x.DateFinish == null).OrderBy(x => x.DateStart);
			return new TourDto
			{
				TourId = tour.TourId,
				CurrentCountPassenger = tour.CurrentCountPassenger,
				DateStart = tour.DateStart,
				DateFinish = tour.DateFinish,
				Price = tour.Price,
				Plane = new PlaneDto
				{
					Name = tour.Plane.Name,
					PlaneId = tour.Plane.PlaneId,
					MaxCountPassenger = tour.Plane.MaxCountPassenger
				},
				AirportFrom = airportFrom != null ? new AirportDto
				{
					AirportId = airportFrom.AirportId,
					Name = airportFrom.Name,
					City = airportFrom.Address.City,
					Country = airportFrom.Address.Country
				} : null,
				AirportTo = airportTo != null ? new AirportDto
				{
					AirportId = airportTo.AirportId,
					Name = airportTo.Name,
					City = airportTo.Address.City,
					Country = airportTo.Address.Country
				} : null,
				ItnerimAirports = itnerimAirports.Any() ? itnerimAirports.Select(x => new AirportDto()
				{
					AirportId = x.AirportId,
					Name = x.Airport.Name,
					Country = x.Airport.Address.Country,
					City = x.Airport.Address.City
				}).ToList() : null
			};
		}
	}
}