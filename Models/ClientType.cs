using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class ClientType
    {
        public int ID { get; set; }
        public string TypeDescription { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

    }
}