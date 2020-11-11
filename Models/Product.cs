using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int ClientAddressID { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "Model", ResourceType = typeof(LocalResource.Resource))]
        [StringLength(250, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string Model { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "SerialNumber", ResourceType = typeof(LocalResource.Resource))]
        public string SerialNumber { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "ProductCode", ResourceType = typeof(LocalResource.Resource))]
        public string ProductCode { get; set; }
        
        public bool isDeleted { get; set; }
        public virtual ClientAddress ClientAddress { get; set; }

    }
}