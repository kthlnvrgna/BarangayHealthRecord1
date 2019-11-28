using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LogicLayer
{
    public class CurrentUserDBLogic
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        public CurrentUserModel UserInfo(string ID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = string.Format("SELECT * FROM Users..tbUserInfo WHERE UserID= '{0}'", ID);
                    conn.Open();

                    CurrentUserModel ResultModel;
                    SqlDataReader rdr;
                    rdr = comm.ExecuteReader();

                    if (rdr.Read())
                    {
                        ResultModel = new CurrentUserModel();
                        ResultModel.UserID = rdr["UserID"].ToString();
                        ResultModel.FirstName = rdr["FirstName"].ToString();
                        ResultModel.LastName = rdr["LastName"].ToString();
                        ResultModel.UserName = rdr["UserName"].ToString();
                        ResultModel.Password = rdr["Password"].ToString();
                        ResultModel.Sex = rdr["Sex"].ToString();
                        ResultModel.UserType = rdr["UserType"].ToString();
                        if (!(rdr["BirthDate"] is DBNull))
                            ResultModel.BirthDate = Convert.ToDateTime(rdr["BirthDate"]);

                        return ResultModel;
                    }

                    return null;
                }
            }

        } 
        public void UpdateUserInfo(CurrentUserModel model, string id)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("UPDATE Users..tbUserInfo SET ");
                    query.Append("FirstName = @fName,");
                    query.Append("LastName = @lName,");
                    query.Append("UserName = @username,");
                    query.Append("Password = @password ");
                    query.Append(string.Format("WHERE Userid = '{0}'", id)); 

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = query.ToString();
                    comm.Parameters.AddWithValue("@fName", model.FirstName);
                    comm.Parameters.AddWithValue("@lName", model.LastName);
                    comm.Parameters.AddWithValue("@username", model.UserName);
                    comm.Parameters.AddWithValue("@password", model.Password);

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

        public bool UserNameCorrespondsUserID(string userID,string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = string.Format("SELECT 1 FROM Users..tbUserInfo WHERE UserID = '{0}' AND UserName ='{1}'", userID, username);

                    conn.Open();
                    SqlDataReader rdr;
                    rdr = comm.ExecuteReader();

                    if (rdr.Read())
                        return true;

                    return false;
                }
            }
        }

    }
}
