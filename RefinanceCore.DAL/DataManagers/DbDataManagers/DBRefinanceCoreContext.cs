using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RefinanceCore.DAL.Models.DbModels;


namespace RefinanceCore.DAL.Models.DataManagers.DbDataManagers
{
    internal partial class DBRefinanceCoreContext : DbContext
    {
        public DBRefinanceCoreContext()
        {
            Database.EnsureCreated();
        }

        public DBRefinanceCoreContext(DbContextOptions<DBRefinanceCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbCitiy> Cities { get; set; }
        public virtual DbSet<DbContribution> Contributions { get; set; }
        public virtual DbSet<DbQuota> Quotas { get; set; }
        public virtual DbSet<DbUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBRefinanceCore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCitiy>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DbContribution>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Contributions)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contributions_Cities");
            });

            modelBuilder.Entity<DbQuota>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(1024);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Quotas)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quotas_Cities");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quotas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quotas_Users");
            });

            modelBuilder.Entity<DbUser>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_Users")
                    .IsUnique();

                entity.Property(e => e.Login).IsRequired();
            });
        }
    }
}
