using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{
    public class Patients
    {
        public string PatientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; } 
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string CivilStatus { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
    }
}
