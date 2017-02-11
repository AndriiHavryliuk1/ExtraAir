using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Tours")]
	public class Tour
	{
		public int TourId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }
		public decimal Price { get; set; }
		public int CurrentCountPassenger { get; set; }
		public Plane Plane { get; set; }
		public Order Order { get; set; }

		public virtual ICollection<Passenger> Passengers { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; }
	}
}