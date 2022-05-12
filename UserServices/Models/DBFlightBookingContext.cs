using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserServices.Models
{
    public partial class DBFlightBookingContext : DbContext
    {
        public DBFlightBookingContext()
        {
        }

        public DBFlightBookingContext(DbContextOptions<DBFlightBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airline { get; set; }
        public virtual DbSet<BookingDetails> BookingDetails { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Passanger> Passanger { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<SeatNumber> SeatNumber { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=DBFlightBooking;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookingDetails>(entity =>
            {
                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JourneyDate).HasColumnType("datetime");

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.CouponCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Passanger>(entity =>
            {
                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pnr).HasColumnName("PNR");

                entity.Property(e => e.SeatNo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Bcprice).HasColumnName("BCPrice");

                entity.Property(e => e.Bcseats).HasColumnName("BCSeats");

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Meal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nbcprice).HasColumnName("NBCPrice");

                entity.Property(e => e.Nbcseats).HasColumnName("NBCSeats");

                entity.Property(e => e.ScheduledDays)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SeatNumber>(entity =>
            {
                entity.Property(e => e.SeatNumbers)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
