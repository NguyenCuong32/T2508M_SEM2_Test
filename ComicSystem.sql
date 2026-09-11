-- =============================================================
-- FPT Aptech - Developing ASP.NET Core MVC Applications - SET01
-- Database Script for: ComicSystem (comicsys.com)
-- Author: Senior Principal Engineer
-- =============================================================

USE master;
GO

-- 1. Create Database ComicSystem if not exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ComicSystem')
BEGIN
    CREATE DATABASE ComicSystem;
    PRINT N'Database ComicSystem created successfully.';
END
GO

USE ComicSystem;
GO

-- 2. Drop existing foreign keys and tables if recreating
IF OBJECT_ID(N'dbo.RentalDetails', N'U') IS NOT NULL DROP TABLE dbo.RentalDetails;
IF OBJECT_ID(N'dbo.Rentals', N'U') IS NOT NULL DROP TABLE dbo.Rentals;
IF OBJECT_ID(N'dbo.ComicBooks', N'U') IS NOT NULL DROP TABLE dbo.ComicBooks;
IF OBJECT_ID(N'dbo.Customers', N'U') IS NOT NULL DROP TABLE dbo.Customers;
GO

-- =============================================================
-- Table: Customers (Question 2)
-- Description: Lưu trữ thông tin khách hàng đăng ký thuê truyện
-- =============================================================
CREATE TABLE dbo.Customers (
    CustomerID INT IDENTITY(1,1) NOT NULL,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT PK_Customers PRIMARY KEY CLUSTERED (CustomerID ASC)
);
GO

-- =============================================================
-- Table: ComicBooks (Question 1)
-- Description: Lưu trữ thông tin sách truyện tranh trong kho
-- =============================================================
CREATE TABLE dbo.ComicBooks (
    ComicBookID INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_ComicBooks PRIMARY KEY CLUSTERED (ComicBookID ASC)
);
GO

-- =============================================================
-- Table: Rentals (Question 3)
-- Description: Lưu trữ thông tin phiếu thuê sách truyện
-- =============================================================
CREATE TABLE dbo.Rentals (
    RentalID INT IDENTITY(1,1) NOT NULL,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    ReturnDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT (N'Đang thuê'),
    CONSTRAINT PK_Rentals PRIMARY KEY CLUSTERED (RentalID ASC),
    CONSTRAINT FK_Rentals_Customers FOREIGN KEY (CustomerID) 
        REFERENCES dbo.Customers (CustomerID)
);
GO

-- =============================================================
-- Table: RentalDetails (Question 3)
-- Description: Lưu trữ chi tiết từng cuốn truyện trong phiếu thuê
-- =============================================================
CREATE TABLE dbo.RentalDetails (
    RentalDetailID INT IDENTITY(1,1) NOT NULL,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT (1),
    PricePerDay DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_RentalDetails PRIMARY KEY CLUSTERED (RentalDetailID ASC),
    CONSTRAINT FK_RentalDetails_Rentals FOREIGN KEY (RentalID) 
        REFERENCES dbo.Rentals (RentalID) ON DELETE CASCADE,
    CONSTRAINT FK_RentalDetails_ComicBooks FOREIGN KEY (ComicBookID) 
        REFERENCES dbo.ComicBooks (ComicBookID)
);
GO

-- =============================================================
-- Sample Seed Data (Khớp chính xác dữ liệu mẫu trong đề thi)
-- =============================================================

-- Sách truyện mẫu
INSERT INTO dbo.ComicBooks (Title, Author, PricePerDay) VALUES
(N'Conan', N'Gosho Aoyama', 5000.00),
(N'Doraemon', N'Fujiko F. Fujio', 4000.00),
(N'Dragon Ball', N'Akira Toriyama', 6000.00),
(N'One Piece', N'Eiichiro Oda', 7000.00),
(N'Naruto', N'Masashi Kishimoto', 5500.00);

-- Khách hàng mẫu
INSERT INTO dbo.Customers (FullName, PhoneNumber, RegistrationDate) VALUES
(N'Nguyen Hung', N'0901234567', '2024-09-15 09:00:00'),
(N'Tran Minh', N'0987654321', '2024-09-20 14:30:00');

-- Phiếu thuê 1 (Nguyen Hung thuê Conan 1 cuốn: 01/10/2024 -> 10/10/2024)
INSERT INTO dbo.Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(1, '2024-10-01 00:00:00', '2024-10-10 00:00:00', N'Đang thuê');

INSERT INTO dbo.RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(1, 1, 1, 5000.00);

-- Phiếu thuê 2 (Nguyen Hung thuê Doraemon 3 cuốn: 01/10/2024 -> 20/10/2024)
INSERT INTO dbo.Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(1, '2024-10-01 00:00:00', '2024-10-20 00:00:00', N'Đang thuê');

INSERT INTO dbo.RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(2, 2, 3, 4000.00);
GO

-- =============================================================
-- Stored Procedure: usp_GetRentalReport (Question 4)
-- Description: Báo cáo danh sách truyện đã thuê từ ngày đến ngày
-- =============================================================
IF OBJECT_ID(N'dbo.usp_GetRentalReport', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_GetRentalReport;
GO

CREATE PROCEDURE dbo.usp_GetRentalReport
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY r.RentalDate, cb.Title) AS [No],
        cb.Title AS [Book name],
        CONVERT(VARCHAR(10), r.RentalDate, 103) AS [Rental date],
        CONVERT(VARCHAR(10), r.ReturnDate, 103) AS [Return date],
        c.FullName AS [Customer name],
        rd.Quantity AS [Quantity]
    FROM dbo.RentalDetails rd
    INNER JOIN dbo.Rentals r ON rd.RentalID = r.RentalID
    INNER JOIN dbo.ComicBooks cb ON rd.ComicBookID = cb.ComicBookID
    INNER JOIN dbo.Customers c ON r.CustomerID = c.CustomerID
    WHERE r.RentalDate >= @StartDate 
      AND r.RentalDate <= DATEADD(DAY, 1, @EndDate)
    ORDER BY [No];
END;
GO

PRINT N'ComicSystem database setup and stored procedure created successfully!';
