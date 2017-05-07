using System;
using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;
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
				return l.Select(MapOrderHellper).Where(x => x.tour != null).ToList();
			}
		}

		public IEnumerable<OrderDto> GetFutureOrders(int userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var l = dbContext.Orders.Where(x => x.UserId == userId && (x.DateStartTour != null
				&& x.DateStartTour > DateTime.Now)).ToList();
				return l.Select(MapOrderHellper).Where(x => x.tour != null).ToList();
			}
		}

		public IEnumerable<OrderDto> GetLastOrders(int userId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var l = dbContext.Orders.Where(x => x.UserId == userId && (x.DateStartTour != null 
				&& x.DateStartTour < DateTime.Now)).ToList();
				return l.Select(MapOrderHellper).Where(x => x.tour != null).ToList();
			}
		}


		public object GetOrdersWithPagination(PaginFilteringHelper pfHelper, IEnumerable<OrderDto> list)
		{
			list = list.OrderByDescending(x => x.OrderId).ToList();
			if (pfHelper.Search != null)
			{
				pfHelper.Search = pfHelper.Search.ToLower();
				list = list.Where(x =>
					(x.tour.AirportFrom.City + x.tour.AirportFrom.Country + x.tour.AirportFrom.Name +
					x.tour.AirportTo.City + x.tour.AirportTo.Country + x.tour.AirportTo.Name).ToLower().Contains(pfHelper.Search.Replace(" ", "")));
			}

			var listPaged = list.Skip((pfHelper.Page - 1) * pfHelper.ItemsPerPage).Take(pfHelper.ItemsPerPage).ToList();

			var json = new
			{
				count = list.Count(),
				list = listPaged
			};

			return json;
		}

		public OrderDetailsDto GetOrder(int orderId)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var orderBase = dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
				var mapedOrder = MapOrderHellper(orderBase);
				return MapOrderDetailsHellper(mapedOrder, orderBase);
			}
		}

		private static OrderDto MapOrderHellper(Order order)
		{
			var l = order.Tours.Select(MapHelder).ToList();
			return new OrderDto
			{
				OrderId = order.OrderId,
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
				Plane = new PlaneDto
				{
					Name = tour.Plane.Name,
					PlaneId = tour.Plane.PlaneId,
					MaxCountPassenger = new CountPassengerDto()
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
				}).ToList() : null
			};
		}

		private OrderDetailsDto MapOrderDetailsHellper(OrderDto order, Order orderBase)
		{

			return new OrderDetailsDto
			{
				order = order,
				Passengers = orderBase.Passengers.Select(x =>
				   new PassengerDto()
				   {
					   FirstName = x.FirstName,
					   LastName = x.LastName,
					   IdCard = x.IdCard,
					   BaggageInternal = x.BaggageInternal,
					   BaggageeExternal = x.BaggageeExternal,
					   CoordinateValue = x.CoordinateValue,
					   Gender = x.Gender,
					   TicketPrice = x.TicketPrice
				   }).ToList(),
				Comfort = orderBase.Tours.First().TourDetailses.First().BookedPlaces.First().ComfortType == ComfortType.Economy ? "Economy" : "Business"
			};
		}
	}
}