using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace UploadEmployee.DAL
{
    public class DatabaseConnectorWarehouse
    {
        private SqlConnection connection;

        //public DatabaseConnectorWarehouse()
        //{
        //    string connectionString = ConfigurationSettings.AppSettings["DB_CON"].ToString();
        //    connection = new SqlConnection(connectionString);
        //}
        public DatabaseConnectorWarehouse(string username, string password)
        {
            string _connectionString = "";
            string _name = "";
            DbProviderFactory _provider;

            //if (connectionName == null) throw new ArgumentNullException("connectionName");

            var conStr = ConfigurationManager.ConnectionStrings["DB_CON"];
            if (conStr == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in app/web.config."));

            _name = conStr.ProviderName;
            _provider = DbProviderFactories.GetFactory(conStr.ProviderName);
            _connectionString = string.Format(conStr.ConnectionString + "User Id={0};Password={1};", username, password);
            connection = new SqlConnection(_connectionString);

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
