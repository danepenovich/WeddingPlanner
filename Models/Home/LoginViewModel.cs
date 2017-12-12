using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class LogUser : BaseEntity
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string LogEmail {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LogPW {get;set;}
    }
}