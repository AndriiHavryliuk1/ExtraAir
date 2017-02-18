using System;
using System.Collections.Generic;
using System.Linq;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;

namespace Data.Api.Client
{
	public class GetClients : IGetUsers
	{
		public IEnumerable<T> GetAllUsers<T>()
		{
			using (var dbContext = new ExtraAirContext())
			{
				return dbContext.Clients.Select(x => new UserForViewDto()
				{
					AddressId = x.AddressId,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					UserId = x.UserId,
					Birthday = x.Birthday,
					Phone = x.Phone
				}).ToList().Cast<T>();
			}
		}


		public T GetUser<T>(int id)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var client = dbContext.Clients.FirstOrDefault();

				if (client == null)
					throw new Exception("User doesn`t exist");

				var clientToCast = new UserForViewDto()
				{
					AddressId = client.AddressId,
					FirstName = client.FirstName,
					LastName = client.LastName,
					Email = client.Email,
					UserId = client.UserId,
					Birthday = client.Birthday,
					Phone = client.Phone
				};

				return (T) Convert.ChangeType(clientToCast, typeof(T));
			}
		}
	}
}