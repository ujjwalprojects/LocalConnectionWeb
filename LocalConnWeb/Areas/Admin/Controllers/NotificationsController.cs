using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class NotificationsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        // GET: Admin/Notifications
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                NotificationAPIVM apiModel = objAPI.GetRecordByQueryString<NotificationAPIVM>("configuration", "notification", query);
                NotificationVM model = new NotificationVM();
                model.Notificationlist = apiModel.Notification;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvNotificationsList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NotificationSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.notificationsave.NotificationImagePath = model.cropper.PhotoNormal;
                    string jsonStr = JsonConvert.SerializeObject(model.notificationsave);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savenotification", jsonStr);
                    return RedirectToAction("index", "notifications", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult Edit(long id)
        {
            try
            {
               NotificationSaveModel model = new NotificationSaveModel();
                model.notificationsave = objAPI.GetObjectByKey<utblLCNotification>("configuration", "notificationbyid", id.ToString(), "id");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NotificationSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.cropper.PhotoNormal != null)
                    {
                        model.notificationsave.NotificationImagePath = model.cropper.PhotoNormal;

                    }
                    else
                    {
                        model.notificationsave.NotificationImagePath = model.notificationsave.NotificationImagePath;
                    }
                    string jsonStr = JsonConvert.SerializeObject(model.notificationsave);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savenotification", jsonStr);
                    return RedirectToAction("index", "notifications", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteNotification", id.ToString(), "id");
            return RedirectToAction("index", "notifications", new { Area = "Admin" });
        }
    }
}