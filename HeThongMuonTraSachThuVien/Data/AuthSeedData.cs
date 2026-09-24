using HeThongMuonTraSachThuVien.Models;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Data;

public static class AuthSeedData
{
    // Trùng với giá trị mẫu đang để sẵn trong Views/Sprint1Lab/Index.cshtml
    public const string TaiKhoanAdminEmail = "admin@thuvien.local";
    public const string TaiKhoanAdminMatKhau = "123456";

    public static async Task SeedAdminAccountAsync(ThuVienDbContext context)
    {
        var daTonTai = await context.NguoiDungs
            .AnyAsync(x => x.Email.ToLower() == TaiKhoanAdminEmail);

        if (daTonTai)
        {
            return;
        }

        var vaiTroAdmin = await context.VaiTros
            .FirstOrDefaultAsync(x => x.TenVaiTro == "Admin");

        if (vaiTroAdmin is null)
        {
            // Chưa chạy Database/Sprint1/05_SeedData.sql thì bỏ qua, tránh lỗi khi khởi động app
            return;
        }

        var admin = new NguoiDung
        {
            VaiTroId = vaiTroAdmin.VaiTroId,
            TenDangNhap = "admin",
            MatKhauHash = BCrypt.Net.BCrypt.HashPassword(TaiKhoanAdminMatKhau),
            HoTen = "Quản trị viên hệ thống",
            Email = TaiKhoanAdminEmail,
            TrangThai = true,
            NgayTao = DateTime.Now
        };

        context.NguoiDungs.Add(admin);
        await context.SaveChangesAsync();
    }
}