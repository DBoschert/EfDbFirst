using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.Models;

public partial class SqlprojectContext : DbContext
{
    public SqlprojectContext()
    {
    }

    public SqlprojectContext(DbContextOptions<SqlprojectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestLine> RequestLines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=sqlproject;trusted_connection=true;trustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0718244E96");

            entity.HasIndex(e => e.PartNbr, "UQ__Products__DAFC0C1E05BFC25B").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PartNbr)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.Unit)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Vendor__2B3F6F97");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC07C890477F");

            entity.Property(e => e.DeliveryMode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('Pickup')");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Justification)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.RejectionReason)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('NEW')");
            entity.Property(e => e.Total).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__30F848ED");
        });

        modelBuilder.Entity<RequestLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestL__3214EC07E112D5B0");

            entity.ToTable("RequestLine");

            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Product).WithMany(p => p.RequestLines)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestLi__Produ__34C8D9D1");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestLines)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestLi__Reque__33D4B598");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC071D31B4B2");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4D6D94287").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendors__3214EC07A3BFDAE9");

            entity.HasIndex(e => e.Code, "UQ__Vendors__A25C5AA7181D4512").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
