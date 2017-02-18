using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command;
using ExtraAirCore.Command.Address;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Addresses
{
	public class SaveAddress : ISaveAddress
	{
		public void PostAddress(AddressDto address)
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
	}
}