using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCMstHotelPremise
    {
        [Key]
        public long HotelPremID { get; set; }
        public string HotelPremName { get; set; }
    }
}