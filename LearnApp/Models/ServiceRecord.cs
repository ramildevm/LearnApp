namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceRecord")]
    public partial class ServiceRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceRecord()
        {
            EmployeeServiceRecord = new HashSet<EmployeeServiceRecord>();
            ServiceRecordDocument = new HashSet<ServiceRecordDocument>();
        }

        public int Id { get; set; }

        public int ServiceId { get; set; }

        public DateTime ServiceStart { get; set; }

        public int ClientId { get; set; }

        public string Comment { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeServiceRecord> EmployeeServiceRecord { get; set; }

        public virtual Service Service { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRecordDocument> ServiceRecordDocument { get; set; }
    }
}
