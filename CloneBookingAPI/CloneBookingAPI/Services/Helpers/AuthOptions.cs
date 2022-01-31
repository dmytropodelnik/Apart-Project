using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CloneBookingAPI.Services.Helpers
{
    public class AuthOptions
    {
		public const string ISSUER = "ApartProject";
		public const string AUDIENCE = "ApartProjectClient";
		const string KEY = "authentification_security_key!apartproject2022qweq";
		public const int LIFETIME = 5;
		public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
			 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
