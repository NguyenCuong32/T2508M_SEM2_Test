-- ============================================================================
-- FPT Aptech - Developing ASP.NET Core MVC Applications - SET01
-- Database: ComicSystem
-- Tables: Customers, ComicBooks, Rentals, RentalDetails
-- ============================================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ComicSystem')
BEGIN
    CREATE DATABASE [ComicSystem];
END;
GO

USE [ComicSystem];
GO

-- 1. Table: Customers
IF OBJECT_ID(N'[dbo].[Customers]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Customers] (
        [CustomerID] INT IDENTITY(1,1) NOT NULL,
        [FullName] NVARCHAR(255) NOT NULL,
        [PhoneNumber] NVARCHAR(15) NOT NULL,
        [RegistrationDate] DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
    );
END;
GO

-- 2. Table: ComicBooks
IF OBJECT_ID(N'[dbo].[ComicBooks]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ComicBooks] (
        [ComicBookID] INT IDENTITY(1,1) NOT NULL,
        [Title] NVARCHAR(255) NOT NULL,
        [Author] NVARCHAR(255) NOT NULL,
        [PricePerDay] DECIMAL(10, 2) NOT NULL,
        CONSTRAINT [PK_ComicBooks] PRIMARY KEY CLUSTERED ([ComicBookID] ASC)
    );
END;
GO

-- 3. Table: Rentals
IF OBJECT_ID(N'[dbo].[Rentals]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Rentals] (
        [RentalID] INT IDENTITY(1,1) NOT NULL,
        [CustomerID] INT NOT NULL,
        [RentalDate] DATETIME NOT NULL,
        [ReturnDate] DATETIME NOT NULL,
        [Status] NVARCHAR(50) NOT NULL DEFAULT N'Đang thuê',
        CONSTRAINT [PK_Rentals] PRIMARY KEY CLUSTERED ([RentalID] ASC),
        CONSTRAINT [FK_Rentals_Customers] FOREIGN KEY ([CustomerID]) 
            REFERENCES [dbo].[Customers] ([CustomerID]) ON DELETE NO ACTION
    );
END;
GO

-- 4. Table: RentalDetails
IF OBJECT_ID(N'[dbo].[RentalDetails]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[RentalDetails] (
        [RentalDetailID] INT IDENTITY(1,1) NOT NULL,
        [RentalID] INT NOT NULL,
        [ComicBookID] INT NOT NULL,
        [Quantity] INT NOT NULL DEFAULT 1,
        [PricePerDay] DECIMAL(10, 2) NOT NULL,
        CONSTRAINT [PK_RentalDetails] PRIMARY KEY CLUSTERED ([RentalDetailID] ASC),
        CONSTRAINT [FK_RentalDetails_Rentals] FOREIGN KEY ([RentalID]) 
            REFERENCES [dbo].[Rentals] ([RentalID]) ON DELETE CASCADE,
        CONSTRAINT [FK_RentalDetails_ComicBooks] FOREIGN KEY ([ComicBookID]) 
            REFERENCES [dbo].[ComicBooks] ([ComicBookID]) ON DELETE NO ACTION
    );
END;
GO

-- Create Indexes for performance
IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_Rentals_CustomerID')
    CREATE NONCLUSTERED INDEX [IX_Rentals_CustomerID] ON [dbo].[Rentals]([CustomerID]);
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_RentalDetails_RentalID')
    CREATE NONCLUSTERED INDEX [IX_RentalDetails_RentalID] ON [dbo].[RentalDetails]([RentalID]);
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_RentalDetails_ComicBookID')
    CREATE NONCLUSTERED INDEX [IX_RentalDetails_ComicBookID] ON [dbo].[RentalDetails]([ComicBookID]);
GO

-- ============================================================================
-- SEED SAMPLE DATA (Matching Exam Requirements)
-- ============================================================================

