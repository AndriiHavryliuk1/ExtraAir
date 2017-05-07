using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.API_DTOs
{
	public class PassengerDto
	{
		public string FirstName;
		public string LastName;
		public GenderType Gender;
		public string CoordinateValue;
		public string IdCard;
		public decimal TicketPrice;
		public bool BaggageInternal;
		public bool BaggageeExternal;
	}
}