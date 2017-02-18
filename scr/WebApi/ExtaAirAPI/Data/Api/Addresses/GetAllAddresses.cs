using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command;
using ExtraAirCore.Command.Address;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Addresses
{
	public class GetAllAddresses : IGetAllAddresses
	{
		public ICollection<AddressDto> GetAddresses()
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Addresses.Select(x => new AddressDto
				{
					AddressId = x.AddressId,
					Country = x.Country,
					City = x.City,
					Street = x.Street,
					StreetNumber = x.StreetNumber,
					PostIndex = x.PostIndex
				}).ToList();
			}
		}
	}
}