using ServiceManagement.Models;
using System.Collections.Generic;

namespace ServiceManagement.ViewModels
{
    public class RemontCardSelProduct
    {
        public int idClient { get; set; }
        public IEnumerable<ClientAddress> ClientAddresses { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public bool Empty
        {
            get
            {
                return (Products == null);
            }
        }
    }
}