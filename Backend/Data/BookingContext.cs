using System.Security.Cryptography.X509Certificates;
using Contracts.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BookingContext: DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

            
        }

        public DbSet<GeographicBoundary> GeographicBoundaries { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelFacility> HotelFacilities { get; set; }
        public DbSet<HotelRoomPriceAvailable> HotelRoomPrices { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<Sleep> Sleeps { get; set; }
        public DbSet<DiscountOnCount> Discounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeographicBoundary>().HasKey(g=>g.Id);
            modelBuilder.Entity<GeographicBoundary>().HasOne(x => x.Parent).WithOne(x => x.Child).IsRequired(false)
                .HasForeignKey<GeographicBoundary>(c => c.ParentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Neighbourhood>().HasKey(n => n.Id);
            modelBuilder.Entity<Neighbourhood>().HasOne(h => h.GeographicBoundary).WithMany(x => x.Neighbourhoods)
                .HasForeignKey(x => x.GeographicBoundaryId);

            modelBuilder.Entity<Hotel>().HasKey(h => h.Id);
            modelBuilder.Entity<Hotel>().HasOne(h => h.GeographicBoundary).WithMany(x => x.Hotels)
                .HasForeignKey(x => x.GeographicBoundaryId);
            modelBuilder.Entity<Neighbourhood>().HasOne(h => h.GeographicBoundary).WithMany(x => x.Neighbourhoods)
                .HasForeignKey(x => x.GeographicBoundaryId);
            modelBuilder.Entity<Hotel>().HasMany(x => x.Facilities).WithOne(x => x.Hotel).HasForeignKey(x=>x.HotelId);
            modelBuilder.Entity<Hotel>().HasOne(h => h.PaymentCurrency).WithMany(x => x.Hotels)
                .HasForeignKey(x => x.PaymentCurrencyId);

            modelBuilder.Entity<Facility>().HasKey(x => x.Id);

            modelBuilder.Entity<HotelFacility>().HasKey(x => x.Id);
            modelBuilder.Entity<RoomFacility>().HasKey(x => x.Id);

            modelBuilder.Entity<HotelRoomPriceAvailable>().HasKey(x => x.Id);
            modelBuilder.Entity<HotelRoomPriceAvailable>().HasOne(x => x.Hotel).WithMany(x => x.Rooms)
                .HasForeignKey(x => x.HotelId);
            modelBuilder.Entity<HotelRoomPriceAvailable>().HasMany(x => x.Discounts).WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<HotelRoomPriceAvailable>().HasMany(x => x.Sleeps).WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<HotelRoomPriceAvailable>().HasMany(x => x.Facilities).WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<HotelRoomPriceAvailable>().HasMany(x => x.CancellationPolicies).WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            modelBuilder.Entity<DiscountOnCount>().HasKey(x => x.Id);
            modelBuilder.Entity<Sleep>().HasKey(x => x.Id);
            modelBuilder.Entity<CancellationPolicy>().HasKey(x => x.Id);

            modelBuilder.Entity<Currency>().HasKey(x => x.Id);
            
            foreach(var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(25);
                property.SetScale(10);
            }

        }
    }
}