using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class BidsM
    {
        public int BidId { get; set; }
        [Required]
        public int StockId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int LocationId { get; set; }
        public int GradeId { get; set; }
        public string Grade { get; set; }
        public int UOMId { get; set; }
        public string UOMName { get; set; }
        [Required]
        public string RequestedBy { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal RequestedPrice { get; set; }
        [Required]
        public decimal RequestedQty { get; set; }
        public bool IsConfirmed { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}