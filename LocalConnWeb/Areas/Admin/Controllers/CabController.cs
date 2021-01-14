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
    public class CabController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        //
        // GET: /Admin/Cab/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                CabAPIVM apiModel = objAPI.GetRecordByQueryString<CabAPIVM>("cabconfig", "cabs", query);
                CabVM model = new CabVM();
                model.Cabs = apiModel.Cabs;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvCabList", model);
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
                ViewBag.ActiveURL = "/Admin/cab";
                CabSaveModel model = new CabSaveModel();
                model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("cabconfig", "cabtypes");
                model.CabHeads = objAPI.GetAllRecords<utblMstCabHead>("cabconfig", "allcabheads");
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
        public ActionResult Add(CabSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cab";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Cab);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecab", jsonStr);
                    return RedirectToAction("index", "cab", new { Area = "Admin" });
                }
                model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("cabconfig", "cabtypes");
                model.CabHeads = objAPI.GetAllRecords<utblMstCabHead>("cabconfig", "allcabheads");
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
                ViewBag.ActiveURL = "/Admin/cab";
                CabSaveModel model = new CabSaveModel();
                model.Cab = objAPI.GetObjectByKey<utblMstCab>("cabconfig", "cabbyid", id.ToString(), "id");
                model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("cabconfig", "cabtypes");
                model.CabHeads = objAPI.GetAllRecords<utblMstCabHead>("cabconfig", "allcabheads");
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
        public ActionResult Edit(CabSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cab";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Cab);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecab", jsonStr);
                    return RedirectToAction("index", "cab", new { Area = "Admin" });
                }
                model.CabTypes = objAPI.GetAllRecords<utblMstCabType>("cabconfig", "cabtypes");
                model.CabHeads = objAPI.GetAllRecords<utblMstCabHead>("cabconfig", "allcabheads");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("cabconfig", "deletecab", id.ToString(), "id");
            return RedirectToAction("index", "cab", new { Area = "Admin" });
        }
    }
}