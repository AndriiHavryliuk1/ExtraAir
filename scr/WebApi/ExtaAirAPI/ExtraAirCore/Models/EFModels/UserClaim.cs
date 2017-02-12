using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ExtraAirCore.Models.EFModels
{
	public class UserClaim
	{
		public UserClaim()
		{
			Users = new List<User>();
		}

		[Key]
		public int Id { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }

		[JsonIgnore]
		public virtual ICollection<User> Users { get; set; }
	}
}