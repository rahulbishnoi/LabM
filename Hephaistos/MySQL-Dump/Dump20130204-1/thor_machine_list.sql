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
-- Table structure for table `machine_list`
--

DROP TABLE IF EXISTS `machine_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_list` (
  `idmachine_list` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `Description` tinytext,
  `Connection_type_list_ID` int(11) DEFAULT NULL,
  `Color` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmachine_list`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_list`
--

LOCK TABLES `machine_list` WRITE;
/*!40000 ALTER TABLE `machine_list` DISABLE KEYS */;
INSERT INTO `machine_list` VALUES (1,'Magazine','Magazines for in- and output',5,16744448),(2,'Dosing','Dosing stations',1,8421631),(3,'Robot','Robot to pick up the valves and crucibles',4,33023),(4,'TGA','TGA oven for the \"loss of ignition\" calculation',4,12632256),(5,'TransportSystem','Belt transport system',1,12632256),(6,'Mixer','Mixer for the crucibles',1,12632256),(7,'Scanner','Barcode scanner',1,12632256),(8,'GeneralPLC','General information ',1,NULL),(9,'CrucibleTurnTable','Turntable for the crucible magazines',1,NULL),(10,'SampleDrawer','Drawer for the vials magazines',1,NULL);
/*!40000 ALTER TABLE `machine_list` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:49
