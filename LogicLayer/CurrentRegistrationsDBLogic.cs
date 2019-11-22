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
        public CurrentRegistrationsModel GetCheckUpData(int id)
        {
            CurrentRegistrationsModel CRDetails = null;
            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                { 
                    _queryStr = new StringBuilder();
                    _queryStr.Append("SELECT * ");
                    _queryStr.Append("FROM PatientData..tbPatientRegistration a ");
                    _queryStr.Append("LEFT JOIN PatientData..tbPatientMaster b on a.PatientID = b.PatientID ");
                    _queryStr.Append("LEFT JOIN PatientData..tbRegistrationDetails c on b.PatientID = c.PatientID ");
                    _queryStr.Append(string.Format("WHERE b.PatientID = '{0}'", id));

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
        public void UdpatePatientCheckUpDetails(CurrentRegistrationsModel CRDetails)
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
                    _queryStr.Append("Consultation= @consultation, "); 


                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("@familyRecord", CRDetails.FamilyRecord);
                    comm.Parameters.AddWithValue("@meds", CRDetails.Medicines);
                    comm.Parameters.AddWithValue("@allergies", CRDetails.Allergies);
                    comm.Parameters.AddWithValue("@consultations", CRDetails.Consultation); 

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
