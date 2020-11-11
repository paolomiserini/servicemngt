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
    public class ClientAddressesController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: ClientAddresses
        public ActionResult Index(int? idClient)
        {
            if (idClient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idClient = idClient;
            var clientAddresses = db.ClientAddresses.Include(c => c.Client).Where(c => c.ClientID == idClient);
            return View(clientAddresses.ToList());
        }

        // GET: ClientAddresses/Create
        public ActionResult Create(int? idClient)
        {
            if (idClient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAddress newClientAddress = new ClientAddress();
            newClientAddress.ClientID = (int)idClient;
            return View(newClientAddress);
        }

        // POST: ClientAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientID,ProductID,Address,Region,City,Street,Building,Apartment,IndexCode")] ClientAddress clientAddress)
        {
            if (ModelState.IsValid)
            {
                //  seleziono il cliente dove attaccare l'indirizzo
                Client clientOnWOrk = db.Clients.Where(s => s.ID == clientAddress.ClientID).FirstOrDefault();
                // rendo valido l'indirizzo selezionato
                clientAddress.isDeleted = false;
                clientOnWOrk.ClientAddresses.Add(clientAddress);
                db.SaveChanges();
                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index", new { idClient = clientAddress.ClientID });
            }

            return View(clientAddress);
        }

        // GET: ClientAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAddress clientAddress = db.ClientAddresses.Find(id);
            if (clientAddress == null)
            {
                return HttpNotFound();
            }
            return View(clientAddress);
        }

        // POST: ClientAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientID,ProductID,Address,Region,City,Street,Building,Apartment,IndexCode, isDeleted")] ClientAddress clientAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientAddress).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ErrorType"] = Common.INFORMATION;
                TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                return RedirectToAction("Index", new { idClient = clientAddress.ClientID });
            }
            return View(clientAddress);
        }

        // GET: ClientAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientAddress clientAddress = db.ClientAddresses.Find(id);
            if (clientAddress == null)
            {
                return HttpNotFound();
            }
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteGeneric");

            return View(clientAddress);
        }

        // POST: ClientAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientAddress clientAddress = db.ClientAddresses.Find(id);
            clientAddress.isDeleted = true;
            db.SaveChanges();
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

            return RedirectToAction("Index", new { idClient = clientAddress.ClientID });
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
