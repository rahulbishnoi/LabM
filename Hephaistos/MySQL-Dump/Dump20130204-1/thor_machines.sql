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
-- Table structure for table `machines`
--

DROP TABLE IF EXISTS `machines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machines` (
  `idmachines` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Number` int(11) DEFAULT NULL,
  `Machine_list_ID` int(11) DEFAULT NULL,
  `Description` tinytext,
  `DailyCounter` int(11) DEFAULT '0',
  `TotalCounter` int(11) DEFAULT '0',
  `ViewInSampleTracking` int(11) DEFAULT '0',
  PRIMARY KEY (`idmachines`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machines`
--

LOCK TABLES `machines` WRITE;
/*!40000 ALTER TABLE `machines` DISABLE KEYS */;
INSERT INTO `machines` VALUES (1,'SampleDrawer1',1,10,'Sample drawer for vial magazine 1',0,0,0),(2,'SampleDrawer2',1,10,'Sample drawer for vial magazine 2',0,0,0),(3,'SampleDrawer3QC',1,10,'Sample drawer for vial magazine 3 QC',0,0,0),(4,'CrucibleTurntable',1,2,'Turntable for the crucible magazines',0,0,0),(5,'Mixer',1,6,'Mixer for the crucibles to mix flux with the sample material',0,0,1),(6,'Dosing1',1,2,'Dosing station where the crucibles will be filled up with sample material',0,0,1),(7,'Dosing2',1,2,'Dosing station where the crucibles will be filled up with sample material',0,0,1),(8,'Transport',1,5,'Transport system with input- and outputbelt and two turntables',0,0,1),(9,'Scanner',1,7,'Barcode scanner for the vials',0,0,1),(999,'GeneralPLC',1,8,'General information fo the whole machine',0,0,0),(1001,'CrucibleInputmagazine1',1,1,'Crucible magazine 1',0,0,1),(1002,'CrucibleInputmagazine2',1,1,'Crucible magazine 2',0,0,1),(1003,'SampleInputmagazine1',1,1,'Vial magazine 1',0,0,1),(1004,'SampleInputmagazine2',1,1,'Vial magazine 2',0,0,1),(1005,'SampleQCMagazine1',1,1,'Vial magazine 3 QC',0,0,1),(1006,'Crucible coolingMagazine',1,1,'Magazine with samples to cool down',0,0,1),(1007,'InputbeltMagazine',1,1,'Inputbelt magazine for the acid glasses',0,0,1),(1008,'OutputbeltMagazine',1,1,'Outputbelt magazine for the acid glasses',0,0,1),(1009,'TubeMagazine',NULL,1,'Tube Magazine ',0,0,0),(2001,'TGA',1,4,'TGA',0,0,1),(2002,'Robot',1,3,'Robot',0,0,1),(2003,'Robot Remote',1,3,'Robot Remote Control',0,0,1);
/*!40000 ALTER TABLE `machines` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:43
