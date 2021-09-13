using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LocalConnWeb.Models;

namespace LocalConnWeb.Controllers
{
    public class BaseController : Controller
    {
        protected SessionModel _sModel;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sModel = Session["SessionVar"] as SessionModel;

            if (_sModel == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();

                TempData["ErrMsg"] = "Your login session has expired. Please login again to continue.";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index",
                    area = ""
                }));
            }
            else
            {
                ViewBag.CurrentUserName = _sModel.UserName;
                ViewBag.Role = _sModel.Role;

                if (_sModel.UserImage != "")
                {
                    ViewBag.UserImageSrc = _sModel.UserImage;
                }
            }
                    
        }
	}
}