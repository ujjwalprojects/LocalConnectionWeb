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
    public class InclusionsController : BaseController
    {
        //
        // GET: /Admin/Inclusions/
        // GET: /Admin/Package/
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult InclusionList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Inclusionlist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                InclusionAPIVM apiModel = objAPI.GetRecordByQueryString<InclusionAPIVM>("configuration", "inclusionlist", query);
                InclusionVM model = new InclusionVM();
                model.InclusionList = apiModel.InclusionList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvInclusionList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddInclusion()
        {
            ViewBag.ActiveURL = "/Admin/Inclusionlist";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInclusion(InclusionSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Inclusion);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveinclusion", jsonStr);
                    return RedirectToAction("inclusionlist", "Inclusions", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditInclusion(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/inclusionlist";
                InclusionSave model = new InclusionSave();
                model.Inclusion = objAPI.GetObjectByKey<utblMstInclusion>("configuration", "inclusionbyid", id.ToString(), "InclusionID");
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
        public ActionResult EditInclusion(InclusionSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/inclusionlist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Inclusion);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveinclusion", jsonStr);
                    return RedirectToAction("inclusionlist", "Inclusions", new { Area = "Admin" });
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
        public ActionResult DeleteInclusion(long InclusionID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteinclusion", InclusionID.ToString(), "InclusionID");
            return RedirectToAction("InclusionList", "Inclusions", new { Area = "Admin" });
        }
    }
}