using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAMS_03.Models
{
    public class AdminAccountCreateModel
    {
        [Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        [Display(Name = "User ID")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //public int ID { get; set; }

        [Required]
        public string AdminID { get; set; }

        [Required]
        public string Name { get; set; }

        //public string Email { get; set; }

        [Required]
        [Range (1, 3)]
        public int SecurityLevel { get; set; }

        //public string AspNetID { get; set; }
    }

    public class AdminAccountEditModel
    {
        [Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        [Display(Name = "User ID")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        [Required]
        public int ID { get; set; }

        [Required]
        public string AdminID { get; set; }

        [Required]
        public string Name { get; set; }

        //public string Email { get; set; }

        [Required]
        [Range(1, 3)]
        public int SecurityLevel { get; set; }

        //public string AspNetID { get; set; }

    }

}