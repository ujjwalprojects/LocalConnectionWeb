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
    public class StarRatingController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        //
        // GET: /Admin/StarRating/
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                StarRatingAPIVM apiModel = objAPI.GetRecordByQueryString<StarRatingAPIVM>("configuration", "StarRating", query);
                StarRatingVM model = new StarRatingVM();
                model.StarRatings = apiModel.StarRatings;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvStarRatingList", model);
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(StarRatingSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.StarRatings);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveStarRating", jsonStr);
                    return RedirectToAction("index", "StarRating", new { Area = "Admin" });
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
                StarRatingSaveModel model = new StarRatingSaveModel();
                model.StarRatings = objAPI.GetObjectByKey<utblLCMstStarRating>("configuration", "StarRatingByID", id.ToString(), "id");
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
        public ActionResult Edit(StarRatingSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.StarRatings);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SaveStarRating", jsonStr);
                    return RedirectToAction("index", "StarRating", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "DeleteStarRating", id.ToString(), "id");
            return RedirectToAction("index", "StarRating", new { Area = "Admin" });
        }
	}
}