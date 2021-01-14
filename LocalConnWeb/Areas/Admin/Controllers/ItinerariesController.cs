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
    public class ItinerariesController : BaseController
    {
        //
        // GET: /Admin/Itineraries/
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult ItineraryList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Itinerarylist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                ItineraryAPIVM apiModel = objAPI.GetRecordByQueryString<ItineraryAPIVM>("configuration", "itinerarylist", query);
                ItineraryVM model = new ItineraryVM();
                model.ItinenaryList = apiModel.ItinenaryList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvItineraryList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddItinerary()
        {
            ViewBag.ActiveURL = "/Admin/Itinerarylist";
            ItinerarySave objVM = new ItinerarySave();
            objVM.DestinationDD = objAPI.GetAllRecords<utblMstDestination>("configuration", "destionationDD");
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItinerary(ItinerarySave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Itinerarylist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.itinerarie);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveitinerary", jsonStr);
                    return RedirectToAction("ItineraryList", "Itineraries", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditItinerary(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/inclusionlist";
                ItinerarySave model = new ItinerarySave();
                model.DestinationDD = objAPI.GetAllRecords<utblMstDestination>("configuration", "destionationDD");
                model.itinerarie = objAPI.GetObjectByKey<utblMstitinerarie>("configuration", "itinerarybyid", id.ToString(), "ItineraryID");
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
        public ActionResult EditItinerary(ItinerarySave model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/inclusionlist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.itinerarie);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveitinerary", jsonStr);
                    return RedirectToAction("ItineraryList", "Itineraries", new { Area = "Admin" });
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
        public ActionResult DeleteItinerary(long ItineraryID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteitinerary", ItineraryID.ToString(), "ItineraryID");
            return RedirectToAction("ItineraryList", "Itineraries", new { Area = "Admin" });
        }
    }
}