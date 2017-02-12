using System.Collections.Generic;
using ExtraAirCore.API_DTO;


namespace ExtraAirApi.Api.Addresses
{
	public class Address_Impl
	{
		public ICollection<Address_Dto> GetAddreses()
		{
			return new Data.Api.Addresses.Addresses().GetAddresses();
		}


		public void AddAddress(Address_Dto address)
		{
			new Data.Api.Addresses.Addresses().PostAddress(address);
		}
	}
}