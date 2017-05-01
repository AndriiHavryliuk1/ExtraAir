using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Planes")]
	public class Plane
	{
		public Plane()
		{
			Tours = new List<Tour>();
			Comforts = new List<Comfort>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int PlaneId { get; set; }
		public string Name { get; set; }
		public int MaxCountPassengerEconomy { get; set; }
		public int MaxCountPassengerBusiness { get; set; }

		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Comfort> Comforts { get; set; }
	}
}