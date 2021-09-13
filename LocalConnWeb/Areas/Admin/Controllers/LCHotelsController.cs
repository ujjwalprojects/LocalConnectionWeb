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
                //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
                    //return RedirectToAction("index", "lchotels", new { Area = "admin" });
                    return RedirectToAction("AddRoomType", new { @id = Convert.ToInt64(result) });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
                utblLCHotelLatLong lchotelLatLong = objAPI.GetObjectByKey<utblLCHotelLatLong>("lchotelconfig", "LCHotelLatLongByID", id.ToString(), "id");
                string latlong = "";
                if(lchotelLatLong != null)
                {
                    latlong=  lchotelLatLong.LatLong;
                }
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
                    MaxOccupant= lchotel.MaxOccupant,
                    MaxRooms = lchotel.MaxRooms,
                    OverallOfferPercentage=lchotel.OverallOfferPercentage,
                    TwoOccupantPercentage=lchotel.TwoOccupantPercentage,
                    ThreeOccupantPercentage=lchotel.ThreeOccupantPercentage,
                    FourPlusOccupantPercentage=lchotel.FourPlusOccupantPercentage,
                    ChildOccupantNote=lchotel.ChildOccupantNote,
                    LatLong = latlong,
                    IsActive=lchotel.IsActive,
                   
                };
                //model.RoomID = HotelRoomTypelist;
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
                        //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
                        return View(model);
                    }
                    //return RedirectToAction("index", "lchotels", new { Area = "admin" });
                    return RedirectToAction("AddRoomType", new { @id = Convert.ToInt64(result) });
                }
                model.CountryList = objAPI.GetAllRecords<CountryDD>("configuration", "CountriesList");
                model.HomeTypeList = objAPI.GetAllRecords<utblLCMstHomeType>("configuration", "AllHomeTypes");
                model.StarRatingList = objAPI.GetAllRecords<utblLCMstStarRating>("configuration", "AllStarRating");
                //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
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
        public ActionResult CalculateBasePrice(LCHotelManageModel model)
        {
            long id = model.LCHotel.HotelID;
            utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
            string query = "id=" + id;
            model.HotelRoomTypeMapView = objAPI.GetRecordsByQueryString<HotelRoomTypeMapView>("lchotelconfig", "GetHotelRoomTypeMapList", query);
            decimal BasePrice = 0, OfferPrice = 0, FinalPrice = 0, DefaultRoomTypePrice = 0;
            foreach (var item in model.HotelRoomTypeMapView)
            {
                if (item.IsStandard == true)
                {
                    DefaultRoomTypePrice = item.RoomTypePrice;
                }
            }
            //BasePrice = lchotel.RatePerRoom + lchotel.RatePerNight + lchotel.RatePerGuest + DefaultRoomTypePrice;
            //OfferPrice = (lchotel.OfferPercentage * BasePrice) / 100;
            //FinalPrice = BasePrice - OfferPrice;

            //model.LCHotel.HotelID = id;
            //model.LCHotel.HotelBaseFare = BasePrice;
            //model.LCHotel.HotelOfferPrice = FinalPrice;

            string jsonStr = JsonConvert.SerializeObject(model.LCHotel);
            string result = objAPI.PostRecordtoApI("lchotelconfig", "UpdateLCHotelRate", jsonStr);
            TempData["ErrMsg"] = result;
            if (!result.ToLower().Contains("error"))
            {
                //return RedirectToAction("Index", "lchotels");
                return RedirectToAction("CancellationAndTerms", new { id = model.LCHotel.HotelID });
            }
            return View(model);
        }
        //RoomType
        public ActionResult AddRoomType(long id)
        {
            LCHotelManageModel model = new LCHotelManageModel();
            utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
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
                MaxOccupant = lchotel.MaxOccupant,
                OverallOfferPercentage = lchotel.OverallOfferPercentage,
                TwoOccupantPercentage = lchotel.TwoOccupantPercentage,
                ThreeOccupantPercentage = lchotel.ThreeOccupantPercentage,
                FourPlusOccupantPercentage = lchotel.FourPlusOccupantPercentage,
                ChildOccupantNote = lchotel.ChildOccupantNote,
                IsActive = lchotel.IsActive,


            };
            //model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
            string query = "id=" + id;
            model.HotelRoomTypeMapView = objAPI.GetRecordsByQueryString<HotelRoomTypeMapView>("lchotelconfig", "GetHotelRoomTypeMapList", query);
            List<RoomsTypeDD> RTypelist = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
            foreach (var item in model.HotelRoomTypeMapView)
            {
                RTypelist.RemoveAll(x => x.RoomID == item.RoomID);
            }
            model.RoomsList = RTypelist.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoomType(LCHotelManageModel model)
        {
            try
            {
                    model.HotelRoomTypeMap.HotelID = model.LCHotel.HotelID;
                    string jsonStr = JsonConvert.SerializeObject(model.HotelRoomTypeMap);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveHotelRoomTypeMap", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", model.LCHotel.HotelID.ToString(), "id");
                        model.RoomsList = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
                        return View(model);
                    }
                
                return RedirectToAction("AddRoomType", new { id = model.LCHotel.HotelID });
                //return RedirectToAction("CancellationAndTerms", new { area = "LCHotels", id = model.LCHotel.HotelID });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditRoomType(long id, long rid)
        {
            try
            {
                LCHotelManageModel model = new LCHotelManageModel();
                string query = "id=" + id + "&rid=" + rid;
                model.HotelRoomTypeMap = objAPI.GetRecordByQueryString<HotelRoomTypeMap>("lchotelconfig", "GetHotelRoomTypeMapByID", query);
                string querylist = "id=" + id;
                model.HotelRoomTypeMapView = objAPI.GetRecordsByQueryString<HotelRoomTypeMapView>("lchotelconfig", "GetHotelRoomTypeMapList", querylist);
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
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
                    MaxOccupant = lchotel.MaxOccupant,
                    OverallOfferPercentage = lchotel.OverallOfferPercentage,
                    TwoOccupantPercentage = lchotel.TwoOccupantPercentage,
                    ThreeOccupantPercentage = lchotel.ThreeOccupantPercentage,
                    FourPlusOccupantPercentage = lchotel.FourPlusOccupantPercentage,
                    ChildOccupantNote = lchotel.ChildOccupantNote,
                    IsActive = lchotel.IsActive,


                };
                List<RoomsTypeDD> RTypelist = objAPI.GetAllRecords<RoomsTypeDD>("configuration", "RoomTypeDD");
                foreach (var item in model.HotelRoomTypeMapView)
                {
                    RTypelist.RemoveAll(x => x.RoomID == item.RoomID && x.RoomID != rid);
                }
                model.RoomsList = RTypelist.ToList();
                return View("AddRoomType", model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            //return View();
        }
        public ActionResult DeleteRoomType(long hid, long rid)
        {
            string query = "id=" + hid + "&rid=" + rid;
            TempData["ErrMsg"] = objAPI.DeleteRecordByQuerystring("lchotelconfig", "DeleteHotelRoomTypeMap", query);
            return RedirectToAction("AddRoomType", new { id = hid });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("lchotelconfig", "DeleteLCHotel", id.ToString(), "id");
            return RedirectToAction("index", "lchotels", new { Area = "Admin" });
        }
        public ActionResult CancellationAndTerms(long id)
        {
            try
            {
                LCHotelManageModel model = new LCHotelManageModel();
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
                model.HotelInfo = new HotelBriefInfo()
                {
                    HotelID = lchotel.HotelID,
                    HotelName = lchotel.HotelName,
                };
                model.Terms = objAPI.GetRecordsByID<HotelTerms>("lchotelconfig", "HotelTerms", id);
                model.Cancellations = objAPI.GetRecordsByID<HotelCancellations>("lchotelconfig", "HotelCancellations", id);
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
        public ActionResult CancellationAndTerms(LCHotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LCHotelManageModel sendModel = new LCHotelManageModel();

                    if (model.Terms == null)
                        sendModel.Terms = new List<HotelTerms>();
                    else
                        sendModel.Terms = model.Terms.Where(x => x.IsSelected).ToList();
                    if (model.Cancellations == null)
                        sendModel.Cancellations = new List<HotelCancellations>();
                    else
                        sendModel.Cancellations = model.Cancellations.Where(x => x.IsSelected).ToList();
                    sendModel.HotelID = model.HotelInfo.HotelID;

                    string jsonStr = JsonConvert.SerializeObject(sendModel);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveTermsCancellations", jsonStr);
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "Tour Package Details Saved";
                        //return RedirectToAction("index");
                        return RedirectToAction("AddAmenitiesMap", new { id = model.HotelInfo.HotelID });
                    }
                    TempData["ErrMsg"] = result;
                }
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", model.HotelInfo.HotelID.ToString(), "id");
                model.HotelInfo = new HotelBriefInfo()
                {
                    HotelID = lchotel.HotelID,
                    HotelName = lchotel.HotelName,
                };
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        //NearBy
        public ActionResult AddNearBys(long id)
        {
            LCHotelManageModel model = new LCHotelManageModel();
            utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
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
                MaxOccupant = lchotel.MaxOccupant,
                OverallOfferPercentage = lchotel.OverallOfferPercentage,
                TwoOccupantPercentage = lchotel.TwoOccupantPercentage,
                ThreeOccupantPercentage = lchotel.ThreeOccupantPercentage,
                FourPlusOccupantPercentage = lchotel.FourPlusOccupantPercentage,
                ChildOccupantNote = lchotel.ChildOccupantNote,
                IsActive = lchotel.IsActive,
            };
            string query = "id=" + id;
            model.LCNearByPointsView = objAPI.GetRecordsByQueryString<LCNearByPointsView>("lchotelconfig", "GetLCNearByPointsMapList", query);
            model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNearBys(LCHotelManageModel model)
        {
            try
            {
                model.LCNearByPoints.HotelID = model.LCHotel.HotelID;
                string jsonStr = JsonConvert.SerializeObject(model.LCNearByPoints);
                string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveNearByPoints", jsonStr);
                TempData["ErrMsg"] = result;
                if (result.ToLower().Contains("error"))
                {
                    utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", model.LCHotel.HotelID.ToString(), "id");
                    string query = "id=" + model.LCHotel.HotelID;
                    model.LCNearByPointsView = objAPI.GetRecordsByQueryString<LCNearByPointsView>("lchotelconfig", "GetLCNearByPointsMapList", query);
                    model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
                    return View(model);
                }
                return RedirectToAction("AddNearBys", new { id = model.LCHotel.HotelID });
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });

            }
        }
        public ActionResult EditNearBys(long id, long nid)
        {
            try
            {
                LCHotelManageModel model = new LCHotelManageModel();
                string query = "id=" + nid;
                model.NearByPoints = objAPI.GetRecordByQueryString<utblLCNearByPoint>("lchotelconfig", "NearByPointsByID", query);
                model.LCNearByPoints = new LCNearByPoints()
                {
                    NearbyPointsID = model.NearByPoints.NearbyPointsID,
                    NearByID = model.NearByPoints.NearByID,
                    HotelID = model.NearByPoints.HotelID,
                    NearByPoints = model.NearByPoints.NearByPoints,
                    NearByDistance = model.NearByPoints.NearByDistance
                };
                model.LCNearByPointsDD = objAPI.GetAllRecords<LCNearBysTypeDD>("lchotelconfig", "NearByDD");
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
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
                    MaxOccupant = lchotel.MaxOccupant,
                    OverallOfferPercentage = lchotel.OverallOfferPercentage,
                    TwoOccupantPercentage = lchotel.TwoOccupantPercentage,
                    ThreeOccupantPercentage = lchotel.ThreeOccupantPercentage,
                    FourPlusOccupantPercentage = lchotel.FourPlusOccupantPercentage,
                    ChildOccupantNote = lchotel.ChildOccupantNote,
                    IsActive = lchotel.IsActive,


                };
                string _query = "id=" + id;
                model.LCNearByPointsView = objAPI.GetRecordsByQueryString<LCNearByPointsView>("lchotelconfig", "GetLCNearByPointsMapList", _query);
                return View("AddNearBys", model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            //return View();
        }
        public ActionResult DeleteNearBys(long id, long hid)
        {
            string query = "id=" + id;
            TempData["ErrMsg"] = objAPI.DeleteRecordByQuerystring("lchotelconfig", "DeleteNearByPoints" , query);
               
            return RedirectToAction("AddNearBys", new { id = hid });
        }
        //Amenities
        public ActionResult AddAmenitiesMap(long id)
        {
            LCHotelManageModel model = new LCHotelManageModel();
            utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
            model.HotelInfo = new HotelBriefInfo()
            {
                HotelID = lchotel.HotelID,
                HotelName = lchotel.HotelName,
            };
           
            model.HotelAmenitiesMapView = objAPI.GetRecordsByID<HotelAmenitiesMapView>("lchotelconfig", "HotelAmenities", id);
            //objAPI.GetRecordsByQueryString<HotelAmenitiesMapView>("lchotelconfig", "GetHotelAmenitiesMapList", query);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAmenitiesMap(LCHotelManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LCHotelManageModel sendModel = new LCHotelManageModel();
                    if (model.HotelAmenitiesMapView == null)
                        sendModel.HotelAmenitiesMapView = new List<HotelAmenitiesMapView>();
                    else
                        sendModel.HotelAmenitiesMapView = model.HotelAmenitiesMapView.Where(x => x.IsSelected).ToList();
                    sendModel.HotelID = model.HotelInfo.HotelID;
                    string jsonStr = JsonConvert.SerializeObject(sendModel);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveHotelAmenitiesMap", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (!result.ToLower().Contains("error"))
                    {
                        TempData["ErrMsg"] = "record Saved";
                        return RedirectToAction("index");
                    }
                    TempData["ErrMsg"] = result;
                }
                utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", model.LCHotel.HotelID.ToString(), "id");
                model.HotelInfo = new HotelBriefInfo()
                {
                    HotelID = lchotel.HotelID,
                    HotelName = lchotel.HotelName,
                };
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        //public ActionResult EditAmenities(long id, long haid)
        //{
        //    try
        //    {
        //        LCHotelManageModel model = new LCHotelManageModel();
        //        string query = "id=" + haid;
        //        model.HotelRoomTypeMap = objAPI.GetRecordByQueryString<HotelRoomTypeMap>("lchotelconfig", "GetHotelAmenitiesMapByID", query);
        //        string querylist = "id=" + id;
        //        model.HotelAmenitiesMapView = objAPI.GetRecordsByQueryString<HotelAmenitiesMapView>("lchotelconfig", "GetHotelAmenitiesMapList", querylist);
        //        utblLCHotel lchotel = objAPI.GetObjectByKey<utblLCHotel>("lchotelconfig", "lchotelbyid", id.ToString(), "id");
        //        model.LCHotel = new LCHotelSaveModel()
        //        {
        //            HotelID = lchotel.HotelID,
        //            HotelName = lchotel.HotelName,
        //            HotelAddress = lchotel.HotelAddress,
        //            HotelDesc = lchotel.HotelDesc,
        //            HotelContactNo = lchotel.HotelContactNo,
        //            HotelEmail = lchotel.HotelEmail,
        //            CountryID = lchotel.CountryID,
        //            StateID = lchotel.StateID,
        //            CityID = lchotel.CityID,
        //            LocalityID = lchotel.LocalityID,
        //            HomeTypeID = lchotel.HomeTypeID,
        //            StarRatingID = lchotel.StarRatingID,
        //            MaxOccupant = lchotel.MaxOccupant,
        //            OverallOfferPercentage = lchotel.OverallOfferPercentage,
        //            TwoOccupantPercentage = lchotel.TwoOccupantPercentage,
        //            ThreeOccupantPercentage = lchotel.ThreeOccupantPercentage,
        //            FourPlusOccupantPercentage = lchotel.FourPlusOccupantPercentage,
        //            ChildOccupantNote = lchotel.ChildOccupantNote,
        //            IsActive = lchotel.IsActive,


        //        };
        //        List<AmenitiesDD> Amelist = objAPI.GetAllRecords<AmenitiesDD>("configuration", "AmenitiesDD");
        //        foreach (var item in model.HotelAmenitiesMapView)
        //        {
        //            Amelist.RemoveAll(x => x.AmenitiesID == item.AmenitiesID);
        //        }
        //        model.AmenitiesDD = Amelist.ToList();
        //        return View("AddAmenities", model);
        //    }
        //    catch (AuthorizationException)
        //    {
        //        TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    //return View();
        //}
        //public ActionResult DeleteAmenities(long haid)
        //{
        //    string query = "id=" + haid ;
        //    TempData["ErrMsg"] = objAPI.DeleteRecordByQuerystring("lchotelconfig", "DeleteHotelAmenitiesMap", query);
        //    return RedirectToAction("AddAmenities", new { id = haid });
        //}


        //Customer Booking details
        public ActionResult LcCustBookingList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                LCCustomerBookingAPIVM apiModel = objAPI.GetRecordByQueryString<LCCustomerBookingAPIVM>("lchotelconfig", "LCCustBookingDtl", query);
                LCCustomerBookingVM model = new LCCustomerBookingVM();
                model.LCCustomerBookingView = apiModel.LCCustomerBookingView;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvLCCustBookingList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult LCCustBookingDtl(string id)
        {
            LCCustomerBookingView model = new LCCustomerBookingView();
            model = objAPI.GetObjectByKey<LCCustomerBookingView>("lchotelconfig", "LCCustBookingByID", id.ToString(), "id");
            return View(model);
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