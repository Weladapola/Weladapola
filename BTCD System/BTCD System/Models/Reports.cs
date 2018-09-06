using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class Reports
    {
        public int ID { get; set; }
        public int ReportNumber { get; set; }
        public string ReportCategory { get; set; }
        public string ReportDisplayName { get; set; }
        public string ReportName { get; set; }
        public string ServerPath { get; set; }
        public string ReportFolder { get; set; }
        public DateTime TrnDate { get; set; }
        public string TrnUser { get; set; }
    }
}