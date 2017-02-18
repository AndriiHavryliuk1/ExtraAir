using System.Collections.Generic;
using ExtraAirCore.API_DTOs;


namespace ExtraAirApi.Api.Addresses
{
	public class Address_Impl
	{
		public ICollection<AddressDto> GetAddreses()
		{
			return new Data.Api.Addresses.GetAllAddresses().GetAddresses();
		}


		public void AddAddress(AddressDto address)
		{
			new Data.Api.Addresses.SaveAddress().PostAddress(address);
		}
	}
}