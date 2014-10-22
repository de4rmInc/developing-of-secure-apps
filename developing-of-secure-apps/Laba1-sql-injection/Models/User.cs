namespace Laba1_sql_injection.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
