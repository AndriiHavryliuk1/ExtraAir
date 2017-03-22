using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("TourToAirports")]
	public class TourToAirport
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int TourToAirportId { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateFinish { get; set; }
		public bool isInterim { get; set; } = false;

		[ForeignKey("Tour")]
		public int TourId { get; set; }
		[ForeignKey("Airport")]
		public int AirportId { get; set; }

		public virtual Tour Tour { get; set; }
		public virtual Airport Airport { get; set; }
	}
}