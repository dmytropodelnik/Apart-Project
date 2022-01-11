using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CloneBookingAPI.Services.Helpers
{
    public class AuthOptions
    {
		public const string ISSUER = "CloneBooking";
		public const string AUDIENCE = "CloneBookingUser";
		const string KEY = "authentification_security_key!qwe123";
		public const int LIFETIME = 5;
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
