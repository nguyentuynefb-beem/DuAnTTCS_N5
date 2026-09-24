using System.ComponentModel.DataAnnotations;

namespace HeThongMuonTraSachThuVien.Models.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    public string MatKhau { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public UserInfo NguoiDung { get; set; } = null!;
}

public class UserInfo
{
    public int NguoiDungId { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string VaiTro { get; set; } = string.Empty;
}