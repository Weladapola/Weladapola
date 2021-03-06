﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTCD_System.Models
{
    public class ItemM
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string SinghalaDescription { get; set; }
        public string TamilDescription { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }

    }
}