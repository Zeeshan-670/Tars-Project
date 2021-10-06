using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Semester_End.Models
{
    public partial class TarsDeliveryContext : IdentityDbContext
    {
        public TarsDeliveryContext()
        {
        }

        public TarsDeliveryContext(DbContextOptions<TarsDeliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Mailing> Mailings { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Tars-Delivery;Integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Branch1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Branch");

                entity.Property(e => e.EndTime).HasMaxLength(10);

                entity.Property(e => e.StartTime).HasMaxLength(10);

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.City)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Branches__City__2E1BDC42");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ContactNO");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ContactNO");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<Mailing>(entity =>
            {
                entity.ToTable("Mailing");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.CashOnDelivery).HasColumnType("money");

                entity.Property(e => e.PaymentMethod1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PaymentMethod");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.HasIndex(e => e.TrackingId)
                    .IsUnique();

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Charges).HasColumnType("money");

                entity.Property(e => e.DateShipped).HasColumnType("date");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(150);

                entity.Property(e => e.DeliveryStatus).HasMaxLength(10);

                entity.Property(e => e.OrderStatus).HasMaxLength(10);

                entity.Property(e => e.SenderAddress).HasMaxLength(150);

                entity.Property(e => e.TimeShipped).HasColumnType("datetime");

                entity.Property(e => e.TrackingId)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Weight)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shipment__Branch__35BCFE0A");

                entity.HasOne(d => d.PaymentNavigation)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.Payment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shipment__Paymen__37A5467C");

                entity.HasOne(d => d.ServicesTypeNavigation)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.ServicesType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shipment__Servic__36B12243");
            });

            modelBuilder.Entity<ShipmentDetail>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ShipmentDetails)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShipmentD__Branc__3B75D760");

                entity.HasOne(d => d.ChargesNavigation)
                    .WithMany(p => p.ShipmentDetailChargesNavigations)
                    .HasForeignKey(d => d.Charges)
                    .HasConstraintName("FK__ShipmentD__Charg__3D5E1FD2");

                entity.HasOne(d => d.DeliveryAddressNavigation)
                    .WithMany(p => p.ShipmentDetailDeliveryAddressNavigations)
                    .HasForeignKey(d => d.DeliveryAddress)
                    .HasConstraintName("FK__ShipmentD__Deliv__3C69FB99");

                entity.HasOne(d => d.Tracking)
                    .WithMany(p => p.ShipmentDetailTrackings)
                    .HasForeignKey(d => d.TrackingId)
                    .HasConstraintName("FK__ShipmentD__Track__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
