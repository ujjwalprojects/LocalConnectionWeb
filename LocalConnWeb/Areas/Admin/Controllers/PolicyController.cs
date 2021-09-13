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
    public class PolicyController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");

        #region Policy 
        public ActionResult PolicyList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                AboutPolicyApiVM apiModel = objAPI.GetRecordByQueryString<AboutPolicyApiVM>("configuration", "PolicyList", query);
                PolicyVM model = new PolicyVM();
                model.PolicyList = apiModel.PolicyList;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvPolicyList", model);
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
        public ActionResult Add(utblPolicie model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SavePolicy", jsonStr);
                    return RedirectToAction("PolicyList", "policy", new { Area = "Admin" });
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
                 utblPolicie model = objAPI.GetObjectByKey<utblPolicie>("configuration", "PolicyByID", id.ToString(), "id");
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
        public ActionResult Edit(utblPolicie model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SavePolicy", jsonStr);
                    return RedirectToAction("PolicyList", "policy", new { Area = "Admin" });

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
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "DeletePolicy", id.ToString(), "id");
            return RedirectToAction("PolicyList", "policy", new { Area = "Admin" });
        }
        #endregion


        #region Policy Points
        public ActionResult PolicyPtList(int PageNo = 1, int PageSize = 10, string SearchTerm = "")
        {
            try
            {
                string query = "PageNo=" + PageNo + "&PageSize=" + PageSize + "&SearchTerm=" + SearchTerm;
                AboutPolicyApiVM apiModel = objAPI.GetRecordByQueryString<AboutPolicyApiVM>("configuration", "PolicyPtList", query);
                PolicyVM model = new PolicyVM();
                model.PolicyPointView = apiModel.PolicyPointView;
                model.PagingInfo = new PagingInfo { CurrentPage = PageNo, ItemsPerPage = PageSize, TotalItems = apiModel.TotalRecords };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_pvPolicyPtList", model);
                }
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult AddPolicyPt()
        {
            PolicyPtSaveModel model = new PolicyPtSaveModel();
            model.PolicyDD = objAPI.GetAllRecords<utblPolicie>("configuration", "PolicyDD"); ;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPolicyPt(PolicyPtSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string jsonStr = JsonConvert.SerializeObject(model.PolicyPt);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SavePolicyPt", jsonStr);
                    return RedirectToAction("PolicyPtList", "Policy", new { Area = "Admin" });
                }
                model.PolicyDD = objAPI.GetAllRecords<utblPolicie>("configuration", "PolicyDD"); ;
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
        public ActionResult EditPolicyPt(long id)
        {
            try
            {
                PolicyPtSaveModel model = new PolicyPtSaveModel();
                model.PolicyDD = objAPI.GetAllRecords<utblPolicie>("configuration", "PolicyDD");
                model.PolicyPt = objAPI.GetObjectByKey<utblPolicyPoint>("configuration", "PolicyPtByID", id.ToString(), "id");
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
        public ActionResult EditPolicyPt(PolicyPtSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model.PolicyPt);
                    TempData["ErrMsg"] = objAPI.PostRecordtoApI("configuration", "SavePolicyPt", jsonStr);
                    return RedirectToAction("PolicyPtList", "policy", new { Area = "Admin" });
                }
                model.PolicyDD = objAPI.GetAllRecords<utblPolicie>("configuration", "PolicyDD"); ;
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
        public ActionResult DeletePolicyPt(long id)
        {
            TempData["ErrMsg"] = objAPI.DeleteRecordByKey("configuration", "DeletePolicyPt", id.ToString(), "id");
            return RedirectToAction("PolicyPtList", "policy", new { Area = "Admin" });
        }
        #endregion
    }
}