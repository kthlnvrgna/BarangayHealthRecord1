using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{
    public class PatientsModel
    { 
        public int PatientID { get; set; }
        public string RegNum { get; set; }

        [Required]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Sex { get; set; } 

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CivilStatus { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nationality { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Religion { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Category { get; set; }
    }
}
