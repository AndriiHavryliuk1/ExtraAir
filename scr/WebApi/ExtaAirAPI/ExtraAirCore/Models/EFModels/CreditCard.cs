using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("CreditCards")]
	public class CreditCard
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int CreditCardId { get; set; }
		[Required]
		public int CardNumber { get; set; }
		[Required]
		public DateTime ExpirationDate { get; set; }
		[Required]
		public int SecurityCode { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }

		public virtual User User { get; set; }
	}
}