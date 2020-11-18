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
        public int ClientId { get; set; }
        public int AddressId { get; set; }
        public int ProductId { get; set; }
        public string RemontCardLongId { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DtClientCall { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)] 
        public DateTime DtEndWork { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DtMasterTook { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DtLastUpdate { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource.Resource), ErrorMessageResourceName = "MandatoryField")]
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