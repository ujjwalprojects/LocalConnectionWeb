using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class StarRatingView
    {
        public long StarRatingID { get; set; }
        public string StarRatingName { get; set; }
    }
    public class StarRatingAPIVM
    {
        public IEnumerable<StarRatingView> StarRatings { get; set; }
        public int TotalRecords { get; set; }
    }
    public class StarRatingVM
    {
        public IEnumerable<StarRatingView> StarRatings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class StarRatingSaveModel
    {
        public utblLCMstStarRating StarRatings { get; set; }
    }
    public class StarRatingDD
    {
        public long StarRatingID { get; set; }
        public string StarRatingName { get; set; }
    }
}