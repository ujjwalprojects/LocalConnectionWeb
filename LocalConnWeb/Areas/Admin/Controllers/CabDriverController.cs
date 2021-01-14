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
    public class CabDriverController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        //
        // GET: /Admin/Cab/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                CabDriverAPIVM apiModel = objAPI.GetRecordByQueryString<CabDriverAPIVM>("cabconfig", "drivers", query);
                CabDriverVM model = new CabDriverVM();
                model.Drivers = apiModel.Drivers;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvCabDriverList", model);
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
        public ActionResult Add(utblMstCabDriver model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabdriver";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savedriver", jsonStr);
                    return RedirectToAction("index", "cabdriver", new { Area = "Admin" });
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
                ViewBag.ActiveURL = "/Admin/cabdriver";
                utblMstCabDriver model = objAPI.GetObjectByKey<utblMstCabDriver>("cabconfig", "driverbyid", id.ToString(), "id");
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
        public ActionResult Edit(utblMstCabDriver model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabdriver";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savedriver", jsonStr);
                    return RedirectToAction("index", "cabdriver", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("cabconfig", "deletedriver", id.ToString(), "id");
            return RedirectToAction("index", "cabdriver", new { Area = "Admin" });
        }
    }
}