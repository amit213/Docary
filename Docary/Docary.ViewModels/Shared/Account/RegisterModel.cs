using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace Docary.ViewModels.Shared.Account
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Time zone")]
        public IEnumerable<TimeZoneInfo> TimeZones { get; set; }
        
        [Required]        
        public string TimeZoneId { get; set; }
    }
}
