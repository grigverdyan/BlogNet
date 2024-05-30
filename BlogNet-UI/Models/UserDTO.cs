namespace BlogNet_UI.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }
        public int UserID { get; set; }

        public UserDTO(User user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Password = user.Password;
            RegistrationDate = user.RegistrationDate;
            PhotoUrl = user.PhotoUrl;
            UserID = user.UserID;
        }

        public UserDTO(string userName, string email, string password, DateTime registrationDate, string photoUrl, int userID)
        {
            UserName = userName;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
            PhotoUrl = photoUrl;
            UserID = userID;
        }

        public UserDTO() { }
    }
}
