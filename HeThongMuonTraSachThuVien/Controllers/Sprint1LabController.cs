using HeThongMuonTraSachThuVien.Data;
using HeThongMuonTraSachThuVien.Models;
using HeThongMuonTraSachThuVien.Models.S1_02;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Controllers
{
    public class Sprint1LabController : Controller
    {
        private readonly ThuVienDbContext _context;

        public Sprint1LabController(ThuVienDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVaiTro()
        {
            var vaiTros = await _context.VaiTros
                .Where(v =>
                    v.TrangThai &&
                    (v.TenVaiTro == "Admin" ||
                     v.TenVaiTro == "QuanLy" ||
                     v.TenVaiTro == "ThuThu"))
                .OrderBy(v => v.TenVaiTro)
                .Select(v => new
                {
                    id = v.VaiTroId,
                    ten = v.TenVaiTro
                })
                .ToListAsync();

            return Ok(vaiTros);
        }
        [HttpGet]
        public async Task<IActionResult> GetTaiKhoan()
        {
            var taiKhoans = await (
                from nguoiDung in _context.NguoiDungs
                join vaiTro in _context.VaiTros
                    on nguoiDung.VaiTroId equals vaiTro.VaiTroId
                where vaiTro.TenVaiTro == "Admin"
                   || vaiTro.TenVaiTro == "QuanLy"
                   || vaiTro.TenVaiTro == "ThuThu"
                orderby nguoiDung.NguoiDungId descending
                select new
                {
                    id = nguoiDung.NguoiDungId,
                    hoTen = nguoiDung.HoTen,
                    email = nguoiDung.Email,
                    soDienThoai = nguoiDung.SoDienThoai,
                    vaiTroId = nguoiDung.VaiTroId,
                    vaiTro = vaiTro.TenVaiTro,
                    trangThai = nguoiDung.TrangThai
                }
            ).ToListAsync();

            return Ok(taiKhoans);
        }

        [HttpPost]
        public async Task<IActionResult> TaoTaiKhoan([FromBody] TaoTaiKhoanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Thông tin tài khoản không hợp lệ.",
                    errors = ModelState
                        .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value!.Errors
                                .Select(e => e.ErrorMessage)
                                .ToArray()
                        )
                });
            }

            var vaiTro = await _context.VaiTros
                .FirstOrDefaultAsync(v =>
                    v.VaiTroId == model.VaiTroId &&
                    v.TrangThai);

            if (vaiTro == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Vai trò không hợp lệ."
                });
            }

            var taiKhoan = new NguoiDung
            {
                HoTen = model.HoTen.Trim(),
                Email = model.Email.Trim(),
                SoDienThoai = string.IsNullOrWhiteSpace(model.SoDienThoai)
                    ? null
                    : model.SoDienThoai.Trim(),

                VaiTroId = vaiTro.VaiTroId,

                // Tạm sinh tên đăng nhập từ email.
                // Chức năng email đặt mật khẩu lần đầu sẽ làm ở Story khác.
                TenDangNhap = model.Email.Trim(),

                // Mật khẩu tạm thời chỉ để đáp ứng cấu trúc DB.
                // Không hiển thị cho người dùng ở S1-02.1.
                MatKhauHash = new PasswordHasher<NguoiDung>()
                .HashPassword(null!, Guid.NewGuid().ToString()),

                TrangThai = model.TrangThai,
                NgayTao = DateTime.Now
            };

            _context.NguoiDungs.Add(taiKhoan);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Tạo tài khoản thành công.",
                data = new
                {
                    id = taiKhoan.NguoiDungId,
                    hoTen = taiKhoan.HoTen,
                    email = taiKhoan.Email,
                    soDienThoai = taiKhoan.SoDienThoai,
                    vaiTroId = taiKhoan.VaiTroId,
                    vaiTro = vaiTro.TenVaiTro,
                    trangThai = taiKhoan.TrangThai
                }
            });
        }
    }
}