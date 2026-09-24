USE ThuVienDB;
GO

/*==============================================================*/
/* File: 05_SeedData.sql                                        */
/* Sprint: 1                                                    */
/* Mô tả: Seed dữ liệu nền cho Sprint 1                          */
/* Lưu ý:                                                       */
/* - Chạy sau 03_Constraints.sql và 04_Indexes.sql               */
/* - Dùng IF NOT EXISTS để có thể chạy lại an toàn               */
/*==============================================================*/


/*==============================================================*/
/* 1) VaiTro                                                    */
/*==============================================================*/
IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'Admin')
BEGIN
    INSERT INTO VaiTro (TenVaiTro, MoTa, TrangThai)
    VALUES (N'Admin', NULL, 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'QuanLy')
BEGIN
    INSERT INTO VaiTro (TenVaiTro, MoTa, TrangThai)
    VALUES (N'QuanLy', NULL, 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'ThuThu')
BEGIN
    INSERT INTO VaiTro (TenVaiTro, MoTa, TrangThai)
    VALUES (N'ThuThu', NULL, 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'BanDoc')
BEGIN
    INSERT INTO VaiTro (TenVaiTro, MoTa, TrangThai)
    VALUES (N'BanDoc', NULL, 1);
END
GO


/*==============================================================*/
/* 2) LoaiThe                                                   */
/* Theo tài liệu: Sinh viên, Giảng viên, Học viên, Khách, VIP   */
/*==============================================================*/
IF NOT EXISTS (SELECT 1 FROM LoaiThe WHERE TenLoaiThe = N'Sinh viên')
BEGIN
    INSERT INTO LoaiThe
    (
        TenLoaiThe, PhiDangKy, MoTa, TrangThai,
        NgayTao, NgayCapNhat, NguoiTao, NguoiCapNhat
    )
    VALUES
    (
        N'Sinh viên', 0, NULL, 1,
        GETDATE(), NULL, NULL, NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM LoaiThe WHERE TenLoaiThe = N'Giảng viên')
BEGIN
    INSERT INTO LoaiThe
    (
        TenLoaiThe, PhiDangKy, MoTa, TrangThai,
        NgayTao, NgayCapNhat, NguoiTao, NguoiCapNhat
    )
    VALUES
    (
        N'Giảng viên', 0, NULL, 1,
        GETDATE(), NULL, NULL, NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM LoaiThe WHERE TenLoaiThe = N'Học viên')
BEGIN
    INSERT INTO LoaiThe
    (
        TenLoaiThe, PhiDangKy, MoTa, TrangThai,
        NgayTao, NgayCapNhat, NguoiTao, NguoiCapNhat
    )
    VALUES
    (
        N'Học viên', 0, NULL, 1,
        GETDATE(), NULL, NULL, NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM LoaiThe WHERE TenLoaiThe = N'Khách')
BEGIN
    INSERT INTO LoaiThe
    (
        TenLoaiThe, PhiDangKy, MoTa, TrangThai,
        NgayTao, NgayCapNhat, NguoiTao, NguoiCapNhat
    )
    VALUES
    (
        N'Khách', 0, NULL, 1,
        GETDATE(), NULL, NULL, NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM LoaiThe WHERE TenLoaiThe = N'Thành viên VIP')
BEGIN
    INSERT INTO LoaiThe
    (
        TenLoaiThe, PhiDangKy, MoTa, TrangThai,
        NgayTao, NgayCapNhat, NguoiTao, NguoiCapNhat
    )
    VALUES
    (
        N'Thành viên VIP', 0, NULL, 1,
        GETDATE(), NULL, NULL, NULL
    );
END
GO


/*==============================================================*/
/* 3) ChinhSachMuon                                             */
/* Ví dụ trong tài liệu: Sinh viên áp dụng theo từng giai đoạn  */
/*==============================================================*/
DECLARE @LoaiTheSinhVienID INT;
SELECT @LoaiTheSinhVienID = LoaiTheID
FROM LoaiThe
WHERE TenLoaiThe = N'Sinh viên';

IF @LoaiTheSinhVienID IS NOT NULL
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM ChinhSachMuon
        WHERE LoaiTheID = @LoaiTheSinhVienID
          AND NgayApDung = '2026-01-01'
    )
    BEGIN
        INSERT INTO ChinhSachMuon
        (
            LoaiTheID,
            SoSachToiDa,
            SoNgayMuonToiDa,
            SoLanGiaHanToiDa,
            MucPhatTreHanMoiNgay,
            MucPhatMatSach,
            MucPhatHuHong,
            NgayApDung,
            NgayKetThuc,
            TrangThai,
            GhiChu,
            NgayTao,
            NgayCapNhat,
            NguoiTao,
            NguoiCapNhat
        )
        VALUES
        (
            @LoaiTheSinhVienID,
            5, 14, 1,
            0, 0, 0,
            '2026-01-01',
            NULL,
            1,
            N'Chính sách mẫu giai đoạn 2026 cho Sinh viên',
            GETDATE(),
            NULL,
            NULL,
            NULL
        );
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM ChinhSachMuon
        WHERE LoaiTheID = @LoaiTheSinhVienID
          AND NgayApDung = '2027-01-01'
    )
    BEGIN
        INSERT INTO ChinhSachMuon
        (
            LoaiTheID,
            SoSachToiDa,
            SoNgayMuonToiDa,
            SoLanGiaHanToiDa,
            MucPhatTreHanMoiNgay,
            MucPhatMatSach,
            MucPhatHuHong,
            NgayApDung,
            NgayKetThuc,
            TrangThai,
            GhiChu,
            NgayTao,
            NgayCapNhat,
            NguoiTao,
            NguoiCapNhat
        )
        VALUES
        (
            @LoaiTheSinhVienID,
            7, 14, 1,
            0, 0, 0,
            '2027-01-01',
            NULL,
            1,
            N'Chính sách mẫu giai đoạn 2027 cho Sinh viên',
            GETDATE(),
            NULL,
            NULL,
            NULL
        );
    END
END
GO