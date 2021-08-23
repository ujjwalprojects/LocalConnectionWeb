using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class CitiesController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        //
        // GET: /Admin/Cities/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                CitiesAPIVM apiModel = objAPI.GetRecordByQueryString<CitiesAPIVM>("configuration", "cities", query);
                CitiesVM model = new CitiesVM();
                model.Cities = apiModel.Cities;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvCitiesList", model);
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
                CitiesSaveModel model = new CitiesSaveModel();
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
        public ActionResult Add(CitiesSaveModel model)
        {
            try
            {
                //Random rand = new Random();
                //string name = "City_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                //bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                //if (!isValidFile)
                //{
                //    TempData["ErrMsg"] = "Please Select A Valid Image File!";
                //    return View(model);
                //}
                //string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                //if (normal_result.Contains("Error"))
                //{
                //    TempData["ErrMsg"] = normal_result;
                //    return RedirectToAction("index", "cities", new { Area = "Admin" });
                //}
                //else
                //{
                //    model.Cities.CityIconPath = "/Uploads/Cities/" + normal_result;
                //}

                if (ModelState.IsValid)
                { 
                    model.Cities.CityIconPath = model.cropper.PhotoNormal;
                    string jsonStr = JsonConvert.SerializeObject(model.Cities);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "savecities", jsonStr);
                    return RedirectToAction("index", "cities", new { Area = "Admin" });
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
                CitiesSaveModel model = new CitiesSaveModel();
                model.Cities = objAPI.GetObjectByKey<utblLCMstCitie>("configuration", "citiesbyid", id.ToString(), "id");
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
        public ActionResult Edit(CitiesSaveModel model)
        {
            try
            {
                //if (model.cropper.PhotoNormal != null)
                //{
                //    bool isValidFile = FileTypeCheck.DataImage(model.cropper.PhotoNormal);
                //    //bool isValidThumb = FileTypeCheck.DataImage(model.cropper.PhotoThumb);
                //    if (!isValidFile)
                //    {
                //        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                //        return View();
                //    }
                //    Random rand = new Random();
                //    string name = "Banner_" + DateTime.Now.ToString("yyyyMMdd") + "_" + rand.Next(50) + ".webp";
                //    string normal_result = SaveImage(model.cropper.PhotoNormal, name);
                //    if (normal_result.Contains("Error"))
                //    {
                //        TempData["ErrMsg"] = "Please Select A Valid Image File!";
                //        return View();
                //    }
                //    else
                //    {
                //        model.Cities.CityIconPath = "/Uploads/Cities/" + normal_result;
                //    }
                //}
                //else
                //{
                //    model.Cities.CityIconPath = model.Cities.CityIconPath;
                //}
                if (ModelState.IsValid)
                {
                    if(model.cropper.PhotoNormal != null)
                    {
                        model.Cities.CityIconPath = model.cropper.PhotoNormal;
                       
                    }
                    else
                    {
                        model.Cities.CityIconPath = model.Cities.CityIconPath;
                    }
                    string jsonStr = JsonConvert.SerializeObject(model.Cities);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveCities", jsonStr);
                    return RedirectToAction("index", "Cities", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteCities", id.ToString(), "id");
            return RedirectToAction("index", "Cities", new { Area = "Admin" });
        }
        public JsonResult GetStates(long id)
        {
            var model = objAPI.GetRecordsByID<StateDD>("configuration", "statebycountry", id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        private string SaveImage(string imageStr, string name)
        {

            try
            {
                var path = ""; var folderpath = "";
                path = Path.Combine(Server.MapPath("~/Uploads/Cities"), name);
                folderpath = Server.MapPath("~/Uploads/Cities");

                //Check if directory exist
                if (!System.IO.Directory.Exists(folderpath))
                {
                    System.IO.Directory.CreateDirectory(folderpath); //Create directory if it doesn't exist
                }
                string x = imageStr.Replace("data:image/jpeg;base64,", "");
                byte[] imageBytes = Convert.FromBase64String(x);

                System.IO.File.WriteAllBytes(path, imageBytes);
                return name;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
	}
}