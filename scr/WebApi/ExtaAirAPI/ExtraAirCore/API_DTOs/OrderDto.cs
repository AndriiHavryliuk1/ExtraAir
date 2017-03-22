using System;

namespace ExtraAirCore.API_DTOs
{
	public class OrderDto
	{
		public TourDto tour;
		public decimal Price;
		public DateTime? DateStart;
		public DateTime? DateFinish;
		public bool Paid;

		//public int CountPassenger;
	}
}