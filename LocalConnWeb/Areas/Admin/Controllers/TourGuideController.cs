using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Controllers;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class TourGuideController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                TourGuideAPIVM apiModel = objAPI.GetRecordByQueryString<TourGuideAPIVM>("tourguide", "guidepaged", query);
                TourGuideVM model = new TourGuideVM();
                model.TourGuides = apiModel.TourGuides;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvGuideList", model);
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
        public ActionResult Add(utblTourGuide model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string linktext = Regex.Replace(model.TourGuideName, @"[^0-9A-Za-z ,]", "").Replace(" ", "-");
                    linktext = linktext.Replace("--", "-").ToLower();

                    model.TourGuideLink = linktext;
                    string jsonStr = JsonConvert.SerializeObject(model);
                    string result = objAPI.PostRecordtoApI("tourguide", "saveguide", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        ModelState.SetModelValue("TourGuideDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.TourGuideDesc), CultureInfo.InvariantCulture));
                        return View(model);
                    }
                    return RedirectToAction("index", "tourguide", new { Area = "admin" });
                }
                ModelState.SetModelValue("TourGuideDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.TourGuideDesc), CultureInfo.InvariantCulture));
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
                utblTourGuide model = objAPI.GetObjectByKey<utblTourGuide>("tourguide", "guidebyid", id.ToString(), "id");
                model.TourGuideDesc = HttpUtility.HtmlDecode(model.TourGuideDesc);
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
        public ActionResult Edit(utblTourGuide model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string linktext = Regex.Replace(model.TourGuideName, @"[^0-9A-Za-z ,]", "").Replace(" ", "-");
                    linktext = linktext.Replace("--", "-").ToLower();

                    model.TourGuideLink = linktext;
                    string jsonStr = JsonConvert.SerializeObject(model);
                    string result = objAPI.PostRecordtoApI("tourguide", "saveguide", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        ModelState.SetModelValue("TourGuideDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.TourGuideDesc), CultureInfo.InvariantCulture));
                        return View(model);
                    }
                    return RedirectToAction("index", "tourguide", new { Area = "admin" });
                }
                ModelState.SetModelValue("TourGuideDesc", new ValueProviderResult(null, HttpUtility.HtmlDecode(model.TourGuideDesc), CultureInfo.InvariantCulture));
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("tourguide", "deleteguide", id.ToString(), "id");
            return RedirectToAction("index", "tourguide", new { Area = "Admin" });
        }
	}
}