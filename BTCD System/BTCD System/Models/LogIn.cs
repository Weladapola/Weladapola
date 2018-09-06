using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class LogIn
    {
        [Required(ErrorMessage = "Username Required.",AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}