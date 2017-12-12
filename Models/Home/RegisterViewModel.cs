using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class NewUser : BaseEntity
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least two characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least two characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
 
        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string Email { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Must be at least six characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{6,}$", ErrorMessage = "Must contain one uppercase, one lowercase, one number, and one special character.")]
        [Display(Name="Password")]
        public string PW { get; set; }
 
        [Compare("PW", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        public string CPW { get; set; }
    }
}
