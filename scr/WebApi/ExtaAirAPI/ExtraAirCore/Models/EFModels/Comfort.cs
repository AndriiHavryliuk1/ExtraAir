using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Comforts")]
	public class Comfort
	{
		public int ComfortId { get; set; }
		public string Name { get; set; }
		public ComfortType ComfortType { get; set; }
		public decimal Price { get; set; }
		public Plane Plane { get; set; }
	}

	public enum ComfortType
	{
		Economic = 0,
		Business = 1,
		FirstClass = 2
	}
}