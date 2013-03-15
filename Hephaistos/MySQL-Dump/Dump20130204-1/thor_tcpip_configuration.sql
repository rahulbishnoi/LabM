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
-- Table structure for table `tcpip_configuration`
--

DROP TABLE IF EXISTS `tcpip_configuration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tcpip_configuration` (
  `idTCPIP_configuration` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(60) DEFAULT NULL,
  `Port` int(11) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  `IP_Address` varchar(25) DEFAULT NULL,
  `Machine_ID` int(11) DEFAULT NULL,
  `Description` text,
  `Activate` tinyint(4) DEFAULT NULL,
  `AnalyseType_ID` int(11) DEFAULT '-1',
  `TerminationString` varchar(8) DEFAULT '',
  PRIMARY KEY (`idTCPIP_configuration`),
  UNIQUE KEY `Port_UNIQUE` (`Port`),
  UNIQUE KEY `Machine_ID_UNIQUE` (`Machine_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tcpip_configuration`
--

LOCK TABLES `tcpip_configuration` WRITE;
/*!40000 ALTER TABLE `tcpip_configuration` DISABLE KEYS */;
INSERT INTO `tcpip_configuration` VALUES (1,'TGA',1235,0,NULL,2001,'TGA oven',0,2,''),(2,'Robot',2000,0,NULL,2002,'Robot',0,4,'\\r\\n'),(5,'RobotRemoteControl',5000,1,'192.168.1.20',2003,'Robot remote control',0,3,'\\r\\n');
/*!40000 ALTER TABLE `tcpip_configuration` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:06
