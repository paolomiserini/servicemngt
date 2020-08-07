using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public string ProductCode { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }

    }
}