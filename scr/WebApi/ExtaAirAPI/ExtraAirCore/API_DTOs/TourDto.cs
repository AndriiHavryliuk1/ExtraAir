using System;
using System.Collections.Generic;

namespace ExtraAirCore.API_DTOs
{
	public class TourDto
	{
		public int TourId;
		public DateTime DateStart;
		public DateTime DateFinish;
		public decimal Price;
		public int CurrentCountPassenger;

		public AirportDto AirportFrom;
		public AirportDto AirportTo;
		public PlaneDto Plane;

		public List<AirportDto> ItnerimAirports = new List<AirportDto>();
	}
}