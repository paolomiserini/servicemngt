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
using ServiceManagement.Session;
using ServiceManagement.ViewModels;

namespace ServiceManagement.Controllers
{
    public class RemontCardsController : AuthenticationController
    {
        private ServiceManagementContext db = new ServiceManagementContext();

        // GET: RemontCards
        public ActionResult Index()
        {
            var remontCards = db.RemontCards.Include(r => r.Client);
            return View(remontCards.ToList());
        }
        
        // Creazione della Remont Card : Primo step selezione del cliente
        public ActionResult StepCliente(string sortOrder, string currentFilter, string searchString, int? page)
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
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                items_list = items_list.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString)
                                       || s.CompanyName.Contains(searchString));
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

        // Creazione della Remont Card : Secondo step selezione dell'indirizzo
        public ActionResult StepAddressProduct(int idClient)
        {
            RemontCardSelProduct remontCardSelProducts = new RemontCardSelProduct();

            remontCardSelProducts.idClient = idClient;

            // accesso al database prelevo lista dati da ordinare o ricercare
            // la variabile utilizzata e' generica per un copia incolla
            var address_list = db.ClientAddresses.Where(ca => ca.ClientID == idClient && ca.isDeleted == false);

            remontCardSelProducts.ClientAddresses = address_list;

            return View("StepAddressProduct", remontCardSelProducts);

        }

        // Creazione della Remont Card : Terzo step selezione del prodotto
        public ActionResult StepProduct(int idClient, int idAddress)
        {
            RemontCardSelProduct remontCardSelProducts = new RemontCardSelProduct();

            remontCardSelProducts.idClient = idClient;

            // accesso al database prelevo lista dati da ordinare o ricercare
            // la variabile utilizzata e' generica per un copia incolla
            var address_list = db.ClientAddresses.Where(ca => ca.ClientID == idClient && ca.isDeleted == false);

            remontCardSelProducts.ClientAddresses = address_list;

            var product_list = db.Products.Where(ca => ca.ClientAddressID == idAddress && ca.isDeleted ==  false);

            remontCardSelProducts.Products = product_list;

            return View("StepAddressProduct", remontCardSelProducts);

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
        public ActionResult Create(int idCliente, int idAddress, int idProduct)
        {
            // creo un nuovo oggetto remontcardview
            RemontCardView remontCardView = new RemontCardView();

            // creo unanuova remontcard
            RemontCard remontCard = new RemontCard();

            // Nella remontcard salvo i valori arrivati in input
            remontCard.ClientId = idCliente;
            remontCard.AddressId = idAddress;
            remontCard.ProductId = idProduct;

            // Creo l'ID visibile all'utente
            remontCard.RemontCardLongId = Common.GenerateRemontId.RemontIdGeneration(idCliente, Common.OFFICEGENERATED);

            // Prelevo i dati di testata per il cliente

            Common.ProductInfo productInfo = getProductInfo(idCliente, idAddress, idProduct);
            
            remontCardView.infoCliente = productInfo.infoClient;
            remontCardView.infoAddress = productInfo.infoAddress;
            remontCardView.infoProduct = productInfo.infoProduct;

            remontCardView.RemontCard = remontCard;
            remontCardView.StatusTypes = GetStatusTypes("");

            return View(remontCardView);
        }

        // POST: RemontCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RemontCardID,Tecnicianid,ClientId,AddressId,ProductId,RemontCardLongId,DtClientCall,DtEndWork,DtMasterTook,DtLastUpdate,UserLastUpdate,RequestState,ClientProblemDescription,OfficeProblemDescription,SupervisorAdditionalNotes,NoNeedSpareParts,Warranty,Amount,AdditionalAmount")] RemontCard remontCard, RemontCardView remontCardView)
        public ActionResult Create(RemontCardView remontCardView)
        {
            if (ModelState.IsValid)
            {
                // imposto gli utlimi valori necessari
                remontCardView.RemontCard.DtLastUpdate = DateTime.UtcNow;
                remontCardView.RemontCard.UserLastUpdate = TypedSession.Current.LoggedUser.Username;

                db.RemontCards.Add(remontCardView.RemontCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Prelevo i dati di testata per il cliente

            Common.ProductInfo productInfo = getProductInfo(remontCardView.RemontCard.ClientId, remontCardView.RemontCard.AddressId, remontCardView.RemontCard.ProductId);

            remontCardView.infoCliente = productInfo.infoClient;
            remontCardView.infoAddress = productInfo.infoAddress;
            remontCardView.infoProduct = productInfo.infoProduct;

            remontCardView.StatusTypeSelected = remontCardView.RemontCard.RequestState;

            remontCardView.StatusTypes = GetStatusTypes(remontCardView.StatusTypeSelected);

            return View(remontCardView);
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

        private Common.ProductInfo getProductInfo(int idClient, int idAddress, int idProduct) 
        {
            Common.ProductInfo productInfo = new Common.ProductInfo();

            Client client = db.Clients.Include(p => p.ClientType).Where(cl => cl.ID == idClient).FirstOrDefault();
            ClientAddress address = db.ClientAddresses.Where(ad => ad.ID == idAddress).FirstOrDefault();
            Product product = db.Products.Where(p => p.ID == idProduct).FirstOrDefault();

            if (client.ClientType.TypeCode == Common.JURIDIC)
            {
                productInfo.infoClient = client.CompanyName;
            }
            else
            {
                productInfo.infoClient = client.Name + " " + client.Surname;
            }

            productInfo.infoAddress = address.Region + " " + address.City + " " + address.Address;
            productInfo.infoProduct = product.Model;

            return productInfo;
        }

        private IEnumerable<SelectListItem> GetStatusTypes(string ischecked)
        {

            // List of types of possible controls to apply during reconciliation

            if (ischecked == "")
            {
                List<SelectListItem> checktypes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "запрос вставлен", Value = Common.INSERITA },

                    new SelectListItem { Text = "запрос принят", Value = Common.ACCETTATA },

                    new SelectListItem { Text = "запрос в обработке", Value = Common.INLAVORAZIONE },

                    new SelectListItem { Text = "запрос на запасные части", Value = Common.ATTESARICAMBIO },

                    new SelectListItem { Text = "запрос разрешен", Value = Common.CHIUSA },

                    new SelectListItem { Text = "запрос отклонен", Value = Common.RIFIUTATA }
                };
                return checktypes;

            }
            else
            {
                List<SelectListItem> checktypes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "запрос вставлен", Value = Common.INSERITA, Selected = ischecked == Common.INSERITA },

                    new SelectListItem { Text = "запрос принят", Value = Common.ACCETTATA, Selected = ischecked == Common.ACCETTATA },

                    new SelectListItem { Text = "запрос в обработке", Value = Common.INLAVORAZIONE, Selected = ischecked == Common.INLAVORAZIONE  },

                    new SelectListItem { Text = "запрос на запасные части", Value = Common.ATTESARICAMBIO, Selected = ischecked == Common.ATTESARICAMBIO },

                    new SelectListItem { Text = "запрос разрешен", Value = Common.CHIUSA, Selected = ischecked == Common.CHIUSA  },

                    new SelectListItem { Text = "запрос отклонен", Value = Common.RIFIUTATA, Selected = ischecked == Common.RIFIUTATA  }
                };
                return checktypes;

            }
        }

    }
}
