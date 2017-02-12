using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Users")]
	public abstract class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }
		[Required]
		public DateTime Birthday { get; set; }
		[Required]
		public bool Deleted { get; set; }
		[ForeignKey("Address")]
		public int? AddressId { get; set; }
		[ForeignKey("UserClaim")]
		public int UserClaimId { get; set; }

		public virtual Address Address { get; set; }
		public virtual UserClaim UserClaim { get; set; }


	}
}