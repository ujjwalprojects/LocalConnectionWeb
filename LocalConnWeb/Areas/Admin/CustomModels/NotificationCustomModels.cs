using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class NotificationView
    {
        public long NotificationID { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDesc { get; set; }
        public string NotificationImagePath { get; set; }
    }
    public class NotificationAPIVM
    {
        public IEnumerable<NotificationView> Notification { get; set; }
        public int TotalRecords { get; set; }
    }
    public class NotificationVM
    {
        public IEnumerable<NotificationView> Notificationlist { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class NotificationSaveModel
    {
        public utblLCNotification notificationsave { get; set; }
        public Cropper cropper { get; set; }
    }
}