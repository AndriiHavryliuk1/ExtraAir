using System;
using System.Collections.Generic;

namespace ExtraAirCore.API_DTOs
{
	public class OrderDto
	{
		public int OrderId;
		public TourDto tour;
		public decimal Price;
		public DateTime? DateStart;
		public DateTime? DateFinish;
		public bool Paid;

		//public int CountPassenger;
	}

	public class OrderDetailsDto
	{
		public OrderDto order;
		public List<PassengerDto> Passengers;
		public string Comfort;
	}
}