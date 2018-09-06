using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class UserU
    {
        public int  Id { get; set; }

        [Required]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password",ErrorMessage = "Password doesn't match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string EmployeeCategory { get; set; }

        public string NickName { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TrnDate { get; set; }

        [Display(Name = "Transaction User")]
        public string TrnUser { get; set; }

        [Display(Name = "Activation")]
        public bool Active { get; set; }
    }
}