using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceManagement.Models
{
    public class RemontCard
    {

        public int RemontCardID { get; set; }
        public int TecnicianID { get; set; }
        public int ClientID { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public DateTime ClientCall { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public DateTime SolutionDate { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string ProblemDescription { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string AdditionalNote { get; set; }
        public Boolean NoNeedSpareParts { get; set; }
        public Boolean Warranty { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public string TypeOfWork { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public string ProductLocation { get; set; }
        public double Amount { get; set; }
        public double AdditionalAmount { get; set; }
        public virtual Tecnician Tecnician { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Sparepart> Spareparts { get; set; }

    }
}