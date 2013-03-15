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
-- Table structure for table `machine_commands`
--

DROP TABLE IF EXISTS `machine_commands`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_commands` (
  `idmachine_commands` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Number` int(11) DEFAULT NULL,
  `Machine_ID` int(11) DEFAULT NULL,
  `Description` tinytext,
  PRIMARY KEY (`idmachine_commands`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_commands`
--

LOCK TABLES `machine_commands` WRITE;
/*!40000 ALTER TABLE `machine_commands` DISABLE KEYS */;
INSERT INTO `machine_commands` VALUES (18,'stop',1,6,NULL),(19,'reset',2,6,NULL),(20,'set to manual',3,6,NULL),(21,'bring object from pos X to pos Y',10,2002,NULL),(23,'stop',1,8,NULL),(27,'start analyse TGA',10,2001,NULL),(28,'set to auto',4,6,''),(29,'insert sample',1,1001,NULL),(30,'delete sample',2,1001,NULL),(31,'insert sample',1,1002,NULL),(32,'delete sample',2,1002,NULL),(33,'start mixer',10,5,NULL),(34,'force sortorder ON',3,1001,NULL),(35,'force sortorder OFF',4,1001,NULL),(36,'insert',1,1008,NULL),(37,'delete',2,1008,NULL),(38,'Bring sample OK',20,2001,NULL),(39,'Take sample OK',21,2001,NULL),(40,'Insert entry to sample list',11,2001,NULL),(41,'Delete from sample list',12,2001,NULL),(42,'insert sample',1,1003,NULL),(43,'delete sample',2,1003,NULL),(44,'force sortorder ON',3,1003,NULL),(45,'force sortorder OFF',4,1003,NULL),(46,'put sample back',5,1001,NULL),(47,'put sample back',5,1003,NULL),(48,'run a duplicate sample',6,1001,NULL),(49,'run a duplicate sample',6,1003,NULL),(50,'sync',5,6,NULL),(51,'start dosing',10,6,NULL),(52,'stop',1,9,NULL),(53,'tare',11,6,NULL),(54,'reset',2,9,NULL),(55,'set to manual',3,9,NULL),(56,'set to auto',4,9,''),(57,'sync',5,9,NULL),(58,'scan barcode',10,9,NULL),(59,'stop',1,1,NULL),(60,'reset',2,1,NULL),(61,'set to manual',3,1,NULL),(62,'set to auto',4,1,NULL),(63,'sync',5,1,NULL),(64,'set robot access',10,1,NULL),(65,'reset robot access',11,1,NULL),(66,'stop ',1,2,NULL),(67,'reset',2,2,NULL),(68,'set to manual',3,2,NULL),(69,'set to auto',4,2,NULL),(70,'sync',5,2,NULL),(71,'stop',1,3,NULL),(72,'reset',2,3,NULL),(73,'set to manual',3,3,NULL),(74,'set to auto',4,3,NULL),(75,'sync',5,3,NULL),(76,'set robot access',10,3,NULL),(77,'reset robot access',11,3,NULL),(78,'set robot access',10,2,NULL),(79,'reset robot access',11,2,NULL),(80,'stop',1,4,NULL),(81,'reset',2,4,NULL),(82,'set to manual',3,4,NULL),(83,'set to auto',4,4,NULL),(84,'sync',5,4,NULL),(85,'set robot access',10,4,NULL),(86,'reset robot access',11,4,NULL),(87,'reset',2,8,NULL),(88,'set to manual',3,8,NULL),(89,'set to auto',4,8,NULL),(90,'sync',5,8,NULL),(91,'insert glas to machine',10,8,NULL),(92,'remove glas from machine',11,8,NULL),(93,'stop',1,7,NULL),(94,'reset',2,7,NULL),(95,'set to manual',3,7,NULL),(96,'set to auto',4,7,NULL),(97,'sync',5,7,NULL),(98,'start dosing',10,7,NULL),(99,'tare',11,7,NULL);
/*!40000 ALTER TABLE `machine_commands` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:24
