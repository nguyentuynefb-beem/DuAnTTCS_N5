using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("ChinhSachMuon")]
[Index("LoaiTheId", Name = "IX_ChinhSachMuon_LoaiThe")]
[Index("NgayApDung", Name = "IX_ChinhSachMuon_NgayApDung")]
[Index("TrangThai", Name = "IX_ChinhSachMuon_TrangThai")]
public partial class ChinhSachMuon
{
    [Key]
    [Column("ChinhSachMuonID")]
    public int ChinhSachMuonId { get; set; }

    [Column("LoaiTheID")]
    public int LoaiTheId { get; set; }

    public byte SoSachToiDa { get; set; }

    public short SoNgayMuonToiDa { get; set; }

    public byte SoLanGiaHanToiDa { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MucPhatTreHanMoiNgay { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MucPhatMatSach { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MucPhatHuHong { get; set; }

    public DateOnly NgayApDung { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public bool TrangThai { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? NguoiTao { get; set; }

    public int? NguoiCapNhat { get; set; }
}
