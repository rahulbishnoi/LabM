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
-- Table structure for table `machine_state_list`
--

DROP TABLE IF EXISTS `machine_state_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_state_list` (
  `idmachine_state_list` int(11) NOT NULL AUTO_INCREMENT,
  `MachineList_ID` int(11) DEFAULT '0',
  `StatusWord0` bit(1) DEFAULT b'0',
  `StatusWord1` bit(1) DEFAULT b'0',
  `StatusWord2` bit(1) DEFAULT b'0',
  `StatusWord3` bit(1) DEFAULT b'0',
  `StatusWord4` bit(1) DEFAULT b'0',
  `StatusWord5` bit(1) DEFAULT b'0',
  `StatusWord6` bit(1) DEFAULT b'0',
  `StatusWord7` bit(1) DEFAULT b'0',
  `StatusWord8` bit(1) DEFAULT b'0',
  `StatusWord9` bit(1) DEFAULT b'0',
  PRIMARY KEY (`idmachine_state_list`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_state_list`
--

LOCK TABLES `machine_state_list` WRITE;
/*!40000 ALTER TABLE `machine_state_list` DISABLE KEYS */;
INSERT INTO `machine_state_list` VALUES (1,1,'\0','','\0','\0','\0','\0','\0','\0','',''),(2,2,'','','','','','\0','','','\0','\0'),(3,3,'','','','','\0','\0','','','\0','\0'),(4,4,'','','\0','','\0','','','','\0','\0'),(5,5,'','','','','','\0','','','\0','\0'),(6,6,'','','','','','\0','','\0','\0','\0'),(7,7,'','','','\0','\0','\0','','\0','\0','\0'),(8,8,'','','','','','\0','','','\0','\0'),(9,9,'','','','','','\0','','','\0','\0'),(10,10,'','','','','','\0','','','\0','\0');
/*!40000 ALTER TABLE `machine_state_list` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:15
