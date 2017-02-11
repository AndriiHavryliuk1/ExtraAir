using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Planes")]
	public class Plane
	{
		public int PlaneId { get; set; }
		public string Name { get; set; }
		public int MaxCountPassenger { get; set; }
		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<Comfort> Comforts { get; set; }
	}
}