using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{
    public class Patients
    {
        public string PatientID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Sex { get; set; } 

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CivilStatus { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nationality { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Religion { get; set; }
    }
}
