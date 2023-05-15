using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LearnApp.Models
{
    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeService> EmployeeService { get; set; }
        public virtual DbSet<EmployeeServiceRecord> EmployeeServiceRecord { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public virtual DbSet<RelatedProducts> RelatedProducts { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServicePhoto> ServicePhoto { get; set; }
        public virtual DbSet<ServiceRecord> ServiceRecord { get; set; }
        public virtual DbSet<ServiceRecordDocument> ServiceRecordDocument { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TimeType> TimeType { get; set; }
        public virtual DbSet<TransactionProduct> TransactionProduct { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionService> TransactionService { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Category1)
                .WithOptional(e => e.Category2)
                .HasForeignKey(e => e.ParentCategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ServiceRecord)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.TransactionProduct)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.TransactionService)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PaymentCoefficient)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeService)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeServiceRecord)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TransactionProduct)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TransactionService)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Width)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Height)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Length)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.RelatedProducts)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.RelatedProducts1)
                .WithOptional(e => e.Product1)
                .HasForeignKey(e => e.RelatedProductId);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.TransactionProduct)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.EmployeeService)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServicePhoto)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceRecord)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.TransactionService)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceRecord>()
                .HasMany(e => e.EmployeeServiceRecord)
                .WithRequired(e => e.ServiceRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceRecord>()
                .HasMany(e => e.ServiceRecordDocument)
                .WithRequired(e => e.ServiceRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeType>()
                .HasMany(e => e.Service)
                .WithRequired(e => e.TimeType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transactions>()
                .Property(e => e.TransactionType)
                .IsUnicode(false);

            modelBuilder.Entity<Transactions>()
                .Property(e => e.PaymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Transactions>()
                .HasMany(e => e.TransactionProduct)
                .WithRequired(e => e.Transactions)
                .HasForeignKey(e => e.TransactionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transactions>()
                .HasMany(e => e.TransactionService)
                .WithRequired(e => e.Transactions)
                .HasForeignKey(e => e.TransactionId)
                .WillCascadeOnDelete(false);
        }
    }
}
