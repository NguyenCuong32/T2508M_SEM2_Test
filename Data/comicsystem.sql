-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Máy chủ: localhost:3306
-- Thời gian đã tạo: Th9 11, 2026 lúc 02:09 AM
-- Phiên bản máy phục vụ: 5.7.24
-- Phiên bản PHP: 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `comicsystem`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `comicbooks`
--

CREATE TABLE `comicbooks` (
  `ComicBookID` int(11) NOT NULL,
  `Title` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Author` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `PricePerDay` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `comicbooks`
--

INSERT INTO `comicbooks` (`ComicBookID`, `Title`, `Author`, `PricePerDay`) VALUES
(1, 'Hello Baby', 'Dieu Chinh Duc', '12000.00'),
(3, 'I Am Spider Man', 'Dieu Chinh Duc', '15000.00');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customers`
--

CREATE TABLE `customers` (
  `CustomerID` int(11) NOT NULL,
  `FullName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `PhoneNumber` varchar(15) COLLATE utf8mb4_unicode_ci NOT NULL,
  `RegistrationDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `customers`
--

INSERT INTO `customers` (`CustomerID`, `FullName`, `PhoneNumber`, `RegistrationDate`) VALUES
(1, 'Dieu Chinh Duc', '0123456789', '2026-09-11 08:45:50'),
(2, 'Nguyen Van A', '0987654321', '2026-09-11 09:07:45');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `rentaldetails`
--

CREATE TABLE `rentaldetails` (
  `RentalDetailID` int(11) NOT NULL,
  `RentalID` int(11) NOT NULL,
  `ComicBookID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `PricePerDay` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `rentaldetails`
--

INSERT INTO `rentaldetails` (`RentalDetailID`, `RentalID`, `ComicBookID`, `Quantity`, `PricePerDay`) VALUES
(1, 1, 1, 1, '12000.00'),
(2, 2, 3, 1, '15000.00');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `rentals`
--

CREATE TABLE `rentals` (
  `RentalID` int(11) NOT NULL,
  `CustomerID` int(11) NOT NULL,
  `RentalDate` datetime NOT NULL,
  `ReturnDate` datetime NOT NULL,
  `Status` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `rentals`
--

INSERT INTO `rentals` (`RentalID`, `CustomerID`, `RentalDate`, `ReturnDate`, `Status`) VALUES
(1, 1, '2026-09-11 08:51:05', '2026-09-13 08:51:05', 'Active'),
(2, 2, '2026-09-11 09:07:57', '2026-09-12 09:07:57', 'Active');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `comicbooks`
--
ALTER TABLE `comicbooks`
  ADD PRIMARY KEY (`ComicBookID`);

--
-- Chỉ mục cho bảng `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`CustomerID`);

--
-- Chỉ mục cho bảng `rentaldetails`
--
ALTER TABLE `rentaldetails`
  ADD PRIMARY KEY (`RentalDetailID`),
  ADD KEY `FK_RentalDetails_Rentals` (`RentalID`),
  ADD KEY `FK_RentalDetails_ComicBooks` (`ComicBookID`);

--
-- Chỉ mục cho bảng `rentals`
--
ALTER TABLE `rentals`
  ADD PRIMARY KEY (`RentalID`),
  ADD KEY `FK_Rentals_Customers` (`CustomerID`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `comicbooks`
--
ALTER TABLE `comicbooks`
  MODIFY `ComicBookID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `customers`
--
ALTER TABLE `customers`
  MODIFY `CustomerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `rentaldetails`
--
ALTER TABLE `rentaldetails`
  MODIFY `RentalDetailID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `rentals`
--
ALTER TABLE `rentals`
  MODIFY `RentalID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `rentaldetails`
--
ALTER TABLE `rentaldetails`
  ADD CONSTRAINT `FK_RentalDetails_ComicBooks` FOREIGN KEY (`ComicBookID`) REFERENCES `comicbooks` (`ComicBookID`),
  ADD CONSTRAINT `FK_RentalDetails_Rentals` FOREIGN KEY (`RentalID`) REFERENCES `rentals` (`RentalID`) ON DELETE CASCADE;

--
-- Các ràng buộc cho bảng `rentals`
--
ALTER TABLE `rentals`
  ADD CONSTRAINT `FK_Rentals_Customers` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`CustomerID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
