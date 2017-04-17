using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	public class TourSearchHistory
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int TourSearchHistoryId { get; set; }
		public string DateStart { get; set; }
		public string DateSearch { get; set; }
		public int? PassengerCount { get; set; }
		public string MacAddress { get; set; } = null;
		public int AirportFromId { get; set; }
		public int AirportToId { get; set; }

		[ForeignKey("Comfort")]
		public int ComfortId { get; set; }
		[ForeignKey("User")]
		public int? UserId { get; set; }

		public virtual Comfort Comfort { get; set; }
		public virtual User User { get; set; }
	}
}