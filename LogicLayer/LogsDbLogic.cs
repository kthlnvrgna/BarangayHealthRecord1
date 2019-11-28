using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LogicLayer
{
    public class LogsDbLogic
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        public void SaveLogs(int type, string userID, string details)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    StringBuilder query = new StringBuilder();
                    string remarks;

                    switch (type)
                    {
                        case 1:
                            remarks = "New login.";
                            break;
                        case 2:
                            remarks = "New user created. " + details;
                            break;
                        case 3:
                            remarks = "Edit user information. " + details;
                            break;
                        case 4:
                            remarks = "New patient registration. " + details;
                            break;
                        case 5:
                            remarks = "New admission. " + details;
                            break;
                        case 6:
                            remarks = "Edited patient information. " + details;
                            break;
                        case 7:
                            remarks = "Saved patient consultation data. " + details;
                            break;
                        default:
                            remarks = "";
                            break;
                    } 

                    query.Append("INSERT INTO PatientData..Logs ");
                    query.Append("(Remarks, RemarksDate, UserID) ");
                    query.Append("VALUES ");
                    query.Append(string.Format("('{0}', GETDATE(), '{1}') ", remarks, userID));

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = query.ToString();


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
