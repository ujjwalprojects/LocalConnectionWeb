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
    [Authorize]
    public class LocalitiesController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        //
        // GET: /Admin/Localities/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                LocalitiesAPIVM apiModel = objAPI.GetRecordByQueryString<LocalitiesAPIVM>("configuration", "localities", query);
                LocalitiesVM model = new LocalitiesVM();
                model.Localities = apiModel.Localities;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvLocalitiesList", model);
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
                LocalitiesSaveModel model = new LocalitiesSaveModel();
                model.CitiesList = objAPI.GetAllRecords<CitiesDD>("configuration", "CitiesList");
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
        public ActionResult Add(LocalitiesSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Localities);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveLocalities", jsonStr);
                    return RedirectToAction("index", "localities", new { Area = "Admin" });
                }
                model.CitiesList = objAPI.GetAllRecords<CitiesDD>("configuration", "CitiesList");
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
                LocalitiesSaveModel model = new LocalitiesSaveModel();
                model.Localities = objAPI.GetObjectByKey<utblLCMstLocalitie>("configuration", "LocalitiesByID", id.ToString(), "id");
                model.CitiesList = objAPI.GetAllRecords<CitiesDD>("configuration", "CitiesList");
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
        public ActionResult Edit(LocalitiesSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Localities);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveLocalities", jsonStr);
                    return RedirectToAction("index", "localities", new { Area = "Admin" });
                }
                model.CitiesList = objAPI.GetAllRecords<CitiesDD>("configuration", "CitiesList");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "DeleteLocalities", id.ToString(), "id");
            return RedirectToAction("index", "localities", new { Area = "Admin" });
        }
	}
}