using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class ClientAddress
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int ProductID { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set;}
        public string Building { get; set; }
        public string Apartment { get; set; }
        public string IndexCode { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Product> Products { get; set; }


    }
}