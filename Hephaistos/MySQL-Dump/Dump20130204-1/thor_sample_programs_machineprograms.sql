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
-- Table structure for table `sample_programs_machineprograms`
--

DROP TABLE IF EXISTS `sample_programs_machineprograms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sample_programs_machineprograms` (
  `idsample_programs_machineprograms` int(11) NOT NULL AUTO_INCREMENT,
  `MachineProgram_ID` int(11) DEFAULT NULL,
  `SampleTypeList_ID` int(11) DEFAULT NULL,
  `SampleProgram_ID` int(11) DEFAULT NULL,
  `MachineList_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`idsample_programs_machineprograms`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sample_programs_machineprograms`
--

LOCK TABLES `sample_programs_machineprograms` WRITE;
/*!40000 ALTER TABLE `sample_programs_machineprograms` DISABLE KEYS */;
INSERT INTO `sample_programs_machineprograms` VALUES (26,27,1,1,2),(27,28,1,2,2),(28,30,1,3,2),(29,33,1,3,4),(30,32,1,2,4),(31,32,1,1,4),(33,34,1,1,6),(34,35,1,2,6),(35,36,1,3,6),(36,28,4,1,2),(37,28,1,1,3),(38,27,2,4,2),(39,27,2,5,2),(40,28,2,6,2),(41,40,1,19,2),(42,35,1,19,6),(43,32,1,19,4),(44,29,2,5,4),(45,34,2,5,6),(46,35,2,6,6),(47,32,2,6,4);
/*!40000 ALTER TABLE `sample_programs_machineprograms` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:26
