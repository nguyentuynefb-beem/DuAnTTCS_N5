USE ThuVienDB;
GO

/*==============================================================*/
/* File: 06_NhatKyHeThong.sql                                   */
/* Sprint: 1 - Story S1-10                                      */
/* Mô tả: Tạo bảng NhatKyHeThong để lưu nhật ký hoạt động       */
/*        (tra cứu theo thời gian, người thực hiện, hành động)  */
/*==============================================================*/


/*==============================================================*/
/* TABLE: NhatKyHeThong                                         */
/*==============================================================*/
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'NhatKyHeThong')
BEGIN
    CREATE TABLE NhatKyHeThong
    (
        NhatKyID            INT IDENTITY(1,1) NOT NULL,

        NguoiThucHienID      INT               NULL,
        TenNguoiThucHien     NVARCHAR(100)     NOT NULL,

        HanhDong             NVARCHAR(50)      NOT NULL,
        DoiTuong             NVARCHAR(255)     NULL,

        ThoiGian             DATETIME2         NOT NULL
    );
END
GO


/*==============================================================*/
/* PRIMARY KEY                                                   */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1 FROM sys.key_constraints
    WHERE name = 'PK_NhatKyHeThong' AND parent_object_id = OBJECT_ID('NhatKyHeThong')
)
BEGIN
    ALTER TABLE NhatKyHeThong
    ADD CONSTRAINT PK_NhatKyHeThong
    PRIMARY KEY (NhatKyID);
END
GO


/*==============================================================*/
/* FOREIGN KEY                                                   */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_NhatKyHeThong_NguoiDung'
)
BEGIN
    ALTER TABLE NhatKyHeThong
    ADD CONSTRAINT FK_NhatKyHeThong_NguoiDung
    FOREIGN KEY (NguoiThucHienID) REFERENCES NguoiDung (NguoiDungID)
    ON DELETE SET NULL;
END
GO


/*==============================================================*/
/* DEFAULT                                                       */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1 FROM sys.default_constraints WHERE name = 'DF_NhatKyHeThong_ThoiGian'
)
BEGIN
    ALTER TABLE NhatKyHeThong
    ADD CONSTRAINT DF_NhatKyHeThong_ThoiGian
    DEFAULT (GETDATE()) FOR ThoiGian;
END
GO


/*==============================================================*/
/* INDEXES (phục vụ tra cứu, lọc theo thời gian/actor/action)   */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_NhatKyHeThong_ThoiGian' AND object_id = OBJECT_ID('NhatKyHeThong')
)
BEGIN
    CREATE INDEX IX_NhatKyHeThong_ThoiGian
    ON NhatKyHeThong (ThoiGian DESC);
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_NhatKyHeThong_TenNguoiThucHien' AND object_id = OBJECT_ID('NhatKyHeThong')
)
BEGIN
    CREATE INDEX IX_NhatKyHeThong_TenNguoiThucHien
    ON NhatKyHeThong (TenNguoiThucHien);
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_NhatKyHeThong_HanhDong' AND object_id = OBJECT_ID('NhatKyHeThong')
)
BEGIN
    CREATE INDEX IX_NhatKyHeThong_HanhDong
    ON NhatKyHeThong (HanhDong);
END
GO


/*==============================================================*/
/* SEED DATA MẪU (để test tra cứu ngay, không bắt buộc)         */
/*==============================================================*/
IF NOT EXISTS (SELECT 1 FROM NhatKyHeThong)
BEGIN
    INSERT INTO NhatKyHeThong (TenNguoiThucHien, HanhDong, DoiTuong, ThoiGian)
    VALUES
        (N'admin@thuvien.local', N'Đăng nhập', N'Hệ thống', DATEADD(DAY, -1, GETDATE())),
        (N'thuthu@thuvien.local', N'Thêm', N'Cấp thẻ TV-0001', DATEADD(HOUR, -5, GETDATE())),
        (N'quanly@thuvien.local', N'Sửa', N'Chính sách mượn', DATEADD(HOUR, -2, GETDATE()));
END
GO