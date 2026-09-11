CREATE DATABASE IF NOT EXISTS ComicSystem
    CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE ComicSystem;

CREATE TABLE IF NOT EXISTS ComicBooks (
    ComicBookId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS Customers (
    CustomerId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    RegistrationDate DATETIME(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS Rentals (
    RentalId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    CustomerId INT NOT NULL,
    RentalDate DATETIME(6) NOT NULL,
    ReturnDate DATETIME(6) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    CONSTRAINT FK_Rentals_Customers_CustomerId FOREIGN KEY (CustomerId)
        REFERENCES Customers(CustomerId) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS RentalDetails (
    RentalDetailId INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    RentalId INT NOT NULL,
    ComicBookId INT NOT NULL,
    Quantity INT NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_RentalDetails_Rentals_RentalId FOREIGN KEY (RentalId)
        REFERENCES Rentals(RentalId) ON DELETE CASCADE,
    CONSTRAINT FK_RentalDetails_ComicBooks_ComicBookId FOREIGN KEY (ComicBookId)
        REFERENCES ComicBooks(ComicBookId) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
INSERT INTO ComicBooks (Title, Author, PricePerDay)
SELECT 'Conan', 'Gosho Aoyama', 5000
WHERE NOT EXISTS (SELECT 1 FROM ComicBooks WHERE Title = 'Conan' AND Author = 'Gosho Aoyama');

INSERT INTO ComicBooks (Title, Author, PricePerDay)
SELECT 'Doraemon', 'Fujiko F. Fujio', 4000
WHERE NOT EXISTS (SELECT 1 FROM ComicBooks WHERE Title = 'Doraemon' AND Author = 'Fujiko F. Fujio');

INSERT INTO ComicBooks (Title, Author, PricePerDay)
SELECT 'One Piece', 'Eiichiro Oda', 6000
WHERE NOT EXISTS (SELECT 1 FROM ComicBooks WHERE Title = 'One Piece' AND Author = 'Eiichiro Oda');
