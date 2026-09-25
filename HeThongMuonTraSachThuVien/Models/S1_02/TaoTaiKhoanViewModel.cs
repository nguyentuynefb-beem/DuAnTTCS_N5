using System.ComponentModel.DataAnnotations;

namespace HeThongMuonTraSachThuVien.Models.S1_02
{
    public class TaoTaiKhoanViewModel
    {
        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
        public int VaiTroId { get; set; }

        public bool TrangThai { get; set; }
    }
}