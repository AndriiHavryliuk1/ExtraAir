using ExtraAirCore.Models.EFModels;

namespace ExtraAirCore.Command.User
{
	public interface ISaveUser
	{
		void Save(Client user);
	}
}
