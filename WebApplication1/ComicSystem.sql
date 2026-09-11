-- ==========================================================
-- Database Script: ComicSystem
-- Website: comicsys.com
-- Practical Paper: Developing ASP.NET Core MVC Applications - SET01
-- ==========================================================

USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ComicSystem')
BEGIN
    CREATE DATABASE ComicSystem;
END
GO

USE ComicSystem;
GO

-- 1. Table Customers
IF OBJECT_ID('dbo.RentalDetails', 'U') IS NOT NULL DROP TABLE dbo.RentalDetails;
IF OBJECT_ID('dbo.Rentals', 'U') IS NOT NULL DROP TABLE dbo.Rentals;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
IF OBJECT_ID('dbo.ComicBooks', 'U') IS NOT NULL DROP TABLE dbo.ComicBooks;
GO

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- 2. Table ComicBooks
CREATE TABLE ComicBooks (
    ComicBookID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NULL,
    PricePerDay DECIMAL(10, 2) NOT NULL
);
GO

-- 3. Table Rentals
CREATE TABLE Rentals (
    RentalID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    ReturnDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Đang thuê',
    CONSTRAINT FK_Rentals_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
GO

-- 4. Table RentalDetails
CREATE TABLE RentalDetails (
    RentalDetailID INT IDENTITY(1,1) PRIMARY KEY,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    PricePerDay DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_RentalDetails_Rentals FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID) ON DELETE CASCADE,
    CONSTRAINT FK_RentalDetails_ComicBooks FOREIGN KEY (ComicBookID) REFERENCES ComicBooks(ComicBookID)
);
GO

-- ==========================================================
-- Sample Data (Matching Set01 Practical Paper)
-- ==========================================================

-- Insert ComicBooks
INSERT INTO ComicBooks (Title, Author, PricePerDay) VALUES 
(N'Conan', N'Gosho Aoyama', 5000),
(N'Doraemon', N'Fujiko F. Fujio', 4000),
(N'Dragon Ball', N'Akira Toriyama', 6000),
(N'One Piece', N'Eiichiro Oda', 7000);

-- Insert Customers
INSERT INTO Customers (FullName, PhoneNumber, RegistrationDate) VALUES 
(N'NguyenHung', '0987654321', '2024-01-10 08:30:00'),
(N'Tran Minh Anh', '0912345678', '2024-05-15 09:00:00');

-- Insert Rentals & RentalDetails matching Question 4 example table:
-- 1 | Conan | 01/10/2024 | 10/10/2024 | NguyenHung | 1
-- 2 | Doraemon | 01/10/2024 | 10/20/2024 | NguyenHung | 3
INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES 
(1, '2024-10-01', '2024-10-10', N'Đang thuê'),
(1, '2024-10-01', '2024-10-20', N'Đang thuê');

INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES 
(1, 1, 1, 5000),
(2, 2, 3, 4000);
GO

-- Stored Procedure for Question 4 Report
IF OBJECT_ID('sp_GetRentalReport', 'P') IS NOT NULL DROP PROCEDURE sp_GetRentalReport;
GO
CREATE PROCEDURE sp_GetRentalReport
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER(ORDER BY r.RentalDate, rd.RentalDetailID) AS [No],
        cb.Title AS [Bookname],
        CONVERT(VARCHAR(10), r.RentalDate, 103) AS [Rentaldate],
        CONVERT(VARCHAR(10), r.ReturnDate, 103) AS [Return date],
        c.FullName AS [Customer name],
        rd.Quantity AS [Quantity]
    FROM RentalDetails rd
    INNER JOIN Rentals r ON rd.RentalID = r.RentalID
    INNER JOIN Customers c ON r.CustomerID = c.CustomerID
    INNER JOIN ComicBooks cb ON rd.ComicBookID = cb.ComicBookID
    WHERE (@StartDate IS NULL OR r.RentalDate >= @StartDate)
      AND (@EndDate IS NULL OR r.RentalDate <= @EndDate)
    ORDER BY r.RentalDate, rd.RentalDetailID;
END;
GO
