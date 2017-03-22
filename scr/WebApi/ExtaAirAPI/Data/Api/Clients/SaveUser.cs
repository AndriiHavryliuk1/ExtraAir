using System;
using System.Linq;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Clients
{
	public class SaveUser : ISaveUser
	{
		public void Save(ExtraAirCore.Models.EFModels.Client client)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var user = dbContext.Users.SingleOrDefault(x => x.Email == client.Email);
				if (user != null)
				{
					throw new Exception("User Exist!");
				}

				dbContext.Clients.Add(client);
				dbContext.SaveChanges();
			}
		}
	}
}