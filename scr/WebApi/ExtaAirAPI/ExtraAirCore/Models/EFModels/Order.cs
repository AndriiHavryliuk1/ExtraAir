using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Orders")]
	public class Order
	{
		public int OrderId { get; set; }
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
		public bool Paid { get; set; }
		public CreditCard CreditCard { get; set; }
		public User User { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Passenger> Passengers { get; set; }
	}
}