namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeServiceRecord")]
    public partial class EmployeeServiceRecord
    {
        public int Id { get; set; }

        public int ServiceRecordId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ServiceRecord ServiceRecord { get; set; }
    }
}
