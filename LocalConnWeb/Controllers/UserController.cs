using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalConnWeb.Helpers;
using LocalConnWeb.Models;

namespace LocalConnWeb.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {

            ApiConnection objAPI = new ApiConnection("");
        private string LoginUrl = ConfigurationManager.AppSettings["LoginURL"];

        public UserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        //
        // GET: /User/
        public ActionResult Dashboard()
        {
            return View();
        }


        //
        // GET: /Account/Register from Admin Section
        [AllowAnonymous]
        public ActionResult Register()
        {
            //objVM.DestinationDD = objAPI.GetAllRecords<utblMstDestination>("configuration", "destionationDD");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                TempData["ErrMsg"] = objAPI.PostRecordtoApI("account", "register", jsonStr);
                return RedirectToAction("login", "account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
	}
}