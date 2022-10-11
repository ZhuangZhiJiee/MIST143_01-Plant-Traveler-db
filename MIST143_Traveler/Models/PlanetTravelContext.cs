using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class PlanetTravelContext : DbContext
    {
        public PlanetTravelContext()
        {
        }

        public PlanetTravelContext(DbContextOptions<PlanetTravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminStatus> AdminStatuses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberMessage> MemberMessages { get; set; }
        public virtual DbSet<MemberStatus> MemberStatuses { get; set; }
        public virtual DbSet<Myfavorite> Myfavorites { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ProductToTransportation> ProductToTransportations { get; set; }
        public virtual DbSet<Trasportation> Trasportations { get; set; }
        public virtual DbSet<TravelPicture> TravelPictures { get; set; }
        public virtual DbSet<TravelProduct> TravelProducts { get; set; }
        public virtual DbSet<TravelProductType> TravelProductTypes { get; set; }
        public virtual DbSet<View> Views { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PlanetTravel;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AdminStatus>(entity =>
            {
                entity.ToTable("AdminStatus");

                entity.Property(e => e.AdminStatusId).HasColumnName("AdminStatusID");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminStatuses)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminStatus_Admin");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Discount).HasColumnType("numeric(3, 2)");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_City");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MembersId);

                entity.Property(e => e.MembersId).HasColumnName("MembersID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberStatusId).HasColumnName("MemberStatusID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_City");

                entity.HasOne(d => d.MemberStatus)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.MemberStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_MemberStatus");
            });

            modelBuilder.Entity<MemberMessage>(entity =>
            {
                entity.ToTable("MemberMessage");

                entity.Property(e => e.MemberMessageId).HasColumnName("MemberMessageID");

                entity.Property(e => e.MemberMessageData).IsRequired();

                entity.Property(e => e.MembersId).HasColumnName("MembersID");

                entity.HasOne(d => d.Members)
                    .WithMany(p => p.MemberMessages)
                    .HasForeignKey(d => d.MembersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberMessage_Members");
            });

            modelBuilder.Entity<MemberStatus>(entity =>
            {
                entity.ToTable("MemberStatus");

                entity.Property(e => e.MemberStatusId).HasColumnName("MemberStatusID");

                entity.Property(e => e.MemberStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Myfavorite>(entity =>
            {
                entity.HasKey(e => e.MyfavoritesId);

                entity.Property(e => e.MyfavoritesId)
                    .ValueGeneratedNever()
                    .HasColumnName("MyfavoritesID");

                entity.Property(e => e.MembersId).HasColumnName("MembersID");

                entity.Property(e => e.TravelProductId).HasColumnName("TravelProductID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Members");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderDetail");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatus");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TravelProductId).HasColumnName("TravelProductID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Coupon");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Payment");

                entity.HasOne(d => d.TravelProduct)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.TravelProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_TravelProduct");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PaymentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductToTransportation>(entity =>
            {
                entity.ToTable("ProductToTransportation");

                entity.Property(e => e.ProductToTransportationId).HasColumnName("ProductToTransportationID");

                entity.Property(e => e.TrasportationId).HasColumnName("TrasportationID");

                entity.Property(e => e.TravelProductId).HasColumnName("TravelProductID");

                entity.HasOne(d => d.Trasportation)
                    .WithMany(p => p.ProductToTransportations)
                    .HasForeignKey(d => d.TrasportationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductToTransportation_Trasportation");

                entity.HasOne(d => d.TravelProduct)
                    .WithMany(p => p.ProductToTransportations)
                    .HasForeignKey(d => d.TravelProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductToTransportation_TravelProduct");
            });

            modelBuilder.Entity<Trasportation>(entity =>
            {
                entity.ToTable("Trasportation");

                entity.Property(e => e.TrasportationId).HasColumnName("TrasportationID");

                entity.Property(e => e.TrasportationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TravelPicture>(entity =>
            {
                entity.ToTable("TravelPicture");

                entity.Property(e => e.TravelPictureId).HasColumnName("TravelPictureID");

                entity.Property(e => e.TravelPicture1).HasColumnName("TravelPicture");

                entity.Property(e => e.TravelProductId).HasColumnName("TravelProductID");

                entity.HasOne(d => d.TravelProduct)
                    .WithMany(p => p.TravelPictures)
                    .HasForeignKey(d => d.TravelProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelPicture_TravelProduct");
            });

            modelBuilder.Entity<TravelProduct>(entity =>
            {
                entity.ToTable("TravelProduct");

                entity.Property(e => e.TravelProductId).HasColumnName("TravelProductID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TravelProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TravelProductTypeId).HasColumnName("TravelProductTypeID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TravelProducts)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelProduct_Country");

                entity.HasOne(d => d.TravelProductType)
                    .WithMany(p => p.TravelProducts)
                    .HasForeignKey(d => d.TravelProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelProduct_TravelProductType");
            });

            modelBuilder.Entity<TravelProductType>(entity =>
            {
                entity.ToTable("TravelProductType");

                entity.Property(e => e.TravelProductTypeId).HasColumnName("TravelProductTypeID");

                entity.Property(e => e.TravelProductTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.ToTable("View");

                entity.Property(e => e.ViewId).HasColumnName("ViewID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.ViewName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_View_City");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
