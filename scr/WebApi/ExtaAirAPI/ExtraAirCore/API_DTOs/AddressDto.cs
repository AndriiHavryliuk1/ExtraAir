using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.API_DTOs
{
	public class AddressDto
	{
		public int AddressId;
		public string Country;
		public string City;
		public string Street;
		public int StreetNumber;
		public int PostIndex;


		public AddressDto() { }

		public AddressDto(Address address)
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