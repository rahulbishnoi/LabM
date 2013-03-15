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
-- Table structure for table `sample_programs`
--

DROP TABLE IF EXISTS `sample_programs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sample_programs` (
  `idsample_programs` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Description` tinytext,
  `SampleTypeList_ID` int(11) DEFAULT NULL,
  `Detection` varchar(60) DEFAULT NULL,
  `Color` int(11) DEFAULT '-1',
  `ShowStatistic` tinyint(1) DEFAULT '0',
  `DefaultProgram` tinyint(4) DEFAULT '0',
  `SampleObjectList_ID` int(11) DEFAULT NULL,
  `Reactivate` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`idsample_programs`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sample_programs`
--

LOCK TABLES `sample_programs` WRITE;
/*!40000 ALTER TABLE `sample_programs` DISABLE KEYS */;
INSERT INTO `sample_programs` VALUES (0,'Reservation Point',NULL,0,'',12632256,0,0,NULL,NULL),(1,'Production1','Production sample',1,'A(1,2,4)?????(a-d)',16711935,1,0,1,0),(2,'Production2',NULL,1,'B(1,2,4)?????(a-d)',16776960,1,0,1,1),(3,'Production3',NULL,1,'C(1,2,4)?????(a-d)',33023,1,0,1,1),(5,'Crucible Type 1',NULL,2,'Crucible_1????',16744576,0,0,2,0),(6,'Crucible Type 2',NULL,2,'Crucible_2????',16744448,0,0,2,0),(7,'QualityCheckSample1',NULL,3,'Q-(1-2)?????',12632256,1,0,1,0),(8,'QualityCheckSample2',NULL,3,'Q-(3-4)?????',65280,1,0,1,0);
/*!40000 ALTER TABLE `sample_programs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:14
