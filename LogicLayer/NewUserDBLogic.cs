using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace LogicLayer
{
    public class NewUserDBLogic
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        private StringBuilder _query;
        public void AddNewUser(NewUserModel Model)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    _query = new StringBuilder();
                    _query.Append("INSERT INTO Users..tbUserInfo ");
                    _query.Append("(UserName, Password, FirstName, LastName, Sex, BirthDate, UserType, DateCreated) ");
                    _query.Append("VALUES (@username, @password, @fname, @lname, @sex, @bday, @uType, GETDATE())");

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query.ToString();
                    comm.Parameters.AddWithValue("@username", Model.UserName);
                    comm.Parameters.AddWithValue("@password", Model.Password);
                    comm.Parameters.AddWithValue("@fname", Model.FirstName);
                    comm.Parameters.AddWithValue("@lname", Model.LastName);
                    comm.Parameters.AddWithValue("@sex", Model.Sex);
                    comm.Parameters.AddWithValue("@bday", Model.BirthDate);
                    comm.Parameters.AddWithValue("@uType", Model.UserType);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
    }
}
