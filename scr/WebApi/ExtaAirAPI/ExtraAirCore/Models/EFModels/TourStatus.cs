using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtraAirCore.Models.Enumeration;

namespace ExtraAirCore.Models.EFModels
{
	[Table("TourStatus")]
	public class TourStatus
	{

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int TourStatusId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }
		public TourStatusType TourStatusType { get; set; }
		[ForeignKey("Tour")]
		public int TourId { get; set; }


		public Tour Tour { get; set; }
	}
}