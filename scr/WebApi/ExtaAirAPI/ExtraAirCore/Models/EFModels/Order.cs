using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Orders")]
	public class Order
	{
		public Order()
		{
			Tours = new List<Tour>();
			Passengers = new List<Passenger>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int OrderId { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public bool Paid { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		[ForeignKey("CreditCard")]
		public int CreditCardId { get; set; }

		public virtual CreditCard CreditCard { get; set; }
		public virtual User User { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Passenger> Passengers { get; set; }
	}
}