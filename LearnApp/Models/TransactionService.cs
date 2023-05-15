namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionService")]
    public partial class TransactionService
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }

        public int TransactionId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Service Service { get; set; }

        public virtual Transactions Transactions { get; set; }
    }
}
