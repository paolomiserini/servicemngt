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

        [Display(Name = "PartCode", ResourceType = typeof(LocalResource.Resource))]
        [StringLength(150, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string PartCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "PartDescription", ResourceType = typeof(LocalResource.Resource))]
        [StringLength(500, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string PartDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "PartPrice", ResourceType = typeof(LocalResource.Resource))]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "NumFormat")]
        [Range(0, 9999999999999999.99)]
        public decimal PartPrice { get; set; }
        public bool isDeleted { get; set; }
    }
}