using BlogNet_DAL.Models;
using System.Data.SqlClient;

namespace BlogNet_DAL
{
    public class UserRepository
    {
        private static readonly string sqlUserID = "UserID";
        private static readonly string sqlUsername = "Username";
        private static readonly string sqlEmail = "Email";
        private static readonly string sqlPassword = "Password";
        private static readonly string sqlRegistrationDate = "RegistrationDate";
        private static readonly string sqlPhotoUrl = "PhotoUrl";
        private readonly ConnectionManager connection;

        public UserRepository(ConnectionManager connection)
        {
            this.connection = connection;
        }
        public User Get(int id)
        {
            const string getUser = "GetUser";
            SqlCommand getCommand = connection.CreateStoredProcedure(getUser);
            getCommand.Parameters.Add(sqlUserID, System.Data.SqlDbType.Int).Value = id;
            
            using SqlDataReader reader = getCommand.ExecuteReader();
            User user = null;
            if (reader.Read()) 
            {
                user = new User
                {
                    UserID = id,
                    Username = reader.GetString(reader.GetOrdinal(sqlUsername)),
                    Email = reader.GetString(reader.GetOrdinal(sqlEmail)),
                    Password = reader.GetString(reader.GetOrdinal(sqlPassword)),
                    RegistrationDate = reader.GetDateTime(reader.GetOrdinal(sqlRegistrationDate)),
                    PhotoUrl = reader.GetString(reader.GetOrdinal(sqlPhotoUrl))
                };
            }
            return user;
        }
        public IEnumerable<User> Get()
        {

            const string getUsers = "GetUsers";
            SqlCommand getCommand = connection.CreateStoredProcedure(getUsers);

            using SqlDataReader reader = getCommand.ExecuteReader();
            List<User> users = new ();
            if (reader.Read())
            {
                User user = new User();
                user.UserID = reader.GetInt32(reader.GetOrdinal(sqlUserID));
                user.Username = reader.GetString(reader.GetOrdinal(sqlUsername));
                user.Email = reader.GetString(reader.GetOrdinal(sqlEmail));
                user.Password = reader.GetString(reader.GetOrdinal(sqlPassword));
                user.RegistrationDate = reader.GetDateTime(reader.GetOrdinal(sqlRegistrationDate));
                user.PhotoUrl = reader.GetString(reader.GetOrdinal(sqlPhotoUrl));
                
                users.Add(user);
            }
            return users;
        }
        private bool Upsert(User user)
        {
            const string upsertUser = "UpsertUser";
            const string id = "userID";
            const string username = "username";
            const string email = "email";
            const string password = "password";
            const string registrationDate = "registrationDate";
            const string photoUrl = "photoUrl";

            SqlCommand cmd = connection.CreateStoredProcedure(upsertUser);
            SqlParameter idParam = cmd.Parameters.Add(id, System.Data.SqlDbType.Int);
            idParam.Direction = System.Data.ParameterDirection.InputOutput;
            idParam.Value = user.UserID;
            cmd.Parameters.Add(username, System.Data.SqlDbType.NVarChar).Value = user.Username;
            cmd.Parameters.Add(email, System.Data.SqlDbType.NVarChar).Value = user.Email;
            cmd.Parameters.Add(password, System.Data.SqlDbType.NVarChar).Value = user.Password;
            cmd.Parameters.Add(registrationDate, System.Data.SqlDbType.DateTime).Value = user.RegistrationDate;
            cmd.Parameters.Add(photoUrl, System.Data.SqlDbType.NVarChar).Value = user.PhotoUrl;
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
               user.UserID = Convert.ToInt32(idParam.Value);
            }
            return ret > 0;
        }
        public User Insert(UserBase userToInsert)
        {
            User user = new User
            {
                Username = userToInsert.Username,
                Email = userToInsert.Email,
                Password = userToInsert.Password,
                RegistrationDate = userToInsert.RegistrationDate,
                PhotoUrl = userToInsert.PhotoUrl,
                UserID = 0
            };
            if (!Upsert(user))
            {
                return null;
            }
            return user;
        }
        public bool Update(User user)
        {
            return Upsert(user);
        }
        public bool Delete(int id)
        {
            const string idParam = "id";
            const string deleteUser = "deleteUser";

            SqlCommand cmd = connection.CreateStoredProcedure(deleteUser);
            cmd.Parameters.Add(idParam, System.Data.SqlDbType.Int).Value = id;
            int ret = cmd.ExecuteNonQuery();
            return ret > 0;
        }
    }
}
