using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class ItemGradeM
    {
        public int GradeId { get; set; }
        public int ItemId { get; set; }
        public string Grade { get; set; }
        public string GradeDescription { get; set; }
        public int GradeLevel { get; set; }
    }
}