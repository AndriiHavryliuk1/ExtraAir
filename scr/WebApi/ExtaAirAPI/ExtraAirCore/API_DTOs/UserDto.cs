using System;

namespace ExtraAirCore.API_DTOs
{
	public class UserDto
	{
		public int UserId;
		public string FirstName;
		public string LastName;
		public string Email;
		public string Password;
		public string Phone;
		public DateTime Birthday;
		public bool Deleted;
		public int? AddressId;
		public int UserClaimId;
	}


	public class UserForViewDto
	{
		public int UserId;
		public string FirstName;
		public string LastName;
		public string Email;
		public string Phone;
		public DateTime Birthday;
		public bool Deleted;
		public int? AddressId;
	}
}