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


        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string EmployeeCode { get; set; }

        public int LocationId { get; set; }

        public int GradeId { get; set; }
        public string Grade { get; set; }

        public decimal Quantity { get; set; }
        public decimal RemainQuantity { get; set; }

        public int UOMId { get; set; }
        public string UOMName { get; set; }


        public decimal UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public string StockOwner { get; set; }

        // Anuradha Added Comment Field
        public string Description { get; set; }
        public int NoOfBids { get; set; }

        public DateTime CreateUntill { get; set; }
    }
}