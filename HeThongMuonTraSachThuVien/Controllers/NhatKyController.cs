using HeThongMuonTraSachThuVien.Data;
using HeThongMuonTraSachThuVien.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Controllers
{
    // S1-10: Quản trị hệ thống có thể xem nhật ký đăng nhập/hoạt động
    // (thời điểm, người thực hiện, hành động, đối tượng, địa chỉ IP).
    // Chỉ Admin được xem danh sách.
    [ApiController]
    [Route("api/[controller]")]
    public class NhatKyController : ControllerBase
    {
        private readonly ThuVienDbContext _context;

        public NhatKyController(ThuVienDbContext context)
        {
            _context = context;
        }

        public class NhatKyDto
        {
            public int NhatKyId { get; set; }
            public DateTime ThoiGian { get; set; }
            public string TenNguoiThucHien { get; set; } = null!;
            public string HanhDong { get; set; } = null!;
            public string? DoiTuong { get; set; }
            public string? DiaChiIP { get; set; }
        }

        public class TaoNhatKyRequest
        {
            public int? NguoiThucHienId { get; set; }
            public string TenNguoiThucHien { get; set; } = null!;
            public string HanhDong { get; set; } = null!;
            public string? DoiTuong { get; set; }
        }

        // GET api/NhatKy?nguoiThucHien=&hanhDong=&tuNgay=&denNgay=
        // Chỉ Quản trị hệ thống (Admin) được xem danh sách nhật ký.
        // Do hệ thống đăng nhập hiện tại còn ở mức mô phỏng (chưa có JWT thật),
        // vai trò người gọi được gửi qua header "X-Actor-Role".
        // Khi làm đăng nhập thật (JWT/Session), thay điều kiện dưới đây
        // bằng [Authorize(Roles = "Admin")] cho chuẩn.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhatKyDto>>> GetAll(
            [FromQuery] string? nguoiThucHien,
            [FromQuery] string? hanhDong,
            [FromQuery] DateOnly? tuNgay,
            [FromQuery] DateOnly? denNgay)
        {
            var actorRole = Request.Headers["X-Actor-Role"].ToString();
            if (!string.Equals(actorRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(403, "Chỉ Quản trị hệ thống (Admin) được phép truy cập nhật ký hoạt động.");
            }

            var query = _context.NhatKyHeThongs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nguoiThucHien))
            {
                var keyword = nguoiThucHien.Trim();
                query = query.Where(n => EF.Functions.Like(n.TenNguoiThucHien, $"%{keyword}%"));
            }

            if (!string.IsNullOrWhiteSpace(hanhDong))
            {
                query = query.Where(n => n.HanhDong == hanhDong);
            }

            if (tuNgay.HasValue)
            {
                var tu = tuNgay.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(n => n.ThoiGian >= tu);
            }

            if (denNgay.HasValue)
            {
                var den = denNgay.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where(n => n.ThoiGian <= den);
            }

            var result = await query
                .OrderByDescending(n => n.ThoiGian)
                .Select(n => new NhatKyDto
                {
                    NhatKyId = n.NhatKyId,
                    ThoiGian = n.ThoiGian,
                    TenNguoiThucHien = n.TenNguoiThucHien,
                    HanhDong = n.HanhDong,
                    DoiTuong = n.DoiTuong,
                    DiaChiIP = n.DiaChiIP
                })
                .ToListAsync();

            return Ok(result);
        }

        // POST api/NhatKy
        // Ghi 1 dòng nhật ký mới. Địa chỉ IP do máy chủ tự lấy từ request,
        // không nhận từ client (tránh giả mạo).
        [HttpPost]
        public async Task<ActionResult<NhatKyDto>> Create([FromBody] TaoNhatKyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TenNguoiThucHien) || string.IsNullOrWhiteSpace(request.HanhDong))
            {
                return BadRequest("Thiếu thông tin người thực hiện hoặc hành động.");
            }

            var ip = HttpContext.Connection.RemoteIpAddress;
            if (ip != null && ip.IsIPv4MappedToIPv6)
            {
                ip = ip.MapToIPv4();
            }

            var log = new NhatKyHeThong
            {
                NguoiThucHienId = request.NguoiThucHienId,
                TenNguoiThucHien = request.TenNguoiThucHien.Trim(),
                HanhDong = request.HanhDong.Trim(),
                DoiTuong = request.DoiTuong?.Trim(),
                DiaChiIP = ip?.ToString(),
                ThoiGian = DateTime.Now
            };

            _context.NhatKyHeThongs.Add(log);
            await _context.SaveChangesAsync();

            var dto = new NhatKyDto
            {
                NhatKyId = log.NhatKyId,
                ThoiGian = log.ThoiGian,
                TenNguoiThucHien = log.TenNguoiThucHien,
                HanhDong = log.HanhDong,
                DoiTuong = log.DoiTuong,
                DiaChiIP = log.DiaChiIP
            };

            return CreatedAtAction(nameof(GetAll), new { id = log.NhatKyId }, dto);
        }
    }
}