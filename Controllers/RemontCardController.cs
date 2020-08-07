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
    public class RemontCardController : Controller
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: RemontCard
        public ActionResult Index()
        {
            var remontCards = db.RemontCards.Include(r => r.Tecnician);
            return View(remontCards.ToList());
        }

        // GET: RemontCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RemontCard remontCard = db.RemontCards.Find(id);
            if (remontCard == null)
            {
                return HttpNotFound();
            }
            return View(remontCard);
        }

        // GET: RemontCard/Create
        public ActionResult Create()
        {
            ViewBag.TecnicianID = new SelectList(db.Tecnicians, "ID", "Name");
            return View();
        }

        // POST: RemontCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RemontCardID,TecnicianID,ClientCall,SolutionDate,ProblemDescription,AdditionalNote,NoNeedSpareParts,Warranty,TypeOfWork,ProductLocation,Amount,AdditionalAmount")] RemontCard remontCard)
        {
            if (ModelState.IsValid)
            {
                db.RemontCards.Add(remontCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TecnicianID = new SelectList(db.Tecnicians, "ID", "Name", remontCard.TecnicianID);
            return View(remontCard);
        }

        // GET: RemontCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RemontCard remontCard = db.RemontCards.Find(id);
            if (remontCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.TecnicianID = new SelectList(db.Tecnicians, "ID", "Name", remontCard.TecnicianID);
            return View(remontCard);
        }

        // POST: RemontCard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RemontCardID,TecnicianID,ClientCall,SolutionDate,ProblemDescription,AdditionalNote,NoNeedSpareParts,Warranty,TypeOfWork,ProductLocation,Amount,AdditionalAmount")] RemontCard remontCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(remontCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TecnicianID = new SelectList(db.Tecnicians, "ID", "Name", remontCard.TecnicianID);
            return View(remontCard);
        }

        // GET: RemontCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RemontCard remontCard = db.RemontCards.Find(id);
            if (remontCard == null)
            {
                return HttpNotFound();
            }
            return View(remontCard);
        }

        // POST: RemontCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RemontCard remontCard = db.RemontCards.Find(id);
            db.RemontCards.Remove(remontCard);
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
