using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class LocationM
    {
        public int LocationId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }

    }
}