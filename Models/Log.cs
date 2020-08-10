namespace ServiceManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Log
    {
        public int id { get; set; }

        [StringLength(100)]
        [Display(Name = "LogTableName", ResourceType = typeof(LocalResource.Resource))]

        public string LogTableName { get; set; }

        [Display(Name = "LogIdTable", ResourceType = typeof(LocalResource.Resource))]
        public int? LogIdTable { get; set; }

        [StringLength(150)]
        [Display(Name = "LogUsrUpd", ResourceType = typeof(LocalResource.Resource))]
        public string LogUsrUpd { get; set; }

        [Display(Name = "LogDtUpd", ResourceType = typeof(LocalResource.Resource))]
        public DateTime? LogDtUpd { get; set; }

        [StringLength(500)]
        [Display(Name = "LogMessage", ResourceType = typeof(LocalResource.Resource))]
        public string LogMessage { get; set; }

        [Display(Name = "LogType", ResourceType = typeof(LocalResource.Resource))]
        public string LogType { get; set; }
    }
}
