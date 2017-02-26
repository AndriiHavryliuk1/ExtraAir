using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Command.User;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;
using ExtraAirCore.Models.Enumeration;

namespace Data.Api.Clients
{
	public class GetClients : IGetUsers
	{
		public IEnumerable<UserForViewDto> GetAllUsers(UserType userType)
		{
			using (var dbContext = new ExtraAirContext())
			{
				switch (userType)
				{
					case UserType.Client:
					{
						return dbContext.Clients.ToList().Select(MapHelper).ToList();
					}
					case UserType.User:
						{
							return dbContext.Users.ToList().Select(MapHelper).ToList();
						}
					case UserType.Dispatcher:
						{
							return dbContext.Dispatchers.ToList().Select(MapHelper).ToList();
						}
					default: return null;
				}
			}
		}


		public T GetUser<T>(int id)
		{
			using (var dbContext = new ExtraAirContext())
			{
				var client = dbContext.Clients.FirstOrDefault(x => x.UserId == id);

				if (client == null)
					throw new Exception("User doesn`t exist");

				Mapper.Initialize(cfg => cfg.CreateMap<Address, AddressDto>());
				var address = Mapper.Map<Address, AddressDto>(client.Address);

				Mapper.Initialize(cfg => cfg.CreateMap<Client, UserForViewDto>().ForMember(x => x.Address, y => y.Ignore()));
				var clientToCast = Mapper.Map<Client, UserForViewDto>(client);
				clientToCast.Address = address;

				return (T) Convert.ChangeType(clientToCast, typeof(T));
			}
		}


		private static UserForViewDto MapHelper(User user)
		{
			return new UserForViewDto
			{
				Birthday = user.Birthday,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Deleted = user.Deleted,
				Email = user.Email,
				Phone = user.Phone,
				UserId = user.UserId,
				Address = new AddressDto
				{
					AddressId = user.Address.AddressId,
					City = user.Address.City,
					PostIndex = user.Address.PostIndex,
					Street = user.Address.Street,
					StreetNumber = user.Address.StreetNumber,
					Country = user.Address.Country
				}
			};
		}
	}
}