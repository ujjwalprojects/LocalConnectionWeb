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
    public class TourCancelController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult TourCancelList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Tourcancellist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                TourCancelAPIVM apiModel = objAPI.GetRecordByQueryString<TourCancelAPIVM>("configuration", "tourcancellist", query);
                TourCancelVM model = new TourCancelVM();
                model.tourCancelList = apiModel.tourCancelList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvTourCancelList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddTourCancel()
        {
            ViewBag.ActiveURL = "/Admin/Tourcancellist";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTourCancel(TourCancelSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Tourcancellist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.saveTourCancel);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savetourcancel", jsonStr);
                    return RedirectToAction("TourCancelList", "TourCancel", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditTourCancel(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Tourcancellist";
                TourCancelSave model = new TourCancelSave();
                model.saveTourCancel = objAPI.GetObjectByKey<utblMstTourCancellation>("configuration", "tourcancelbyid", id.ToString(), "CancellationID");
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
        public ActionResult EditTourCancel(TourCancelSave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Tourcancellist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.saveTourCancel);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savetourcancel", jsonStr);
                    return RedirectToAction("TourCancelList", "TourCancel", new { Area = "Admin" });
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
        public ActionResult DeleteTourCancel(long CancellationID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deletetourcancel", CancellationID.ToString(), "CancellationID");
            return RedirectToAction("TourCancelList", "TourCancel", new { Area = "Admin" });
        }
    }
}
