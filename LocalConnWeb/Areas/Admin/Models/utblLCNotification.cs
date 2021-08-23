using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCNotification
    {
        [Key]
        public long NotificationID { get; set; }
        [Display(Name = "Title")]
        public string NotificationTitle { get; set; }
        [Display(Name = "Content")]
        public string NotificationDesc { get; set; }
        public string NotificationImagePath { get; set; }
    }
}