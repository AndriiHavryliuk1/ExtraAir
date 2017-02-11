using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Feedbacks")]
	public class Feedback
	{
		public int FeedbackId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Tour Tour { get; set; }
		public Airport Airport { get; set; }
	}
}