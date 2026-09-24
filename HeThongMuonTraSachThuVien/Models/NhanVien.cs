using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("NhanVien")]
[Index("MaNhanVien", Name = "IX_NhanVien_MaNhanVien")]
[Index("MaNhanVien", Name = "UQ_NhanVien_MaNhanVien", IsUnique = true)]
[Index("NguoiDungId", Name = "UQ_NhanVien_NguoiDungID", IsUnique = true)]
public partial class NhanVien
{
    [Key]
    [Column("NhanVienID")]
    public int NhanVienId { get; set; }

    [Column("NguoiDungID")]
    public int NguoiDungId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string MaNhanVien { get; set; } = null!;

    [StringLength(50)]
    public string ChucVu { get; set; } = null!;

    public DateOnly NgayVaoLam { get; set; }

    public bool TrangThai { get; set; }
}
