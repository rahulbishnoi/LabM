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
-- Table structure for table `machine_program_parameter`
--

DROP TABLE IF EXISTS `machine_program_parameter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_program_parameter` (
  `idmachine_program_parameter` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(256) DEFAULT NULL,
  `machine_ID` int(11) DEFAULT NULL,
  `name` varchar(60) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `program_number` int(11) DEFAULT NULL,
  `parameter_number` int(11) DEFAULT NULL,
  `parameter_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idmachine_program_parameter`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_program_parameter`
--

LOCK TABLES `machine_program_parameter` WRITE;
/*!40000 ALTER TABLE `machine_program_parameter` DISABLE KEYS */;
INSERT INTO `machine_program_parameter` VALUES (1,NULL,5,'5_MixingTime_1_1',NULL,NULL,NULL,NULL),(2,NULL,5,'5_MixingTime_2_1',NULL,NULL,NULL,NULL),(3,NULL,5,'5_MixingTime_3_1',NULL,NULL,NULL,NULL),(4,NULL,5,'5_MixingTime_4_1',NULL,NULL,NULL,NULL),(5,NULL,5,'5_MixingTime_5_1',NULL,NULL,NULL,NULL),(6,NULL,6,'6_TargetWeightMaterial_1_1',NULL,NULL,NULL,NULL),(7,NULL,6,'6_TargetWeightFlux_1_2',NULL,NULL,NULL,NULL),(8,NULL,6,'6_TargetWeightMaterial_2_1',NULL,NULL,NULL,NULL),(9,NULL,6,'6_TargetWeightFlux_2_2',NULL,NULL,NULL,NULL),(10,NULL,6,'6_TargetWeightMaterial_3_1',NULL,NULL,NULL,NULL),(11,NULL,6,'6_TargetWeightFlux_3_2',NULL,NULL,NULL,NULL),(12,NULL,6,'6_TargetWeightMaterial_4_1',NULL,NULL,NULL,NULL),(13,NULL,6,'6_TargetWeightFlux_4_2',NULL,NULL,NULL,NULL),(14,NULL,6,'6_TargetWeightMaterial_5_1',NULL,NULL,NULL,NULL),(15,NULL,6,'6_TargetWeightFlux_5_2',NULL,NULL,NULL,NULL),(16,NULL,7,'7_TargetWeightMaterial_1_1',NULL,NULL,NULL,NULL),(17,NULL,7,'7_TargetWeightFlux_1_2',NULL,NULL,NULL,NULL),(18,NULL,7,'7_TargetWeightMaterial_2_1',NULL,NULL,NULL,NULL),(19,NULL,7,'7_TargetWeightFlux_2_2',NULL,NULL,NULL,NULL),(20,NULL,7,'7_TargetWeightMaterial_3_1',NULL,NULL,NULL,NULL),(21,NULL,7,'7_TargetWeightFlux_3_2',NULL,NULL,NULL,NULL),(22,NULL,7,'7_TargetWeightMaterial_4_1',NULL,NULL,NULL,NULL),(23,NULL,7,'7_TargetWeightFlux_4_2',NULL,NULL,NULL,NULL),(24,NULL,7,'7_TargetWeightMaterial_5_1',NULL,NULL,NULL,NULL),(25,NULL,7,'7_TargetWeightFlux_5_2',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `machine_program_parameter` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:10
