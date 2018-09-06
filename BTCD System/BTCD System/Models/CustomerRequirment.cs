using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class CustomerRequirment
    {
        public int? RequirementId { get; set; }
        public int? CustomerId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public int GradeId { get; set; }
        [Required]
        public int UOMId { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        [Required]
        public decimal RequiredQty { get; set; }
        [Required]
        public decimal RequiredPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }


        //Reference 
        public string ItemName { get; set; }
        public string GradeDesc { get; set; }
        public string UOMDesc { get; set; }

        public string CustomerName { get; set; }

    }
}