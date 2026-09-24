using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("BanDoc")]
[Index("MaBanDoc", Name = "IX_BanDoc_MaBanDoc")]
[Index("MaBanDoc", Name = "UQ_BanDoc_MaBanDoc", IsUnique = true)]
[Index("NguoiDungId", Name = "UQ_BanDoc_NguoiDungID", IsUnique = true)]
public partial class BanDoc
{
    [Key]
    [Column("BanDocID")]
    public int BanDocId { get; set; }

    [Column("NguoiDungID")]
    public int NguoiDungId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string MaBanDoc { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public bool? GioiTinh { get; set; }

    [StringLength(255)]
    public string? DiaChi { get; set; }

    public DateOnly NgayDangKy { get; set; }

    public bool TrangThai { get; set; }
}
