using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class BannerView
    {
        public long BannerID { get; set; }
        public string BannerPath { get; set; }
    }

    public class BannerAPIVM
    {
        public IEnumerable<BannerView> BannerList{ get; set; }
        public int TotalRecords { get; set; }
    }


    public class BannerVM
    {
        public IEnumerable<BannerView> BannerList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class SaveBanner
    {
        public utblMstBanner Banner { get; set; }
        public Cropper cropper { get; set; }
    }


    public class Cropper
    {
        public string PhotoNormal { get; set; }
        public string PhotoThumb { get; set; }
    }
}