-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 06, 2025 at 09:49 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `baksoantedar`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_admin`
--

CREATE TABLE `tbl_admin` (
  `noadmin` varchar(10) NOT NULL,
  `namaadmin` varchar(50) NOT NULL,
  `passwordadmin` varchar(50) NOT NULL,
  `leveladmin` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_admin`
--

INSERT INTO `tbl_admin` (`noadmin`, `namaadmin`, `passwordadmin`, `leveladmin`) VALUES
('admin001', 'User', '123', 'User'),
('admin002', 'antedar', '123', 'Admin'),
('admin004', 'aisyah', '12345', 'USER'),
('admin005', 'Aqila', '12345', 'ADMIN');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_barang`
--

CREATE TABLE `tbl_barang` (
  `nobarang` varchar(10) NOT NULL,
  `namabarang` varchar(50) NOT NULL,
  `hargabarang` int(11) NOT NULL,
  `stokbarang` int(11) NOT NULL,
  `satuanbarang` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_barang`
--

INSERT INTO `tbl_barang` (`nobarang`, `namabarang`, `hargabarang`, `stokbarang`, `satuanbarang`) VALUES
('BRG001', 'Bakso Telur', 11000, 20, 'Porsi'),
('BRG002', 'Gandum', 10000, 20, 'Kg');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_detailtransaksi`
--

CREATE TABLE `tbl_detailtransaksi` (
  `notrans` varchar(10) NOT NULL,
  `nomenu` varchar(10) NOT NULL,
  `namamenu` varchar(50) NOT NULL,
  `hargajual` int(11) NOT NULL,
  `jumlahjual` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_detailtransaksi`
--

INSERT INTO `tbl_detailtransaksi` (`notrans`, `nomenu`, `namamenu`, `hargajual`, `jumlahjual`, `subtotal`) VALUES
('TR24122200', 'MNB002', 'Bakso Urat', 11000, 4, 44000),
('T241222201', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('TR24122220', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T241222001', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T241222002', 'MNB001', 'Bakso Daging', 11000, 2, 22000),
('T241222003', 'MNB002', 'Bakso Urat', 11000, 1, 11000),
('T241222004', '1MNB003', 'Bakso Biasa', 7000, 1, 7000),
('T241222005', 'MNB002', 'Bakso Urat', 11000, 1, 11000),
('T241222006', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T241222007', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T241222008', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T241222009', 'MNB001', 'Bakso Daging', 11000, 5, 55000),
('T241223010', 'MNB001', 'Bakso Daging', 11000, 2, 22000),
('T241223010', 'MNB003', 'Bakso Biasa', 7000, 5, 35000),
('T250306011', 'MNB001', 'Bakso Daging', 11000, 1, 11000),
('T250306011', 'MNB002', 'Bakso Urat', 11000, 1, 11000);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_menu`
--

CREATE TABLE `tbl_menu` (
  `nomenu` varchar(10) NOT NULL,
  `namamenu` varchar(50) NOT NULL,
  `hargamenu` int(11) NOT NULL,
  `stokmenu` int(11) NOT NULL,
  `satuanmenu` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_menu`
--

INSERT INTO `tbl_menu` (`nomenu`, `namamenu`, `hargamenu`, `stokmenu`, `satuanmenu`) VALUES
('MNB001', 'Bakso Daging', 11000, 29, 'Porsi'),
('MNB002', 'Bakso Urat', 11000, 43, 'Porsi'),
('MNB003', 'Bakso Biasa', 7000, 45, 'Porsi');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_transaksi`
--

CREATE TABLE `tbl_transaksi` (
  `notrans` varchar(10) NOT NULL,
  `tgltrans` date NOT NULL,
  `jamtrans` time NOT NULL,
  `itemtrans` int(11) NOT NULL,
  `totaltrans` int(11) NOT NULL,
  `jumlahbayar` int(11) NOT NULL,
  `jumlahkembali` int(11) NOT NULL,
  `noadmin` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_transaksi`
--

INSERT INTO `tbl_transaksi` (`notrans`, `tgltrans`, `jamtrans`, `itemtrans`, `totaltrans`, `jumlahbayar`, `jumlahkembali`, `noadmin`) VALUES
('T241222001', '2024-12-22', '15:31:11', 1, 11000, 20000, 9000, 'admin001'),
('T241222002', '2024-12-22', '16:08:02', 2, 22000, 30000, 8000, 'admin001'),
('T241222003', '2024-12-22', '16:11:51', 1, 11000, 20000, 9000, 'admin001'),
('T241222005', '2024-12-22', '16:14:39', 1, 11000, 15000, 4000, 'admin001'),
('T241222006', '2024-12-22', '16:16:05', 1, 11000, 11000, 0, 'admin001'),
('T241222007', '2024-12-22', '16:54:52', 1, 11000, 12000, 1000, 'admin001'),
('T241222008', '2024-12-22', '17:51:12', 1, 11000, 12000, 1000, 'admin001'),
('T241222009', '2024-12-22', '21:28:43', 5, 55000, 60000, 5000, 'admin002'),
('T241223010', '2024-12-23', '00:41:47', 7, 57000, 60000, 3000, 'admin002'),
('T250306011', '2025-03-06', '09:09:42', 2, 22000, 30000, 8000, 'admin001');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_admin`
--
ALTER TABLE `tbl_admin`
  ADD PRIMARY KEY (`noadmin`);

--
-- Indexes for table `tbl_barang`
--
ALTER TABLE `tbl_barang`
  ADD PRIMARY KEY (`nobarang`);

--
-- Indexes for table `tbl_menu`
--
ALTER TABLE `tbl_menu`
  ADD PRIMARY KEY (`nomenu`);

--
-- Indexes for table `tbl_transaksi`
--
ALTER TABLE `tbl_transaksi`
  ADD PRIMARY KEY (`notrans`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
