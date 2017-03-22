using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Comforts")]
	public class Comfort
	{
		public Comfort()
		{
			TourSearchHistories = new List<TourSearchHistory>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int ComfortId { get; set; }
		public string Name { get; set; }
		[Required]
		public ComfortType ComfortType { get; set; }
		[Required]
		public double PriceCorf { get; set; }
		[ForeignKey("Plane")]
		public int? PlaneId { get; set; }

		public virtual Plane Plane { get; set; }

		public virtual ICollection<TourSearchHistory> TourSearchHistories { get; set; }
	}

	public enum ComfortType
	{
		Economy = 0,
		Business = 1
	}
}