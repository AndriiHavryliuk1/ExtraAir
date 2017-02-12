using System.Collections.Generic;
using ExtraAirCore.API_DTO;

namespace ExtraAirCore.Command
{
	public interface IAddresses
	{
		void PostAddress(Address_Dto address);
		ICollection<Address_Dto> GetAddresses();
	}
}
