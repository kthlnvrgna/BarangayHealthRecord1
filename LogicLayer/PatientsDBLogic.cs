using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LogicLayer;
namespace LogicLayer
{
    public class PatientsDBLogic
    {

        private string _connectionStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        private StringBuilder _queryStr;
        private string _pxID;
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
                        patient.Category = dr["Category"].ToString();
                        patient.CivilStatus = dr["CivilStatus"].ToString();
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
        public void AddNewPatientRegistration(PatientsModel Model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {  
                _queryStr = new StringBuilder();
                _queryStr.Append("INSERT INTO PatientData..tbPatientMaster ");
                _queryStr.Append("(PatientID, FirstName, MiddleName, LastName, Address, CivilStatus, Nationality, Religion, BirthDate, Sex, Category) ");
                _queryStr.Append("VALUES (@patientID, @fname, @mName, @lName, @address, @civilStatus, @nationality, @Religion, @Bday, @sex, @category) ");

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = _queryStr.ToString();
                comm.Parameters.AddWithValue("@patientID", GetPxRxNum("PxID"));
                comm.Parameters.AddWithValue("@fname", Model.FirstName);
                comm.Parameters.AddWithValue("@mName", Model.MiddleName);
                comm.Parameters.AddWithValue("@lName", Model.LastName);
                comm.Parameters.AddWithValue("@sex", Model.Sex);
                comm.Parameters.AddWithValue("@address", Model.Address);
                comm.Parameters.AddWithValue("@civilStatus", Model.CivilStatus);
                comm.Parameters.AddWithValue("@nationality", Model.Nationality);
                comm.Parameters.AddWithValue("@Religion", Model.Religion); 
                comm.Parameters.AddWithValue("@Bday", Model.BirthDate);  
                comm.Parameters.AddWithValue("@category", Model.Category);

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

        public void InsertPatientAdmission(string pxID)
        {
            if (pxID == null)
            {
                _pxID = GetPxRxNum("PxID");
            }
            else _pxID = pxID;

            using(SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    _queryStr = new StringBuilder(); 
                    _queryStr.Append("INSERT INTO PatientData..tbPatientRegistration ");
                    _queryStr.Append("(PatientID, RegDate, RegNum) ");
                    _queryStr.Append("VALUES ");
                    _queryStr.Append("(@patientID, GETDATE(), @regnum)");

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString(); 
                    comm.Parameters.AddWithValue("@patientID", _pxID);
                    comm.Parameters.AddWithValue("@regnum", GetPxRxNum("RxID"));

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

        public void InsertInitialCheckUpDetails(string pxID)
        {
            if (pxID == null)
            {
                _pxID = GetPxRxNum("PxID");
            }
            else _pxID = pxID;

            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    _queryStr = new StringBuilder();   
                    _queryStr.Append("INSERT INTO PatientData..tbRegistrationDetails ");
                    _queryStr.Append("(PatientID, RegNum) ");
                    _queryStr.Append("VALUES ");
                    _queryStr.Append("(@patientID, @patientRegNum)");

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    comm.Parameters.AddWithValue("@patientID", _pxID);
                    comm.Parameters.AddWithValue("@patientRegNum", GetPxRxNum("RxID")); 

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

        private string GetPxRxNum(string Field)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT * FROM PatientData..PxRegNum";

                    conn.Open();

                    SqlDataReader rdr; 
                    rdr = comm.ExecuteReader();
                        if (rdr.Read())
                            if (rdr[Field] != System.DBNull.Value)
                                return rdr[Field].ToString();

                    return null;
                }
            }
        }

        public void IncPxRxNum()
        {
            using(SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "UPDATE PatientData..PxRegNum SET PxID = (CAST(PxID AS INT) + 1), RxID = (CAST(RxID AS INT) + 1)";

                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        public void IncRxNum()
        {
            using(SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using(SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "UPDATE PatientData..PxRegNum SET RxID = (CAST(RxID AS INT) + 1)";

                    conn.Open();
                    comm.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePatientInfo(PatientsModel Model)
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

                    comm.Parameters.AddWithValue("@fname", Model.FirstName);
                    comm.Parameters.AddWithValue("@mName", Model.MiddleName);
                    comm.Parameters.AddWithValue("@lName", Model.LastName);
                    comm.Parameters.AddWithValue("@sex", Model.Sex);
                    comm.Parameters.AddWithValue("@address", Model.Address);
                    comm.Parameters.AddWithValue("@civilStatus", Model.CivilStatus);
                    comm.Parameters.AddWithValue("@nationality", Model.Nationality);
                    comm.Parameters.AddWithValue("@Religion", Model.Religion);
                    comm.Parameters.AddWithValue("@Bday", Model.BirthDate);
                    comm.Parameters.AddWithValue("@patientID", Model.PatientID);

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


