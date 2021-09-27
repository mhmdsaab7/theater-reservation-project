
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TheaterPrjt.Data
{
    public class BaseSQLDataManager
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        protected List<T> GetSPItems<T>(string spName, Func<IDataReader, T> mapper, out object count, params object[] parameters)
        {
            CreateDBConnection();
            sqlConnection.Open();

            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            sqlCommand.Connection = sqlConnection;

            SqlCommandBuilder.DeriveParameters(sqlCommand);

            int outParamIndex = 0;
            int j = 0;

            for (int i = 0; i < sqlCommand.Parameters.Count; i++)
            {
                if (sqlCommand.Parameters[i].Direction == ParameterDirection.Input)
                {
                    sqlCommand.Parameters[i].Value = parameters[j] ?? DBNull.Value;
                    j++;
                }
                else if (sqlCommand.Parameters[i].Direction == ParameterDirection.InputOutput)
                {
                    sqlCommand.Parameters[i].Direction = ParameterDirection.Output;
                    outParamIndex = i;
                }
            }

            List<T> items = new List<T>();

            using (sqlConnection)
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    items.Add(mapper(sqlDataReader));
                }
                sqlDataReader.Close();
                count = sqlCommand.Parameters[outParamIndex].Value;
            }

            return items;
        }

        protected List<T> GetSPItems<T>(string spName, Func<IDataReader, T> mapper, params object[] parameters)
        {
            CreateDBConnection();
            sqlConnection.Open();

            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            sqlCommand.Connection = sqlConnection;

            SqlCommandBuilder.DeriveParameters(sqlCommand);

            int j = 0;

            for (int i = 0; i < sqlCommand.Parameters.Count; i++)
            {
                if (sqlCommand.Parameters[i].Direction == ParameterDirection.Input)
                {
                    sqlCommand.Parameters[i].Value = parameters[j] ?? DBNull.Value;
                    j++;
                }
            }

            List<T> items = new List<T>();

            using (sqlConnection)
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    items.Add(mapper(sqlDataReader));
                }
                sqlDataReader.Close();
            }

            return items;
        }

        protected T GetSPItem<T>(string spName, Func<IDataReader, T> mapper, params object[] parameters)
        {
            CreateDBConnection();
            sqlConnection.Open();

            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            sqlCommand.Connection = sqlConnection;

            SqlCommandBuilder.DeriveParameters(sqlCommand);


            for (int i = 0; i < parameters.Length; i++)
            {
                sqlCommand.Parameters[i + 1].Value = parameters[i] ?? DBNull.Value;
            }

            T item = default(T);
            using (sqlConnection)
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    item = mapper(sqlDataReader);
                }
                sqlDataReader.Close();
            }

            return item;
        }

        protected bool ExecuteNonQuery(string spName, params object[] parameters)
        {
            CreateDBConnection();
            sqlConnection.Open();

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;

            SqlCommandBuilder.DeriveParameters(sqlCommand);

            for (int i = 0; i < parameters.Length; i++)
            {
                sqlCommand.Parameters[i + 1].Value = parameters[i] ?? DBNull.Value;
            }

            int output;
            using (sqlConnection)
            {
                output = sqlCommand.ExecuteNonQuery();
            }

            return (output > 0) ? true : false;
        }

        protected bool ExecuteNonQuery(string spName, out object id, params object[] parameters)
        {
            CreateDBConnection();
            sqlConnection.Open();

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;

            SqlCommandBuilder.DeriveParameters(sqlCommand);
            int outParamIndex = 0;
            int j = 0;

            for (int i = 0; i < sqlCommand.Parameters.Count; i++)
            {
                if (sqlCommand.Parameters[i].Direction == ParameterDirection.Input)
                {
                    sqlCommand.Parameters[i].Value = parameters[j] ?? DBNull.Value;
                    j++;
                }
                else if (sqlCommand.Parameters[i].Direction == ParameterDirection.InputOutput)
                {
                    sqlCommand.Parameters[i].Direction = ParameterDirection.Output;
                    outParamIndex = i;
                }
            }

            int output;
            using (sqlConnection)
            {
                output = sqlCommand.ExecuteNonQuery();
                id = sqlCommand.Parameters[outParamIndex].Value;
            }
            return (output > 0) ? true : false;
        }

        private void CreateDBConnection()
        {
            var connection = "Server=localhost\\SQLEXPRESS;Database=TheaterReservation;Trusted_Connection=True;";
            sqlConnection = new SqlConnection(connection);
        }
    }
}