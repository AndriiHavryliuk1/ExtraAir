using System;
using System.Collections.Generic;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Tour
{
	public interface IGetTourStatus
	{
		List<TourStatusDto> GetTourWithStatus(int? tourId, DateTime? dateStart, DateTime? dateFinish, int? airportFromId, int? airportToId);
	}
}