-- Seed ComicBooks
IF NOT EXISTS (SELECT 1 FROM [dbo].[ComicBooks])
BEGIN
    SET IDENTITY_INSERT [dbo].[ComicBooks] ON;
    INSERT INTO [dbo].[ComicBooks] ([ComicBookID], [Title], [Author], [PricePerDay]) VALUES
    (1, N'Conan', N'Gosho Aoyama', 5000.00),
    (2, N'Doraemon', N'Fujiko F. Fujio', 4000.00),
    (3, N'Dragon Ball', N'Akira Toriyama', 6000.00),
    (4, N'One Piece', N'Eiichiro Oda', 7000.00),
    (5, N'Naruto', N'Masashi Kishimoto', 5000.00);
    SET IDENTITY_INSERT [dbo].[ComicBooks] OFF;
END;
GO

-- Seed Customers
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers])
BEGIN
    SET IDENTITY_INSERT [dbo].[Customers] ON;
    INSERT INTO [dbo].[Customers] ([CustomerID], [FullName], [PhoneNumber], [RegistrationDate]) VALUES
    (1, N'Nguyen Hung', N'0912345678', '2024-10-01 08:30:00'),
    (2, N'Tran Van An', N'0987654321', '2024-10-05 09:15:00'),
    (3, N'Le Thi Mai', N'0905123456', '2024-10-15 14:00:00');
    SET IDENTITY_INSERT [dbo].[Customers] OFF;
END;
GO

-- Seed Rentals & Details (Question 4 Exam Sample Report Data)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Rentals])
BEGIN
    SET IDENTITY_INSERT [dbo].[Rentals] ON;
    INSERT INTO [dbo].[Rentals] ([RentalID], [CustomerID], [RentalDate], [ReturnDate], [Status]) VALUES
    (1, 1, '2024-10-01', '2024-10-10', N'Đang thuê'),
    (2, 1, '2024-10-01', '2024-10-20', N'Đang thuê');
    SET IDENTITY_INSERT [dbo].[Rentals] OFF;

    SET IDENTITY_INSERT [dbo].[RentalDetails] ON;
    INSERT INTO [dbo].[RentalDetails] ([RentalDetailID], [RentalID], [ComicBookID], [Quantity], [PricePerDay]) VALUES
    (1, 1, 1, 1, 5000.00), -- 1 Conan, 01/10/2024 -> 10/10/2024, Nguyen Hung
    (2, 2, 2, 3, 4000.00); -- 3 Doraemon, 01/10/2024 -> 20/10/2024, Nguyen Hung
    SET IDENTITY_INSERT [dbo].[RentalDetails] OFF;
END;
GO

-- ============================================================================
-- STORED PROCEDURE (Question 4: Report All Book Rents In Date Range)
-- ============================================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_GetRentalReportByDateRange]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY r.RentalDate ASC, cb.Title ASC) AS [No],
        cb.Title AS [Book name],
        CONVERT(VARCHAR(10), r.RentalDate, 103) AS [Rental date], -- dd/MM/yyyy
        CONVERT(VARCHAR(10), r.ReturnDate, 103) AS [Return date], -- dd/MM/yyyy
        c.FullName AS [Customer name],
        rd.Quantity AS [Quantity],
        rd.PricePerDay AS [PricePerDay],
        DATEDIFF(day, r.RentalDate, r.ReturnDate) AS [TotalDays],
        (DATEDIFF(day, r.RentalDate, r.ReturnDate) * rd.PricePerDay * rd.Quantity) AS [TotalAmount]
    FROM [dbo].[RentalDetails] rd
    INNER JOIN [dbo].[Rentals] r ON rd.RentalID = r.RentalID
    INNER JOIN [dbo].[ComicBooks] cb ON rd.ComicBookID = cb.ComicBookID
    INNER JOIN [dbo].[Customers] c ON r.CustomerID = c.CustomerID
    WHERE r.RentalDate >= CAST(@StartDate AS DATE)
      AND r.RentalDate <= DATEADD(day, 1, CAST(@EndDate AS DATE))
    ORDER BY r.RentalDate ASC, cb.Title ASC;
END;
GO
