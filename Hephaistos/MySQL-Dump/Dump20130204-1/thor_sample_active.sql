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
-- Table structure for table `sample_active`
--

DROP TABLE IF EXISTS `sample_active`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sample_active` (
  `idactive_samples` int(11) NOT NULL AUTO_INCREMENT,
  `SampleProgramType_ID` int(11) DEFAULT NULL,
  `TimestampInserted` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `TimestampFinished` timestamp NULL DEFAULT NULL,
  `SampleID` varchar(128) DEFAULT NULL,
  `ActualSamplePosition_ID` int(11) DEFAULT NULL,
  `Priority` int(11) DEFAULT NULL,
  `StartPosition_ID` int(11) DEFAULT NULL,
  `Command_Active` int(11) DEFAULT '0',
  `LastCommandActionPosition_ID` int(11) DEFAULT '0',
  `Magazine` int(11) DEFAULT '0',
  `MagazinePos` int(11) DEFAULT NULL,
  `MagazineOrderForce` int(11) DEFAULT '0',
  `MagazineDoneFlag` int(11) DEFAULT '0',
  `LineStepReached` int(11) DEFAULT '0',
  PRIMARY KEY (`idactive_samples`),
  UNIQUE KEY `SampleID_UNIQUE` (`SampleID`),
  KEY `active_samples` (`ActualSamplePosition_ID`,`SampleID`,`TimestampInserted`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sample_active`
--

LOCK TABLES `sample_active` WRITE;
/*!40000 ALTER TABLE `sample_active` DISABLE KEYS */;
INSERT INTO `sample_active` VALUES (1,5,'2013-01-07 14:00:48',NULL,'crucible_15:00:48ggg',54,100,43,0,54,0,0,0,1,0),(4,7,'2013-01-09 15:48:35',NULL,'sdfs',102,100,93,0,0,0,0,0,1,0),(5,8,'2013-01-09 16:55:48',NULL,'asdghffs',114,100,-1,0,0,0,0,0,1,0),(6,3,'2013-01-09 17:01:34',NULL,'kgzfhtd',53,100,117,1,53,0,0,0,1,0);
/*!40000 ALTER TABLE `sample_active` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `thor`.`sample_active_sample_movement_OnInsert`
AFTER INSERT ON `thor`.`sample_active`
FOR EACH ROW
BEGIN
IF NEW.SampleProgramType_ID> 0 AND NEW.ActualSamplePosition_ID !=0 AND NEW.SampleProgramType_ID>0 THEN
   INSERT INTO sample_movement SET SampleID_ID = NEW.idactive_samples,MachinePosition_ID=NEW.ActualSamplePosition_ID,Actualtime=Now(),SampleID=New.SampleID;
 END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `thor`.`sample_active_sample_movement`
BEFORE UPDATE ON `thor`.`sample_active`
FOR EACH ROW
BEGIN
 IF OLD.ActualSamplePosition_ID != NEW.ActualSamplePosition_ID AND NEW.SampleProgramType_ID>0  AND  NEW.ActualSamplePosition_ID > 0 THEN
   INSERT INTO sample_movement SET SampleID_ID = NEW.idactive_samples,MachinePosition_ID=NEW.ActualSamplePosition_ID,Actualtime=Now(),SampleID=New.SampleID;
   SET NEW.Command_Active=0;
   DELETE FROM sample_reservation WHERE ActualSamplePosition_ID=NEW.ActualSamplePosition_ID;
 END IF;
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:01
