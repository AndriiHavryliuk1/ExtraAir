using System.Collections.Generic;

namespace ExtraAirCore.Command.User
{
	public interface IGetUsers
	{
		IEnumerable<T> GetAllUsers<T>();
		T GetUser<T>(int id);
	}
}
