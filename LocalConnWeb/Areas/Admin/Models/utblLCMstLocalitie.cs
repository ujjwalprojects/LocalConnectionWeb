using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCMstLocalitie
    {
        [Key]
        public long LocalityID { get; set; }
        [Display(Name="Locality")]
        public string LocalityName { get; set; }
        public long CityID { get; set; }
    }
}