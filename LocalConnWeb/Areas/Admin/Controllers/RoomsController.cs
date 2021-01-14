using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class RoomsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        //
        // GET: /Admin/Rooms/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                RoomsAPIVM apiModel = objAPI.GetRecordByQueryString<RoomsAPIVM>("configuration", "rooms", query);
                RoomsVM model = new RoomsVM();
                model.Rooms = apiModel.Rooms;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvRoomsList", model);
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
                return View();
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RoomsSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Rooms);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveRooms", jsonStr);
                    return RedirectToAction("index", "rooms", new { Area = "Admin" });
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
                RoomsSaveModel model = new RoomsSaveModel();
                model.Rooms = objAPI.GetObjectByKey<utblLCRoom>("configuration", "roomsbyid", id.ToString(), "id");
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
        public ActionResult Edit(RoomsSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Rooms);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveRooms", jsonStr);
                    return RedirectToAction("index", "rooms", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteRooms", id.ToString(), "id");
            return RedirectToAction("index", "Rooms", new { Area = "Admin" });
        }
	}
}