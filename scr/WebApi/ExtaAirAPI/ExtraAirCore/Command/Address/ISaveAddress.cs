using ExtraAirCore.API_DTOs;

namespace ExtraAirCore.Command.Address
{
	public interface ISaveAddress
	{
		void PostAddress(AddressDto address);
	}
}