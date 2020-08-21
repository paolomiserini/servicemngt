using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Display(Name = "CompanyType", ResourceType = typeof(LocalResource.Resource))]
        public int CompanyTypeID { get; set; }
        [Display(Name = "ClientType", ResourceType = typeof(LocalResource.Resource))]
        public int ClientTypeID { get; set; }
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Name", ResourceType = typeof(LocalResource.Resource))]
        public string Name { get; set; }
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Surname", ResourceType = typeof(LocalResource.Resource))]
        public string Surname { get; set; }
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "Patronimic", ResourceType = typeof(LocalResource.Resource))]
        public string Patronimic { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [StringLength(15)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "TelephoneNotValid")]
        [Display(Name = "Telephone", ResourceType = typeof(LocalResource.Resource))]
        public string Telephone { get; set; }
        [StringLength(150, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "CompanyName", ResourceType = typeof(LocalResource.Resource))]
        public string CompanyName { get; set; }
        [Display(Name = "ContactPerson", ResourceType = typeof(LocalResource.Resource))]
        public string ContactPerson { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        [Display(Name = "ExtraInfo", ResourceType = typeof(LocalResource.Resource))]
        public string ExtraInfo { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<RemontCard> RemontCards { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public CompanyType CompanyType { get; set; }
        public ClientType ClientType { get; set; }

    }
}