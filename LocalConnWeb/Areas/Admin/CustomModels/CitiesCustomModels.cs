using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class CitiesView
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
        public string CityIconPath { get; set; }
        public long StateID { get; set; }
        public long CountryID { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public bool IsPopular { get; set; }
    }
    public class CitiesAPIVM
    {
        public IEnumerable<CitiesView> Cities { get; set; }
        public int TotalRecords { get; set; }
    }
    public class CitiesVM
    {
        public IEnumerable<CitiesView> Cities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class CitiesDD
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
    }
    public class CitiesSaveModel
    {
        public utblLCMstCitie Cities { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
        public Cropper cropper { get; set; }
    }
}