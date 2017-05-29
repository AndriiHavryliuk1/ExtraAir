using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using ExtraAirCore.API_DTOs.Helper_DTOs;
using ExtraAirCore.Constants;
using Microsoft.AspNet.Identity;

namespace Rest.Helpers
{
	public class EMailHelper
	{
		private const string Email = "extraair111@gmail.com";
		private const string fromPassword = "ExtraAir123";

		public static void SendNotification(EmailInputDto emailInput)
		{
			var fromAddress = new MailAddress(Email, "Extra Air");
			var toAddress = new MailAddress(emailInput.Email, emailInput.UserName);
			var message = new MailMessage(fromAddress, toAddress);

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};

			message.Subject = emailInput.Subject;
			message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailInput.Body, null, MediaTypeNames.Text.Html));

			smtp.Send(message);
			smtp.Dispose();
			message.Dispose();

		}


		public static void SendConfirmRegisterNotification(EmailInputDto emailInput, int idUser)
		{
			var fromAddress = new MailAddress(Email, "Extra Air");
			var toAddress = new MailAddress(emailInput.Email, emailInput.UserName);
			var html = "Будь ласка підтвердіть реєстрацію свого акаунта перейшовши за посиланням: <a href=\"" + Path.WebClientPath
				+ "#/confirmRegistration/" + idUser + "\">link</a><br/>";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};

			var message = new MailMessage(fromAddress, toAddress);
			message.Subject = emailInput.Subject;
			message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

			smtp.Send(message);
			smtp.Dispose();
			message.Dispose();
		}


	}
}