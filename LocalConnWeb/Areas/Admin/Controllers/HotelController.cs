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
    public class HotelController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                HotelAPIVM apiModel = objAPI.GetRecordByQueryString<HotelAPIVM>("hotelconfig", "hotels", query);
                HotelVM model = new HotelVM();
                model.Hotels = apiModel.Hotels;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvHotelList", model);
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
                HotelManageModel model = new HotelManageModel();
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
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
        public ActionResult Add(HotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Hotel);
                    string result = objAPI.PostRecordtoApI("hotelconfig", "savehotel", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                        model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
                        return View(model);
                    }
                    return RedirectToAction("index", "hotel", new { Area = "admin" });
                }
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
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
                utblMstHotel hotel = objAPI.GetObjectByKey<utblMstHotel>("hotelconfig", "hotelbyid", id.ToString(), "id");
                var hoteltypes = objAPI.GetRecordsByQueryString<long>("hotelconfig", "HotelTypesOfHotel", "id=" + id);
                HotelManageModel model = new HotelManageModel();
                model.Hotel = new HotelSaveModel()
                {
                    HotelID = hotel.HotelID,
                    DestinationID = hotel.DestinationID,
                    HotelName = hotel.HotelName,
                    HotelAddress = hotel.HotelAddress,
                    HotelContact = hotel.HotelContact,
                    HotelEmail = hotel.HotelEmail,
                    HotelTypes = hoteltypes
                };
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
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
        public ActionResult Edit(HotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Hotel);
                    string result = objAPI.PostRecordtoApI("hotelconfig", "savehotel", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                        model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
                        return View(model);
                    }
                    return RedirectToAction("index", "hotel", new { Area = "admin" });
                }
                model.Destinations = objAPI.GetAllRecords<utblMstDestination>("configuration", "alldestinations");
                model.HotelTypes = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("hotelconfig", "deletehotel", id.ToString(), "id");
            return RedirectToAction("index", "hotel", new { Area = "Admin" });
        }
    }
}