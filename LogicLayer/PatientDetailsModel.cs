using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer
{
    public class PatientDetailsModel
    { 
        public string RegNum { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? DcrDate { get; set; }
        public string FamilyRecord { get; set; }
        public string Medicines { get; set; }
        public string Allergies { get; set; }
        public string ChiefComplaint { get; set; }
        public string Consultation { get; set; }
        public string Treatment { get; set; }
        public string Diagnosis { get; set; }
    }
}
