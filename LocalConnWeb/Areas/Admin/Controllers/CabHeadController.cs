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
    public class CabHeadController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        //
        // GET: /Admin/Cab/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                CabHeadAPIVM apiModel = objAPI.GetRecordByQueryString<CabHeadAPIVM>("cabconfig", "cabheads", query);
                CabHeadVM model = new CabHeadVM();
                model.CabHeads = apiModel.CabHeads;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvCabHeadList", model);
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
        public ActionResult Add(utblMstCabHead model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabhead";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecabhead", jsonStr);
                    return RedirectToAction("index", "cabhead", new { Area = "Admin" });
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
                ViewBag.ActiveURL = "/Admin/cabhead";
                utblMstCabHead model = objAPI.GetObjectByKey<utblMstCabHead>("cabconfig", "cabheadbyid", id.ToString(), "id");
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
        public ActionResult Edit(utblMstCabHead model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabhead";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecabhead", jsonStr);
                    return RedirectToAction("index", "cabhead", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("cabconfig", "deletecabhead", id.ToString(), "id");
            return RedirectToAction("index", "cabhead", new { Area = "Admin" });
        }
	}
}