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
-- Table structure for table `routing_error_text`
--

DROP TABLE IF EXISTS `routing_error_text`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `routing_error_text` (
  `idrouting_error_text` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(45) DEFAULT NULL,
  `Type_ID` int(11) DEFAULT NULL,
  `Field1` varchar(60) DEFAULT '',
  `Field2` varchar(60) DEFAULT '',
  `Field3` varchar(60) DEFAULT '',
  `Field4` varchar(60) DEFAULT '',
  PRIMARY KEY (`idrouting_error_text`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routing_error_text`
--

LOCK TABLES `routing_error_text` WRITE;
/*!40000 ALTER TABLE `routing_error_text` DISABLE KEYS */;
INSERT INTO `routing_error_text` VALUES (1,'Condition',1,NULL,'Please enter a time!',NULL,NULL),(2,'Condition',2,NULL,'Please enter a time!',NULL,NULL),(3,'Condition',3,NULL,'Please select a machine!','Please select an operation!','Please enter \'sample free\''),(4,'Condition',4,NULL,'Please select a global tag!','Please select an operation!','Please enter a value!'),(5,'Condition',5,NULL,'Please select a machine tag!','Please select an operation!','Please enter a value!'),(6,'Condition',6,NULL,'Please enter a value!','Please select an operation!','Please enter a value!'),(7,'Condition',7,NULL,'Please select a position!','Please select an operation!','Please select occupied/not occupied!'),(8,'Condition',8,NULL,'Please select a position!','Please select an operation!','Please select a sample type!'),(9,'Condition',9,NULL,'Please select a position!','Please select an operation!','Please enter a sample priority!'),(10,'Condition',10,NULL,'Please select a statusbit!','Please select an operation!','Please select a true or false!'),(11,'Command',1,'Please select a position!',NULL,NULL,NULL),(12,'Command',2,'Please select a position!','Please select a program!','Please enter a name!','Please select a priority!'),(13,'Command',3,NULL,NULL,NULL,NULL),(14,'Command',4,'Please select a sample type!',NULL,NULL,NULL),(15,'Command',5,'Please select a priority!',NULL,NULL,NULL),(16,'Command',6,NULL,NULL,'Please select a tag!','Please enter a value!'),(17,'Command',7,NULL,'Please select a machine!','Please select a tag!','Please enter a value!'),(18,'Command',8,'Please select type of position!',NULL,'Please enter a name!','Please enter a value!'),(19,'Command',9,NULL,NULL,'Please enter a name!',NULL),(20,'Command',10,'Please select a machine!','Please select a command!',NULL,NULL),(21,'Command',11,'Please select a position!',NULL,NULL,NULL),(22,'Command',12,'Please select a position!',NULL,NULL,NULL),(23,'Condition',11,NULL,NULL,'Please select an operation!','Please enter \'sample free\''),(24,'Command',13,'','','','');
/*!40000 ALTER TABLE `routing_error_text` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:50
