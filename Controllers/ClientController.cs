using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using PagedList;
using ServiceManagement.DAL;
using ServiceManagement.Migrations;
using ServiceManagement.Models;

namespace ServiceManagement.Controllers
{
    public class ClientController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: Client
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
            var items_list = from s in db.Clients.Include(c => c.ClientType).Include(c => c.CompanyType)
                             where s.ClientType.TypeCode == Common.PHISICAL
                             select s;

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

        public ActionResult Index_yur(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // parametro di sort corrente
            ViewBag.CurrentSort = sortOrder;

            // parametri del sort passati come viewbag
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

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
            var items_list = from s in db.Clients.Include(c => c.ClientType).Include(c => c.CompanyType) 
                             where s.ClientType.TypeCode == Common.JURIDIC
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                items_list = items_list.Where(s => s.CompanyName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items_list = items_list.OrderByDescending(s => s.CompanyName);
                    break;
                default:
                    items_list = items_list.OrderBy(s => s.CompanyName);
                    break;
            }

            // Prendo la stringa dalla resource
            string strTranslated = Common.StringFromResource.Translation("CompanyType");
            ViewBag.CompanyTypeTxtx = strTranslated;

            strTranslated = Common.StringFromResource.Translation("FindByCompanyName");
            ViewBag.FindBy = strTranslated;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(items_list.ToPagedList(pageNumber, pageSize));

        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Where(c => c.ID == id).Include(c => c.ClientType).Include(c => c.CompanyType).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create(string clientType)
        {
            string strTranslated = Common.StringFromResource.Translation("CompanyType");
            ViewBag.CompanyTypeTxt = strTranslated;
            strTranslated = Common.StringFromResource.Translation("ClientType");
            ViewBag.ClientTypeTxt = strTranslated;
            // per selezionare il valore corretto nella combo box
            var _clientTypeId = db.ClientTypes.Where(s => s.TypeCode == clientType).FirstOrDefault();
            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription", _clientTypeId.ID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription");
            ViewBag.ClientType = clientType;
            var _newclient = new Client();
            _newclient.ClientTypeID = _clientTypeId.ID;
            return View(_newclient);
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyTypeID,ClientTypeID,Name,Surname,Patronimic,Telephone,CompanyName,ContactPerson,ExtraInfo")] Client client)
        {
            // con il tipo cliente type decido quale index ritornare
            var _clientType = db.ClientTypes.Where(s => s.ID == client.ClientTypeID).FirstOrDefault();

            bool _errorstate = false;

            if (ModelState.IsValid)
            {
                _errorstate = checkMandatory(client, _clientType.TypeCode);
                if (!_errorstate)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    // Ok
                    TempData["ErrorType"] = Common.INFORMATION;
                    TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                    if (_clientType.TypeCode == Common.PHISICAL)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index_yur");
                    }
                }

            }

            // Stringhe localizzate per le due label delle combo
            string strTranslated = Common.StringFromResource.Translation("CompanyType");
            ViewBag.CompanyTypeTxt = strTranslated;
            strTranslated = Common.StringFromResource.Translation("ClientType");
            ViewBag.ClientTypeTxt = strTranslated;
            // Reimposto il tipo cliente per filtrare i campi da visualizzare
            ViewBag.ClientType = _clientType.TypeCode;

            ViewBag.ClientTypeID = new SelectList(db.ClientTypes, "ID", "TypeDescription", client.ClientTypeID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "TypeDescription", client.CompanyTypeID);
            return View(client);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            string strTranslated = Common.StringFromResource.Translation("CompanyType");
            ViewBag.CompanyTypeTxt = strTranslated;
            strTranslated = Common.StringFromResource.Translation("ClientType");
            ViewBag.ClientTypeTxt = strTranslated;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            // Leggo il tipo cliente (non c'e' nel client che arriva...
            var _clientType = db.ClientTypes.Where(s => s.ID == client.ClientTypeID).FirstOrDefault();

            // Reimposto il tipo cliente per filtrare i campi da visualizzare
            ViewBag.ClientType = _clientType.TypeCode;


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
            bool _errorstate = false;

            // Leggo il tipo cliente (non c'e' nel client che arriva...
            var _clientType = db.ClientTypes.Where(s => s.ID == client.ClientTypeID).FirstOrDefault();
  
            if (ModelState.IsValid)
            {
          
                _errorstate = checkMandatory(client, _clientType.TypeCode);
                if (!_errorstate)
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    // Ok
                    TempData["ErrorType"] = Common.INFORMATION;
                    TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

                    if (_clientType.TypeCode == Common.PHISICAL)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index_yur");
                    }
                }
                
            }

            string strTranslated = Common.StringFromResource.Translation("CompanyType");
            ViewBag.CompanyTypeTxt = strTranslated;
            strTranslated = Common.StringFromResource.Translation("ClientType");
            ViewBag.ClientTypeTxt = strTranslated;

            // Reimposto il tipo cliente per filtrare i campi da visualizzare
            ViewBag.ClientType = _clientType.TypeCode;

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
            Client client = db.Clients.Include(c => c.ClientType).Where(s => s.ID == id).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();
            }
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("MsgDeleteGeneric");

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Include(c => c.ClientType).Where(s => s.ID == id).FirstOrDefault();
            client.IsDeleted = true;
            db.SaveChanges();
            TempData["ErrorType"] = Common.INFORMATION;
            TempData["GenericError"] = Common.StringFromResource.Translation("DoneOk");

            if (client.ClientType.TypeCode ==  Common.PHISICAL)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index_yur");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool checkMandatory(Client client, string _clientType)
        {
            // check dati obbligatori
            if (_clientType == Common.PHISICAL)
            {
                if (client.Name.IsEmpty() || client.Name.IsNullOrWhiteSpace())
                {
                    // Nome obbligatorio
                    TempData["ErrorType"] = Common.ERROR;
                    TempData["GenericError"] = Common.StringFromResource.Translation("MsgMandatoryName");
                    return true;
                }
               
                if (client.Surname.IsEmpty() || client.Surname.IsNullOrWhiteSpace())
                {
                    // Cognome obbligatorio
                    TempData["ErrorType"] = Common.ERROR;
                    TempData["GenericError"] = Common.StringFromResource.Translation("MsgMandatorySurname");
                    return true;
                }
            }
            else // Cliente giuridico
            {
                if (client.CompanyName.IsEmpty() || client.CompanyName.IsNullOrWhiteSpace())
                {
                    // Cognome obbligatorio
                    TempData["ErrorType"] = Common.ERROR;
                    TempData["GenericError"] = Common.StringFromResource.Translation("MsgMandatoryCompanyName");
                    return true;
                }
                if (client.ContactPerson.IsEmpty() || client.ContactPerson.IsNullOrWhiteSpace())
                {
                    // Cognome obbligatorio
                    TempData["ErrorType"] = Common.ERROR;
                    TempData["GenericError"] = Common.StringFromResource.Translation("MsgMandatoryContact");
                    return true;
                }
            }
            return false;
        }
    }
}
