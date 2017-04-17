using System;
using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.Orders;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.Orders
{
	public class GetOrders : IGetOrders
	{
		public IEnumerable<OrderDto> GetAllOrders(int userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var l = dbContext.Orders.Where(x => x.UserId == userId && (x.DateStartTour != null && x.DateFinishTour != null)).ToList();
				return l.Select(MapOrderHellper).ToList();
			}
		}

		public IEnumerable<OrderDto> GetFutureOrders(int userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var l = dbContext.Orders.Where(x => x.UserId == userId && (x.DateStartTour != null
				&& x.DateStartTour > DateTime.Now)).ToList();
				return l.Select(MapOrderHellper).ToList();
			}
		}

		public IEnumerable<OrderDto> GetLastOrders(int userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var l = dbContext.Orders.Where(x => x.UserId == userId && (x.DateStartTour != null 
				&& x.DateStartTour < DateTime.Now)).ToList();
				return l.Select(MapOrderHellper).ToList();
			}
		}

		private OrderDto MapOrderHellper(Order order)
		{
			var l = order.Tours.Select(MapHelder).ToList();
			return new OrderDto
			{
				tour = l.Any() ? l[0] : null,
				Price = order.Price,
				DateStart = order.DateStartTour,
				DateFinish = order.DateFinishTour,
				Paid = order.Paid
			};
		}



		private static TourDto MapHelder(Tour tour)
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
				CurrentCountPassenger = tour.CurrentCountPassenger,
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
				}).ToList() : null
			};
		}
	}
}