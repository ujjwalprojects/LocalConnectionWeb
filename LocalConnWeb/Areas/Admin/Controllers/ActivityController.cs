using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using Newtonsoft.Json;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    public class ActivityController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");

        public ActionResult ActivityList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/ActivityList";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                ActivityAPIVM apiModel = objAPI.GetRecordByQueryString<ActivityAPIVM>("configuration", "activitylist", query);
                ActivityVM model = new ActivityVM();
                model.ActivityList = apiModel.ActivityList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvActivityList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult AddActivity()
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Activitylist";
                ActivityManageModel model = new ActivityManageModel();
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
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
        public ActionResult AddActivity(ActivityManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Activity);
                    string result = objAPI.PostRecordtoApI("configuration", "saveactivity", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                        return View(model);
                    }
                    return RedirectToAction("activitylist", "activity", new { Area = "admin" });
                }
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditActivity(long id)
        {
            try
            {
                utblMstActivitie modelapi = objAPI.GetObjectByKey<utblMstActivitie>("configuration", "activitybyid", id.ToString(), "id");
                ActivityManageModel model = new ActivityManageModel();
                model.Activity = new ActivitySaveModel()
                {
                    ActivityID = modelapi.ActivityID,
                    ActivityName = modelapi.ActivityName,
                    ActivityDesc = modelapi.ActivityDesc,
                    DestinationID = modelapi.DestinationID,
                    BaseFare = modelapi.BaseFare
                };
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
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
        public ActionResult EditActivity(ActivityManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Activity);
                    string result = objAPI.PostRecordtoApI("configuration", "saveactivity", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                        return View(model);
                    }
                    return RedirectToAction("activitylist", "activity", new { Area = "admin" });
                }
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
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
        public ActionResult DeleteActivity(long ActivityID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteactivity", ActivityID.ToString(), "ActivityID");
            return RedirectToAction("ActivityList", "Activity", new { Area = "Admin" });
        }
	}
}