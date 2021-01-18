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
    public class LCHotelImgController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        // GET: Admin/LCHotelImg
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                LCHotelImageAPIVM apiModel = objAPI.GetRecordByQueryString<LCHotelImageAPIVM>("lchotelconfig", "LCHotelImages", query);
                LCHotelImageVM model = new LCHotelImageVM();
                model.LCHotelImageList = apiModel.LCHotelImageList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvHotelImageList", model);
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
                LCHotelImageManageModel model = new LCHotelImageManageModel();
                model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
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
        public ActionResult Add(LCHotelImageManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.LCHotelImage);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveLCHotelImages", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "LCHotelImg", new { Area = "admin" });
                }
                model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
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
                LCHotelImageManageModel model = new LCHotelImageManageModel();
                utblLCHotelImage lchotel = objAPI.GetObjectByKey<utblLCHotelImage>("lchotelconfig", "LCHotelImagesByID", id.ToString(), "id");
                model.LCHotelImage = new LCHotelImageSaveModel()
                {
                    HotelImageID = lchotel.HotelImageID,
                    HotelID = lchotel.HotelID,
                    IsHotelCover = lchotel.IsHotelCover,
                    PhotoNormalPath = lchotel.PhotoNormalPath,
                    PhotoThumbPath = lchotel.PhotoThumbPath,
                    PhotoCaption = lchotel.PhotoCaption
                    
                };
                model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
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
        public ActionResult Edit(LCHotelImageManageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.LCHotelImage);
                    string result = objAPI.PostRecordtoApI("lchotelconfig", "SaveLCHotelImages", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
                        return View(model);
                    }
                    return RedirectToAction("index", "LCHotelImg", new { Area = "admin" });
                }
                model.HotelList = objAPI.GetAllRecords<LCHotelDD>("lchotelconfig", "LCHotelDD");
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("lchotelconfig", "DeleteLCHotelImages", id.ToString(), "id");
            return RedirectToAction("index", "LCHotelImg", new { Area = "Admin" });
        }
    }
}