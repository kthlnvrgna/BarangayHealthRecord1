using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer
{
    public abstract class ViewModelBase
    { 
        public int PatientID { get; set; } 
        public string FirstName { get; set; } 
        public string MiddleName { get; set; } 
        public string LastName { get; set; } 
        public string Sex { get; set; } 
        public DateTime? BirthDate { get; set; }
    }
}
