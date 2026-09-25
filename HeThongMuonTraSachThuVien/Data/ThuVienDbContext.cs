using System;
using System.Collections.Generic;
using HeThongMuonTraSachThuVien.Models;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Data;

public partial class ThuVienDbContext : DbContext
{
    public ThuVienDbContext()
    {
    }

    public ThuVienDbContext(DbContextOptions<ThuVienDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanDoc> BanDocs { get; set; }

    public virtual DbSet<ChinhSachMuon> ChinhSachMuons { get; set; }

    public virtual DbSet<LoaiThe> LoaiThes { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }
    public virtual DbSet<NhatKyHeThong> NhatKyHeThongs { get; set; }

    public virtual DbSet<TheThuVien> TheThuViens { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ThuVienDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanDoc>(entity =>
        {
            entity.Property(e => e.NgayDangKy).HasDefaultValueSql("(getdate())", "DF_BanDoc_NgayDangKy");
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_BanDoc_TrangThai");
        });

        modelBuilder.Entity<ChinhSachMuon>(entity =>
        {
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())", "DF_ChinhSachMuon_NgayTao");
            entity.Property(e => e.SoLanGiaHanToiDa).HasDefaultValue((byte)1, "DF_ChinhSachMuon_SoLanGiaHanToiDa");
            entity.Property(e => e.SoNgayMuonToiDa).HasDefaultValue((short)14, "DF_ChinhSachMuon_SoNgayMuonToiDa");
            entity.Property(e => e.SoSachToiDa).HasDefaultValue((byte)5, "DF_ChinhSachMuon_SoSachToiDa");
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_ChinhSachMuon_TrangThai");
        });

        modelBuilder.Entity<LoaiThe>(entity =>
        {
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())", "DF_LoaiThe_NgayTao");
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_LoaiThe_TrangThai");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())", "DF_NguoiDung_NgayTao");
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_NguoiDung_TrangThai");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_NhanVien_TrangThai");
        });

        modelBuilder.Entity<NhatKyHeThong>(entity =>
        {
            entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())", "DF_NhatKyHeThong_ThoiGian");
        });
        modelBuilder.Entity<TheThuVien>(entity =>
        {
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())", "DF_TheThuVien_NgayTao");
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)1, "DF_TheThuVien_TrangThai");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.Property(e => e.TrangThai).HasDefaultValue(true, "DF_VaiTro_TrangThai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
