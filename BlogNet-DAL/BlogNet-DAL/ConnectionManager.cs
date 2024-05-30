using System.Data;
using System.Data.SqlClient;

namespace BlogNet_DAL
{
    public class ConnectionManager : IDisposable
    {
        private string connectionString;
        private static SqlConnection sqlConnect = null;

        public ConnectionManager(string connectionSt)
        {
            connectionString = connectionSt;
        }
        public void CreateConnection() 
        {
            if (sqlConnect == null) 
            {
                sqlConnect = new SqlConnection(connectionString);
            }
        }
        public SqlCommand CreateStoredProcedure(string name)
        {
            CreateConnection();
            sqlConnect.Open();
            SqlCommand getCommand = sqlConnect.CreateCommand();
            getCommand.CommandText = name;
            getCommand.CommandType = System.Data.CommandType.StoredProcedure;
            return getCommand;
        }
        public void Dispose() 
        {
            if (sqlConnect != null && sqlConnect.State == ConnectionState.Open)
            {
                sqlConnect.Close();
            }
        }

    }
}
