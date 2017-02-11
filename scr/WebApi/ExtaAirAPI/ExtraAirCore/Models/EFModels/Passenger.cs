using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Passengers")]
	public class Passenger
	{
		public int PassengerId { get; set; }
		public PassengerType PassengerType { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Tour> Tours { get; set; }
	}


	public enum PassengerType
	{
		Adult = 0,
		Child = 1,
		Infant = 2
	}
}