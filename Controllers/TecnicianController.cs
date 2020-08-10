using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    public class TecnicianController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Tecnician
        public ActionResult Index()
        {
            return View(db.Tecnicians.ToList());
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
            db.Tecnicians.Remove(tecnician);
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
