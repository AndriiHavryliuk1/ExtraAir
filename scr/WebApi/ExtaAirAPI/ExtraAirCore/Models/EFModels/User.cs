using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Users")]
	public class User
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
		public DateTime Birthday { get; set; }
		public bool Deleted { get; set; }
		public Address Address { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}