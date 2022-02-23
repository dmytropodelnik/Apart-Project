namespace CloneBookingAPI.Services.POCOs
{
    public class UserData
    {
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public int RoleId { get; set; }
    }
}
