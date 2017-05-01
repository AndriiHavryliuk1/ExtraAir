namespace ExtraAirCore.API_DTOs
{
	public class PlaneDto
	{
		public int PlaneId;
		public string Name;
		public CountPassengerDto MaxCountPassenger;
	}

	public class CountPassengerDto
	{
		public int CountOfEconomyPassenger;
		public int CountOfBusinessPassenger;
	}
}