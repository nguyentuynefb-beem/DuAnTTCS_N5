using HeThongMuonTraSachThuVien.Data;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Services;

public class AuthService : IAuthService
{
    // TODO (PO): chốt lại nội dung chính xác của thông báo lỗi chung này (story S1-01)
    public const string ThongBaoLoiDangNhap = "Email hoặc mật khẩu không đúng.";

    private readonly ThuVienDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(ThuVienDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResult> LoginAsync(string email, string matKhau, CancellationToken ct = default)
    {
        var emailChuanHoa = (email ?? string.Empty).Trim().ToLowerInvariant();

        var nguoiDung = await _context.NguoiDungs
            .FirstOrDefaultAsync(x => x.Email.ToLower() == emailChuanHoa, ct);

        // Không tìm thấy tài khoản, hoặc tài khoản đang bị khoá/vô hiệu hoá
        if (nguoiDung is null || !nguoiDung.TrangThai)
        {
            return AuthResult.ThatBai();
        }

        bool matKhauDung;
        try
        {
            matKhauDung = BCrypt.Net.BCrypt.Verify(matKhau, nguoiDung.MatKhauHash);
        }
        catch
        {
            // Hash lưu sai định dạng thì coi như sai mật khẩu, không để lộ lỗi 500 ra ngoài
            matKhauDung = false;
        }

        if (!matKhauDung)
        {
            return AuthResult.ThatBai();
        }

        var vaiTro = await _context.VaiTros
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.VaiTroId == nguoiDung.VaiTroId, ct);

        var tenVaiTro = vaiTro?.TenVaiTro ?? "KhongXacDinh";

        var (accessToken, expiresAtUtc) = _jwtTokenService.GenerateAccessToken(nguoiDung, tenVaiTro);

        nguoiDung.LanDangNhapCuoi = DateTime.Now;
        await _context.SaveChangesAsync(ct);

        return AuthResult.ThanhCongVoi(accessToken, expiresAtUtc, nguoiDung, tenVaiTro);
    }
}