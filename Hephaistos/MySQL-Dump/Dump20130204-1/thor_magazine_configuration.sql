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
-- Table structure for table `magazine_configuration`
--

DROP TABLE IF EXISTS `magazine_configuration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `magazine_configuration` (
  `idmagazine_configuration` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(64) DEFAULT NULL,
  `Dimension_X` int(11) DEFAULT NULL,
  `Dimension_Y` int(11) DEFAULT NULL,
  `ForceFIFO` tinyint(1) DEFAULT '0',
  `SortType_ID` int(11) DEFAULT '1',
  `InputPosition` int(11) DEFAULT NULL,
  `OutputPosition` int(11) DEFAULT NULL,
  `Machine_ID` int(11) DEFAULT NULL,
  `StopMode` tinyint(1) DEFAULT '0',
  `RegistrationPoint` tinyint(1) DEFAULT '0',
  `SampleObjectList_ID` int(11) DEFAULT NULL,
  `Description` tinytext,
  `IsRobot` bit(1) DEFAULT b'0',
  `RobotMagazinePosition` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmagazine_configuration`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=227 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magazine_configuration`
--

LOCK TABLES `magazine_configuration` WRITE;
/*!40000 ALTER TABLE `magazine_configuration` DISABLE KEYS */;
INSERT INTO `magazine_configuration` VALUES (1,'CrucibleMagazine',10,6,0,1,NULL,54,1001,0,1,2,NULL,'',0),(2,'VialMagazine1',10,10,0,0,101,102,1003,0,1,1,NULL,'',2),(4,'VialMagazine2',10,10,0,0,113,114,1004,0,1,1,NULL,'',4),(5,'VialMagazineQC',6,3,0,0,125,126,1005,0,1,1,NULL,'',1),(6,'CrucibleCoolingMagazine',4,3,0,1,264,265,1006,0,1,2,NULL,'',6),(7,'Inputbelt',10,1,1,1,137,138,1007,0,0,2,NULL,'\0',NULL),(8,'Outputbelt',10,1,1,1,139,140,1008,0,0,2,NULL,'\0',NULL),(226,'TubeMagazine',5,10,0,1,602,603,1009,0,0,2,NULL,'',7);
/*!40000 ALTER TABLE `magazine_configuration` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:36
