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
-- Table structure for table `sample_values`
--

DROP TABLE IF EXISTS `sample_values`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sample_values` (
  `idsample_values` int(11) NOT NULL AUTO_INCREMENT,
  `ActiveSample_ID` int(11) DEFAULT NULL,
  `Name` varchar(128) DEFAULT NULL,
  `Value` varchar(128) DEFAULT NULL,
  `SampleID` varchar(128) DEFAULT NULL,
  `Hidden` bit(1) DEFAULT b'0',
  PRIMARY KEY (`idsample_values`),
  KEY `index2` (`ActiveSample_ID`,`SampleID`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sample_values`
--

LOCK TABLES `sample_values` WRITE;
/*!40000 ALTER TABLE `sample_values` DISABLE KEYS */;
INSERT INTO `sample_values` VALUES (43,10,'MAGAZINE_POS','100','barcode','\0'),(44,10,'MAGAZINE','2','barcode','\0'),(45,10,'SetRobotAccess','0','barcode',''),(46,10,'ScanAttempts','1','barcode',''),(47,10,'Scan','NotSuccessful','barcode',''),(48,11,'MAGAZINE_POS','98','uuu','\0'),(49,11,'MAGAZINE','2','uuu','\0'),(50,11,'SetRobotAccess','0','uuu',''),(51,11,'ScanAttempts','1','uuu',''),(52,11,'Scan','NotSuccessful','uuu',''),(53,12,'MAGAZINE_POS','20','fdhdf2012-11-30 16:27:17','\0'),(54,12,'MAGAZINE','2','fdhdf2012-11-30 16:27:17','\0'),(55,12,'SetRobotAccess','0','fdhdf2012-11-30 16:27:17',''),(56,12,'ScanAttempts','1','fdhdf2012-11-30 16:27:17',''),(57,12,'Scan','NotSuccessful','fdhdf2012-11-30 16:27:17',''),(58,13,'MAGAZINE_POS','100','test','\0'),(59,13,'MAGAZINE','2','test','\0'),(60,13,'SetRobotAccess','0','test',''),(61,13,'ScanAttempts','1','test',''),(62,13,'Scan','NotSuccessful','test',''),(63,14,'MAGAZINE_POS','100','test1','\0'),(64,14,'MAGAZINE','2','test1','\0'),(65,14,'SetRobotAccess','0','test1',''),(66,14,'ScanAttempts','1','test1',''),(67,14,'Scan','NotSuccessful','test1',''),(68,15,'MAGAZINE_POS','99','test3','\0'),(69,15,'MAGAZINE','2','test3','\0'),(70,15,'SetRobotAccess','0','test3',''),(71,15,'ScanAttempts','1','test3',''),(72,15,'Scan','NotSuccessful','test3',''),(79,17,'MAGAZINE_POS','1','crucible_1','\0'),(80,17,'MAGAZINE','1','crucible_1','\0'),(81,27,'MAGAZINE_POS','99','test5','\0'),(82,27,'MAGAZINE','2','test5','\0'),(83,27,'SetRobotAccess','0','test5',''),(84,27,'ScanAttempts','1','test5',''),(85,27,'Barcode','2530100\r','test5',''),(86,27,'Scan','Successfull','test5',''),(87,17,'SetRobotAccess','0','crucible_1',''),(88,18,'MAGAZINE_POS','2','crucible_2','\0'),(89,18,'MAGAZINE','1','crucible_2','\0'),(90,19,'MAGAZINE_POS','3','crucible_3','\0'),(91,19,'MAGAZINE','0','crucible_3','\0'),(92,1,'MAGAZINE_POS','1','crucible_2013-01-07 15:00:48','\0'),(93,1,'MAGAZINE','0','crucible_2013-01-07 15:00:48','\0'),(94,1,'StayActiveTest','1','crucible_2013-01-07 15:00:48',''),(99,4,'MAGAZINE_POS','3','sdfs','\0'),(100,4,'MAGAZINE','2','sdfs','\0'),(101,5,'MAGAZINE_POS','1','asdghf','\0'),(102,5,'MAGAZINE','4','asdghf','\0'),(103,6,'MAGAZINE_POS','3','kgzfhtd','\0'),(104,6,'MAGAZINE','1','kgzfhtd','\0');
/*!40000 ALTER TABLE `sample_values` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:42
