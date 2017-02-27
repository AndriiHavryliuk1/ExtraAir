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
			var airportFrom = airports.OrderBy(x => x.DateStart).FirstOrDefault().Airport;
			airports[0] = null;
			var airportTo = airports.OrderByDescending(x => x.DateStart).FirstOrDefault().Airport;
			airports[0] = null;
			var itnerimAirports = airports.OrderBy(x => x.DateStart);
			return new TourDto
			{
				TourId = tour.TourId,
				CurrentCountPassenger = tour.CurrentCountPassenger,
				OrderId = tour.OrderId,
				DateStart = tour.DateStart,
				DateFinish = tour.DateFinish,
				Price = tour.Price,
				Plane = new PlaneDto
				{
					Name = tour.Plane.Name,
					PlaneId = tour.Plane.PlaneId,
					MaxCountPassenger = tour.Plane.MaxCountPassenger
				},
				AirportFrom = new AirportDto
				{
					AirportId = airportFrom.AirportId,
					Name = airportFrom.Name,
					City = airportFrom.Address.City,
					Country = airportFrom.Address.Country
				},
				AirportTo = new AirportDto
				{
					AirportId = airportTo.AirportId,
					Name = airportTo.Name,
					City = airportTo.Address.City,
					Country = airportTo.Address.Country
				},
				ItnerimAirports = itnerimAirports.Select(x => new AirportDto()
				{
					AirportId = x.AirportId,
					Name = x.Airport.Name,
					Country = x.Airport.Address.Country,
					City = x.Airport.Address.City
				}).ToList()
			};
		}
	}
}