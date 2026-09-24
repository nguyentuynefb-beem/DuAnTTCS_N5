using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("NhatKyHeThong")]
[Index("ThoiGian", Name = "IX_NhatKyHeThong_ThoiGian")]
[Index("TenNguoiThucHien", Name = "IX_NhatKyHeThong_TenNguoiThucHien")]
[Index("HanhDong", Name = "IX_NhatKyHeThong_HanhDong")]
public partial class NhatKyHeThong
{
    [Key]
    [Column("NhatKyID")]
    public int NhatKyId { get; set; }

    [Column("NguoiThucHienID")]
    public int? NguoiThucHienId { get; set; }

    [StringLength(100)]
    public string TenNguoiThucHien { get; set; } = null!;

    [StringLength(50)]
    public string HanhDong { get; set; } = null!;

    [StringLength(255)]
    public string? DoiTuong { get; set; }

    [StringLength(45)]
    public string? DiaChiIP { get; set; }

    public DateTime ThoiGian { get; set; }
}