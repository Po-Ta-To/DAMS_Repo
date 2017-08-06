using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAMS_03.Models
{
    public class UserAccountIndexModel
    {
        public List<UserAccount> UserAccounts { get; set; }
        public int page { get; set; }
        public int perpage { get; set; }
        public string search { get; set; }
        public int maxPages { get; set; }
        public int maxRecords { get; set; }
    }

    public class UserAccountCreateModel
    {
        [Required]
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

        //public int ID { get; set; }

        [Required]
        public string NRIC { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public string DOB { get; set; }

        [Required]
        [Display(Name = "Preferred gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Mobile no")]
        public int Mobile { get; set; }

        //[Required]
        [Display(Name = "Address")]
        public string Addrress { get; set; }


    }


    public class UserAccountEditModel
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
        public int ID { get; set; }

        [Required]
        public string NRIC { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Mobile { get; set; }

        //[Required]
        public string Addrress { get; set; }


    }


}