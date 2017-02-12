using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.API_DTO
{
	public class Address_Dto
	{
		public int AddressId;
		public string Country;
		public string City;
		public string Street;
		public int StreetNumber;
		public int PostIndex;


		public Address_Dto() { }

		public Address_Dto(Address address)
		{
			AddressId = address.AddressId;
			Country = address.Country;
			City = address.City;
			Street = address.Street;
			StreetNumber = address.StreetNumber;
			PostIndex = address.PostIndex;
		}
	}
}