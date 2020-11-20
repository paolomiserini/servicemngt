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
        public string infoCliente { get; set; }
        public string infoAddress { get; set; }
        public string infoProduct { get; set; }
        public RemontCard RemontCard { get; set; }
        public string StatusTypeSelected { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

    }
}