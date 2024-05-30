namespace BlogNet_UI.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }
        public int UserID { get; set; }

        public User(UserDTO udto)
        {
            UserName = udto.UserName;
            Email = udto.Email;
            Password = udto.Password;
            RegistrationDate = udto.RegistrationDate;
            PhotoUrl = udto.PhotoUrl;
            UserID = udto.UserID;
        }

        public User(string userName, string email, string password, DateTime registrationDate, string photoUrl, int userID)
        {
            UserName = userName;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
            PhotoUrl = photoUrl;
            UserID = userID;
        }

        public User() {}
    }
}
