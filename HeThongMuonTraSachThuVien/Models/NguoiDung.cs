using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("NguoiDung")]
[Index("Email", Name = "IX_NguoiDung_Email")]
[Index("TenDangNhap", Name = "IX_NguoiDung_TenDangNhap")]
[Index("VaiTroId", Name = "IX_NguoiDung_VaiTro")]
[Index("Email", Name = "UQ_NguoiDung_Email", IsUnique = true)]
[Index("TenDangNhap", Name = "UQ_NguoiDung_TenDangNhap", IsUnique = true)]
public partial class NguoiDung
{
    [Key]
    [Column("NguoiDungID")]
    public int NguoiDungId { get; set; }

    [Column("VaiTroID")]
    public int VaiTroId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TenDangNhap { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string MatKhauHash { get; set; } = null!;

    [StringLength(100)]
    public string HoTen { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string? SoDienThoai { get; set; }

    [StringLength(255)]
    public string? Avatar { get; set; }

    public bool TrangThai { get; set; }

    public DateTime NgayTao { get; set; }

    public DateTime? LanDangNhapCuoi { get; set; }
}
