using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Airports")]
	public class Airport
	{
		public int AirportId { get; set; }
		public string Name { get; set; }
		public Address Address { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; }
	}
}