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
        public int Tecnicianid { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public int ClientId { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public int AddressId { get; set; }
        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public int ProductId { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public string RemontCardLongId { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [StringLength(10, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string DtClientCall { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string DtEndWork { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string DtMasterTook { get; set; }
        public DateTime DtLastUpdate { get; set; }
        public string UserLastUpdate { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        public string RequestState { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string ClientProblemDescription { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string OfficeProblemDescription { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MaxTextFieldLenght")]
        public string SupervisorAdditionalNotes { get; set; }

        public Boolean NoNeedSpareParts { get; set; }
        public Boolean Warranty { get; set; }

        public double Amount { get; set; }
        public double AdditionalAmount { get; set; }
        public virtual Client Client { get; set; }
    }
}