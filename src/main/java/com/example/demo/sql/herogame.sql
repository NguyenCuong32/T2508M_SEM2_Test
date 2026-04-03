-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Máy chủ: localhost:3306
-- Thời gian đã tạo: Th4 03, 2026 lúc 02:28 AM
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
-- Cơ sở dữ liệu: `herogame`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `national`
--

CREATE TABLE `national` (
  `NationalId` int(11) NOT NULL,
  `NationalName` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `national`
--

INSERT INTO `national` (`NationalId`, `NationalName`) VALUES
(1, 'Vietnam'),
(2, 'USA'),
(3, 'Japan'),
(4, 'Korea'),
(5, 'YEah'),
(10, 'Singapore');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `player`
--

CREATE TABLE `player` (
  `PlayerId` int(11) NOT NULL,
  `NationalId` int(11) DEFAULT NULL,
  `PlayerName` varchar(100) DEFAULT NULL,
  `HighScore` int(11) DEFAULT NULL,
  `Level` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `player`
--

INSERT INTO `player` (`PlayerId`, `NationalId`, `PlayerName`, `HighScore`, `Level`) VALUES
(1, 1, 'Nguyen Van A', 1000, 10),
(2, 2, 'John Smith', 1500, 12),
(3, 3, 'Sakura', 2000, 15),
(4, 4, 'Kim', 1800, 14),
(5, 1, 'Tran Van B', 800, 8),
(6, 2, 'David', 900, 9),
(7, 3, 'Naruto', 2200, 20),
(9, 1, 'Chinh Duc', 99, 100);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `national`
--
ALTER TABLE `national`
  ADD PRIMARY KEY (`NationalId`);

--
-- Chỉ mục cho bảng `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`PlayerId`),
  ADD KEY `NationalId` (`NationalId`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `player`
--
ALTER TABLE `player`
  MODIFY `PlayerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `player`
--
ALTER TABLE `player`
  ADD CONSTRAINT `player_ibfk_1` FOREIGN KEY (`NationalId`) REFERENCES `national` (`NationalId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
