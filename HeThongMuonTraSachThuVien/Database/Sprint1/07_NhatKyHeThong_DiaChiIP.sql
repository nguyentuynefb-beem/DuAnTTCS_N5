USE ThuVienDB;
GO

/*==============================================================*/
/* File: 07_NhatKyHeThong_DiaChiIP.sql                          */
/* Sprint: 1 - Story S1-10                                      */
/* Mô tả: Bổ sung cột địa chỉ IP vào bảng nhật ký hệ thống      */
/*==============================================================*/
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('NhatKyHeThong') AND name = 'DiaChiIP'
)
BEGIN
    ALTER TABLE NhatKyHeThong
    ADD DiaChiIP VARCHAR(45) NULL;
END
GO