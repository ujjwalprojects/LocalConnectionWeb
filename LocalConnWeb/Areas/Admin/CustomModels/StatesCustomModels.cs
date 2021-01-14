using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class StateView
    {
        public long StateID { get; set; }
        public long CountryID { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
    public class CountryDD
    {
        public long CountryID { get; set; }
        public string CountryName { get; set; }
    }
    public class StateSaveModel
    {
        public utblMstState State { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
    }
    public class StateDD
    {
        public long StateID { get; set; }
        public string StateName { get; set; }
    }
}