using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class Tecnician
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "Name", ResourceType = typeof(LocalResource.Resource))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "Surname", ResourceType = typeof(LocalResource.Resource))]
        public string Surname { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [Display(Name = "Patronimic", ResourceType = typeof(LocalResource.Resource))]
        public string Patronimic { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [StringLength(15)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "TelephoneNotValid")]
        [Display(Name = "Telephone", ResourceType = typeof(LocalResource.Resource))]
        public string Telephone { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<RemontCard> RemontCards { get; set; } 

    }
}