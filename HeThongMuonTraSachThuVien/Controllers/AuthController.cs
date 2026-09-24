using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HeThongMuonTraSachThuVien.Models.Auth;
using HeThongMuonTraSachThuVien.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeThongMuonTraSachThuVien.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// S1-01: Đăng nhập bằng email/mật khẩu, trả về access token (hiệu lực 30 phút).
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(request.Email, request.MatKhau, ct);

        if (!result.ThanhCong)
        {
            // Thông báo lỗi CHUNG - không tiết lộ sai ở email hay mật khẩu
            return Unauthorized(new { message = AuthService.ThongBaoLoiDangNhap });
        }

        var response = new LoginResponse
        {
            AccessToken = result.AccessToken!,
            ExpiresAtUtc = result.ExpiresAtUtc!.Value,
            NguoiDung = new UserInfo
            {
                NguoiDungId = result.NguoiDung!.NguoiDungId,
                HoTen = result.NguoiDung.HoTen,
                Email = result.NguoiDung.Email,
                VaiTro = result.TenVaiTro!
            }
        };

        return Ok(response);
    }

    /// Endpoint để test access token (cần Bearer token hợp lệ) - dùng để kiểm tra case token hết hạn.
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var nguoiDungId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
        var hoTen = User.FindFirst("hoTen")?.Value;
        var vaiTro = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new { nguoiDungId, email, hoTen, vaiTro });
    }
}