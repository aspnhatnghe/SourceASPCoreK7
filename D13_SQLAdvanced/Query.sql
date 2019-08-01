USE MyeStore
GO

SELECT * FROM vHangHoa
WHERE DonGia BETWEEN 50 AND 1000
ORDER BY DonGia DESC, TenHH

--Tạo view xem thông tin hóa đơn
CREATE VIEW vThongTinHoaDon AS
SELECT cthd.*, TenHH, TenLoai
FROM ChiTietHD cthd JOIN HangHoa hh ON hh.MaHH = cthd.MaHH
	JOIN Loai lo ON lo.MaLoai = hh.MaLoai

--demo
SELECT * FROM vThongTinHoaDon WHERE MaHD = 10248

----------STORE PROCEDURE--------------
--Tạo store tra cứu hàng hóa theo loại và nhà cung cấp
CREATE PROC spTraCuuHangHoa
	@MaLoai int,
	@MaNcc nvarchar(50)
AS BEGIN
	SELECT * FROM HangHoa 
	WHERE MaLoai = @MaLoai AND MaNCC = @MaNcc
END

--Demo
EXEC spTraCuuHangHoa 1001, 'NK'


--Thêm loại
CREATE PROC spThemLoai
	@MaLoai int output,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	--1. Thêm
	INSERT INTO Loai(TenLoai, MoTa, Hinh)
		VALUES(@TenLoai, @MoTa, @Hinh)
	--2. Lấy giá trị vừa tăng
	SELECT @MaLoai = @@IDENTITY
END

--Demo
DECLARE @Ma int
EXEC spThemLoai @Ma output, N'Nước giải khát', NULL, NULL
PRINT CONCAT(N'Vừa thêm loại có mã : ', @Ma)


---Sửa loại
CREATE PROC spSuaLoai
	@MaLoai int,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	UPDATE Loai SET TenLoai = @TenLoai, MoTa = @MoTa,
		Hinh = @Hinh
	WHERE MaLoai = @MaLoai
END

---Xóa loại
CREATE PROC spXoaLoai
	@MaLoai int
AS BEGIN
	DELETE FROM Loai WHERE MaLoai = @MaLoai
END

---Lấy loại
CREATE PROC spLayLoai
	@MaLoai int = 0
AS BEGIN
	IF @MaLoai > 0
		SELECT * FROM Loai WHERE MaLoai = @MaLoai
	ELSE
		SELECT * FROM Loai
END

--Demo
EXEC spLayLoai
EXEC spLayLoai 1005

--Tìm loại gần đúng theo tên hoặc mô tả
CREATE PROC spTimLoai
	@TuKhoa nvarchar(250)
AS BEGIN
	SELECT * FROM Loai
	WHERE TenLoai LIKE N'%' + @TuKhoa + N'%'
		OR MoTa LIKE N'%' + @TuKhoa + N'%'
END

--Demo
EXEC spTimLoai N'Đồ'

--Tìm hàng hóa gần đúng theo tên hoặc mô tả
CREATE PROC spTimHangHoa
	@TuKhoa nvarchar(250)
AS BEGIN
	SELECT * FROM HangHoa
	WHERE TenHH LIKE N'%' + @TuKhoa + N'%'
		OR MoTa LIKE N'%' + @TuKhoa + N'%'
END

---------FUNCTION---------
--Tính doanh thu khách hàng
CREATE FUNCTION DoanhThu
(
	@MaKH nvarchar(50),
	@TuNgay datetime,
	@DenNgay datetime
)
RETURNS float
AS
BEGIN
	DECLARE @DoanhSo float

	SELECT @DoanhSo = SUM(SoLuong * DonGia * (1 - GiamGia))
	FROM ChiTietHD cthd JOIN HoaDon hd 
		ON hd.MaHD = cthd.MaHD
	WHERE MaKH = @MaKH AND 
		NgayDat BETWEEN @TuNgay AND @DenNgay

	RETURN @DoanhSo
END

--demo
SELECT dbo.DoanhThu('ANTON', '2005-1-1', GETDATE())

SELECT MaKh, HoTen, 
	dbo.DoanhThu(MaKH, '2005-1-1', GETDATE()) as DoanhThu
FROM KhachHang
WHERE dbo.DoanhThu(MaKH, '2005-1-1', GETDATE()) IS NOT NULL
ORDER BY DoanhThu DESC

--Hàm thống kê doanh thu theo khách hàng
CREATE FUNCTION DoanhThuKhachHang
(
	@TuNgay datetime,
	@DenNgay datetime
)
RETURNS TABLE
AS RETURN
	SELECT kh.HoTen, 
		SUM(SoLuong * DonGia * (1 - GiamGia)) as DoanhSo
	FROM ChiTietHD cthd JOIN HoaDon hd 
		ON hd.MaHD = cthd.MaHD
		JOIN KhachHang kh ON kh.MaKH = hd.MaKH
	WHERE NgayDat BETWEEN @TuNgay AND @DenNgay
	GROUP BY hd.MaKH, kh.HoTen

--Demo
SELECT * FROM dbo.DoanhThuKhachHang('2005-1-1', GETDATE())

--Ví dụ 2
CREATE FUNCTION DoanhThuTheoNam
( @Nam int)
RETURNS @MyTable TABLE(
		MaHH int, TenHH nvarchar(50),
		DoanhSo float
	)
AS BEGIN
	INSERT INTO @MyTable
	SELECT cthd.MaHH, TenHH, 
		SUM(SoLuong * cthd.DonGia * (1 - cthd.GiamGia))
	FROM ChiTietHD cthd JOIN HoaDon hd 
		ON hd.MaHD = cthd.MaHD
		JOIN HangHoa hh On hh.MaHH = cthd.MaHH
	WHERE YEAR(NgayDat) = @Nam
	GROUP BY cthd.MaHH, TenHH

	RETURN
END

--Demo
SELECT * FROM dbo.DoanhThuTheoNam(2010)

----TRIGGER
--Thêm cột ThanhTien cho bảng HoaDon
ALTER TABLE HoaDon ADD 
	ThanhTien float NOT NULL DEFAULT(0)

--Viết trigger tự động cập nhập thành tiền khi
--thêm/xóa/sửa chi tiết hóa đơn
CREATE TRIGGER trgCapNhapThanhTien ON ChiTietHD
	AFTER INSERT, UPDATE, DELETE
AS BEGIN
	DECLARE @MaHD int;
	DECLARE @Tong float;

	WITH BANG_TAM AS(
		SELECT MaHD FROM inserted
		UNION SELECT MaHD FROM deleted
	)

	SELECT @MaHD = MaHD FROM BANG_TAM

	SELECT @Tong = SUM(SoLuong * DonGia * (1 - GiamGia))
	FROM ChiTietHD WHERE MaHD = @MaHD

	UPDATE HoaDon SET ThanhTien = @Tong
	FROM HoaDon WHERE MaHD = @MaHD

END

--Demo
