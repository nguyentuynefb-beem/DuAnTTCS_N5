using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Models;

[Table("VaiTro")]
[Index("TenVaiTro", Name = "IX_VaiTro_TenVaiTro")]
[Index("TenVaiTro", Name = "UQ_VaiTro_TenVaiTro", IsUnique = true)]
public partial class VaiTro
{
    [Key]
    [Column("VaiTroID")]
    public int VaiTroId { get; set; }

    [StringLength(50)]
    public string TenVaiTro { get; set; } = null!;

    [StringLength(255)]
    public string? MoTa { get; set; }

    public bool TrangThai { get; set; }
}
