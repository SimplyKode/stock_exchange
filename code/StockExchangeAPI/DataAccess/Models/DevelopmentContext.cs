using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class DevelopmentContext : DbContext
    {
        public DevelopmentContext()
        {
        }

        public DevelopmentContext(DbContextOptions<DevelopmentContext> options)
            : base(options)
        {
        }
      
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StockBroker> StockBrokers { get; set; } = null!;
        public virtual DbSet<StockPrice> StockPrices { get; set; } = null!;
        public virtual DbSet<StockTransaction> StockTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Development;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Symbol)
                    .HasName("PK__Stock__DF7EEB80A4C1ED3E");

                entity.ToTable("Stock");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("symbol");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<StockBroker>(entity =>
            {
                entity.ToTable("StockBroker");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<StockPrice>(entity =>
            {
                entity.HasKey(e => e.Symbol)
                    .HasName("PK__StockPri__DF7EEB8024EF3494");

                entity.ToTable("StockPrice");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("symbol");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.SymbolNavigation)
                    .WithOne(p => p.StockPrice)
                    .HasForeignKey<StockPrice>(d => d.Symbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockPric__symbo__70DDC3D8");
            });

            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.ToTable("StockTransaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Brokerid).HasColumnName("brokerid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Stocksymbol)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("stocksymbol");

                entity.Property(e => e.Unitprice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("unitprice");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.StockTransactions)
                    .HasForeignKey(d => d.Brokerid)
                    .HasConstraintName("FK__StockTran__broke__76969D2E");

                entity.HasOne(d => d.StocksymbolNavigation)
                    .WithMany(p => p.StockTransactions)
                    .HasForeignKey(d => d.Stocksymbol)
                    .HasConstraintName("FK__StockTran__stock__75A278F5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
