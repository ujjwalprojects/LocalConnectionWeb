using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class LocalitiesView
    {
        public long LocalityID { get; set; }
        public string LocalityName { get; set; }
        public long CityID { get; set; }
        public string CityName { get; set; }
    }
    public class LocalitiesAPIVM
    {
        public IEnumerable<LocalitiesView> Localities { get; set; }
        public int TotalRecords { get; set; }
    }
    public class LocalitiesVM
    {
        public IEnumerable<LocalitiesView> Localities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class LocalitiesDD
    {
        public long LocalityID { get; set; }
        public string LocalityName { get; set; }
    }
    public class LocalitiesSaveModel
    {
        public utblLCMstLocalitie Localities { get; set; }
        public IEnumerable<CitiesDD> CitiesList { get; set; }
        public Cropper cropper { get; set; }
    }
}