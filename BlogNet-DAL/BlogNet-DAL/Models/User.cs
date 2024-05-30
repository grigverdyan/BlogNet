namespace BlogNet_DAL.Models
{
    public class UserBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class User : UserBase
    {
        public int UserID { get; set; }
    }
}
