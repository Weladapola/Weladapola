using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class RequestT
    {
        public int RequirementId { get; set; }
        public int StockId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal RequiredQty { get; set; }
        public decimal RequiredPrice { get; set; }
        public DateTime RequiredDate { get; set; }
        public string CreatedBy { get; set; }
        public decimal Payment { get; set; }
        public string RequestedBy { get; set; }
        public string StockCreatedBy { get; set; }
        public decimal RemainQty { get; set; }
    }
}