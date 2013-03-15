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
-- Table structure for table `machine_parameter`
--

DROP TABLE IF EXISTS `machine_parameter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_parameter` (
  `idmachine_parameter` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(256) DEFAULT NULL,
  `machine_ID` int(11) DEFAULT NULL,
  `name` varchar(60) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmachine_parameter`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_parameter`
--

LOCK TABLES `machine_parameter` WRITE;
/*!40000 ALTER TABLE `machine_parameter` DISABLE KEYS */;
INSERT INTO `machine_parameter` VALUES (1,NULL,6,'6_Mat_StartVibrationPhase2',NULL),(2,NULL,6,'6_Mat_StartVibrationPhase3',NULL),(3,NULL,6,'6_Flux_StartVibrationPhase2',NULL),(4,NULL,6,'6_Flux_StartVibrationPhase3',NULL),(5,NULL,6,'6_Mat_AcceptWeightRange',NULL),(6,NULL,6,'6_Flux_AcceptWeightRange',NULL),(7,NULL,6,'6_Mat_CancelTime',NULL),(8,NULL,6,'6_Flux_CancelTime',NULL),(9,NULL,6,'6_Flux_PercentPreDosing',NULL),(10,NULL,6,'6_Clean_AmountOfClycles',NULL),(11,NULL,6,'6_Clean_TimeBlowPipe',NULL),(12,NULL,6,'6_Clean_spare36',NULL),(13,NULL,6,'6_Clean_spare38',NULL),(14,NULL,6,'6_Bal_AmountOfValuesToCheck',NULL),(15,NULL,6,'6_Bal_Tolerance',NULL),(16,NULL,6,'6_Mat_DosingTimeOnPhase1',NULL),(17,NULL,6,'6_Mat_DosingTimeOffPhase1',NULL),(18,NULL,6,'6_Mat_DosingTimeOnPhase2',NULL),(19,NULL,6,'6_Mat_DosingTimeOffPhase2',NULL),(20,NULL,6,'6_Mat_DosingTimeOnPhase3',NULL),(21,NULL,6,'6_Mat_DosingTimeOffPhase3',NULL),(22,NULL,6,'6_Flux_DosingTimeOnPhase1',NULL),(23,NULL,6,'6_Flux_DosingTimeOffPhase1',NULL),(24,NULL,6,'6_Flux_DosingTimeOnPhase2',NULL),(25,NULL,6,'6_Flux_DosingTimeOffPhase2',NULL),(26,NULL,6,'6_Flux_DosingTimeOnPhase3',NULL),(27,NULL,6,'6_Flux_DosingTimeOffPhase3',NULL),(28,NULL,6,'6_Mat_DosingTimeOnManualFunction',NULL),(29,NULL,6,'6_Mat_DosingTimeOffManualFunction',NULL),(30,NULL,6,'6_Flux_DosingTimeOnManualFunction',NULL),(31,NULL,6,'6_Flux_DosingTimeOffManualFunction',NULL),(32,NULL,7,'7_Mat_StartVibrationPhase2',NULL),(33,NULL,7,'7_Mat_StartVibrationPhase3',NULL),(34,NULL,7,'7_Flux_StartVibrationPhase2',NULL),(35,NULL,7,'7_Flux_StartVibrationPhase3',NULL),(36,NULL,7,'7_Mat_AcceptWeightRange',NULL),(37,NULL,7,'7_Flux_AcceptWeightRange',NULL),(38,NULL,7,'7_Mat_CancelTime',NULL),(39,NULL,7,'7_Flux_CancelTime',NULL),(40,NULL,7,'7_Flux_PercentPreDosing',NULL),(41,NULL,7,'7_Clean_AmountOfClycles',NULL),(42,NULL,7,'7_Clean_TimeBlowPipe',NULL),(43,NULL,7,'7_Clean_spare36',NULL),(44,NULL,7,'7_Clean_spare38',NULL),(45,NULL,7,'7_Bal_AmountOfValuesToCheck',NULL),(46,NULL,7,'7_Bal_Tolerance',NULL),(47,NULL,7,'7_Mat_DosingTimeOnPhase1',NULL),(48,NULL,7,'7_Mat_DosingTimeOffPhase1',NULL),(49,NULL,7,'7_Mat_DosingTimeOnPhase2',NULL),(50,NULL,7,'7_Mat_DosingTimeOffPhase2',NULL),(51,NULL,7,'7_Mat_DosingTimeOnPhase3',NULL),(52,NULL,7,'7_Mat_DosingTimeOffPhase3',NULL),(53,NULL,7,'7_Flux_DosingTimeOnPhase1',NULL),(54,NULL,7,'7_Flux_DosingTimeOffPhase1',NULL),(55,NULL,7,'7_Flux_DosingTimeOnPhase2',NULL),(56,NULL,7,'7_Flux_DosingTimeOffPhase2',NULL),(57,NULL,7,'7_Flux_DosingTimeOnPhase3',NULL),(58,NULL,7,'7_Flux_DosingTimeOffPhase3',NULL),(59,NULL,7,'7_Mat_DosingTimeOnManualFunction',NULL),(60,NULL,7,'7_Mat_DosingTimeOffManualFunction',NULL),(61,NULL,7,'7_Flux_DosingTimeOnManualFunction',NULL),(62,NULL,7,'7_Flux_DosingTimeOffManualFunction',NULL),(63,NULL,8,'8_InputBeltRuntime',NULL),(64,NULL,8,'8_OutputBeltRuntime',NULL);
/*!40000 ALTER TABLE `machine_parameter` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:40:54
