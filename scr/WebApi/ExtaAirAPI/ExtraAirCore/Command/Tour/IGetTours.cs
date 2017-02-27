using System.Collections.Generic;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;

namespace ExtraAirCore.Command.Tour
{
	public interface IGetTours
	{
		IEnumerable<TourDto> GetAllTours();
		IEnumerable<TourDto> GetToursBySearch(TourSearchHelperDto addressFrom, TourSearchHelperDto addressTo);
	}
}
