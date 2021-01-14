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
    public class HotelTypeController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        //
        // GET: /Admin/HotelType/
        public ActionResult Index()
        {
            try
            {
                var model = objAPI.GetAllRecords<utblMstHotelType>("hotelconfig", "hoteltypes");
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvHotelTypeList", model);
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
            ViewBag.ActiveURL = "/Admin/hoteltype";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(utblMstHotelType model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/hoteltype";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("hotelconfig", "savehoteltype", jsonStr);
                    return RedirectToAction("index", "hoteltype", new { Area = "Admin" });
                }
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
                ViewBag.ActiveURL = "/Admin/hoteltype";
                utblMstHotelType model = objAPI.GetObjectByKey<utblMstHotelType>("hotelconfig", "hoteltypebyid", id.ToString(), "id");
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
        public ActionResult Edit(utblMstHotelType model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/hoteltype";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("hotelconfig", "savehoteltype", jsonStr);
                    return RedirectToAction("index", "hoteltype", new { Area = "Admin" });
                }
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("hotelconfig", "deletehoteltype", id.ToString(), "id");
            return RedirectToAction("index", "hoteltype", new { Area = "Admin" });
        }
    }
}