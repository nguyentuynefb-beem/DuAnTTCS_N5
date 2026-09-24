using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("LoaiThe")]
[Index("TenLoaiThe", Name = "IX_LoaiThe_TenLoaiThe")]
[Index("TrangThai", Name = "IX_LoaiThe_TrangThai")]
[Index("TenLoaiThe", Name = "UQ_LoaiThe_TenLoaiThe", IsUnique = true)]
public partial class LoaiThe
{
    [Key]
    [Column("LoaiTheID")]
    public int LoaiTheId { get; set; }

    [StringLength(100)]
    public string TenLoaiThe { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PhiDangKy { get; set; }

    [StringLength(500)]
    public string? MoTa { get; set; }

    public bool TrangThai { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? NguoiTao { get; set; }

    public int? NguoiCapNhat { get; set; }
}
