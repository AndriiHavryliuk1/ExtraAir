using System;
using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.Tour;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;
using ExtraAirCore.Models.Enumeration;

namespace Data.Api.Tours
{
	public class GetTourStatus : IGetTourStatus
	{
		public List<TourStatusDto> GetTourWithStatus(int? tourId = null, DateTime? dateStart = null, DateTime? dateFinish = null, int? airportFromId = null, int? airportToId = null)
		{
			using (var dbContext = new ExtraAirContext())
			{

				var mappedTours = dbContext.Tours.Select(TourMapHelder).ToList();
				var tourStatuses = dbContext.TourStatuses.ToList();

				if (tourId != null)
				{
					mappedTours = mappedTours.Where(x => x.TourId == tourId.Value).ToList();
					tourStatuses = tourStatuses.Where(x => x.TourId == tourId.Value).ToList();
				}
				if (airportFromId != null)
				{
					mappedTours = mappedTours.Where(x => x.AirportFrom.AirportId == airportFromId).ToList();
					tourStatuses = tourStatuses.Where(x => x.AirportFromId == airportFromId).ToList();
				}
				if (airportToId != null)
				{
					mappedTours = mappedTours.Where(x => x.AirportTo.AirportId == airportToId).ToList();
					tourStatuses = tourStatuses.Where(x => x.AirportToId == airportToId).ToList();
				}
				if (dateStart != null)
				{
					mappedTours = mappedTours.Where(x => x.PossibleDays.Contains(dateStart.Value.ToString("dddd"))).ToList();
					tourStatuses = tourStatuses.Where(x => x.DateStart.ToString("dddd") == dateStart.Value.ToString("dddd")).ToList();
				}
				if (dateFinish != null)
				{
					mappedTours = mappedTours.Where(x => x.PossibleDays.Contains(dateFinish.Value.ToString("dddd"))).ToList();
					tourStatuses = tourStatuses.Where(x => x.DateFinish.ToString("dddd") == dateFinish.Value.ToString("dddd")).ToList();
				}


				var res = new List<TourStatusDto>();

				foreach (var mappedTour in mappedTours)
				{
					var stausFromTs = tourStatuses.FirstOrDefault(x => x.TourId == mappedTour.TourId
																	   && (x.DateStart.Date.ToString("dddd") == mappedTour.DateStart.Value.ToString("dddd") ||
																	   x.DateFinish.Date.ToString("dddd") == mappedTour.DateFinish.Value.ToString("dddd")) && 
																	   ((x.DateStart.Date == dateStart.Value.Date) || (x.DateFinish.Date == dateStart.Value.Date)));
					var tourStatus = new TourStatusType();
					var newMappedDateStart = new DateTime(dateStart.Value.Year, dateStart.Value.Month, dateStart.Value.Day, mappedTour.DateStart.Value.Hour, 
						mappedTour.DateStart.Value.Minute, mappedTour.DateStart.Value.Second);
					var newMappedDateFinish = new DateTime(dateStart.Value.Year, dateStart.Value.Month, dateStart.Value.Day, mappedTour.DateFinish.Value.Hour,
						mappedTour.DateFinish.Value.Minute, mappedTour.DateFinish.Value.Second);
					if (stausFromTs != null)
					{
						tourStatus = stausFromTs.TourStatusType;
					}
					else
					{
						if (newMappedDateStart > DateTime.Now && newMappedDateFinish < DateTime.Now)
						{
							tourStatus = TourStatusType.Departed;
						}

						else if (newMappedDateStart > DateTime.Now)
						{
							tourStatus = TourStatusType.Pending;
						}
						else
						{
							tourStatus = TourStatusType.InTime;
						}
					}

					res.Add(new TourStatusDto
					{
						TourId = mappedTour.TourId,
						TourStatusId = stausFromTs?.TourStatusId,
						DateStart = stausFromTs?.DateStart ?? newMappedDateStart,
						DateFinish = stausFromTs?.DateFinish ?? newMappedDateFinish,
						AirportFrom = mappedTour.AirportFrom,
						AirportTo = mappedTour.AirportTo,
						TourStatusType = tourStatus
					});
				}

				return res;
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
				TourStatusIds = tour.TourStatuses.Any() ? tour.TourStatuses.Select(x => x.TourId).ToList() : null
			};
		}
	}
}