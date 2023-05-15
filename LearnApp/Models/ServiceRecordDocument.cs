namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceRecordDocument")]
    public partial class ServiceRecordDocument
    {
        public int Id { get; set; }

        public int ServiceRecordId { get; set; }

        [Required]
        public string DocumentPath { get; set; }

        public virtual ServiceRecord ServiceRecord { get; set; }
    }
}
