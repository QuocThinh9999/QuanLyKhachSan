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
        modelBuilder.Entity<TuVan>(entity =>
        {
            entity.Property(e => e.IdTuVan).HasDefaultValueSql("(N'')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
