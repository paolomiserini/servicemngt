using System;
using System.Web.Mvc;
using ServiceManagement.Session;
using System.Linq;
using ServiceManagement.Models;
using ServiceManagement;
using ServiceManagement.DAL;

namespace SchoolMngt.Controllers
{
    public class LoginController : Controller
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        public ActionResult Index()
        {
            return View(new LoginDataInfo());
        }

        [HttpPost]
        public ActionResult Index(LoginDataInfo loginData)
        {
            try
            {
                // Check if user exists
                var singleuser = db.Users.Where(u => u.Username == loginData.Username.Trim()).FirstOrDefault();

                // User not found
                if (singleuser == null)
                {
                    loginData.LoginFailed = true;
                }
                else
                {
                    // User not active
                    if (!singleuser.UserIsActive)
                    {
                        loginData.UserNotActive = true;
                    }
                    // user active password wrong
                    else 
                    {
                        // Decrypto la password
                        var _pwdcrypt = Common.EcryptDecrypt.Encrypt(loginData.Password);

                        if (_pwdcrypt != singleuser.Password)
                        {
                            loginData.WrongPassword = true;
                        }
                        // user active and password correct
                        else
                        {
                            var LoggedUser = LoggedUserInfo.FromDbTiamUser(singleuser);


                            TypedSession.Current.LoggedUser = LoggedUser;
                            TypedSession.Current.UpdateLastAction();

                            singleuser.UserLastLogin = DateTime.Now;

                            db.SaveChanges();

                            // Login information on log

                            Logger.Log("", 0, TypedSession.Current.LoggedUser.Username, "User logged", Common.INFORMATION);

                            return RedirectToAction("Index", "Home");

                        }

                    }
                }
                return View(loginData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Logger.Log("", 0, TypedSession.Current.LoggedUser.Username, "User disconnected", Common.INFORMATION);

            TypedSession.Current.Logout();

            // Msg to User
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("UserLoggedOut");

            return View("Index", new LoginDataInfo());
        }
    }
}
