using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeThongMuonTraSachThuVien.Configurations;
using HeThongMuonTraSachThuVien.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HeThongMuonTraSachThuVien.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public (string AccessToken, DateTime ExpiresAtUtc) GenerateAccessToken(NguoiDung nguoiDung, string tenVaiTro)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, nguoiDung.NguoiDungId.ToString()),
            new(JwtRegisteredClaimNames.Email, nguoiDung.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("hoTen", nguoiDung.HoTen),
            new(ClaimTypes.Role, tenVaiTro)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return (accessToken, expiresAtUtc);
    }
}