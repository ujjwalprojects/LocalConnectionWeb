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
    public class LCHotelsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        //
        // GET: /Admin/LCHotels/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                LCHotelAPIVM apiModel = objAPI.GetRecordByQueryString<LCHotelAPIVM>("lchotelconfig", "LCHotels", query);
                LCHotelVM model = new LCHotelVM();
                model.LCHotels = apiModel.LCHotels;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvLCHotelList", model);
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
                LCHotelManageModel model = new LCHotelManageModel();
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
        public ActionResult Add(LCHotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveLCHotel", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                        model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                        model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                        model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "lchotels", new { Area = "admin" });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
                LCHotelManageModel model = new LCHotelManageModel();
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
                var HotelRoomTypelist = objAPI.GetRecordsByQueryString<long>("configuration", "HotelRoomTypeList", "id=" + id);
                model.LCHotel = new LCHotelSaveModel()
                {
                    HotelID = lchotel.HotelID,
                    HotelName = lchotel.HotelName,
                    HotelAddress = lchotel.HotelAddress,
                    HotelDesc = lchotel.HotelDesc,
                    HotelContactNo = lchotel.HotelContactNo,
                    HotelEmail = lchotel.HotelEmail,
                    CountryID = lchotel.CountryID,
                    StateID = lchotel.StateID,
                    CityID = lchotel.CityID,
                    LocalityID = lchotel.LocalityID,
                    HomeTypeID = lchotel.HomeTypeID,
                    StarRatingID = lchotel.StarRatingID,
                    HotelBaseFare = lchotel.HotelBaseFare,
                    HotelOfferPrice = lchotel.HotelOfferPrice,
                    OfferPercentage = lchotel.OfferPercentage,
                    //HotelHitCount = lchotel.HotelHitCount,
                    MetaText = lchotel.MetaText,
                    TotalSingleRooms = lchotel.TotalSingleRooms,
                    TotalDoubleRooms = lchotel.TotalDoubleRooms,
                   

                };
                model.RoomID = HotelRoomTypelist;
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
        public ActionResult Edit(LCHotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveLCHotel", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                        model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                        model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                        model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "lchotels", new { Area = "admin" });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("lchotelconfig", "DeleteLCHotel", id.ToString(), "id");
            return RedirectToAction("index", "lchotels", new { Area = "Admin" });
        }
        public JsonResult GetStates(long id)
        {
            var model = objAPI.GetRecordsByID<StateDD>("configuration", "statebycountry", id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCities(long id)
        {
            var model = objAPI.GetRecordsByID<CitiesDD>("configuration", "CitiesByState", id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocalities(long id)
        {
            var model = objAPI.GetRecordsByID<LocalitiesDD>("configuration", "LocalitiesByCity", id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
	}
}