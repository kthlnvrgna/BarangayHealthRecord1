using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{ 
    public class CurrentRegistrationsModel
    { 
        public int PatientID { get; set; }
        public int RegNum { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string FamilyRecord { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Medicines { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Allergies { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string ChiefComplaint { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Consultation { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Diagnosis { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Treatment { get; set; }
    }
}
