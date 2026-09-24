using HeThongMuonTraSachThuVien.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongMuonTraSachThuVien.Controllers
{
    public class DbCheckController : Controller
    {
        private readonly ThuVienDbContext _context;

        public DbCheckController(ThuVienDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var conn = _context.Database.GetDbConnection();

                await conn.OpenAsync();

                var html = $@"
                    <h2>Database Connection Check</h2>
                    <p><b>DataSource:</b> {conn.DataSource}</p>
                    <p><b>Database:</b> {conn.Database}</p>
                    <p><b>State:</b> {conn.State}</p>
                ";

                await conn.CloseAsync();

                return Content(html, "text/html");
            }
            catch (Exception ex)
            {
                return Content($@"
                    <h2 style='color:red'>Connection failed</h2>
                    <pre>{ex}</pre>
                ", "text/html");
            }
        }
    }
}