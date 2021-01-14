using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Helpers;
using LocalConnWeb.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class ActivityView
    {
        public long ActivityID { get; set; }
        public string ActivityName { get; set; }
        public long  DestinationID { get; set; }
        public string DestinationName { get; set; }
        public decimal BaseFare { get; set; }
    }
    public class ActivitySaveModel
    {
        public long ActivityID { get; set; }
        [Required(ErrorMessage = "Enter Activity Name")]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }
        [Required(ErrorMessage = "Enter Activity Desctiption")]
        [Display(Name = "Activity Desctiption")]
        public string ActivityDesc { get; set; }
        [Required(ErrorMessage = "Select Destination")]
        [Display(Name = "Destination")]
        public long DestinationID { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Base Fate")]
        [Display(Name = "Base Fare")]

        public decimal BaseFare { get; set; }
    }
    public class ActivityAPIVM
    {
        public IEnumerable<ActivityView> ActivityList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ActivityVM
    {
        public IEnumerable<ActivityView> ActivityList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class ActivityManageModel
    {
        public ActivitySaveModel Activity { get; set; }
        public IEnumerable<utblMstDestination> Destinations { get; set; }
    }
}