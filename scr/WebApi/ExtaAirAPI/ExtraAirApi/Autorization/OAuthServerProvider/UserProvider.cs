using System;
using System.Data.Entity;
using System.Threading.Tasks;
using ExtraAirCore.Models.EFContex;
using ExtraAirCore.Models.EFModels;

namespace ExtraAirApi.Autorization.OAuthServerProvider
{
	public class UserProvider
	{
		readonly ExtraAirContext dbContext;
		public UserProvider(ExtraAirContext context)
		{
			dbContext = context;
		}


		public async Task AddUserAsync(User user, string password)
		{
			if (await UserExists(user))
			{
				throw new Exception(
					"A user with that Email address already exists");
			}
			dbContext.Users.Add(user);
			await dbContext.SaveChangesAsync();
		}


		public async Task<User> FindByEmailAsync(string email)
		{
			return await dbContext.Users
			.FirstOrDefaultAsync(u => u.Email == email);
		}


		public async Task<User> FindByIdAsync(int userId)
		{
			return await dbContext.Users
				.FirstOrDefaultAsync(u => u.UserId == userId);
		}


		public async Task<bool> UserExists(User user)
		{
			return await dbContext.Users
				.AnyAsync(u => u.UserId == user.UserId || u.Email == user.Email);
		}


		public async Task AddClaimAsync(int UserId, UserClaim claim)
		{
			var user = await FindByIdAsync(UserId);
			if (user == null)
			{
				throw new Exception("User does not exist");
			}
			user.UserClaim = claim;
			await dbContext.SaveChangesAsync();
		}
	}
}