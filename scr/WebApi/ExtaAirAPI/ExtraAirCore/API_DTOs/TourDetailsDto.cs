using System;
using System.Collections.Generic;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.API_DTOs
{
	public class TourDetailsDto
	{
		public CountPassengerDto CurrentPassengerCount;
		public List<BookedPoint> BookedPoints;
	}

	public struct BookedPoint
	{
		public int X;
		public int Y;
	}
}