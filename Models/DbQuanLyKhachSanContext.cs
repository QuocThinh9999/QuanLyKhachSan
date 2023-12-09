using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

public partial class DbQuanLyKhachSanContext : DbContext
{
    public DbQuanLyKhachSanContext()
    {
    }

    public DbQuanLyKhachSanContext(DbContextOptions<DbQuanLyKhachSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhQuangCao> AnhQuangCaos { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<ChiTietPhong> ChiTietPhongs { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<ThongTinWeb> ThongTinWebs { get; set; }

    public virtual DbSet<TuVan> TuVans { get; set; }

    public virtual DbSet<XacThuc> XacThucs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=connectString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhong>(entity =>
        {
            entity.HasOne(d => d.IdPhongNavigation).WithOne(p => p.ChiTietPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietPhong_Phong");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasOne(d => d.IdHoaDonNavigation).WithOne(p => p.DanhGium)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhGia_HoaDon");

            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.DanhGia).HasConstraintName("FK_DanhGia_NguoiDung");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.HoaDons).HasConstraintName("FK_HoaDon_NguoiDung");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.HoaDons).HasConstraintName("FK_HoaDon_Phong");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasOne(d => d.IdLoaiPhongNavigation).WithMany(p => p.Phongs).HasConstraintName("FK_Phong_LoaiPhong");
        });

        modelBuilder.Entity<TuVan>(entity =>
        {
            entity.Property(e => e.IdTuVan).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<XacThuc>(entity =>
        {
            entity.HasOne(d => d.IdNguoiDungNavigation).WithOne(p => p.XacThuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_XacThuc_NguoiDung");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
