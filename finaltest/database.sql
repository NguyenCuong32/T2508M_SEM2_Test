-- =============================================
-- DATABASE SCRIPT: ComicSystem
-- =============================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ComicSystem')
BEGIN
    CREATE DATABASE ComicSystem;
END
GO

USE ComicSystem;
GO

-- 1. Create Customers Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        CustomerID INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(255) NOT NULL,
        PhoneNumber NVARCHAR(15) NOT NULL,
        RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 2. Create ComicBooks Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ComicBooks')
BEGIN
    CREATE TABLE ComicBooks (
        ComicBookID INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(255) NOT NULL,
        Author NVARCHAR(255) NOT NULL,
        PricePerDay DECIMAL(10, 2) NOT NULL
    );
END
GO

-- 3. Create Rentals Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Rentals')
BEGIN
    CREATE TABLE Rentals (
        RentalID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL,
        RentalDate DATETIME NOT NULL,
        ReturnDate DATETIME NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT N'Đang thuê',
        CONSTRAINT FK_Rentals_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
    );
END
GO

-- 4. Create RentalDetails Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RentalDetails')
BEGIN
    CREATE TABLE RentalDetails (
        RentalDetailID INT IDENTITY(1,1) PRIMARY KEY,
        RentalID INT NOT NULL,
        ComicBookID INT NOT NULL,
        Quantity INT NOT NULL DEFAULT 1,
        PricePerDay DECIMAL(10, 2) NOT NULL,
        CONSTRAINT FK_RentalDetails_Rentals FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID) ON DELETE CASCADE,
        CONSTRAINT FK_RentalDetails_ComicBooks FOREIGN KEY (ComicBookID) REFERENCES ComicBooks(ComicBookID) ON DELETE CASCADE
    );
END
GO

-- =============================================
-- SEED DATA
-- =============================================

-- Seed ComicBooks
IF NOT EXISTS (SELECT 1 FROM ComicBooks)
BEGIN
    INSERT INTO ComicBooks (Title, Author, PricePerDay) VALUES
    (N'Conan', N'Gosho Aoyama', 5000),
    (N'Doraemon', N'Fujiko F. Fujio', 4000),
    (N'Dragon Ball', N'Akira Toriyama', 6000),
    (N'One Piece', N'Eiichiro Oda', 7000),
    (N'Naruto', N'Masashi Kishimoto', 5500);
END
GO

-- Seed Customers
IF NOT EXISTS (SELECT 1 FROM Customers)
BEGIN
    INSERT INTO Customers (FullName, PhoneNumber, RegistrationDate) VALUES
    (N'Nguyen Hung', N'0912345678', '2024-09-01'),
    (N'Tran Van B', N'0987654321', '2024-09-15'),
    (N'Le Thi C', N'0901122334', '2024-10-01');
END
GO

-- Seed Rentals & RentalDetails (Matching sample in prompt)
IF NOT EXISTS (SELECT 1 FROM Rentals)
BEGIN
    -- Rental 1 for Nguyen Hung (CustomerID = 1)
    INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
    (1, '2024-10-01', '2024-10-10', N'Đang thuê');
    DECLARE @Rental1 INT = SCOPE_IDENTITY();

    -- Conan, Quantity: 1, PricePerDay: 5000
    INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
    (@Rental1, 1, 1, 5000);

    -- Rental 2 for Nguyen Hung (CustomerID = 1)
    INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status) VALUES
    (1, '2024-10-01', '2024-10-20', N'Đang thuê');
    DECLARE @Rental2 INT = SCOPE_IDENTITY();

    -- Doraemon, Quantity: 3, PricePerDay: 4000
    INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay) VALUES
    (@Rental2, 2, 3, 4000);
END
GO
