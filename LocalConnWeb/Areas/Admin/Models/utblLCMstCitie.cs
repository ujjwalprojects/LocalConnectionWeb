using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCMstCitie
    {
        [Key]
        public long CityID { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        [Display(Name = "City Icon")]
        public string CityIconPath { get; set; }
        public long StateID { get; set; }
        public long CountryID { get; set; }
        public bool IsPopular { get; set; }
    }
}