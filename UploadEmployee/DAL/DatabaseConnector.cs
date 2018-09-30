using System.Configuration;
using System.Data.SqlClient;

namespace UploadEmployee.DAL
{
    public class DatabaseConnector
    {
        private SqlConnection connection;

        public DatabaseConnector()
        {
            string connectionString = ConfigurationSettings.AppSettings["DB_CON"].ToString();
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}
