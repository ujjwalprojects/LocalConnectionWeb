using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class CountriesModel
    {
        public long CountryID { get; set; }
        [Required(ErrorMessage="Enter Country Name")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
    }
}