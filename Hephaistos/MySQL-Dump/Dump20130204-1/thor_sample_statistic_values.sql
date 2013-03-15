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
-- Table structure for table `sample_statistic_values`
--

DROP TABLE IF EXISTS `sample_statistic_values`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sample_statistic_values` (
  `idsample_statistic_values` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Description` tinytext,
  `MinMarkerValue` double DEFAULT NULL,
  `MaxMarkerValue` double DEFAULT NULL,
  `Element` varchar(24) DEFAULT NULL,
  `OrdinatenMin` double DEFAULT NULL,
  `OrdinatenMax` double DEFAULT NULL,
  PRIMARY KEY (`idsample_statistic_values`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sample_statistic_values`
--

LOCK TABLES `sample_statistic_values` WRITE;
/*!40000 ALTER TABLE `sample_statistic_values` DISABLE KEYS */;
INSERT INTO `sample_statistic_values` VALUES (1,'IronOre',NULL,3.2,4.9,'FE',0.01,70),(2,'Cupper',NULL,21.8,25,'CU',0.01,100),(3,'Aliminium oxide',NULL,6.5,9,'Al2O3',0.01,100),(4,'Calciummon oxide',NULL,12.7,14.5,'CaO',0.01,100),(5,'Chromium oxide',NULL,30,43,'Cr2O3',0.005,10),(6,'Potassium oxide',NULL,55,66,'K2O',0.01,100),(7,'Magnesium oxide',NULL,13,55,'MgO',0.01,100),(8,'Permanganate',NULL,2,6,'MnO',0.01,100),(9,'Natriun oxide',NULL,8,76,'NaO2',0.01,100),(10,'Phospor',NULL,6,33,'P',0.001,45),(11,'Sulphur',NULL,5,31,'S',0.001,40),(12,'Silicium oxide',NULL,17,77,'SiO2 ',0.01,100),(13,'Titan(IV) oxide',NULL,23,48,'TiO2 ',0.01,100),(14,'Vanadium(V) oxide',NULL,0.9,1.6,'V2O5 ',0.005,10),(15,'Lost off Ignition',NULL,5,6.5,'LOI 1000ºC',0.01,100),(16,'Input weight','',90,110,'gram',0,500);
/*!40000 ALTER TABLE `sample_statistic_values` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:21
