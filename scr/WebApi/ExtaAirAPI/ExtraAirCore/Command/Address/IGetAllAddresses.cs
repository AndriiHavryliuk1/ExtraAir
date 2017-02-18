using System.Collections.Generic;
using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Address
{
	public interface IGetAllAddresses
	{
		ICollection<AddressDto> GetAddresses();
	}
}
