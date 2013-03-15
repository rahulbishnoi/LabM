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
-- Table structure for table `machine_programs`
--

DROP TABLE IF EXISTS `machine_programs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_programs` (
  `idmachine_programs` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `MachineList_ID` int(11) DEFAULT NULL,
  `Description` varchar(127) DEFAULT NULL,
  `Program_number` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmachine_programs`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_programs`
--

LOCK TABLES `machine_programs` WRITE;
/*!40000 ALTER TABLE `machine_programs` DISABLE KEYS */;
INSERT INTO `machine_programs` VALUES (27,'Program_1',2,NULL,1),(28,'Program_2',2,NULL,2),(29,'TGA_Prog_1',4,NULL,1),(30,'Program_3',2,NULL,3),(31,'Program_4',2,NULL,4),(32,'TGA_Prog_2',4,NULL,2),(33,'TGA_Prog_3',4,NULL,3),(34,'Program_1_Mixer',6,NULL,1),(35,'Program_2_Mixer',6,NULL,2),(36,'Program_3_Mixer',6,NULL,3),(37,'Program_5',2,NULL,5),(38,'Program_6',2,NULL,6),(39,'Program_7',2,NULL,7),(40,'Program_8',2,NULL,8),(41,'Program_9',2,NULL,9),(42,'Program_10',2,NULL,10),(43,'Program_11',2,NULL,11),(44,'Program_12',2,NULL,12),(45,'Program_13',2,NULL,13),(46,'Program_14',2,NULL,14),(47,'Program_15',2,NULL,15),(48,'Program_16',2,NULL,16);
/*!40000 ALTER TABLE `machine_programs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:12
