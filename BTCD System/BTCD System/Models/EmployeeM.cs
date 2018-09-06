using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class EmployeeM
    {
        public string EmployeeCode { get; set; }
        public string EmployeeCategory { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NickName { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int LocationId { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Telephone1 { get; set; }
        [Required]
        public string Telephone2 { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountNo { get; set; }
        public string Email { get; set; }
        [Required]
        public string NICNo { get; set; }
        public string Dob { get; set; }
    }
}