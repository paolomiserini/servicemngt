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
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string ClientType { get; set; }
        public string Name { get; set; }
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string Surname { get; set; }
        [StringLength(100, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string Patronimic { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [StringLength(15)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "TelephoneNotValid")]
        public string Telephone { get; set; }
        public string CompanyType { get; set; }
        [StringLength(150, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string ExtraInfo { get; set; }
        public virtual ICollection<RemontCard> RemontCards { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }

    }
}