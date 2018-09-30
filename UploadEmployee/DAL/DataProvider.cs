using System;
using System.Data;
using System.Data.SqlClient;

namespace UploadEmployee.DAL
{
    public class DataProvider
    {
        public static DataTable GetData(string queryString)
        {
            DatabaseConnector connector = new DatabaseConnector();
            connector.OpenConnection();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(queryString, connector.Connection);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                connector.CloseConnection();
                return dt;
            }
            catch (SqlException ex)
            {
                connector.CloseConnection();
                throw ex;
            }
            catch (Exception ex)
            {
                connector.CloseConnection();
                throw ex;
            }
        }

        public static bool TakeBackup(string sqlString)
        {
            //SqlTransaction transaction;
            DatabaseConnector connector = new DatabaseConnector();
            connector.OpenConnection();
            //transaction = connector.Connection.BeginTransaction("SampleTransaction");

            SqlCommand command = new SqlCommand(sqlString, connector.Connection);
            //command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                //transaction.Commit();
                command.Dispose();
                connector.CloseConnection();
                return true;
            }
            catch
            {
                //transaction.Rollback();
                command.Dispose();
                connector.CloseConnection();
                return false;
            }
        }

        public static bool ExecuteNonQuery(string sqlString)
        {
            SqlTransaction transaction;
            DatabaseConnector connector = new DatabaseConnector();
            connector.OpenConnection();
            transaction = connector.Connection.BeginTransaction("SampleTransaction");

            SqlCommand command = new SqlCommand(sqlString, connector.Connection);
            command.Transaction = transaction;
            try
            {
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
                transaction.Commit();
                command.Dispose();
                connector.CloseConnection();
                return true;
            }
            catch
            {
                transaction.Rollback();
                command.Dispose();
                connector.CloseConnection();
                return false;
            }
        }

        public static DataTable GetDataWareHouse(string queryString, string userName, string password)
        {
            DatabaseConnectorWarehouse connector = new DatabaseConnectorWarehouse(userName, password);

            connector.OpenConnection();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(queryString, connector.Connection);
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(dt);
                connector.CloseConnection();
                return dt;
            }
            catch (SqlException ex)
            {
                connector.CloseConnection();
                throw ex;
            }
            catch (Exception ex)
            {
                connector.CloseConnection();
                throw ex;
            }
        }

        public static bool ExecuteNonQueryWarehouse(string sqlString,string userName,string password)
        {
            SqlTransaction transaction;
            DatabaseConnectorWarehouse connector = new DatabaseConnectorWarehouse(userName,password);
            connector.OpenConnection();
            transaction = connector.Connection.BeginTransaction("SampleTransaction");

            SqlCommand command = new SqlCommand(sqlString, connector.Connection);
            command.Transaction = transaction;
            try
            {
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
                transaction.Commit();
                command.Dispose();
                connector.CloseConnection();
                return true;
            }
            catch
            {
                transaction.Rollback();
                command.Dispose();
                connector.CloseConnection();
                return false;
            }
        }
    }
}
