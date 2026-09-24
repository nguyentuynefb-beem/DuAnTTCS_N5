using HeThongMuonTraSachThuVien.Models;

namespace HeThongMuonTraSachThuVien.Services;

public interface IJwtTokenService
{
    (string AccessToken, DateTime ExpiresAtUtc) GenerateAccessToken(NguoiDung nguoiDung, string tenVaiTro);
}