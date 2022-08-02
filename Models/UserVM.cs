using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CBTour.Models
{
    public class UserVM
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^([0-9]+[a-zA-Z]+|[a-zA-Z]+[0-9]+)[0-9a-zA-Z]*$", ErrorMessage = "Password must contain at least one number and one character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your Passwords do not match")]
        public string ConfirmPass { get; set; }

        public int ID { get; set; }

        [Display(Name = "Select Role")]
        public string Role { get; set; }

        public string ImagePath { get; set; }

        public UserVM() { }

    }
}