using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Addresses")]
	public class Address
	{
		public int AddressId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public int StreetNumber { get; set; }
		public int PostIndex { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Airport> Airports { get; set; }
	}
}