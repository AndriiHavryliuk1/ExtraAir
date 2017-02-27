using System.Collections.Generic;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.API_DTOs
{
	public class OrderDto
	{
		public int PlaneId;
		public string Name;
		public int MaxCountPassenger;

		public virtual ICollection<TourDto> Tours { get; set; }
		public virtual ICollection<Comfort> Comforts { get; set; }
	}
}