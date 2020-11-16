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
    public class SparepartController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Sparepart
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // parametro di sort corrente
            ViewBag.CurrentSort = sortOrder;

            // parametri del sort passati come viewbag
            ViewBag.PartCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "partcode_desc" : "";
            ViewBag.PartDescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "partdescription_desc" : "";
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
            var items_list = db.Spareparts.ToList().AsQueryable();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                items_list = items_list.Where(s => s.PartCode.Contains(searchString)
                                       || s.PartDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "partcode_desc":
                    items_list = items_list.OrderByDescending(s => s.PartCode);
                    break;
                case "partdescription_desc":
                    items_list = items_list.OrderByDescending(s => s.PartDescription);
                    break;
                default:
                    items_list = items_list.OrderBy(s => s.PartDescription);
                    break;
            }

            // Prendo la stringa dalla resource
            string strTranslated = Common.StringFromResource.Translation("PartCode");
            ViewBag.PartCode = strTranslated;

            strTranslated = Common.StringFromResource.Translation("PartDescription");
            ViewBag.PartDescription = strTranslated;

            strTranslated = Common.StringFromResource.Translation("FindByPart");
            ViewBag.FindBy = strTranslated;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(items_list.ToPagedList(pageNumber, pageSize));

        }

        // GET: Sparepart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sparepart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PartCode,PartDescription,PartPrice")] Sparepart sparepart)
        {
            if (ModelState.IsValid)
            {
                sparepart.isDeleted = false;
                db.Spareparts.Add(sparepart);
                db.SaveChanges();

                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index");
            }

            return View(sparepart);
        }

        // GET: Sparepart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sparepart sparepart = db.Spareparts.Find(id);
            if (sparepart == null)
            {
                return HttpNotFound();
            }
            return View(sparepart);
        }

        // POST: Sparepart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PartCode,PartDescription,PartPrice")] Sparepart sparepart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sparepart).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index");
            }
            return View(sparepart);
        }

        // GET: Sparepart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sparepart sparepart = db.Spareparts.Find(id);
            if (sparepart == null)
            {
                return HttpNotFound();
            }
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteGeneric");

            return View(sparepart);
        }

        // POST: Sparepart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sparepart sparepart = db.Spareparts.Find(id);
            sparepart.isDeleted = true;
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
