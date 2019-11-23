using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicLayer
{
    public class NewUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}
