using System;
using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.Tours
{
	public class GetTourStatus : IGetTourStatus
	{
		public IEquatable<TourStatusDto> GetTourWithStatus(int? tourId = null, DateTime? dateStart = null, DateTime? dateFinish = null, int? airportFromId = null, int? airportToId = null)
		{
			using (var dbContext = new ExtraAirContext())
			{

				var mappedTours = dbContext.Tours.Select(TourMapHelder).ToList();

				if (tourId != null)
				{
					mappedTours = mappedTours.Where(x => x.TourId == tourId.Value).ToList();
				}
				if (dateStart != null && dateFinish != null)
				{
					mappedTours = mappedTours.Where(x => x.DateStart > dateStart && x.DateFinish < dateFinish).ToList();
				}
				if (airportFromId != null)
				{
					mappedTours = mappedTours.Where(x => x.AirportFrom.AirportId == airportFromId).ToList();
				}
				if (airportToId != null)
				{
					mappedTours = mappedTours.Where(x => x.AirportTo.AirportId == airportToId).ToList();
				}

				return null;
			}
		}


		private static TourDto TourMapHelder(Tour tour)
		{
			var airports = tour.TourToAirports.ToList();
			Airport airportFrom = null;
			Airport airportTo = null;
			if (airports.Any())
			{
				airportFrom = airports.Where(x => x.DateStart != null).OrderBy(x => x.DateStart).FirstOrDefault().Airport;
				airportTo = airports.Where(x => x.DateFinish != null).OrderByDescending(x => x.DateFinish).FirstOrDefault().Airport;
			}

			var itnerimAirports = airports.Skip(1).Take(airports.Count - 2);
			return new TourDto
			{
				TourId = tour.TourId,
				DateStart = tour.DateStart,
				DateFinish = tour.DateFinish,
				Price = tour.Price,
				Plane = new PlaneDto
				{
					Name = tour.Plane.Name,
					PlaneId = tour.Plane.PlaneId,
					MaxCountPassenger = new CountPassengerDto
					{
						CountOfBusinessPassenger = tour.Plane.MaxCountPassengerBusiness,
						CountOfEconomyPassenger = tour.Plane.MaxCountPassengerEconomy
					}
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
				ItnerimAirports = itnerimAirports.Any() ? itnerimAirports.Select(x => new InterimTourDto()
				{
					Airport = new AirportDto()
					{
						AirportId = x.AirportId,
						Name = x.Airport.Name,
						Country = x.Airport.Address.Country,
						City = x.Airport.Address.City
					},
					DateStart = x.DateStart,
					DateFinish = x.DateFinish
				}).ToList() : null,
				PossibleDays = !string.IsNullOrEmpty(tour.StringDays) ? tour.StringDays.Split(' ').ToList() : new List<string>(),
				TourStatusIds = tour.TourStatuses.Any() ?  tour.TourStatuses.Select(x => x.TourId).ToList(): null
			};
		}
	}
}