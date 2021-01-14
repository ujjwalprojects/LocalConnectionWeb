using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class ExclusionController : BaseController
    {
        // GET: Admin/Exclusion
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult ExclusionList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/ExclusionList";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                ExclusionAPIVM apiModel = objAPI.GetRecordByQueryString<ExclusionAPIVM>("configuration", "exclusionlist", query);
                ExclusionVM model = new ExclusionVM();
                model.ExclusionList = apiModel.ExclusionList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvExclusionList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddExclusion()
        {
            ViewBag.ActiveURL = "/Admin/ExclusionList";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExclusion(ExclusionSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/ExclusionList";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Exclusion);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveexclusion", jsonStr);
                    return RedirectToAction("exclusionlist", "exclusion", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditExclusion(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/ExclusionList";
                ExclusionSave model = new ExclusionSave();
                model.Exclusion = objAPI.GetObjectByKey<utblMstExclusion>("configuration", "exclusionbyid", id.ToString(), "ExclusionID");
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
        public ActionResult EditExclusion(ExclusionSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/ExclusionList";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Exclusion);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveexclusion", jsonStr);
                    return RedirectToAction("exclusionlist", "exclusion", new { Area = "Admin" });
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
        public ActionResult DeleteExclusion(long ExclusionID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteexclusion", ExclusionID.ToString(), "ExclusionID");
            return RedirectToAction("exclusionlist", "exclusion", new { Area = "Admin" });
        }
    }
}