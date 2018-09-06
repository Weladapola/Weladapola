using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace BTCD_System.Models
{
    public class UserRoleU
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Role ID")]
        public string RoleID { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Trn Date")]
        public DateTime TrnDate { get; set; }
        [Required]
        [Display(Name = "Trn User")]
        public string TrnUser { get; set; }
    }
}