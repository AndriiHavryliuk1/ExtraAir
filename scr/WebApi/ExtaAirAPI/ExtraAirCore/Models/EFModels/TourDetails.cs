using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("TourDetails")]
	public class TourDetails
	{
		public TourDetails()
		{
			BookedPlaces = new List<BookedPlace>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int TourDetailsId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }
		public int CurrentCountOfBusinessPassenger { get; set; }
		public int CurrentCountOfEconomyPassenger { get; set; }
		public bool Temporary { get; set; }

		[ForeignKey("Tour")]
		public int? TourId { get; set; }

		public virtual Tour Tour { get; set; }
		public virtual List<BookedPlace> BookedPlaces { get; set; }
	}


	[Table("BookedPlace")]
	public class BookedPlace
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int BookedPlaceId { get; set; }
		public int PointX { get; set; }
		public int PointY { get; set; }
		public ComfortType ComfortType { get; set; }

		[ForeignKey("TourDetails")]
		public int TourDetailsId { get; set; }

		public virtual TourDetails TourDetails { get; set; }
	}

}