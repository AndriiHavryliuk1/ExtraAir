using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Feedbacks")]
	public class Feedback
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public int FeedbackId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		[ForeignKey("Tour")]
		public int? TourId { get; set; }
		[ForeignKey("Airport")]
		public int? AirportId { get; set; }
		[ForeignKey("User")]
		public int? UserId { get; set; }

		public virtual Tour Tour { get; set; }
		public virtual Airport Airport { get; set; }
		public virtual User User { get; set; }
	}
}