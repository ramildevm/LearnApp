namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServicePhoto")]
    public partial class ServicePhoto
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public virtual Service Service { get; set; }
    }
}
