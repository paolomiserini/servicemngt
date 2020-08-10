namespace ServiceManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(3)]
        public string UserType { get; set; }

        public bool UserIsActive { get; set; }

        public DateTime? UserLastLogin { get; set; }
    }
}
