USE ThuVienDB;
GO

/*==============================================================*/
/* PRIMARY KEY                                                   */
/*==============================================================*/

ALTER TABLE VaiTro
ADD CONSTRAINT PK_VaiTro
PRIMARY KEY (VaiTroID);
GO

ALTER TABLE NguoiDung
ADD CONSTRAINT PK_NguoiDung
PRIMARY KEY (NguoiDungID);
GO

ALTER TABLE BanDoc
ADD CONSTRAINT PK_BanDoc
PRIMARY KEY (BanDocID);
GO

ALTER TABLE NhanVien
ADD CONSTRAINT PK_NhanVien
PRIMARY KEY (NhanVienID);
GO

ALTER TABLE LoaiThe
ADD CONSTRAINT PK_LoaiThe
PRIMARY KEY (LoaiTheID);
GO

ALTER TABLE TheThuVien
ADD CONSTRAINT PK_TheThuVien
PRIMARY KEY (TheThuVienID);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT PK_ChinhSachMuon
PRIMARY KEY (ChinhSachMuonID);
GO

/*==============================================================*/
/* UNIQUE                                                        */
/*==============================================================*/

ALTER TABLE VaiTro
ADD CONSTRAINT UQ_VaiTro_TenVaiTro
UNIQUE (TenVaiTro);
GO

ALTER TABLE NguoiDung
ADD CONSTRAINT UQ_NguoiDung_TenDangNhap
UNIQUE (TenDangNhap);
GO

ALTER TABLE NguoiDung
ADD CONSTRAINT UQ_NguoiDung_Email
UNIQUE (Email);
GO

ALTER TABLE BanDoc
ADD CONSTRAINT UQ_BanDoc_NguoiDungID
UNIQUE (NguoiDungID);
GO

ALTER TABLE BanDoc
ADD CONSTRAINT UQ_BanDoc_MaBanDoc
UNIQUE (MaBanDoc);
GO

ALTER TABLE NhanVien
ADD CONSTRAINT UQ_NhanVien_NguoiDungID
UNIQUE (NguoiDungID);
GO

ALTER TABLE NhanVien
ADD CONSTRAINT UQ_NhanVien_MaNhanVien
UNIQUE (MaNhanVien);
GO

ALTER TABLE LoaiThe
ADD CONSTRAINT UQ_LoaiThe_TenLoaiThe
UNIQUE (TenLoaiThe);
GO

ALTER TABLE TheThuVien
ADD CONSTRAINT UQ_TheThuVien_MaThe
UNIQUE (MaThe);
GO

/*==============================================================*/
/* CHECK                                                         */
/*==============================================================*/

ALTER TABLE TheThuVien
ADD CONSTRAINT CK_TheThuVien_TrangThai
CHECK (TrangThai IN (1,2,3,4));
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_SoSachToiDa
CHECK (SoSachToiDa > 0);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_SoNgayMuonToiDa
CHECK (SoNgayMuonToiDa > 0);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_SoLanGiaHanToiDa
CHECK (SoLanGiaHanToiDa >= 0);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_MucPhatTreHanMoiNgay
CHECK (MucPhatTreHanMoiNgay >= 0);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_MucPhatMatSach
CHECK (MucPhatMatSach >= 0);
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT CK_ChinhSachMuon_MucPhatHuHong
CHECK (MucPhatHuHong >= 0);
GO

/*==============================================================*/
/* DEFAULT                                                       */
/*==============================================================*/

ALTER TABLE VaiTro
ADD CONSTRAINT DF_VaiTro_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE NguoiDung
ADD CONSTRAINT DF_NguoiDung_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE NguoiDung
ADD CONSTRAINT DF_NguoiDung_NgayTao
DEFAULT (GETDATE()) FOR NgayTao;
GO

ALTER TABLE BanDoc
ADD CONSTRAINT DF_BanDoc_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE BanDoc
ADD CONSTRAINT DF_BanDoc_NgayDangKy
DEFAULT (GETDATE()) FOR NgayDangKy;
GO

ALTER TABLE NhanVien
ADD CONSTRAINT DF_NhanVien_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE LoaiThe
ADD CONSTRAINT DF_LoaiThe_PhiDangKy
DEFAULT (0) FOR PhiDangKy;
GO

ALTER TABLE LoaiThe
ADD CONSTRAINT DF_LoaiThe_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE LoaiThe
ADD CONSTRAINT DF_LoaiThe_NgayTao
DEFAULT (GETDATE()) FOR NgayTao;
GO

ALTER TABLE TheThuVien
ADD CONSTRAINT DF_TheThuVien_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE TheThuVien
ADD CONSTRAINT DF_TheThuVien_NgayTao
DEFAULT (GETDATE()) FOR NgayTao;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_SoSachToiDa
DEFAULT (5) FOR SoSachToiDa;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_SoNgayMuonToiDa
DEFAULT (14) FOR SoNgayMuonToiDa;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_SoLanGiaHanToiDa
DEFAULT (1) FOR SoLanGiaHanToiDa;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_MucPhatTreHanMoiNgay
DEFAULT (0) FOR MucPhatTreHanMoiNgay;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_MucPhatMatSach
DEFAULT (0) FOR MucPhatMatSach;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_MucPhatHuHong
DEFAULT (0) FOR MucPhatHuHong;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_TrangThai
DEFAULT (1) FOR TrangThai;
GO

ALTER TABLE ChinhSachMuon
ADD CONSTRAINT DF_ChinhSachMuon_NgayTao
DEFAULT (GETDATE()) FOR NgayTao;
GO