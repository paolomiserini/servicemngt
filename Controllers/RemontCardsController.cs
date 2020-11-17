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
    public class RemontCardsController : Controller
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: RemontCards
        public ActionResult Index()
        {
            var remontCards = db.RemontCards.Include(r => r.Client);
            return View(remontCards.ToList());
        }

        // GET: RemontCards/Details/5
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

        // GET: RemontCards/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Name");
            return View();
        }

        // POST: RemontCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RemontCardID,Tecnicianid,ClientId,AddressId,ProductId,RemontCardLongId,DtClientCall,DtEndWork,DtMasterTook,DtLastUpdate,UserLastUpdate,RequestState,ClientProblemDescription,OfficeProblemDescription,SupervisorAdditionalNotes,NoNeedSpareParts,Warranty,Amount,AdditionalAmount")] RemontCard remontCard)
        {
            if (ModelState.IsValid)
            {
                db.RemontCards.Add(remontCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Name", remontCard.ClientId);
            return View(remontCard);
        }

        // GET: RemontCards/Edit/5
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
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Name", remontCard.ClientId);
            return View(remontCard);
        }

        // POST: RemontCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RemontCardID,Tecnicianid,ClientId,AddressId,ProductId,RemontCardLongId,DtClientCall,DtEndWork,DtMasterTook,DtLastUpdate,UserLastUpdate,RequestState,ClientProblemDescription,OfficeProblemDescription,SupervisorAdditionalNotes,NoNeedSpareParts,Warranty,Amount,AdditionalAmount")] RemontCard remontCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(remontCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Name", remontCard.ClientId);
            return View(remontCard);
        }

        // GET: RemontCards/Delete/5
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

        // POST: RemontCards/Delete/5
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
