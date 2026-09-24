USE ThuVienDB;
GO

/*==============================================================*/
/* File: 04_Indexes.sql                                         */
/* Sprint: 1                                                    */
/* Mô tả: Tạo các nonclustered index cho Sprint 1               */
/* Lưu ý:                                                       */
/* - Chạy sau 03_Constraints.sql                                */
/* - Không tạo lại index đã sinh từ UNIQUE constraint           */
/* - Dùng IF NOT EXISTS để tránh lỗi khi chạy lại               */
/*==============================================================*/


/*==============================================================*/
/* TABLE: VaiTro                                                 */
/* Index: IX_VaiTro_TenVaiTro                                   */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_VaiTro_TenVaiTro'
      AND object_id = OBJECT_ID('VaiTro')
)
BEGIN
    CREATE INDEX IX_VaiTro_TenVaiTro
    ON VaiTro (TenVaiTro);
END
GO


/*==============================================================*/
/* TABLE: NguoiDung                                              */
/* Index: IX_NguoiDung_TenDangNhap, IX_NguoiDung_Email,         */
/*        IX_NguoiDung_VaiTro                                   */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_NguoiDung_TenDangNhap'
      AND object_id = OBJECT_ID('NguoiDung')
)
BEGIN
    CREATE INDEX IX_NguoiDung_TenDangNhap
    ON NguoiDung (TenDangNhap);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_NguoiDung_Email'
      AND object_id = OBJECT_ID('NguoiDung')
)
BEGIN
    CREATE INDEX IX_NguoiDung_Email
    ON NguoiDung (Email);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_NguoiDung_VaiTro'
      AND object_id = OBJECT_ID('NguoiDung')
)
BEGIN
    CREATE INDEX IX_NguoiDung_VaiTro
    ON NguoiDung (VaiTroID);
END
GO


/*==============================================================*/
/* TABLE: BanDoc                                                 */
/* Index: IX_BanDoc_MaBanDoc                                    */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_BanDoc_MaBanDoc'
      AND object_id = OBJECT_ID('BanDoc')
)
BEGIN
    CREATE INDEX IX_BanDoc_MaBanDoc
    ON BanDoc (MaBanDoc);
END
GO


/*==============================================================*/
/* TABLE: NhanVien                                               */
/* Index: IX_NhanVien_MaNhanVien                                 */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_NhanVien_MaNhanVien'
      AND object_id = OBJECT_ID('NhanVien')
)
BEGIN
    CREATE INDEX IX_NhanVien_MaNhanVien
    ON NhanVien (MaNhanVien);
END
GO


/*==============================================================*/
/* TABLE: LoaiThe                                                */
/* Index: IX_LoaiThe_TenLoaiThe, IX_LoaiThe_TrangThai            */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_LoaiThe_TenLoaiThe'
      AND object_id = OBJECT_ID('LoaiThe')
)
BEGIN
    CREATE INDEX IX_LoaiThe_TenLoaiThe
    ON LoaiThe (TenLoaiThe);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_LoaiThe_TrangThai'
      AND object_id = OBJECT_ID('LoaiThe')
)
BEGIN
    CREATE INDEX IX_LoaiThe_TrangThai
    ON LoaiThe (TrangThai);
END
GO


/*==============================================================*/
/* TABLE: TheThuVien                                             */
/* Index: IX_TheThuVien_MaThe, IX_TheThuVien_BanDoc,            */
/*        IX_TheThuVien_TrangThai                                */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_TheThuVien_MaThe'
      AND object_id = OBJECT_ID('TheThuVien')
)
BEGIN
    CREATE INDEX IX_TheThuVien_MaThe
    ON TheThuVien (MaThe);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_TheThuVien_BanDoc'
      AND object_id = OBJECT_ID('TheThuVien')
)
BEGIN
    CREATE INDEX IX_TheThuVien_BanDoc
    ON TheThuVien (BanDocID);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_TheThuVien_TrangThai'
      AND object_id = OBJECT_ID('TheThuVien')
)
BEGIN
    CREATE INDEX IX_TheThuVien_TrangThai
    ON TheThuVien (TrangThai);
END
GO


/*==============================================================*/
/* TABLE: ChinhSachMuon                                          */
/* Index: IX_ChinhSachMuon_LoaiThe, IX_ChinhSachMuon_NgayApDung,*/
/*        IX_ChinhSachMuon_TrangThai                             */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ChinhSachMuon_LoaiThe'
      AND object_id = OBJECT_ID('ChinhSachMuon')
)
BEGIN
    CREATE INDEX IX_ChinhSachMuon_LoaiThe
    ON ChinhSachMuon (LoaiTheID);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ChinhSachMuon_NgayApDung'
      AND object_id = OBJECT_ID('ChinhSachMuon')
)
BEGIN
    CREATE INDEX IX_ChinhSachMuon_NgayApDung
    ON ChinhSachMuon (NgayApDung);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ChinhSachMuon_TrangThai'
      AND object_id = OBJECT_ID('ChinhSachMuon')
)
BEGIN
    CREATE INDEX IX_ChinhSachMuon_TrangThai
    ON ChinhSachMuon (TrangThai);
END
GO