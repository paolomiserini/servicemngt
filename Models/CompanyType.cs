using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class CompanyType
    {
        public int ID { get; set; }
        [Display(Name = "CompanyType", ResourceType = typeof(LocalResource.Resource))]
        public string TypeDescription { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

    }
}