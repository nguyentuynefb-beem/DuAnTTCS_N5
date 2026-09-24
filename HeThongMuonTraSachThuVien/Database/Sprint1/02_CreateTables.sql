USE ThuVienDB;
GO

/*==============================================================*/
/* File: 02_CreateTables.sql                                    */
/* Sprint: 1                                                    */
/* Mô tả: Tạo toàn bộ bảng của Sprint 1                         */
/* Lưu ý:                                                       */
/* - Chỉ tạo bảng                                               */
/* - Chưa tạo PK, FK, UNIQUE, CHECK, DEFAULT, INDEX             */
/*==============================================================*/


/*==============================================================*/
/* TABLE: VaiTro                                                */
/*==============================================================*/
CREATE TABLE VaiTro
(
    VaiTroID           INT IDENTITY(1,1) NOT NULL,

    TenVaiTro          NVARCHAR(50)      NOT NULL,
    MoTa               NVARCHAR(255)     NULL,
    TrangThai          BIT               NOT NULL
);
GO


/*==============================================================*/
/* TABLE: NguoiDung                                             */
/*==============================================================*/
CREATE TABLE NguoiDung
(
    NguoiDungID        INT IDENTITY(1,1) NOT NULL,

    VaiTroID           INT               NOT NULL,

    TenDangNhap        VARCHAR(50)       NOT NULL,
    MatKhauHash        VARCHAR(255)      NOT NULL,

    HoTen              NVARCHAR(100)     NOT NULL,
    Email              VARCHAR(100)      NOT NULL,
    SoDienThoai        VARCHAR(15)       NULL,
    Avatar             NVARCHAR(255)     NULL,

    TrangThai          BIT               NOT NULL,

    NgayTao            DATETIME2         NOT NULL,
    LanDangNhapCuoi    DATETIME2         NULL
);
GO


/*==============================================================*/
/* TABLE: BanDoc                                                */
/*==============================================================*/
CREATE TABLE BanDoc
(
    BanDocID           INT IDENTITY(1,1) NOT NULL,

    NguoiDungID        INT               NOT NULL,

    MaBanDoc           VARCHAR(20)       NOT NULL,

    NgaySinh           DATE              NULL,
    GioiTinh           BIT               NULL,
    DiaChi             NVARCHAR(255)     NULL,

    NgayDangKy         DATE              NOT NULL,
    TrangThai          BIT               NOT NULL
);
GO


/*==============================================================*/
/* TABLE: NhanVien                                              */
/*==============================================================*/
CREATE TABLE NhanVien
(
    NhanVienID         INT IDENTITY(1,1) NOT NULL,

    NguoiDungID        INT               NOT NULL,

    MaNhanVien         VARCHAR(20)       NOT NULL,
    ChucVu             NVARCHAR(50)      NOT NULL,

    NgayVaoLam         DATE              NOT NULL,

    TrangThai          BIT               NOT NULL
);
GO


/*==============================================================*/
/* TABLE: LoaiThe                                               */
/*==============================================================*/
CREATE TABLE LoaiThe
(
    LoaiTheID          INT IDENTITY(1,1) NOT NULL,

    TenLoaiThe         NVARCHAR(100)     NOT NULL,
    PhiDangKy          DECIMAL(18,2)     NOT NULL,

    MoTa               NVARCHAR(500)     NULL,

    TrangThai          BIT               NOT NULL,

    NgayTao            DATETIME2         NOT NULL,
    NgayCapNhat        DATETIME2         NULL,

    NguoiTao           INT               NULL,
    NguoiCapNhat       INT               NULL
);
GO


/*==============================================================*/
/* TABLE: TheThuVien                                            */
/*==============================================================*/
CREATE TABLE TheThuVien
(
    TheThuVienID       INT IDENTITY(1,1) NOT NULL,

    BanDocID           INT               NOT NULL,
    LoaiTheID          INT               NOT NULL,

    MaThe              VARCHAR(30)       NOT NULL,

    NgayCap            DATE              NOT NULL,
    NgayHetHan         DATE              NOT NULL,

    TrangThai          TINYINT           NOT NULL,
    LyDoKhoa           NVARCHAR(300)     NULL,

    NgayTao            DATETIME2         NOT NULL,
    NgayCapNhat        DATETIME2         NULL,

    NguoiTao           INT               NULL,
    NguoiCapNhat       INT               NULL
);
GO


/*==============================================================*/
/* TABLE: ChinhSachMuon                                         */
/*==============================================================*/
CREATE TABLE ChinhSachMuon
(
    ChinhSachMuonID        INT IDENTITY(1,1) NOT NULL,

    LoaiTheID              INT               NOT NULL,

    SoSachToiDa            TINYINT           NOT NULL,
    SoNgayMuonToiDa        SMALLINT          NOT NULL,
    SoLanGiaHanToiDa       TINYINT           NOT NULL,

    MucPhatTreHanMoiNgay   DECIMAL(18,2)     NOT NULL,
    MucPhatMatSach         DECIMAL(18,2)     NOT NULL,
    MucPhatHuHong          DECIMAL(18,2)     NOT NULL,

    NgayApDung             DATE              NOT NULL,
    NgayKetThuc            DATE              NULL,

    TrangThai              BIT               NOT NULL,
    GhiChu                 NVARCHAR(500)     NULL,

    NgayTao                DATETIME2         NOT NULL,
    NgayCapNhat            DATETIME2         NULL,

    NguoiTao               INT               NULL,
    NguoiCapNhat           INT               NULL
);
GO