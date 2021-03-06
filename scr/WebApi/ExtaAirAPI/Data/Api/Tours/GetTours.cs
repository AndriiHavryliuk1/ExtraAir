﻿using System.Collections.Generic;
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
				return dbContext.Tours.Select(TourMapHelder).ToList();
			}
		}


		public IEnumerable<TourDto> GetToursBySearch(TourSearchHelperDto searchHelper)
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Tours.Select(TourMapHelder).ToList().Where(a => (a.AirportFrom != null
				&& a.AirportFrom.AirportId == searchHelper.AirportFormId)
				&& (a.AirportTo != null && a.AirportTo.AirportId == searchHelper.AirportToId)
				&& (a.PossibleDays.Contains(searchHelper.DayStart))).ToList();
			}
		}

		public object GetToursWithPaginFiltering(PaginFilteringHelper pfHelper, IEnumerable<TourDto> list)
		{
			list = list.OrderBy(x => x.TourId).ToList();

			if (pfHelper.AirportFromId != null)
			{
				list = list.Where(x => x.AirportFrom.AirportId == pfHelper.AirportFromId).ToList();
			}

			if (pfHelper.AirportToId != null)
			{
				list = list.Where(x => x.AirportTo.AirportId == pfHelper.AirportToId).ToList();
			}

			if (pfHelper.Day != null)
			{
				list = list.Where(x => x.PossibleDays.Contains(pfHelper.Day)).ToList();
			}

			if (pfHelper.Search != null)
			{
				pfHelper.Search = pfHelper.Search.ToLower();
				list = list.Where(x =>
					(x.AirportFrom.City + x.AirportFrom.Country + x.AirportFrom.Name +
					x.AirportTo.City + x.AirportTo.Country + x.AirportTo.Name).ToLower().Contains(pfHelper.Search.Replace(" ", "")));
			}

			var listPaged = list.Skip((pfHelper.Page - 1) * pfHelper.ItemsPerPage).Take(pfHelper.ItemsPerPage).ToList();


			var json = new
			{
				count = list.Count(),
				list = listPaged
			};

			return json;
		}


		public static TourDto TourMapHelder(Tour tour)
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
				PossibleDays = !string.IsNullOrEmpty(tour.StringDays) ? tour.StringDays.Split(' ').ToList() : new List<string>()
			};
		}


		public TourDto GetTourById(int id)
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Tours.Where(x => x.TourId == id).Select(TourMapHelder).FirstOrDefault();
			}
		}
	}
}