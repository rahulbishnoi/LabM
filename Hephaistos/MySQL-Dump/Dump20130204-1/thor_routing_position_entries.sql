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
-- Table structure for table `routing_position_entries`
--

DROP TABLE IF EXISTS `routing_position_entries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `routing_position_entries` (
  `idrouting_position_entries` int(11) NOT NULL AUTO_INCREMENT,
  `Position_ID` int(11) DEFAULT NULL,
  `SampleType_ID` int(11) DEFAULT NULL,
  `Description` tinytext,
  `Modified` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `TimeForWarning` int(11) DEFAULT '0',
  `TimeForAlarm` int(11) DEFAULT '0',
  `ActualTime` int(11) DEFAULT '0',
  `TimeWarningOn` tinyint(1) DEFAULT '0',
  `TimeAlarmOn` tinyint(1) DEFAULT '0',
  `Priority` int(11) DEFAULT '0',
  `CommandAlternateExceuted` bit(1) DEFAULT NULL,
  PRIMARY KEY (`idrouting_position_entries`),
  KEY `index2` (`Position_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routing_position_entries`
--

LOCK TABLES `routing_position_entries` WRITE;
/*!40000 ALTER TABLE `routing_position_entries` DISABLE KEYS */;
INSERT INTO `routing_position_entries` VALUES (1,2,2,'Condition: Vial at dosing 1 - request turntable access','2012-08-21 14:42:10',0,0,0,0,0,0,NULL),(9,13,2,NULL,'2012-08-22 09:22:58',0,0,0,0,0,0,NULL),(10,14,1,NULL,'2012-08-22 10:03:55',0,0,0,0,0,0,NULL),(11,16,1,NULL,'2012-08-22 10:06:12',0,0,0,0,0,0,NULL),(12,17,1,NULL,'2012-08-22 10:09:05',0,0,0,0,0,0,NULL),(13,18,1,NULL,'2012-08-22 10:53:02',0,0,0,0,0,0,NULL),(18,21,2,NULL,'2012-08-23 12:37:23',0,0,0,0,0,0,NULL),(19,22,1,NULL,'2012-08-23 15:04:06',0,0,0,0,0,0,NULL),(20,23,2,NULL,'2012-08-29 11:30:22',0,0,0,0,0,0,NULL),(21,25,2,NULL,'2012-08-29 11:55:42',0,0,0,0,0,0,NULL),(23,28,1,NULL,'2012-09-21 09:10:06',0,0,0,0,0,0,NULL),(24,7,1,'Condition: Vial to scanner - check access','2012-11-26 11:23:07',0,0,0,0,0,0,NULL),(26,27,1,'Condition: Scan barcode','2012-11-26 12:16:19',0,0,0,0,0,0,NULL),(27,27,1,'Condition: Scan was successful','2012-11-26 12:47:34',0,0,0,0,0,0,NULL),(28,7,1,'Condition: Vial to scanner - take vial','2012-11-26 16:26:35',0,0,0,0,0,0,NULL),(29,27,1,'Condition: Scan was not successful - request access to drawer 1','2012-11-28 15:54:32',0,0,0,0,0,0,NULL),(31,29,1,'Condition: To dosing 1','2012-11-30 13:47:21',0,0,0,0,0,0,NULL),(32,30,1,'Condition: Request access to drawer 1','2012-11-30 14:13:34',0,0,0,0,0,0,NULL),(33,30,1,'Condition: Back to drawer 1','2012-11-30 14:19:33',0,0,0,0,0,0,NULL),(34,31,1,'Condition: Wait for crucible','2012-12-03 10:29:03',0,0,0,0,0,0,NULL),(36,2,2,'Condition: Vial at dosing 1 - bring crucible to dosing','2012-12-03 11:30:30',0,0,0,0,0,0,NULL),(37,32,2,'Condition: Delete WS','2012-12-03 11:40:42',0,0,0,0,0,0,NULL),(38,33,2,'Condition: Reset access bits','2012-12-03 11:47:23',0,0,0,0,0,0,NULL),(39,34,1,NULL,'2012-12-03 14:24:50',0,0,0,0,0,0,NULL),(40,25,2,NULL,'2012-12-14 10:07:36',0,0,0,0,0,0,NULL),(41,25,1,NULL,'2012-12-14 10:08:45',0,0,0,0,0,0,NULL),(43,31,3,NULL,'2012-12-14 10:56:54',0,0,0,0,0,0,NULL),(44,21,2,NULL,'2012-12-17 14:57:47',0,0,0,0,0,0,NULL);
/*!40000 ALTER TABLE `routing_position_entries` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:22
