CREATE DATABASE  IF NOT EXISTS `thor` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `thor`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: thor
-- ------------------------------------------------------
-- Server version	5.5.25-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `command_done`
--

DROP TABLE IF EXISTS `command_done`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `command_done` (
  `MachineCommand_ID` int(11) DEFAULT NULL,
  `Program_ID` int(11) DEFAULT NULL,
  `Sample_ID` int(11) DEFAULT NULL,
  `InsertCommandTimestamp` timestamp NULL DEFAULT NULL,
  `ExecuteTimestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `command_done`
--

LOCK TABLES `command_done` WRITE;
/*!40000 ALTER TABLE `command_done` DISABLE KEYS */;
INSERT INTO `command_done` VALUES (19,-1,-1,'2013-01-18 14:39:40','2013-01-18 14:39:41'),(50,-1,-1,'2013-01-18 14:39:58','2013-01-18 14:39:59'),(18,-1,-1,'2013-01-18 14:40:59','2013-01-18 14:41:00'),(19,-1,-1,'2013-01-18 14:41:49','2013-01-18 14:41:49'),(51,-1,-1,'2013-01-18 14:42:00','2013-01-18 14:42:00'),(19,-1,-1,'2013-01-18 14:51:51','2013-01-18 14:51:52'),(50,-1,-1,'2013-01-18 14:53:17','2013-01-18 14:53:18'),(19,-1,-1,'2013-01-18 15:47:41','2013-01-18 15:47:42'),(19,-1,-1,'2013-01-18 15:48:02','2013-01-18 15:48:03'),(19,-1,-1,'2013-01-18 15:50:34','2013-01-18 15:50:35'),(19,-1,-1,'2013-01-18 15:50:51','2013-01-18 15:50:52'),(18,-1,-1,'2013-01-18 15:51:32','2013-01-18 15:51:58'),(19,-1,-1,'2013-01-18 15:51:43','2013-01-18 15:51:58'),(19,-1,-1,'2013-01-18 15:52:03','2013-01-18 15:52:04'),(19,-1,-1,'2013-01-18 15:52:18','2013-01-18 15:52:19'),(19,-1,-1,'2013-01-18 15:55:22','2013-01-18 15:55:23'),(19,-1,-1,'2013-01-18 15:55:51','2013-01-18 15:55:52'),(18,-1,-1,'2013-01-18 15:56:52','2013-01-18 15:56:53'),(19,-1,-1,'2013-01-18 15:58:40','2013-01-18 15:59:09'),(19,-1,-1,'2013-01-18 16:00:45','2013-01-18 16:01:41'),(18,-1,-1,'2013-01-18 16:03:40','2013-01-18 16:03:45'),(18,-1,-1,'2013-01-18 16:03:57','2013-01-18 16:03:58'),(19,-1,-1,'2013-01-18 16:04:03','2013-01-18 16:04:03'),(28,-1,-1,'2013-01-18 16:04:14','2013-01-18 16:04:15'),(20,-1,-1,'2013-01-18 16:04:28','2013-01-18 16:04:28'),(28,-1,-1,'2013-01-18 16:04:49','2013-01-18 16:04:50'),(20,-1,-1,'2013-01-18 16:23:19','2013-01-18 16:23:21'),(28,-1,-1,'2013-01-18 16:31:13','2013-01-18 16:31:14'),(20,-1,-1,'2013-01-18 16:31:57','2013-01-18 16:31:58'),(94,-1,-1,'2013-01-18 16:32:33','2013-01-18 16:32:34'),(97,-1,-1,'2013-01-18 16:33:12','2013-01-18 16:33:13'),(94,-1,-1,'2013-01-18 16:34:46','2013-01-18 16:34:47'),(54,-1,-1,'2013-01-18 16:35:06','2013-01-18 16:35:07'),(56,-1,-1,'2013-01-18 16:35:21','2013-01-18 16:35:22'),(71,-1,-1,'2013-01-18 16:40:16','2013-01-18 16:40:17'),(72,-1,-1,'2013-01-18 16:40:19','2013-01-18 16:40:20'),(74,-1,-1,'2013-01-18 16:40:29','2013-01-18 16:40:30'),(81,-1,-1,'2013-01-18 16:44:45','2013-01-18 16:44:46'),(28,-1,-1,'2013-01-18 16:47:40','2013-01-18 16:47:41'),(20,-1,-1,'2013-01-18 16:47:44','2013-01-18 16:47:44'),(28,-1,-1,'2013-01-18 16:47:47','2013-01-18 16:47:48'),(18,-1,-1,'2013-01-18 16:48:05','2013-01-18 16:48:06'),(19,-1,-1,'2013-01-18 16:48:10','2013-01-18 16:48:11'),(18,-1,-1,'2013-01-21 10:22:33','2013-01-21 10:22:34'),(19,-1,-1,'2013-01-21 10:23:30','2013-01-21 10:23:31'),(19,-1,-1,'2013-01-24 12:21:22','2013-01-24 12:21:23'),(50,-1,-1,'2013-01-24 12:21:29','2013-01-24 12:21:30'),(18,-1,-1,'2013-01-24 16:08:41','2013-01-24 16:08:42'),(19,-1,-1,'2013-01-24 16:09:23','2013-01-24 16:09:24'),(18,-1,-1,'2013-01-24 16:09:40','2013-01-24 16:09:41'),(19,-1,-1,'2013-01-24 16:09:54','2013-01-24 16:09:55'),(28,-1,-1,'2013-01-24 16:10:04','2013-01-24 16:10:05');
/*!40000 ALTER TABLE `command_done` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:57
