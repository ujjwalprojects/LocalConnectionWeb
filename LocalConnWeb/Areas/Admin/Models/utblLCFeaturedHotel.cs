using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCFeaturedHotel
    {
        [Key]
        public long FeatureID { get; set; }
        public long HotelID { get; set; }
        public DateTime FeatureStartDate { get; set; }
        public DateTime FeatureEndDate { get; set; }
    }
}