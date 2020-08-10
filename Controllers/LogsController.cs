using System.Linq;
using System.Web.Mvc;
using ServiceManagement;
using ServiceManagement.Controllers;
using ServiceManagement.DAL;
using ServiceManagement.Models;
using ServiceManagement.Session;

namespace SchoolMngt.Controllers
{
    public class LogsController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Logs
        public ActionResult Index()
        {
            return View(db.Logs.ToList());
        }

        // GET: Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // Msg to User
                TempData["ErrorType"] = Common.ERROR;
                TempData["GenericError"] = Common.StringFromResource.Translation("BadRequest400");

                return RedirectToAction("Index");
            }

            Log log = db.Logs.Find(id);

            if (log == null)
            {
                // Msg to User
                TempData["ErrorType"] = Common.ERROR;
                TempData["GenericError"] = Common.StringFromResource.Translation("BadRequest404");

                return RedirectToAction("Index");
            }

            return View(log);
        }

 
        // GET: Logs/Delete/5
        public ActionResult Delete()
        {
            Log log = db.Logs.First();

            // Msg to User
            TempData["ErrorType"] = Common.WARNING;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteLog");

            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE LOG");
            db.SaveChanges();

            // Msg to User
            TempData["ErrorType"] = Common.WARNING;
            TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

            // logging
            Logger.Log("Logs", 0, TypedSession.Current.LoggedUser.Username, "Log table deleted", Common.INFORMATION);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
