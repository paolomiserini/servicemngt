using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceManagement.Models;

namespace ServiceManagement.ViewModels
{
    public class RemontCardView
    {
        public RemontCard RemontCard { get; set; }
        public Client Client { get; set; }
        public ClientAddress Address { get; set; }
        public Product Product { get; set; }
        public string StatusTypeSelected { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

    }
}