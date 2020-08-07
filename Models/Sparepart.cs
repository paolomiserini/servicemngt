using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class Sparepart
    {
        public int ID { get; set; }
        public string PartCode { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string PartDescription { get; set; }
        public double PartPrice { get; set; }
    }
}