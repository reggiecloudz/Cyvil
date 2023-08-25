-- phpMyAdmin SQL Dump
-- version 5.1.1deb5ubuntu1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Aug 22, 2023 at 09:14 PM
-- Server version: 8.0.34-0ubuntu0.22.04.1
-- PHP Version: 8.1.2-1ubuntu2.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cyvilmvc`
--

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) NOT NULL,
  `FullName` longtext NOT NULL,
  `IsMember` tinyint(1) NOT NULL,
  `ProfileImage` longtext NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `Slug` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `FullName`, `IsMember`, `ProfileImage`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `Slug`) VALUES
('0388cba8-6d49-42bc-b241-35e002801755', 'Richard Green', 1, 'ed49388f-dd1b-4710-ada8-fbe83e4598e0_richard.jpg', 'richardgreen', 'RICHARDGREEN', 'richardgreen@local.com', 'RICHARDGREEN@LOCAL.COM', 0, 'AQAAAAEAACcQAAAAEJW4SmqlZbhK+9W97hsMGL3J8CxIXckk1cwBo6VyvI+02zfzw7s0TpACaUpNkspbGw==', 'C5HSVTVKR5MYUON3MSCKXMMNV6UNL2S3', 'd90d7483-5683-4273-828e-8220ea5db7a5', NULL, 0, 0, NULL, 1, 0, 'richard-green-1692763863873'),
('377a0c7b-b644-436d-aff3-69a511cb2163', 'Tanya Woods', 1, '067f92dd-a218-4d85-aa61-cb0235548261_tanya.jpg', 'tanyawoods', 'TANYAWOODS', 'tanyawoods@local.com', 'TANYAWOODS@LOCAL.COM', 0, 'AQAAAAEAACcQAAAAELvaWZg6n0OwQ1seCzTQMt2q3fH/vHjGrPQCCjySm4H4hpRKbGkY9Ubbpbj+uUPTyw==', 'IEF66M4HIB4WMH24YK22EJFFVLHUXYRC', 'a595bd39-9e39-41c1-9c0c-a4eccbb52df2', NULL, 0, 0, NULL, 1, 0, 'tanya-woods-1692763904769'),
('4ea9ca22-de58-4eb9-b27e-fd4e5e843cf2', 'Sammy Bear', 1, '4a7c0748-ca12-47e4-b020-d321256d0711_sammy.jpg', 'sammybear', 'SAMMYBEAR', 'sammybear@local.com', 'SAMMYBEAR@LOCAL.COM', 0, 'AQAAAAEAACcQAAAAENzzDLgH36Q5DWkgvnevt9xfYp0zPeQZWMIVXvs/ZM52fuhN6hgLaiNPzb5FHGG24w==', 'RHSYMXOJP7KQT633POBQX5PB3FMVVNXY', '22ab385d-2076-4dc6-a6dd-f66791b569ab', NULL, 0, 0, NULL, 1, 0, 'sammy-bear-1692763939179');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
