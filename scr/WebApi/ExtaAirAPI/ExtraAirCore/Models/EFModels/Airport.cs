﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Airports")]
	public class Airport
	{
		public Airport()
		{
			Tours = new List<Tour>();
			Feedbacks = new List<Feedback>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int AirportId { get; set; }
		public string Name { get; set; }
		[ForeignKey("Address")]
		public int? AddressId { get; set; }

		public virtual Address Address { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; }
	}
}