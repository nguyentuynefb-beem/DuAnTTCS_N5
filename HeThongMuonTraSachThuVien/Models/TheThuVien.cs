using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("TheThuVien")]
[Index("BanDocId", Name = "IX_TheThuVien_BanDoc")]
[Index("MaThe", Name = "IX_TheThuVien_MaThe")]
[Index("TrangThai", Name = "IX_TheThuVien_TrangThai")]
[Index("MaThe", Name = "UQ_TheThuVien_MaThe", IsUnique = true)]
public partial class TheThuVien
{
    [Key]
    [Column("TheThuVienID")]
    public int TheThuVienId { get; set; }

    [Column("BanDocID")]
    public int BanDocId { get; set; }

    [Column("LoaiTheID")]
    public int LoaiTheId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string MaThe { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly NgayHetHan { get; set; }

    public byte TrangThai { get; set; }

    [StringLength(300)]
    public string? LyDoKhoa { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? NguoiTao { get; set; }

    public int? NguoiCapNhat { get; set; }
}
