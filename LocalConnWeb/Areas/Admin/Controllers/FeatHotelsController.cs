using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class FeatHotelsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        // GET: Admin/FeatHotels
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                FeatHotelsAPIVM apiModel = objAPI.GetRecordByQueryString<FeatHotelsAPIVM>("lchotelconfig", "FeatHotels", query);
                FeatHotelsVM model = new FeatHotelsVM();
                model.FeatHotels = apiModel.FeatHotels;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvFeatHotelsList", model);
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
                FeatHotelsSaveModel model = new FeatHotelsSaveModel();
                model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
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
        public ActionResult Add(FeatHotelsSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.FeatHotels);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveFeatHotels", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "feathotels", new { Area = "admin" });
                }
                model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
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
                FeatHotelsSaveModel model = new FeatHotelsSaveModel();
                model.FeatHotels = objAPI.GetObjectByKey<utblLCFeaturedHotel>("configuration", "citiesbyid", id.ToString(), "id");
                model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
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
        public ActionResult Edit(FeatHotelsSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.FeatHotels);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveFeatHotels", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "feathotels", new { Area = "admin" });
                }
                model.LCHotelDD = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelsDD");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("lchotelconfig", "DeleteFeatHotels", id.ToString(), "id");
            return RedirectToAction("index", "feathotels", new { Area = "Admin" });
        }
    }
}