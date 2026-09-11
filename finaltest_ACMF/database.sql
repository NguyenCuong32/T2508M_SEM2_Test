-- =============================================
-- Database: ComicSystem
-- Description: Database creation script for Comic Rental System
-- =============================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ComicSystem')
BEGIN
    CREATE DATABASE ComicSystem;
END
GO

USE ComicSystem;
GO

-- 1. Table Customers
IF OBJECT_ID('dbo.RentalDetails', 'U') IS NOT NULL DROP TABLE dbo.RentalDetails;
IF OBJECT_ID('dbo.Rentals', 'U') IS NOT NULL DROP TABLE dbo.Rentals;
IF OBJECT_ID('dbo.ComicBooks', 'U') IS NOT NULL DROP TABLE dbo.ComicBooks;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
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
    Author NVARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL
);
GO

-- 3. Table Rentals
CREATE TABLE Rentals (
    RentalID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    ReturnDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Đang thuê',
    CONSTRAINT FK_Rentals_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);
GO

-- 4. Table RentalDetails
CREATE TABLE RentalDetails (
    RentalDetailID INT IDENTITY(1,1) PRIMARY KEY,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_RentalDetails_Rentals FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID) ON DELETE CASCADE,
    CONSTRAINT FK_RentalDetails_ComicBooks FOREIGN KEY (ComicBookID) REFERENCES ComicBooks(ComicBookID) ON DELETE CASCADE
);
GO

-- =============================================
-- Sample Seed Data (Data for testing according to practical paper)
-- =============================================

-- Sample Customers
INSERT INTO Customers (FullName, PhoneNumber, RegistrationDate) VALUES
(N'Nguyen Hung', '0912345678', '2024-09-01 08:30:00'),
(N'Tran Van An', '0987654321', '2024-09-05 09:15:00'),
(N'Le Thi Mai', '0905123456', '2024-09-10 14:00:00');
GO

-- Sample ComicBooks
INSERT INTO ComicBooks (Title, Author, PricePerDay) VALUES
(N'Conan', N'Gosho Aoyama', 5000.00),
(N'Doraemon', N'Fujiko F. Fujio', 4000.00),
(N'Dragon Ball', N'Akira Toriyama', 6000.00),
(N'One Piece', N'Eiichiro Oda', 5500.00),
(N'Naruto', N'Masashi Kishimoto', 5000.00);
GO

-- Sample Rentals & RentalDetails matching the problem statement table
-- Rental 1: Nguyen Hung rented Conan (Qty 1) from 01/10/2024 to 10/10/2024
INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(1, '2024-10-01 08:00:00', '2024-10-10 17:00:00', N'Đã trả');

DECLARE @Rental1ID INT = SCOPE_IDENTITY();
INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(@Rental1ID, 1, 1, 5000.00);

-- Rental 2: Nguyen Hung rented Doraemon (Qty 3) from 01/10/2024 to 20/10/2024
INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(1, '2024-10-01 09:30:00', '2024-10-20 17:00:00', N'Đã trả');

DECLARE @Rental2ID INT = SCOPE_IDENTITY();
INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(@Rental2ID, 2, 3, 4000.00);

-- Rental 3: Tran Van An rented Dragon Ball (Qty 2) from 15/10/2024 to 25/10/2024
INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
(2, '2024-10-15 10:00:00', '2024-10-25 18:00:00', N'Đang thuê');

DECLARE @Rental3ID INT = SCOPE_IDENTITY();
INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
(@Rental3ID, 3, 2, 6000.00);
GO
