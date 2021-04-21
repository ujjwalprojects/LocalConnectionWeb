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
    public class LCNearByPointsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];

        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                LCNearByPointsAPIVM apiModel = objAPI.GetRecordByQueryString<LCNearByPointsAPIVM>("lchotelconfig", "GetNearByPoints", query);
                LCNearByPointsVM model = new LCNearByPointsVM();
                model.LCNearByPointView = apiModel.LCNearByPointView;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvnearbypointsList", model);
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
            try
            {
                LCNearByPointsSaveModel model = new LCNearByPointsSaveModel();
                model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
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
        public ActionResult Add(LCNearByPointsSaveModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.LCNearByPoints);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("lchotelconfig", "SaveNearByPoints", jsonStr);
                    return RedirectToAction("index", "LCNearByPoints", new { Area = "Admin" });
                }
                model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
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
                LCNearByPointsSaveModel model = new LCNearByPointsSaveModel();
                model.NearPoints = objAPI.GetObjectByKey<utblLCNearByPoint>("lchotelconfig", "NearByPointsByID", id.ToString(), "id");
                model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
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
        public ActionResult Edit(LCNearByPointsSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.LCNearByPoints);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("lchotelconfig", "SaveNearByPoints", jsonStr);
                    return RedirectToAction("index", "LCNearByPoints", new { Area = "Admin" });
                }
                model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("lchotelconfig", "DeleteNearByPoints", id.ToString(), "id");
            return RedirectToAction("index", "LCNearByPoints", new { Area = "Admin" });
        }

    }
}