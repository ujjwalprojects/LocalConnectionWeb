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
    public class HelpPageController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");
        private string FileUrl = ConfigurationManager.AppSettings["FileURL"];
        // GET: Admin/HelpPage
        public ActionResult Index(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                HelpPageAPIVM apiModel = objAPI.GetRecordByQueryString<HelpPageAPIVM>("configuration", "helppage", query);
               HelpPageVM model = new HelpPageVM();
                model.HelpPageList = apiModel.HelpPage;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvhelppageList", model);
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
        public ActionResult Add(HelpPageSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.HelpPagesave.HelpPageImgPath = model.cropper.PhotoNormal;
                    string jsonStr = JsonConvert.SerializeObject(model.HelpPagesave);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveHelpPage", jsonStr);
                    return RedirectToAction("index", "HelpPage", new { Area = "Admin" });
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
                HelpPageSaveModel model = new HelpPageSaveModel();
                model.HelpPagesave = objAPI.GetObjectByKey<utblLCHelpPage>("configuration", "HelpPagebyid", id.ToString(), "id");
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
        public ActionResult Edit(HelpPageSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.cropper.PhotoNormal != null)
                    {
                        model.HelpPagesave.HelpPageImgPath = model.cropper.PhotoNormal;

                    }
                    else
                    {
                        model.HelpPagesave.HelpPageImgPath = model.HelpPagesave.HelpPageImgPath;
                    }
                    string jsonStr = JsonConvert.SerializeObject(model.HelpPagesave);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "saveHelpPage", jsonStr);
                    return RedirectToAction("index", "HelpPage", new { Area = "Admin" });
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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "deleteHelpPage", id.ToString(), "id");
            return RedirectToAction("index", "HelpPage", new { Area = "Admin" });
        }
    }
}