using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Tours")]
	public class Tour
	{
		public Tour()
		{
			Passengers = new List<Passenger>();
			Feedbacks = new List<Feedback>();
			TourToAirports = new List<TourToAirport>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int TourId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }
		[Required]
		public decimal Price { get; set; }
		public int CurrentCountPassenger { get; set; }
		[ForeignKey("Plane")]
		public int PlaneId { get; set; }
		[ForeignKey("Order")]
		public int OrderId { get; set; }

		public virtual Plane Plane { get; set; }
		public virtual Order Order { get; set; }

		public virtual ICollection<Passenger> Passengers { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; }
		public virtual ICollection<TourToAirport> TourToAirports { get; set; }
	}
}