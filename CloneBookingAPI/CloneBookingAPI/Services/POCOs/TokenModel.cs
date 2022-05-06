namespace CloneBookingAPI.Services.POCOs
{
    public class TokenModel
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public bool IsFacebookAuth { get; set; }
        public bool IsGoogleAuth { get; set; }

        public TokenModel(string username, string accessToken)
        {
            Username = username;
            AccessToken = accessToken;
        }

        public override string ToString()
        {
            return Username + ";" + AccessToken;
        }
    }
}
