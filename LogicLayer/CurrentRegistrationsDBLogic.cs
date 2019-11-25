using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace LogicLayer
{
    public class CurrentRegistrationsDBLogic
    {
        string _connectionStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        StringBuilder _queryStr;

        public IEnumerable<PatientsModel> PatientList
        {
            get
            {
                List<PatientsModel> Patients = new List<PatientsModel>();

                using (SqlConnection conn = new SqlConnection(_connectionStr))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        _queryStr = new StringBuilder();
                        _queryStr.Append("SELECT * ");
                        _queryStr.Append("FROM PatientData..tbPatientRegistration a ");
                        _queryStr.Append("LEFT JOIN PatientData..tbPatientMaster b on a.PatientID = b.PatientID ");
                        _queryStr.Append("WHERE CAST(RegDate AS DATE) = CAST(GETDATE() AS DATE) ");

                        comm.Connection = conn;
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = _queryStr.ToString();
                        conn.Open();
                        SqlDataReader dr = comm.ExecuteReader();

                        while (dr.Read())
                        {
                            PatientsModel patient = new PatientsModel();
                            patient.PatientID = Convert.ToInt32(dr["PatientID"].ToString());
                            patient.RegNum = dr["RegNum"].ToString();
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
                            Patients.Add(patient);
                        }

                    }
                }
                return Patients;
            }
        }
        public CurrentRegistrationsModel GetCheckUpData(int id, string regnum)
        {
            CurrentRegistrationsModel CRDetails = null;
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                { 
                    _queryStr = new StringBuilder();
                    _queryStr.Append("SELECT b.PatientID, a.RegNum, FirstName, MiddleName, Lastname, sex, FamilyRecord, Medicines, Allergies, ChiefComplaint, Consultation, BirthDate ");
                    _queryStr.Append("FROM PatientData..tbPatientRegistration a ");
                    _queryStr.Append("LEFT JOIN PatientData..tbPatientMaster b on a.PatientID = b.PatientID ");
                    _queryStr.Append("LEFT JOIN PatientData..tbRegistrationDetails c on b.PatientID = c.PatientID ");
                    _queryStr.Append(string.Format("WHERE b.PatientID = '{0}' AND a.RegNum = '{1}'", id, regnum));

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();


                    if (dr.Read())
                    {
                        CRDetails = new CurrentRegistrationsModel
                        {
                            PatientID = Convert.ToInt32(dr["PatientID"].ToString()),
                            RegNum = Convert.ToInt32(dr["RegNum"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            MiddleName = dr["MiddleName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            Sex = dr["Sex"].ToString(),
                            FamilyRecord = dr["FamilyRecord"].ToString(),
                            Medicines = dr["Medicines"].ToString(),
                            Allergies = dr["Allergies"].ToString(),
                            ChiefComplaint = dr["ChiefComplaint"].ToString(),
                            Consultation = dr["Consultation"].ToString()
                        }; 
                        if (!(dr["BirthDate"] is DBNull))
                        {
                            CRDetails.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                        }
                    }
                     
                }
            }
            return CRDetails;
        }
        public void UdpatePatientCheckUpDetails(CurrentRegistrationDetailsModel Model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    _queryStr = new StringBuilder();
                    _queryStr.Append("UPDATE PatientData..tbRegistrationDetails ");
                    _queryStr.Append("SET ");
                    _queryStr.Append("FamilyRecord  = @familyRecord, ");
                    _queryStr.Append("Medicines = @meds, ");
                    _queryStr.Append("Allergies = @allergies, ");
                    _queryStr.Append("ChiefComplaint = @chiefComplaint, ");
                    _queryStr.Append("Consultation= @consultation "); 
                    _queryStr.Append(string.Format("WHERE RegNum = '{0}'", Model.RegNum));


                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("@familyRecord", Model.FamilyRecord);
                    comm.Parameters.AddWithValue("@meds", Model.Medicines);
                    comm.Parameters.AddWithValue("@allergies", Model.Allergies);
                    comm.Parameters.AddWithValue("@chiefComplaint", Model.ChiefComplaint); 
                    comm.Parameters.AddWithValue("@consultation", Model.Consultation); 

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
