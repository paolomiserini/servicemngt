using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class ClientAddress
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "Address", ResourceType = typeof(LocalResource.Resource))] 
        [StringLength(250, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string Address { get; set; }
        
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Region", ResourceType = typeof(LocalResource.Resource))]
        public string Region { get; set; }
        
        
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "City", ResourceType = typeof(LocalResource.Resource))] 
        public string City { get; set; }
        
        [StringLength(50, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Building", ResourceType = typeof(LocalResource.Resource))]
        public string Building { get; set; }
 
        [StringLength(50, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Apartment", ResourceType = typeof(LocalResource.Resource))]
        public string Apartment { get; set; }
        
        [StringLength(15, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "IndexCode", ResourceType = typeof(LocalResource.Resource))]
        public string IndexCode { get; set; }
        public bool isDeleted { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Product> Products { get; set; }


    }
}