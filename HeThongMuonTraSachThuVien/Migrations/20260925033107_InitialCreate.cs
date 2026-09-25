using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongMuonTraSachThuVien.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanDoc",
                columns: table => new
                {
                    BanDocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    MaBanDoc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayDangKy = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_BanDoc_NgayDangKy"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_BanDoc_TrangThai")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanDoc", x => x.BanDocID);
                });

            migrationBuilder.CreateTable(
                name: "ChinhSachMuon",
                columns: table => new
                {
                    ChinhSachMuonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiTheID = table.Column<int>(type: "int", nullable: false),
                    SoSachToiDa = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)5)
                        .Annotation("Relational:DefaultConstraintName", "DF_ChinhSachMuon_SoSachToiDa"),
                    SoNgayMuonToiDa = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)14)
                        .Annotation("Relational:DefaultConstraintName", "DF_ChinhSachMuon_SoNgayMuonToiDa"),
                    SoLanGiaHanToiDa = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
                        .Annotation("Relational:DefaultConstraintName", "DF_ChinhSachMuon_SoLanGiaHanToiDa"),
                    MucPhatTreHanMoiNgay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MucPhatMatSach = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MucPhatHuHong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayApDung = table.Column<DateOnly>(type: "date", nullable: false),
                    NgayKetThuc = table.Column<DateOnly>(type: "date", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_ChinhSachMuon_TrangThai"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_ChinhSachMuon_NgayTao"),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    NguoiCapNhat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChinhSachMuon", x => x.ChinhSachMuonID);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThe",
                columns: table => new
                {
                    LoaiTheID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiThe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhiDangKy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_LoaiThe_TrangThai"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_LoaiThe_NgayTao"),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    NguoiCapNhat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThe", x => x.LoaiTheID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaiTroID = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhauHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_NguoiDung_TrangThai"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_NguoiDung_NgayTao"),
                    LanDangNhapCuoi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.NguoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    NhanVienID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    MaNhanVien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayVaoLam = table.Column<DateOnly>(type: "date", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_NhanVien_TrangThai")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.NhanVienID);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyHeThong",
                columns: table => new
                {
                    NhatKyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiThucHienID = table.Column<int>(type: "int", nullable: true),
                    TenNguoiThucHien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HanhDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoiTuong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DiaChiIP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_NhatKyHeThong_ThoiGian")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyHeThong", x => x.NhatKyID);
                });

            migrationBuilder.CreateTable(
                name: "TheThuVien",
                columns: table => new
                {
                    TheThuVienID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanDocID = table.Column<int>(type: "int", nullable: false),
                    LoaiTheID = table.Column<int>(type: "int", nullable: false),
                    MaThe = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NgayCap = table.Column<DateOnly>(type: "date", nullable: false),
                    NgayHetHan = table.Column<DateOnly>(type: "date", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
                        .Annotation("Relational:DefaultConstraintName", "DF_TheThuVien_TrangThai"),
                    LyDoKhoa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                        .Annotation("Relational:DefaultConstraintName", "DF_TheThuVien_NgayTao"),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    NguoiCapNhat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheThuVien", x => x.TheThuVienID);
                });

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    VaiTroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("Relational:DefaultConstraintName", "DF_VaiTro_TrangThai")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.VaiTroID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanDoc_MaBanDoc",
                table: "BanDoc",
                column: "MaBanDoc");

            migrationBuilder.CreateIndex(
                name: "UQ_BanDoc_MaBanDoc",
                table: "BanDoc",
                column: "MaBanDoc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BanDoc_NguoiDungID",
                table: "BanDoc",
                column: "NguoiDungID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChinhSachMuon_LoaiThe",
                table: "ChinhSachMuon",
                column: "LoaiTheID");

            migrationBuilder.CreateIndex(
                name: "IX_ChinhSachMuon_NgayApDung",
                table: "ChinhSachMuon",
                column: "NgayApDung");

            migrationBuilder.CreateIndex(
                name: "IX_ChinhSachMuon_TrangThai",
                table: "ChinhSachMuon",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiThe_TenLoaiThe",
                table: "LoaiThe",
                column: "TenLoaiThe");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiThe_TrangThai",
                table: "LoaiThe",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "UQ_LoaiThe_TenLoaiThe",
                table: "LoaiThe",
                column: "TenLoaiThe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_Email",
                table: "NguoiDung",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_TenDangNhap",
                table: "NguoiDung",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_VaiTro",
                table: "NguoiDung",
                column: "VaiTroID");

            migrationBuilder.CreateIndex(
                name: "UQ_NguoiDung_Email",
                table: "NguoiDung",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_NguoiDung_TenDangNhap",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaNhanVien",
                table: "NhanVien",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "UQ_NhanVien_MaNhanVien",
                table: "NhanVien",
                column: "MaNhanVien",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_NhanVien_NguoiDungID",
                table: "NhanVien",
                column: "NguoiDungID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyHeThong_HanhDong",
                table: "NhatKyHeThong",
                column: "HanhDong");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyHeThong_TenNguoiThucHien",
                table: "NhatKyHeThong",
                column: "TenNguoiThucHien");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyHeThong_ThoiGian",
                table: "NhatKyHeThong",
                column: "ThoiGian");

            migrationBuilder.CreateIndex(
                name: "IX_TheThuVien_BanDoc",
                table: "TheThuVien",
                column: "BanDocID");

            migrationBuilder.CreateIndex(
                name: "IX_TheThuVien_MaThe",
                table: "TheThuVien",
                column: "MaThe");

            migrationBuilder.CreateIndex(
                name: "IX_TheThuVien_TrangThai",
                table: "TheThuVien",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "UQ_TheThuVien_MaThe",
                table: "TheThuVien",
                column: "MaThe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaiTro_TenVaiTro",
                table: "VaiTro",
                column: "TenVaiTro");

            migrationBuilder.CreateIndex(
                name: "UQ_VaiTro_TenVaiTro",
                table: "VaiTro",
                column: "TenVaiTro",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanDoc");

            migrationBuilder.DropTable(
                name: "ChinhSachMuon");

            migrationBuilder.DropTable(
                name: "LoaiThe");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NhatKyHeThong");

            migrationBuilder.DropTable(
                name: "TheThuVien");

            migrationBuilder.DropTable(
                name: "VaiTro");
        }
    }
}
