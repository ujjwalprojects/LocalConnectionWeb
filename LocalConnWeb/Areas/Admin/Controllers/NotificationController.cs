using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        // GET: Admin/Notification
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                NotificationAPIVM apiModel = objAPI.GetRecordByQueryString<NotificationAPIVM>("configuration", "Notification", query);
                NotificationVM model = new NotificationVM();
                model.Notificationlist = apiModel.Notification;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvNotificationList", model);
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(NotificationSaveModel model)
        //{
        //    try
        //    {
        //        Random rand = new Random();
        //        string name = "Notification_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
        //        bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
        //        if (!isValidFile)
        //        {
        //            TempData["ErrMsg"] = "Please Select A Valid Image File!";
        //            return View(model);
        //        }
        //        string normal_result = SaveImage(model.cropper.PhotoNormal, name);
        //        if (normal_result.Contains("Error"))
        //        {
        //            TempData["ErrMsg"] = normal_result;
        //            return RedirectToAction("index", "cities", new { Area = "Admin" });
        //        }
        //        else
        //        {
        //            model.Cities.CityIconPath = "/Uploads/Cities/" + normal_result;
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            string jsonStr = JsonConvert.SerializeObject(model.Cities);
        //            TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savecities", jsonStr);
        //            return RedirectToAction("index", "cities", new { Area = "Admin" });
        //        }
        //        model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
        //        return View(model);
        //    }
        //    catch (AuthorizationException)
        //    {
        //        TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    //}
        //}
}