using System;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Tour
{
	public interface IGetTourStatus
	{
		IEquatable<TourStatusDto> GetTourWithStatus(int? tourId, DateTime? dateStart, DateTime? dateFinish, int? airportFromId, int? airportToId);
	}
}
