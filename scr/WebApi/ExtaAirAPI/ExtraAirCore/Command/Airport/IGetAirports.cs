using System.Collections.Generic;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Airport
{
	public interface IGetAirports
	{
		IEnumerable<AirportDto> GetAllAirports();
		IEnumerable<AirportDto> GetAirportsById(int id);
	}
}
