using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LogicLayer
{
    public class PatientDetailsDBLogic
    {
        private string _connectionStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        private StringBuilder _queryStr;
        public IEnumerable<PatientDetailsModel> PatientDetails(string PatientId)
        { 
            List<PatientDetailsModel> Model = new List<PatientDetailsModel>();

            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    _queryStr = new StringBuilder();
                    _queryStr.Append("SELECT a.*, b.RegNum, b.RegDate, b.DcrDate, FamilyRecord, Medicines, Allergies, ChiefComplaint, Consultation, Treatment, Diagnosis ");
                    _queryStr.Append("FROM PatientData..tbPatientMaster a ");
                    _queryStr.Append("LEFT JOIN PatientData..tbPatientRegistration b ON a.PatientID = b.PatientID ");
                    _queryStr.Append("LEFT JOIN PatientData..tbRegistrationDetails c ON b.RegNum = c.RegNum ");
                    _queryStr.Append(string.Format("WHERE b.PatientID = '{0}'", PatientId));

                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _queryStr.ToString();
                    conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();

                    while (dr.Read())
                    {
                        PatientDetailsModel model = new PatientDetailsModel(); 
                        model.RegNum = dr["RegNum"].ToString();
                        model.RegDate = Convert.ToDateTime(dr["RegDate"]);
                        if (!(dr["DcrDate"] is DBNull))
                        { 
                            model.DcrDate = Convert.ToDateTime(dr["DcrDate"]);
                        }
                        model.FamilyRecord = dr["FamilyRecord"].ToString();
                        model.Medicines = dr["Medicines"].ToString();
                        model.Allergies = dr["Allergies"].ToString();
                        model.Medicines = dr["Medicines"].ToString();
                        model.Allergies = dr["Allergies"].ToString();
                        model.ChiefComplaint = dr["ChiefComplaint"].ToString();
                        model.Consultation = dr["Consultation"].ToString();
                        model.Treatment = dr["Treatment"].ToString();
                        model.Diagnosis = dr["Diagnosis"].ToString();
                        Model.Add(model);
                    }
                    return Model; 

                }
            }
            return Model; 
        }

    }
}
