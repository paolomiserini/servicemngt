using System;
using System.Linq;
using System.Web.Mvc;
using ServiceManagement;
using ServiceManagement.DAL;
using ServiceManagement.Session;

namespace ServiceManagement.Controllers
{
    public class AuthenticationController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (TypedSession.Current.LoggedUser == null)
            {
                // non sei loggato

                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else if (TypedSession.Current.LastAction < DateTime.Now.AddMinutes(Common.Settings.SessionMinutesTimeout * -1))
            {
                // è scaduta la sessione

                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else if (!GetIsUserEnabled(TypedSession.Current.LoggedUser.id))
            {
                // l'utente è stato disabilitato o cancellato dal db

                filterContext.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                TypedSession.Current.UpdateLastAction();
            }
        }

        private bool GetIsUserEnabled(int IdUser)
        {
            using (var ctx = new ServiceManagementContext())
            {
                var appUser = ctx.Users.Where(s => s.id == IdUser).FirstOrDefault();

                if (appUser != null)
                {
                    return (appUser.UserIsActive);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}