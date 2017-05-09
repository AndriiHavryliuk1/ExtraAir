using System;
using ExtraAirCore.Models.Enumeration;

namespace ExtraAirCore.API_DTOs
{
	public class TourStatusDto
	{
		public int TourId;
		public int? TourDetailsId;
		public AirportDto AirportFrom;
		public AirportDto AirportTo;
		public DateTime? DateStart;
		public DateTime? DateFinish;
		public TourStatusType TourStatusType;
	}
}