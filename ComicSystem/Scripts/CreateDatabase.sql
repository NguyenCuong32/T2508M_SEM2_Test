-- Create Database
CREATE DATABASE IF NOT EXISTS ComicSystem;
USE ComicSystem;

-- Create Customers table
CREATE TABLE IF NOT EXISTS Customers (
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Registration DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Create ComicBooks table
CREATE TABLE IF NOT EXISTS ComicBooks (
    ComicBookID INT AUTO_INCREMENT PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10, 2) NOT NULL
);

-- Create Rentals table
CREATE TABLE IF NOT EXISTS Rentals (
    RentalID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    ReturnDate DATETIME NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Active',
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);

-- Create RentalDetails table
CREATE TABLE IF NOT EXISTS RentalDetails (
    RentalDetailID INT AUTO_INCREMENT PRIMARY KEY,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    PricePerDay DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (RentalID) REFERENCES Rentals(RentalID) ON DELETE CASCADE,
    FOREIGN KEY (ComicBookID) REFERENCES ComicBooks(ComicBookID) ON DELETE CASCADE
);

-- Seed data for ComicBooks
INSERT INTO ComicBooks (Title, Author, PricePerDay) VALUES
('Conan', 'Gosho Aoyama', 2.50),
('Doraemon', 'Fujiko F. Fujio', 2.00),
('Dragon Ball', 'Akira Toriyama', 3.00),
('Naruto', 'Masashi Kishimoto', 2.50),
('One Piece', 'Eiichiro Oda', 3.50);

-- Seed data for Customers
INSERT INTO Customers (FullName, PhoneNumber, Registration) VALUES
('Nguyen Hung', '0901234567', '2024-01-01'),
('Tran Van A', '0912345678', '2024-02-15');
