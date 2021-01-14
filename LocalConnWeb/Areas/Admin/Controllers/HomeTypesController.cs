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
    public class HomeTypesController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        //
        // GET: /Admin/HomeTypes/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                HomeTypeAPIVM apiModel = objAPI.GetRecordByQueryString<HomeTypeAPIVM>("configuration", "HomeTypes", query);
                HomeTypeVM model = new HomeTypeVM();
                model.HomeTypes = apiModel.HomeTypes;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvHomeTypesList", model);
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
        public ActionResult Add(HomeTypeSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.HomeTypes);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveHomeTypes", jsonStr);
                    return RedirectToAction("index", "HomeTypes", new { Area = "Admin" });
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
                HomeTypeSaveModel model = new HomeTypeSaveModel();
                model.HomeTypes = objAPI.GetObjectByKey<utblLCMstHomeType>("configuration", "HomeTypesByID", id.ToString(), "id");
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
        public ActionResult Edit(HomeTypeSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.HomeTypes);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveHomeTypes", jsonStr);
                    return RedirectToAction("index", "HomeTypes", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "DeleteHomeTypes", id.ToString(), "id");
            return RedirectToAction("index", "HomeTypes", new { Area = "Admin" });
        }
	}
}