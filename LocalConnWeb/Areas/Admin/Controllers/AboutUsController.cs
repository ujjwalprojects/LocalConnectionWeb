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
    public class AboutUsController : BaseController
    {
        ApiConnection objAPI = new ApiConnection("Admin");

        // GET: Admin/AboutUs
        public ActionResult AddEditAboutUs()
        {
            try
            {
                utblAboutU model = new utblAboutU();
                model = objAPI.GetRecord<utblAboutU>("configuration", "AboutByID");
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    model = new utblAboutU() { AboutID= 0, AboutContent=""};
                    return View(model);
                }
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditAboutUs(utblAboutU model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string jsonStr = JsonConvert.SerializeObject(model);
                    string result = objAPI.PostRecordtoApI("configuration", "SaveAbout", jsonStr);
                    TempData["ErrMsg"] = result;
                    if (result.ToLower().Contains("error"))
                    {
                        return View(model);
                    }
                }
                model = objAPI.GetRecord<utblAboutU>("configuration", "AboutByID");
                return View(model);
            }
            catch (AuthorizationException)
            {
                TempData["ErrMsg"] = "Your Login Session has expired. Please Login Again";
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
        }
    }
}