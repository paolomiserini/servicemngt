using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    public class TecnicianController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Tecnician
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // parametro di sort corrente
            ViewBag.CurrentSort = sortOrder;

            // parametri del sort passati come viewbag
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SurNameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";


            // codice per paginazione
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // parametro di filtro corrente
            ViewBag.CurrentFilter = searchString;

            // accesso al database prelevo lista dati da ordinare o ricercare
            // la variabile utilizzata e' generica per un copia incolla
            var items_list = from s in db.Tecnicians select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                items_list = items_list.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items_list = items_list.OrderByDescending(s => s.Name);
                    break;
                case "surname_desc":
                    items_list = items_list.OrderByDescending(s => s.Surname);
                    break;
                default:
                    items_list = items_list.OrderBy(s => s.Name);
                    break;
            }

            // Prendo la stringa dalla resource
            string strTranslated = Common.StringFromResource.Translation("Name");
            ViewBag.Name = strTranslated;

            strTranslated = Common.StringFromResource.Translation("Surname");
            ViewBag.Surname = strTranslated;

            strTranslated = Common.StringFromResource.Translation("FindByStaff");
            ViewBag.FindBy = strTranslated;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(items_list.ToPagedList(pageNumber, pageSize));

        }

        // GET: Tecnician/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnician tecnician = db.Tecnicians.Find(id);
            if (tecnician == null)
            {
                return HttpNotFound();
            }
            return View(tecnician);
        }

        // GET: Tecnician/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tecnician/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Patronimic,Telephone")] Tecnician tecnician)
        {
            if (ModelState.IsValid)
            {
                db.Tecnicians.Add(tecnician);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tecnician);
        }

        // GET: Tecnician/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnician tecnician = db.Tecnicians.Find(id);
            if (tecnician == null)
            {
                return HttpNotFound();
            }
            return View(tecnician);
        }

        // POST: Tecnician/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Patronimic,Telephone")] Tecnician tecnician)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tecnician);
        }

        // GET: Tecnician/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnician tecnician = db.Tecnicians.Find(id);
            if (tecnician == null)
            {
                return HttpNotFound();
            }

            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteGeneric");

            return View(tecnician);
        }

        // POST: Tecnician/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnician tecnician = db.Tecnicians.Find(id);
            tecnician.IsDeleted = true;
            //db.Tecnicians.Remove(tecnician);
            db.SaveChanges();

            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

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
