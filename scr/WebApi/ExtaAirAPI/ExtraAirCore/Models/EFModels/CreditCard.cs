using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("CreditCards")]
	public class CreditCard
	{
		public int CreditCardId { get; set; }
		public int CardNumber { get; set; }
		public DateTime ExpirationDate { get; set; }
		public int SecurityCode { get; set; }
		public User User { get; set; }
	}
}