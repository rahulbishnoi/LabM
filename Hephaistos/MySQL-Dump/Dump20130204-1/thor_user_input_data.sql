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
-- Table structure for table `user_input_data`
--

DROP TABLE IF EXISTS `user_input_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_input_data` (
  `iduser_input_data` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Value` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`iduser_input_data`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_input_data`
--

LOCK TABLES `user_input_data` WRITE;
/*!40000 ALTER TABLE `user_input_data` DISABLE KEYS */;
INSERT INTO `user_input_data` VALUES (3,'LastSample_ID_1_2_True','fdhdf%Y-%M-%D %h:%m:%s'),(4,'LastSample_ID_1_1_True','dfhd%m'),(5,'LastSample_ID_1_53_False',''),(6,'LastSample_ID_5_1_True','crucible_%Y-%M-%D %h:%m:%s'),(7,'LastSample_ID_2_3_True','Sample_%Y-%M-%D %h:%m:%s'),(8,'LastSample_ID_1_3_True','sample_%Y-%M-%D %h:%m:%s'),(9,'LastSample_ID_5_3_True','cr_%Y-%M-%D %h:%m:%s'),(10,'LastSample_ID_6_1_True','crucible_%Y-%M-%D %h:%m:%s'),(11,'Administartion_Form_Limit','10000'),(12,'Administration_Form_SQLStatement0','Select * FROM logging_data'),(13,'Administration_Form_SQLStatement1','Select * FROM command_active'),(14,'Administration_Form_SQLStatement2','Select * FROM command_done'),(15,'Administration_Form_SQLStatement3',''),(16,'RoutingFormSplitter1Distance','362'),(17,'RoutingFormSplitter2Distance','383'),(18,'RoutingFormWidth','1196'),(19,'RoutingFormHeight','687'),(20,'LoggingInfo_Form_AlarmCheckBox','0'),(21,'LoggingInfo_Form_ToolTipCheckBox','1'),(22,'LoggingInfo_Form_MessageCheckBox','0'),(23,'LoggingInfo_Form_DebugCheckBox','0'),(24,'LoggingInfo_Form_SendCheckBox','0'),(25,'LoggingInfo_Form_ReceiveCheckBox','0'),(26,'LoggingInfo_Form_AutoRefreshCheckBox','0'),(27,'SampleList_Form_GroupByDoneCheckBox','1'),(28,'SampleList_Form_GroupByCheckBox','0'),(29,'SampleList_Form_FilterCheckBox','0'),(30,'SampleList_Form_FilterDoneCheckBox','0');
/*!40000 ALTER TABLE `user_input_data` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:07
