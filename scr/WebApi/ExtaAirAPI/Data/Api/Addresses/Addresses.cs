using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTO;
using ExtraAirCore.Command;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Addresses
{
	public class Addresses : IAddresses
	{
		public void PostAddress(Address_Dto address)
		{
			using (var db = new ExtraAirContext())
			{
				db.Addresses.Add(new ExtraAirCore.Models.EFModels.Address()
				{
					AddressId = address.AddressId,
					City = address.City,
					Country = address.Country,
					PostIndex = address.PostIndex,
					Street = address.Street,
					StreetNumber = address.StreetNumber
				});
				db.SaveChanges();
			}
		}


		public ICollection<Address_Dto> GetAddresses()
		{
			using (ExtraAirContext dbContext = new ExtraAirContext())
			{
				return dbContext.Addresses.Select(x => new Address_Dto
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