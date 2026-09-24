using HeThongMuonTraSachThuVien.Models;

namespace HeThongMuonTraSachThuVien.Services;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string matKhau, CancellationToken ct = default);
}

public class AuthResult
{
    public bool ThanhCong { get; private set; }
    public string? AccessToken { get; private set; }
    public DateTime? ExpiresAtUtc { get; private set; }
    public NguoiDung? NguoiDung { get; private set; }
    public string? TenVaiTro { get; private set; }

    public static AuthResult ThatBai() => new() { ThanhCong = false };

    public static AuthResult ThanhCongVoi(string accessToken, DateTime expiresAtUtc, NguoiDung nguoiDung, string tenVaiTro)
        => new()
        {
            ThanhCong = true,
            AccessToken = accessToken,
            ExpiresAtUtc = expiresAtUtc,
            NguoiDung = nguoiDung,
            TenVaiTro = tenVaiTro
        };
}