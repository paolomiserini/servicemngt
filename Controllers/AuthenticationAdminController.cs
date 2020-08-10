using System.Web.Mvc;
using ServiceManagement.Session;

namespace ServiceManagement.Controllers
{
    public class AuthenticationAdminController : AuthenticationController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.Result == null)
            {
                if (TypedSession.Current.LoggedUser.UserType != Common.ADMINISTRATOR )
                {
                    filterContext.Result = RedirectToAction("Index", "Home");
                }
            }
        }
    }
}