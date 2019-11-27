using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace LogicLayer
{
    public class LoginDBLogic
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        public string FindUser(LoginModel Model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT UserID FROM Users..tbUserInfo WHERE UserName=@userName AND Password = @password";
                    comm.Parameters.AddWithValue("@userName", Model.UserName);
                    comm.Parameters.AddWithValue("@password", Model.Password);

                    conn.Open();
                    SqlDataReader rdr;
                    rdr = comm.ExecuteReader();
                    if (rdr.Read())
                        return rdr["UserID"].ToString();

                    return null;
                }
            }
        }
    }
}
