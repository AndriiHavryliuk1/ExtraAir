using System.Collections;
using System.Collections.Generic;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.API_DTOs.Helper_DTOs;

namespace ExtraAirCore.Command.TourDetails
{
	public interface IGetTourDetails
	{
		IEnumerable<TourDetailsDto> GetAll();
		TourDetailsDto Get(TourDetailsHelper helper);
	}
}
