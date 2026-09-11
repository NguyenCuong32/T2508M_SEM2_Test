-- ===================================================
-- Database Creation Script for ComicSystem
-- Exam: Developing ASP.NET Core MVC Applications - SET01
-- ===================================================

USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ComicSystem')
BEGIN
    CREATE DATABASE ComicSystem;
END
GO

USE ComicSystem;
GO

-- 1. Create Customers Table
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    RegisterDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- 2. Create ComicBooks Table
IF OBJECT_ID('dbo.ComicBooks', 'U') IS NOT NULL DROP TABLE dbo.ComicBooks;
CREATE TABLE ComicBooks (
    ComicBookID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL
);
GO

-- 3. Create Rentals Table
IF OBJECT_ID('dbo.Rentals', 'U') IS NOT NULL DROP TABLE dbo.Rentals;
CREATE TABLE Rentals (
    RentalID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL DEFAULT GETDATE(),
    ReturnDate DATETIME NOT NULL,
    Status NVARCHAR(50) DEFAULT N'Đang thuê',
    CONSTRAINT FK_Rentals_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);
GO

-- 4. Create RentalDetails Table
IF OBJECT_ID('dbo.RentalDetails', 'U') IS NOT NULL DROP TABLE dbo.RentalDetails;
CREATE TABLE RentalDetails (
    RentalDetailID INT IDENTITY(1,1) PRIMARY KEY,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    PricePerDay DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_RentalDetails_Rentals FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID) ON DELETE CASCADE,
    CONSTRAINT FK_RentalDetails_ComicBooks FOREIGN KEY (ComicBookID) REFERENCES ComicBooks(ComicBookID) ON DELETE CASCADE
);
GO

-- ===================================================
-- Seed Initial Sample Data
-- ===================================================

-- Customers
INSERT INTO Customers (FullName, PhoneNumber, RegisterDate) VALUES
(N'Nguyen Hung', '0912345678', '2024-01-10'),
(N'Tran Van A', '0987654321', '2024-02-15');

-- ComicBooks
INSERT INTO ComicBooks (Title, Author, PricePerDay) VALUES
(N'Conan', N'Gosho Aoyama', 5000.00),
(N'Doraemon', N'Fujiko F. Fujio', 4000.00),
(N'Dragon Ball', N'Akira Toriyama', 6000.00),
(N'One Piece', N'Eiichiro Oda', 7000.00);

-- Rentals & RentalDetails matching exam paper example
INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(1, '2024-10-01', '2024-10-10', N'Đang thuê'),
(1, '2024-10-01', '2024-10-20', N'Đang thuê');

INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(1, 1, 1, 5000.00), -- Conan, qty 1
(2, 2, 3, 4000.00); -- Doraemon, qty 3
GO
