using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AuthService
{
    public partial class DiagnosticLabContext : DbContext
    {
        public DiagnosticLabContext()
        {
        }

        public DiagnosticLabContext(DbContextOptions<DiagnosticLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestAppointment> TestAppointments { get; set; }
        public virtual DbSet<TestResultsTab> TestResultsTabs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6LVD3EJ\\MSSQLSERVER17;Initial Catalog=DiagnosticLab;User ID=sa;Password=admin123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestAppointment>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.ToTable("TestAppointment");

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TestResultsTab>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("TestResultsTab");

                entity.Property(e => e.TestId).ValueGeneratedNever();

                entity.Property(e => e.Result)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("User");

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
