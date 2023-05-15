namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            EmployeeService = new HashSet<EmployeeService>();
            PurchaseHistory = new HashSet<PurchaseHistory>();
            ServicePhoto = new HashSet<ServicePhoto>();
            ServiceRecord = new HashSet<ServiceRecord>();
            TransactionService = new HashSet<TransactionService>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ServiceName { get; set; }

        public string Description { get; set; }

        [Required]
        public string MainImage { get; set; }

        public int Duration { get; set; }

        public int TimeTypeId { get; set; }

        public int Cost { get; set; }

        public int Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeService> EmployeeService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }

        public virtual TimeType TimeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePhoto> ServicePhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRecord> ServiceRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionService> TransactionService { get; set; }
    }
}
