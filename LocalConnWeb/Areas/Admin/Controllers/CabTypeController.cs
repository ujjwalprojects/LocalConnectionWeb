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
    public class CabTypeController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");

        public ActionResult Index()
        {
            try
            {
                var model = objAPI.GetAllRecords<utblMstCabType>("cabconfig", "cabtypes");
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvCabTypeList", model);
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
            ViewBag.ActiveURL = "/Admin/cabtype";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(utblMstCabType model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabtype";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecabtype", jsonStr);
                    return RedirectToAction("index", "cabtype", new { Area = "Admin" });
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
                ViewBag.ActiveURL = "/Admin/cabtype";
                utblMstCabType model = objAPI.GetObjectByKey<utblMstCabType>("cabconfig", "cabtypebyid", id.ToString(), "id");
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
        public ActionResult Edit(utblMstCabType model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/cabtype";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("cabconfig", "savecabtype", jsonStr);
                    return RedirectToAction("index", "cabtype", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("cabconfig", "deletecabtype", id.ToString(), "id");
            return RedirectToAction("index", "cabtype", new { Area = "Admin" });
        }
    }
}