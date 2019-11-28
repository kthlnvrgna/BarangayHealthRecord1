using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LogicLayer;
namespace LogicLayer
{
    public class PatientsLogicLayer
    {

        string _connectionStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        StringBuilder _queryStr;
        public IEnumerable<PatientsModel> PatientList
        {
            get
            { 
                List<PatientsModel> liPatients = new List<PatientsModel>();

                using (SqlConnection conn = new SqlConnection(_connectionStr))
                {
                    string queryStr = "SELECT TOP 100 * FROM PatientData..tbPatientMaster";
                    SqlCommand comm = new SqlCommand(queryStr, conn);
                    conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();

                    while (dr.Read())
                    {
                        PatientsModel patient = new PatientsModel();
                        patient.PatientID = Convert.ToInt32(dr["PatientID"].ToString());
                        patient.FirstName = dr["FirstName"].ToString();
                        patient.MiddleName = dr["MiddleName"].ToString();
                        patient.LastName = dr["LastName"].ToString();
                        patient.Sex = dr["Sex"].ToString();
                        patient.Address = dr["Address"].ToString();
                        patient.Nationality = dr["Nationality"].ToString();
                        patient.Religion = dr["Religion"].ToString();
                        if (!(dr["BirthDate"] is DBNull))
                        { 
                        patient.BirthDate = Convert.ToDateTime(dr["BirthDate"]); 
                        }
                        liPatients.Add(patient);
                    }

                }
                return liPatients;
            }
        }
        public void AddNewPatientRegistration(PatientsModel patient)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {  
                _queryStr = new StringBuilder();
                _queryStr.Append("INSERT INTO PatientData..tbPatientMaster ");
                _queryStr.Append("(FirstName, MiddleName, LastName, Address, CivilStatus, Nationality, Religion, BirthDate, Sex) ");
                _queryStr.Append("VALUES (@fname, @mName, @lName, @address, @civilStatus, @nationality, @Religion, @Bday, @sex) ");

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryStr.ToString();
                comm.Parameters.AddWithValue("@fname", patient.FirstName);
                comm.Parameters.AddWithValue("@mName", patient.MiddleName);
                comm.Parameters.AddWithValue("@lName", patient.LastName);
                comm.Parameters.AddWithValue("@sex", patient.Sex);
                comm.Parameters.AddWithValue("@address", patient.Address);
                comm.Parameters.AddWithValue("@civilStatus", patient.CivilStatus);
                comm.Parameters.AddWithValue("@nationality", patient.Nationality);
                comm.Parameters.AddWithValue("@Religion", patient.Religion); 
                comm.Parameters.AddWithValue("@Bday", patient.BirthDate); 

                conn.Open(); 
                comm.ExecuteNonQuery();

                _queryStr.Clear();
                comm.Parameters.Clear();
                _queryStr = new StringBuilder();

                _queryStr.Append("INSERT INTO PatientData..tbPatientRegistration ");
                _queryStr.Append("(PatientID, RegDate) ");
                _queryStr.Append("VALUES ");
                _queryStr.Append("((SELECT TOP 1 PatientID FROM PatientData..tbPatientMaster ORDER BY PatientID DESC), GETDATE()) "); 
                comm.CommandText = _queryStr.ToString();   
                comm.ExecuteNonQuery();

                _queryStr.Clear();
                comm.Parameters.Clear();
                _queryStr = new StringBuilder();

                string PatientID = string.Empty;
                _queryStr.Append("SELECT PatientID FROM PatientData..tbPatientMaster WHERE  FirstName=@fName AND Lastname=@lName and BirthDate=@Bday"); 
                comm.CommandText = _queryStr.ToString();
                comm.Parameters.AddWithValue("@fname", patient.FirstName);
                comm.Parameters.AddWithValue("@lName", patient.LastName);
                comm.Parameters.AddWithValue("@Bday", patient.BirthDate);

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                        PatientID = dr["PatientID"].ToString(); 

                _queryStr.Clear();
                comm.Parameters.Clear();
                dr.Close();
                _queryStr = new StringBuilder();

                _queryStr.Append("INSERT INTO PatientData..tbRegistrationDetails ");
                _queryStr.Append("(PatientID, RegNum) ");
                _queryStr.Append("VALUES ");
                _queryStr.Append("(@pID, (SELECT RegNum FROM PatientData..tbPatientRegistration WHERE PatientID = @pID AND RegDate = CAST(GETDATE() AS DATE)))");
                comm.CommandText = _queryStr.ToString();
                comm.Parameters.AddWithValue("@pID", PatientID);  
                comm.ExecuteNonQuery();

                _queryStr.Clear();
                comm.Parameters.Clear();
            }
        } 

        public void UpdatePatientInfo(PatientsModel patient)
        {
            using(SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    _queryStr = new StringBuilder();
                    _queryStr.Append("UPDATE PatientData..tbPatientMaster ");
                    _queryStr.Append("SET ");
                    _queryStr.Append("FirstName  = @fName, ");
                    _queryStr.Append("MiddleName = @mName, ");
                    _queryStr.Append("LastName = @lName, ");
                    _queryStr.Append("Sex = @sex, ");
                    _queryStr.Append("Address = @address, ");
                    _queryStr.Append("CivilStatus = @civilStatus, ");
                    _queryStr.Append("Nationality = @nationality, ");
                    _queryStr.Append("Religion = @religion, ");
                    _queryStr.Append("BirthDate = @bDay ");
                    _queryStr.Append("WHERE PatientID = @patientID");


                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("@fname", patient.FirstName);
                    comm.Parameters.AddWithValue("@mName", patient.MiddleName);
                    comm.Parameters.AddWithValue("@lName", patient.LastName);
                    comm.Parameters.AddWithValue("@sex", patient.Sex);
                    comm.Parameters.AddWithValue("@address", patient.Address);
                    comm.Parameters.AddWithValue("@civilStatus", patient.CivilStatus);
                    comm.Parameters.AddWithValue("@nationality", patient.Nationality);
                    comm.Parameters.AddWithValue("@Religion", patient.Religion);
                    comm.Parameters.AddWithValue("@Bday", patient.BirthDate);
                    comm.Parameters.AddWithValue("@patientID", patient.PatientID);

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
