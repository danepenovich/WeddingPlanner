using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Dashboard
    {
        public List<Wedding> Weddings {get;set;}
        public User User {get;set;}
    }

    public class ViewWedding
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least two characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [Display(Name="Wedder One")]

        public string WedderOne {get;set;}

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least two characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        [Display(Name="Wedder Two")]
        public string WedderTwo {get;set;}

        [Required]
        [FutureDate]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        [Required]
        [MinLength(3, ErrorMessage = "Must be at least three characters.")]
        [Display(Name="Wedding Address")]
        public string Address {get;set;}
    }

    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;
            return date < DateTime.Now ? new ValidationResult("Date must be in future.") : ValidationResult.Success;
        }
    }
}