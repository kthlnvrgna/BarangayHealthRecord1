using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{
    public class CurrentRegistrationDetailsModel 
    { 
        public string RegNum { get; set; }
   
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
