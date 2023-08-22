-- phpMyAdmin SQL Dump
-- version 5.1.1deb5ubuntu1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Aug 22, 2023 at 01:47 PM
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
-- Table structure for table `Causes`
--

CREATE TABLE `Causes` (
  `Id` bigint NOT NULL,
  `Slug` longtext NOT NULL,
  `Name` longtext NOT NULL,
  `ParentId` bigint DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Causes`
--

INSERT INTO `Causes` (`Id`, `Slug`, `Name`, `ParentId`, `Created`, `Updated`) VALUES
(1, 'arts-and-culture', 'Arts and culture', NULL, '2023-07-09 01:38:00.000000', '2023-07-09 01:38:00.000000'),
(2, 'education', 'Education', NULL, '2023-07-09 01:38:00.000000', '2023-07-09 01:38:00.000000'),
(3, 'environment', 'Environment', NULL, '2023-07-09 01:39:36.000000', '2023-07-09 01:39:36.000000'),
(4, 'health', 'Health', NULL, '2023-07-09 01:39:36.000000', '2023-07-09 01:39:36.000000'),
(5, 'science', 'Science', NULL, '2023-07-09 01:41:00.000000', '2023-07-09 01:41:00.000000'),
(6, 'social-sciences', 'Social sciences', NULL, '2023-07-09 01:41:00.000000', '2023-07-09 01:41:00.000000'),
(7, 'information-and-communications', 'Information and communications', NULL, '2023-07-09 01:43:25.000000', '2023-07-09 01:43:25.000000'),
(8, 'public-safety', 'Public safety', NULL, '2023-07-09 01:43:25.000000', '2023-07-09 01:43:25.000000'),
(9, 'public-affairs', 'Public affairs', NULL, '2023-07-09 01:44:57.000000', '2023-07-09 01:44:57.000000'),
(10, 'agriculture-fishing-and-forestry', 'Agriculture, fishing and forestry', NULL, '2023-07-09 01:44:57.000000', '2023-07-09 01:44:57.000000'),
(11, 'community-and-economic-development', 'Community and economic development', NULL, '2023-07-09 01:47:09.000000', '2023-07-09 01:47:09.000000'),
(12, 'religion', 'Religion', NULL, '2023-07-09 01:47:09.000000', '2023-07-09 01:47:09.000000'),
(13, 'sports-and-recreation', 'Sports and recreation', NULL, '2023-07-09 01:49:11.000000', '2023-07-09 01:49:11.000000'),
(14, 'human-rights', 'Human rights', NULL, '2023-07-09 01:49:11.000000', '2023-07-09 01:49:11.000000'),
(15, 'human-services', 'Human services', NULL, '2023-07-09 01:51:54.000000', '2023-07-09 01:51:54.000000'),
(16, 'international-relations', 'International relations', NULL, '2023-07-09 01:51:54.000000', '2023-07-09 01:51:54.000000'),
(17, 'unknown-or-not-classified', 'Unknown or not classified', NULL, '2023-07-09 01:53:23.000000', '2023-07-09 01:53:23.000000');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Causes`
--
ALTER TABLE `Causes`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Causes_ParentId` (`ParentId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Causes`
--
ALTER TABLE `Causes`
  MODIFY `Id` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Causes`
--
ALTER TABLE `Causes`
  ADD CONSTRAINT `FK_Causes_Causes_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Causes` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
