using System;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Client
{
	public class SaveUser : ISaveUser
	{
		public void Save(ExtraAirCore.Models.EFModels.Client client)
		{
			using (var dbContext = new ExtraAirContext())
			{
				dbContext.Clients.Add(client);
				dbContext.SaveChanges();
			}
		}
	}
}