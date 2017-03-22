using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraAirCore.Models.EFModels
{
	[Table("Clients")]
	public class Client : User
	{
		public Client()
		{
			Orders = new List<Order>();
			Feedbacks = new List<Feedback>();
			CreditCards = new List<CreditCard>();
			TourSearchHistories = new List<TourSearchHistory>();
		}

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Feedback> Feedbacks { get; set; }
		public virtual ICollection<CreditCard> CreditCards { get; set; }
		public virtual ICollection<TourSearchHistory> TourSearchHistories { get; set; }
	}
}