using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Passengers")]
	public class Passenger
	{
		public Passenger()
		{
			Orders = new List<Order>();
			Tours = new List<Tour>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int PassengerId { get; set; }
		[Required]
		public PassengerType PassengerType { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public GenderType Gender { get; set; }
		public string CoordinateValue { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Tour> Tours { get; set; }
	}


	public enum PassengerType
	{
		Adult = 0,
		Child = 1,
		Infant = 2
	}

	public enum GenderType
	{
		Male = 0,
		Female = 1
	}
}