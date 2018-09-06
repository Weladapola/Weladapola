using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class RecentActivityU
    {
        public long ID { get; set; }
        public string TrnUser { get; set; }
        public System.DateTime TrnDate { get; set; }
        public short LogType { get; set; }
        public short TransactionType { get; set; }
        public short RecordType { get; set; }
        public string Description { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }

    }
}