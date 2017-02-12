using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Comforts")]
	public class Comfort
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int ComfortId { get; set; }
		public string Name { get; set; }
		[Required]
		public ComfortType ComfortType { get; set; }
		[Required]
		public decimal Price { get; set; }
		[ForeignKey("Plane")]
		public int PlaneId { get; set; }

		public virtual Plane Plane { get; set; }
	}

	public enum ComfortType
	{
		Economic = 0,
		Business = 1,
		FirstClass = 2
	}
}