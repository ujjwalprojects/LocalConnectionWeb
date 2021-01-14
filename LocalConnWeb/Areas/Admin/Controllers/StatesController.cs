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
    public class StatesController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");

        public ActionResult Index()
        {
            try
            {
                var model = objAPI.GetAllRecords<StateView>("configuration", "statelist");
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvStateList", model);
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
                ViewBag.ActiveURL = "/Admin/states";
                StateSaveModel model = new StateSaveModel();
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
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
        public ActionResult Add(StateSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/states";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.State);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savestate", jsonStr);
                    return RedirectToAction("index", "states", new { Area = "Admin" });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
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
                ViewBag.ActiveURL = "/Admin/states";
                StateSaveModel model = new StateSaveModel();
                model.State = objAPI.GetObjectByKey<utblMstState>("configuration", "statebyid", id.ToString(), "id");
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
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
        public ActionResult Edit(StateSaveModel model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/states";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.State);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savestate", jsonStr);
                    return RedirectToAction("index", "states", new { Area = "Admin" });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deletestate", id.ToString(), "id");
            return RedirectToAction("index", "states", new { Area = "Admin" });
        }
    }
}