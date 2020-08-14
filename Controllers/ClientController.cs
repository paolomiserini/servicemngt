using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    public class ClientController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Client
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.ClientType).Include(c => c.CompanyType);
            return View(clients.ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription");
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyTypeID,ClientTypeID,Name,Surname,Patronimic,Telephone,CompanyName,ContactPerson,ExtraInfo")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription", client.ClientTypeID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription", client.CompanyTypeID);
            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription", client.ClientTypeID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription", client.CompanyTypeID);
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyTypeID,ClientTypeID,Name,Surname,Patronimic,Telephone,CompanyName,ContactPerson,ExtraInfo")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription", client.ClientTypeID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription", client.CompanyTypeID);
            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
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
