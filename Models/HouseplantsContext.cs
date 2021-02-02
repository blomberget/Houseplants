using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Houseplants.Models
{
    public partial class HouseplantsContext : DbContext
    {
        public HouseplantsContext()
        {
        }

        public HouseplantsContext(DbContextOptions<HouseplantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Care> Care { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<Plant> Plant { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Houseplants;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Care>(entity =>
            {
                entity.ToTable("Care");

                entity.Property(e => e.LightNeed)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NutritionNeed)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.WaterNeed)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");

                entity.HasIndex(e => e.FamilyName, "UQ__Family__3B1353D63F49CC8E")
                    .IsUnique();

                entity.HasIndex(e => e.FamilyLatin, "UQ__Family__D64B1C79B752515F")
                    .IsUnique();

                entity.Property(e => e.FamilyLatin)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToTable("Plant");

                entity.HasIndex(e => e.PlantName, "UQ__Plant__3B758BC2583338C4")
                    .IsUnique();

                entity.HasIndex(e => e.Latin, "UQ__Plant__E71B2F547CFFC7EA")
                    .IsUnique();

                entity.Property(e => e.Latin)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PlantName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CareNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.Care)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Care");

                entity.HasOne(d => d.FamilyNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.Family)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Family");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
