-- ==========================================================
-- Database: ComicSystem (MySQL / phpMyAdmin)
-- Description: Script tạo cơ sở dữ liệu và dữ liệu mẫu cho phpMyAdmin
-- Exam: Developing ASP.NET Core MVC Applications
-- ==========================================================

CREATE DATABASE IF NOT EXISTS `ComicSystem` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `ComicSystem`;

-- 1. Drop existing tables if needed
DROP TABLE IF EXISTS `RentalDetails`;
DROP TABLE IF EXISTS `Rentals`;
DROP TABLE IF EXISTS `ComicBooks`;
DROP TABLE IF EXISTS `Customers`;

-- 2. Create Customers table
CREATE TABLE `Customers` (
    `CustomerID` INT AUTO_INCREMENT PRIMARY KEY,
    `FullName` VARCHAR(255) NOT NULL,
    `PhoneNumber` VARCHAR(15) NOT NULL,
    `RegistrationDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 3. Create ComicBooks table
CREATE TABLE `ComicBooks` (
    `ComicBookID` INT AUTO_INCREMENT PRIMARY KEY,
    `Title` VARCHAR(255) NOT NULL,
    `Author` VARCHAR(255) NOT NULL,
    `PricePerDay` DECIMAL(10, 2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 4. Create Rentals table
CREATE TABLE `Rentals` (
    `RentalID` INT AUTO_INCREMENT PRIMARY KEY,
    `CustomerID` INT NOT NULL,
    `RentalDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `ReturnDate` DATETIME NOT NULL,
    `Status` VARCHAR(50) NOT NULL DEFAULT 'Đang thuê',
    CONSTRAINT `FK_Rentals_Customers` FOREIGN KEY (`CustomerID`) 
        REFERENCES `Customers`(`CustomerID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 5. Create RentalDetails table
CREATE TABLE `RentalDetails` (
    `RentalDetailID` INT AUTO_INCREMENT PRIMARY KEY,
    `RentalID` INT NOT NULL,
    `ComicBookID` INT NOT NULL,
    `Quantity` INT NOT NULL DEFAULT 1,
    `PricePerDay` DECIMAL(10, 2) NOT NULL,
    CONSTRAINT `FK_RentalDetails_Rentals` FOREIGN KEY (`RentalID`) 
        REFERENCES `Rentals`(`RentalID`) ON DELETE CASCADE,
    CONSTRAINT `FK_RentalDetails_ComicBooks` FOREIGN KEY (`ComicBookID`) 
        REFERENCES `ComicBooks`(`ComicBookID`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ==========================================================
-- Sample Seed Data (Đúng dữ liệu câu 4 trong đề thi)
-- ==========================================================

INSERT INTO `Customers` (`CustomerID`, `FullName`, `PhoneNumber`, `RegistrationDate`) VALUES
(1, 'Nguyen Hung', '0987654321', '2024-10-01 08:00:00'),
(2, 'Tran Van An', '0912345678', '2024-10-05 09:30:00'),
(3, 'Le Thi Mai', '0933888999', '2024-10-10 14:15:00');

INSERT INTO `ComicBooks` (`ComicBookID`, `Title`, `Author`, `PricePerDay`) VALUES
(1, 'Conan', 'Gosho Aoyama', 5000.00),
(2, 'Doraemon', 'Fujiko F. Fujio', 4000.00),
(3, 'Dragon Ball', 'Akira Toriyama', 6000.00),
(4, 'One Piece', 'Eiichiro Oda', 5500.00),
(5, 'Naruto', 'Masashi Kishimoto', 5000.00);

INSERT INTO `Rentals` (`RentalID`, `CustomerID`, `RentalDate`, `ReturnDate`, `Status`) VALUES
(1, 1, '2024-10-01 08:00:00', '2024-10-10 17:00:00', 'Đang thuê'),
(2, 1, '2024-10-01 09:00:00', '2024-10-20 17:00:00', 'Đang thuê');

INSERT INTO `RentalDetails` (`RentalDetailID`, `RentalID`, `ComicBookID`, `Quantity`, `PricePerDay`) VALUES
(1, 1, 1, 1, 5000.00), -- Nguyen Hung thuê Conan (SL: 1)
(2, 2, 2, 3, 4000.00); -- Nguyen Hung thuê Doraemon (SL: 3)

-- Truy vấn kiểm tra bảng báo cáo câu 4:
SELECT 
    ROW_NUMBER() OVER (ORDER BY r.RentalDate, b.Title) AS `No`,
    b.Title AS `Book name`,
    DATE_FORMAT(r.RentalDate, '%d/%m/%Y') AS `Rental date`,
    DATE_FORMAT(r.ReturnDate, '%d/%m/%Y') AS `Return date`,
    c.FullName AS `Customer name`,
    rd.Quantity AS `Quantity`
FROM RentalDetails rd
JOIN Rentals r ON rd.RentalID = r.RentalID
JOIN Customers c ON r.CustomerID = c.CustomerID
JOIN ComicBooks b ON rd.ComicBookID = b.ComicBookID
WHERE r.RentalDate >= '2024-10-01' AND r.RentalDate <= '2024-10-31';
