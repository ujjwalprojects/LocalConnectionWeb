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
    public class TermController : BaseController
    {
        // GET: Admin/Term
        //
        // GET: /Admin/Terms/
        // GET: /Admin/Package/
        ApiConnection objAPI = new ApiConnection("Admin");
        public ActionResult TermList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Termlist";
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                TermAPIVM apiModel = objAPI.GetRecordByQueryString<TermAPIVM>("configuration", "termlist", query);
                TermVM model = new TermVM();
                model.TermList = apiModel.TermList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvTermList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult AddTerm()
        {
            ViewBag.ActiveURL = "/Admin/Termlist";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTerm(SaveTerm model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/packagelist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Term);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveterm", jsonStr);
                    return RedirectToAction("Termlist", "Term", new { Area = "Admin" });
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        public ActionResult EditTerm(long id)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Termlist";
                SaveTerm model = new SaveTerm();
                model.Term = objAPI.GetObjectByKey<utblMstTerm>("configuration", "termbyid", id.ToString(), "TermID");
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
        public ActionResult EditTerm(SaveTerm model)
        {
            try
            {
                ViewBag.ActiveURL = "/Admin/Termlist";
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.Term);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveterm", jsonStr);
                    return RedirectToAction("Termlist", "Term", new { Area = "Admin" });
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
        public ActionResult DeleteTerm(long TermID)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteterm", TermID.ToString(), "TermID");
            return RedirectToAction("TermList", "Term", new { Area = "Admin" });
        }
    }
}