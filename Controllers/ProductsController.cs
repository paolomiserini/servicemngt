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
    public class ProductsController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Products
        public ActionResult Index(int? idAddress)
        {
            if (idAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idAddress = idAddress;
            var products = db.Products.Include(p => p.ClientAddress).Where(s => s.ClientAddressID == idAddress).OrderBy(p => p.isDeleted);

            ClientAddress clientAddress = db.ClientAddresses.Where(ca => ca.ID == idAddress).FirstOrDefault();
            ViewBag.idClient = clientAddress.ClientID;

            return View(products.ToList());
        }

         // GET: Products/Create
        public ActionResult Create(int? idAddress)
        {

            if (idAddress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product newProduct = new Product();
            newProduct.ClientAddressID = (int)idAddress;
            return View(newProduct);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientAddressID,Model,SerialNumber,ProductCode,isDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {

                //  seleziono l'indirizzo dove attaccare i prodotti. Ad un indirizzo possono essere presenti piu' prodotti
                ClientAddress RecOnWork = db.ClientAddresses.Where(s => s.ID == product.ClientAddressID).FirstOrDefault();
                // rendo valido l'indirizzo selezionato
                product.isDeleted = false;
                RecOnWork.Products.Add(product);
                db.SaveChanges();

                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index", new { idAddress = product.ClientAddressID });

            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientAddressID = new SelectList(db.ClientAddresses, "ID", "Address", product.ClientAddressID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientAddressID,Model,SerialNumber,ProductCode,isDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index", new { idAddress = product.ClientAddressID });
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteGeneric");

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            product.isDeleted = true;
            db.SaveChanges();

            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

            return RedirectToAction("Index", new { idAddress = product.ClientAddressID });
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
