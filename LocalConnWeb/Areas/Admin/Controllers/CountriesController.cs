using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class CountriesController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult Index()
        {
            var model = objAPI.GetAllRecords<CountriesModel>("configuration", "CountriesList");
            if (Request.IsAjaxRequest())
            {
                return PartialView("_pvCountriesList", model);
            }
            return View(model);
        }
        public ActionResult AddCountries()
        {
            ViewBag.ActiveURL = "/Admin/Countries";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountries(CountriesModel model)
        {
            ViewBag.ActiveURL = "/Admin/Countries";
            if (ModelState.IsValid)
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "addEditCountries", jsonStr);
                return RedirectToAction("index", "Countries", new { Area = "Admin" });
            }
            return View(model);
        }
        public ActionResult EditCountry(long id)
        {
            ViewBag.ActiveURL = "/Admin/Countries";
            CountriesModel model = objAPI.GetObjectByKey<CountriesModel>("configuration", "countriesbyid", id.ToString(), "id");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCountry(CountriesModel model)
        {
            ViewBag.ActiveURL = "/Admin/Countries";
            if (ModelState.IsValid)
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "addEditCountries", jsonStr);
                return RedirectToAction("index", "Countries", new { Area = "Admin" });
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deletecountries", id.ToString(), "id");
            return RedirectToAction("index", "Countries", new { Area = "Admin" });
        }
    }
}