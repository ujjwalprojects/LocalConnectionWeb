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
    public class LCHotelOffersController : BaseController
    {
        // GET: Admin/LCHotelOffers
        ApiConnection objAPI = new ApiConnection("Admin");
       
        public ActionResult HotelOfferList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/lchoteloffers/HotelOfferList";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                HotelOfferAPIVM apiModel = objAPI.GetRecordByQueryString<HotelOfferAPIVM>("lchoteloffer", "HotelOfferList", query);
                HotelOfferVM model = new HotelOfferVM();
                model.HotelOfferList = apiModel.HotelOfferList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvHotelOfferList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddHotelOffer()
        {
            ViewBag.ActiveURL = "/Admin/Hotelofferlist";
            try
            {
                SaveHotelOffer model = new SaveHotelOffer();
                model.HotelList = objAPI.GetAllRecords<HotelDD>("lchotelconfig", "LCHotelDD");
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
        public ActionResult AddHotelOffer(SaveHotelOffer model)
        {
            try
            {
                if(ModelState.ContainsKey("HotelOffer.OfferImagePath"))
                ModelState["HotelOffer.OfferImagePath"].Errors.Clear();
                if (ModelState.IsValid)
                {
                    model.HotelOffer.OfferImagePath = model.cropper.PhotoNormal;
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("lchoteloffer", "SaveHotelOffer", jsonStr);
                    return RedirectToAction("HotelOfferList", "LCHotelOffers", new { Area = "Admin" });
                }
                model.HotelList = objAPI.GetAllRecords<HotelDD>("lchotelconfig", "LCHotelDD");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditHotelOffer(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Hotellist";
                SaveHotelOffer model = new SaveHotelOffer();
                utblLCFeatureOffer Hoteloffer = objAPI.GetObjectByKey<utblLCFeatureOffer>("lchoteloffer", "HotelOfferByID", id.ToString(), "OfferID");
                var OfferHotellist = objAPI.GetRecordsByQueryString<long>("lchoteloffer", "OfferHotellist", "id=" + id);
                model.HotelOffer = new HotelOffer()
                {
                    OfferID = Hoteloffer.OfferID,
                    OfferTagLine = Hoteloffer.OfferTagLine,
                    OfferImagePath = Hoteloffer.OfferImagePath,
                    OfferStartDate = Hoteloffer.OfferStartDate,
                    OfferEndDate = Hoteloffer.OfferEndDate,
                    HotelID = OfferHotellist
                };
                model.HotelList = objAPI.GetAllRecords<HotelDD>("lchotelconfig", "LCHotelDD");
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
        public ActionResult EditHotelOffer(SaveHotelOffer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.cropper.PhotoNormal != null)
                    {
                        model.HotelOffer.OfferImagePath = model.cropper.PhotoNormal;
                    }
                    else
                    {
                        model.HotelOffer.OfferImagePath = model.HotelOffer.OfferImagePath;
                    }
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("lchoteloffer", "SaveHotelOffer", jsonStr);
                    return RedirectToAction("HotelOfferList", "LCHotelOffers", new { Area = "Admin" });
                }
                utblLCFeatureOffer Hoteloffer = objAPI.GetObjectByKey<utblLCFeatureOffer>("lchoteloffer", "HotelOfferByID", model.HotelOffer.OfferID.ToString(), "OfferID");
                var OfferHotellist = objAPI.GetRecordsByQueryString<long>("lchoteloffer", "OfferHotellist", "id=" + model.HotelOffer.OfferID.ToString());
                model.HotelOffer = new HotelOffer()
                {
                    OfferID = Hoteloffer.OfferID,
                    OfferTagLine = Hoteloffer.OfferTagLine,
                    OfferImagePath = Hoteloffer.OfferImagePath,
                    OfferStartDate = Hoteloffer.OfferStartDate,
                    OfferEndDate = Hoteloffer.OfferEndDate,
                    HotelID = OfferHotellist
                };
                model.HotelList = objAPI.GetAllRecords<HotelDD>("lchotelconfig", "LCHotelDD");
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
        public ActionResult DeleteHotelOffer(long id)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("Hotelconfig", "deleteHoteloffer", id.ToString(), "HotelOfferID");
            return RedirectToAction("HotelOfferList", "LCHotelOffers", new { Area = "Admin" });
        }
    }
}