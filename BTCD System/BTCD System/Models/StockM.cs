using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class StockM
    {
        public int StockId { get; set; }

        [Required]
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string EmployeeCode { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int GradeId { get; set; }
        public string Grade { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        public decimal RemainQuantity { get; set; }
        [Required]
        public int UOMId { get; set; }
        public string UOMName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public string StockOwner { get; set; }
    }
}