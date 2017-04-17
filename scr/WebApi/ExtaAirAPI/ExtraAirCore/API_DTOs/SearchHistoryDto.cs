namespace ExtraAirCore.API_DTOs
{
	public class SearchHistoryDto
	{
		public int? UserId;
		public int PassengerCount;
		public string DateStart;
		public string DateSearch;
		public AirportDto AirportFrom;
		public AirportDto AirportTo;
	}
}