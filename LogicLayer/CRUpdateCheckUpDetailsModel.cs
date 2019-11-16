using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text; 

namespace LogicLayer
{
    public class CRUpdateCheckUpDetailsModel
    {
        [Required]
        public string FamilyRecord { get; set; }
        public string Medicines { get; set; }
        public string Allergies { get; set; }
        [Required]
        public string ChiefComplaint { get; set; }
        [Required]
        public string Consultation { get; set; }
    }
}
