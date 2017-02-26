using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.Address;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace Data.Api.Addresses
{
	public class GetAllAddresses : IGetAllAddresses
	{
		public ICollection<AddressDto> GetAddresses()
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Addresses.Select(x => Mapper.Map<Address, AddressDto>(new Address())).ToList();
			}
		}
	}
}