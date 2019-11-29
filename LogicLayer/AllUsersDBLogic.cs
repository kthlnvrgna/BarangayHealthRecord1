using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LogicLayer
{
    public class AllUsersDBLogic
    {
        private string _connectionStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

        public IEnumerable<AllUsersModel> GetAllUsers
        {
            get
            {
                using(SqlConnection conn = new SqlConnection(_connectionStr))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        List<AllUsersModel> modelLi = new List<AllUsersModel>();
                        comm.Connection = conn;
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT * FROM Users..tbUserInfo";

                        try
                        {
                            conn.Open();
                            SqlDataReader rdr;
                            rdr = comm.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                while(rdr.Read())
                                {
                                    AllUsersModel model = new AllUsersModel();

                                    model.UserID = rdr["UserID"].ToString();
                                    model.FirstName = rdr["FirstName"].ToString();
                                    model.LastName = rdr["LastName"].ToString();
                                    model.BirthDate = Convert.ToDateTime(rdr["BirthDate"]);
                                    model.Sex = rdr["Sex"].ToString();
                                    model.UserType = rdr["UserType"].ToString();
                                    model.UserName = rdr["UserName"].ToString();
                                    model.Password = rdr["Password"].ToString();

                                    modelLi.Add(model);
                                }

                                return modelLi;
                            }
                            return null;
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
}
