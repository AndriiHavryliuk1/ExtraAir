using System.Collections.Generic;
using System.Web.UI;
using ExtraAirCore.API_DTOs;
using ExtraAirCore.Models.Enumeration;

namespace ExtraAirCore.Command.User
{
	public interface IGetUsers
	{
		IEnumerable<UserForViewDto> GetAllUsers(UserType userType);
		T GetUser<T>(int id);
	}
}
