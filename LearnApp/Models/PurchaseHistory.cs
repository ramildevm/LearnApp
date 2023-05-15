namespace LearnApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseHistory")]
    public partial class PurchaseHistory
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Quantity { get; set; }

        public int? ProductId { get; set; }

        public int? ClientId { get; set; }

        public int? ServiceId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Product Product { get; set; }

        public virtual Service Service { get; set; }
    }
}
