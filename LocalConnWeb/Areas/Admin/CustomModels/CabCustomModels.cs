using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class CabView
    {
        public long CabID { get; set; }
        public string CabName { get; set; }
        public string CabNo { get; set; }
        public long CabTypeID { get; set; }
        public string CabTypeName { get; set; }
        public long CabHeadID { get; set; }
        public string CabHeadName { get; set; }
    }
    public class CabAPIVM
    {
        public IEnumerable<CabView> Cabs { get; set; }
        public int TotalRecords { get; set; }
    }
    public class CabVM
    {
        public IEnumerable<CabView> Cabs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class CabSaveModel
    {
        public utblMstCab Cab { get; set; }
        public IEnumerable<utblMstCabType> CabTypes { get; set; }
        public IEnumerable<utblMstCabHead> CabHeads { get; set; }
    }
    public class CabDriverAPIVM
    {
        public IEnumerable<utblMstCabDriver> Drivers { get; set; }
        public int TotalRecords { get; set; }
    }
    public class CabDriverVM
    {
        public IEnumerable<utblMstCabDriver> Drivers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class CabHeadAPIVM
    {
        public IEnumerable<utblMstCabHead> CabHeads { get; set; }
        public int TotalRecords { get; set; }
    }
    public class CabHeadVM
    {
        public IEnumerable<utblMstCabHead> CabHeads { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}