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
-- Table structure for table `wincc_statistics`
--

DROP TABLE IF EXISTS `wincc_statistics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `wincc_statistics` (
  `idwincc_statistics` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) DEFAULT NULL,
  `Value` varchar(45) DEFAULT NULL,
  `Machine_ID` varchar(45) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  PRIMARY KEY (`idwincc_statistics`),
  KEY `value` (`Value`)
) ENGINE=InnoDB AUTO_INCREMENT=921 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wincc_statistics`
--

LOCK TABLES `wincc_statistics` WRITE;
/*!40000 ALTER TABLE `wincc_statistics` DISABLE KEYS */;
INSERT INTO `wincc_statistics` VALUES (1,'1_Status_0','256','1',NULL),(2,'1_Status_1',NULL,'1',NULL),(3,'1_TotalCounter',NULL,'1',NULL),(4,'1_DailyCounter',NULL,'1',NULL),(5,'1_ResetStatus_0',NULL,'1',NULL),(6,'1_ResetOutput_0',NULL,'1',NULL),(7,'1_TimeOutput_0_0',NULL,'1',NULL),(8,'1_TimeOutput_0_1',NULL,'1',NULL),(9,'1_TimeOutput_0_2',NULL,'1',NULL),(10,'1_TimeOutput_0_3',NULL,'1',NULL),(11,'1_TimeOutput_0_4',NULL,'1',NULL),(12,'1_TimeOutput_0_5',NULL,'1',NULL),(13,'1_TimeOutput_0_6',NULL,'1',NULL),(14,'1_TimeOutput_0_7',NULL,'1',NULL),(15,'1_TimeOutput_0_8',NULL,'1',NULL),(16,'1_TimeOutput_0_9',NULL,'1',NULL),(17,'1_TimeOutput_0_10',NULL,'1',NULL),(18,'1_TimeOutput_0_11',NULL,'1',NULL),(19,'1_TimeOutput_0_12',NULL,'1',NULL),(20,'1_TimeOutput_0_13',NULL,'1',NULL),(21,'1_TimeOutput_0_14',NULL,'1',NULL),(22,'1_TimeOutput_0_15',NULL,'1',NULL),(23,'1_TimeOutput_0_16',NULL,'1',NULL),(24,'1_TimeOutput_0_17',NULL,'1',NULL),(25,'1_TimeOutput_0_18',NULL,'1',NULL),(26,'1_TimeOutput_0_19',NULL,'1',NULL),(27,'1_TimeOutput_0_20',NULL,'1',NULL),(28,'1_TimeOutput_0_21',NULL,'1',NULL),(29,'1_TimeOutput_0_22',NULL,'1',NULL),(30,'1_TimeOutput_0_23',NULL,'1',NULL),(31,'1_TimeOutput_0_24',NULL,'1',NULL),(32,'1_TimeOutput_0_25',NULL,'1',NULL),(33,'1_TimeOutput_0_26',NULL,'1',NULL),(34,'1_TimeOutput_0_27',NULL,'1',NULL),(35,'1_TimeOutput_0_28',NULL,'1',NULL),(36,'1_TimeOutput_0_29',NULL,'1',NULL),(37,'1_TimeOutput_0_30',NULL,'1',NULL),(38,'1_TimeOutput_0_31',NULL,'1',NULL),(39,'1_CountOutput_0_0',NULL,'1',NULL),(40,'1_CountOutput_0_1',NULL,'1',NULL),(41,'1_CountOutput_0_2',NULL,'1',NULL),(42,'1_CountOutput_0_3',NULL,'1',NULL),(43,'1_CountOutput_0_4',NULL,'1',NULL),(44,'1_CountOutput_0_5',NULL,'1',NULL),(45,'1_CountOutput_0_6',NULL,'1',NULL),(46,'1_CountOutput_0_7',NULL,'1',NULL),(47,'1_CountOutput_0_8',NULL,'1',NULL),(48,'1_CountOutput_0_9',NULL,'1',NULL),(49,'1_CountOutput_0_10',NULL,'1',NULL),(50,'1_CountOutput_0_11',NULL,'1',NULL),(51,'1_CountOutput_0_12',NULL,'1',NULL),(52,'1_CountOutput_0_13',NULL,'1',NULL),(53,'1_CountOutput_0_14',NULL,'1',NULL),(54,'1_CountOutput_0_15',NULL,'1',NULL),(55,'1_CountOutput_0_16',NULL,'1',NULL),(56,'1_CountOutput_0_17',NULL,'1',NULL),(57,'1_CountOutput_0_18',NULL,'1',NULL),(58,'1_CountOutput_0_19',NULL,'1',NULL),(59,'1_CountOutput_0_20',NULL,'1',NULL),(60,'1_CountOutput_0_21',NULL,'1',NULL),(61,'1_CountOutput_0_22',NULL,'1',NULL),(62,'1_CountOutput_0_23',NULL,'1',NULL),(63,'1_CountOutput_0_24',NULL,'1',NULL),(64,'1_CountOutput_0_25',NULL,'1',NULL),(65,'1_CountOutput_0_26',NULL,'1',NULL),(66,'1_CountOutput_0_27',NULL,'1',NULL),(67,'1_CountOutput_0_28',NULL,'1',NULL),(68,'1_CountOutput_0_29',NULL,'1',NULL),(69,'1_CountOutput_0_30',NULL,'1',NULL),(70,'1_CountOutput_0_31',NULL,'1',NULL),(71,'1_TimeStatus_0_0',NULL,'1',NULL),(72,'1_TimeStatus_0_1',NULL,'1',NULL),(73,'1_TimeStatus_0_2',NULL,'1',NULL),(74,'1_TimeStatus_0_3',NULL,'1',NULL),(75,'1_TimeStatus_0_4',NULL,'1',NULL),(76,'1_TimeStatus_0_5',NULL,'1',NULL),(77,'1_TimeStatus_0_6',NULL,'1',NULL),(78,'1_TimeStatus_0_7',NULL,'1',NULL),(79,'1_TimeStatus_0_8',NULL,'1',NULL),(80,'1_TimeStatus_0_9',NULL,'1',NULL),(81,'1_TimeStatus_0_10',NULL,'1',NULL),(82,'1_TimeStatus_0_11',NULL,'1',NULL),(83,'1_TimeStatus_0_12',NULL,'1',NULL),(84,'1_TimeStatus_0_13',NULL,'1',NULL),(85,'1_TimeStatus_0_14',NULL,'1',NULL),(86,'1_TimeStatus_0_15',NULL,'1',NULL),(87,'1_TimeStatus_0_16',NULL,'1',NULL),(88,'1_TimeStatus_0_17',NULL,'1',NULL),(89,'1_TimeStatus_0_18',NULL,'1',NULL),(90,'1_TimeStatus_0_19',NULL,'1',NULL),(91,'1_TimeStatus_0_20',NULL,'1',NULL),(92,'1_TimeStatus_0_21',NULL,'1',NULL),(93,'1_TimeStatus_0_22',NULL,'1',NULL),(94,'1_TimeStatus_0_23',NULL,'1',NULL),(95,'1_TimeStatus_0_24',NULL,'1',NULL),(96,'1_TimeStatus_0_25',NULL,'1',NULL),(97,'1_TimeStatus_0_26',NULL,'1',NULL),(98,'1_TimeStatus_0_27',NULL,'1',NULL),(99,'1_TimeStatus_0_28',NULL,'1',NULL),(100,'1_TimeStatus_0_29',NULL,'1',NULL),(101,'1_TimeStatus_0_30',NULL,'1',NULL),(102,'1_TimeStatus_0_31',NULL,'1',NULL),(103,'2_Status_0','256','2',NULL),(104,'2_Status_1',NULL,'2',NULL),(105,'2_TotalCounter',NULL,'2',NULL),(106,'2_DailyCounter',NULL,'2',NULL),(107,'2_ResetStatus_0',NULL,'2',NULL),(108,'2_ResetOutput_0',NULL,'2',NULL),(109,'2_TimeOutput_0_0',NULL,'2',NULL),(110,'2_TimeOutput_0_1',NULL,'2',NULL),(111,'2_TimeOutput_0_2',NULL,'2',NULL),(112,'2_TimeOutput_0_3',NULL,'2',NULL),(113,'2_TimeOutput_0_4',NULL,'2',NULL),(114,'2_TimeOutput_0_5',NULL,'2',NULL),(115,'2_TimeOutput_0_6',NULL,'2',NULL),(116,'2_TimeOutput_0_7',NULL,'2',NULL),(117,'2_TimeOutput_0_8',NULL,'2',NULL),(118,'2_TimeOutput_0_9',NULL,'2',NULL),(119,'2_TimeOutput_0_10',NULL,'2',NULL),(120,'2_TimeOutput_0_11',NULL,'2',NULL),(121,'2_TimeOutput_0_12',NULL,'2',NULL),(122,'2_TimeOutput_0_13',NULL,'2',NULL),(123,'2_TimeOutput_0_14',NULL,'2',NULL),(124,'2_TimeOutput_0_15',NULL,'2',NULL),(125,'2_TimeOutput_0_16',NULL,'2',NULL),(126,'2_TimeOutput_0_17',NULL,'2',NULL),(127,'2_TimeOutput_0_18',NULL,'2',NULL),(128,'2_TimeOutput_0_19',NULL,'2',NULL),(129,'2_TimeOutput_0_20',NULL,'2',NULL),(130,'2_TimeOutput_0_21',NULL,'2',NULL),(131,'2_TimeOutput_0_22',NULL,'2',NULL),(132,'2_TimeOutput_0_23',NULL,'2',NULL),(133,'2_TimeOutput_0_24',NULL,'2',NULL),(134,'2_TimeOutput_0_25',NULL,'2',NULL),(135,'2_TimeOutput_0_26',NULL,'2',NULL),(136,'2_TimeOutput_0_27',NULL,'2',NULL),(137,'2_TimeOutput_0_28',NULL,'2',NULL),(138,'2_TimeOutput_0_29',NULL,'2',NULL),(139,'2_TimeOutput_0_30',NULL,'2',NULL),(140,'2_TimeOutput_0_31',NULL,'2',NULL),(141,'2_CountOutput_0_0',NULL,'2',NULL),(142,'2_CountOutput_0_1',NULL,'2',NULL),(143,'2_CountOutput_0_2',NULL,'2',NULL),(144,'2_CountOutput_0_3',NULL,'2',NULL),(145,'2_CountOutput_0_4',NULL,'2',NULL),(146,'2_CountOutput_0_5',NULL,'2',NULL),(147,'2_CountOutput_0_6',NULL,'2',NULL),(148,'2_CountOutput_0_7',NULL,'2',NULL),(149,'2_CountOutput_0_8',NULL,'2',NULL),(150,'2_CountOutput_0_9',NULL,'2',NULL),(151,'2_CountOutput_0_10',NULL,'2',NULL),(152,'2_CountOutput_0_11',NULL,'2',NULL),(153,'2_CountOutput_0_12',NULL,'2',NULL),(154,'2_CountOutput_0_13',NULL,'2',NULL),(155,'2_CountOutput_0_14',NULL,'2',NULL),(156,'2_CountOutput_0_15',NULL,'2',NULL),(157,'2_CountOutput_0_16',NULL,'2',NULL),(158,'2_CountOutput_0_17',NULL,'2',NULL),(159,'2_CountOutput_0_18',NULL,'2',NULL),(160,'2_CountOutput_0_19',NULL,'2',NULL),(161,'2_CountOutput_0_20',NULL,'2',NULL),(162,'2_CountOutput_0_21',NULL,'2',NULL),(163,'2_CountOutput_0_22',NULL,'2',NULL),(164,'2_CountOutput_0_23',NULL,'2',NULL),(165,'2_CountOutput_0_24',NULL,'2',NULL),(166,'2_CountOutput_0_25',NULL,'2',NULL),(167,'2_CountOutput_0_26',NULL,'2',NULL),(168,'2_CountOutput_0_27',NULL,'2',NULL),(169,'2_CountOutput_0_28',NULL,'2',NULL),(170,'2_CountOutput_0_29',NULL,'2',NULL),(171,'2_CountOutput_0_30',NULL,'2',NULL),(172,'2_CountOutput_0_31',NULL,'2',NULL),(173,'2_TimeStatus_0_0',NULL,'2',NULL),(174,'2_TimeStatus_0_1',NULL,'2',NULL),(175,'2_TimeStatus_0_2',NULL,'2',NULL),(176,'2_TimeStatus_0_3',NULL,'2',NULL),(177,'2_TimeStatus_0_4',NULL,'2',NULL),(178,'2_TimeStatus_0_5',NULL,'2',NULL),(179,'2_TimeStatus_0_6',NULL,'2',NULL),(180,'2_TimeStatus_0_7',NULL,'2',NULL),(181,'2_TimeStatus_0_8',NULL,'2',NULL),(182,'2_TimeStatus_0_9',NULL,'2',NULL),(183,'2_TimeStatus_0_10',NULL,'2',NULL),(184,'2_TimeStatus_0_11',NULL,'2',NULL),(185,'2_TimeStatus_0_12',NULL,'2',NULL),(186,'2_TimeStatus_0_13',NULL,'2',NULL),(187,'2_TimeStatus_0_14',NULL,'2',NULL),(188,'2_TimeStatus_0_15',NULL,'2',NULL),(189,'2_TimeStatus_0_16',NULL,'2',NULL),(190,'2_TimeStatus_0_17',NULL,'2',NULL),(191,'2_TimeStatus_0_18',NULL,'2',NULL),(192,'2_TimeStatus_0_19',NULL,'2',NULL),(193,'2_TimeStatus_0_20',NULL,'2',NULL),(194,'2_TimeStatus_0_21',NULL,'2',NULL),(195,'2_TimeStatus_0_22',NULL,'2',NULL),(196,'2_TimeStatus_0_23',NULL,'2',NULL),(197,'2_TimeStatus_0_24',NULL,'2',NULL),(198,'2_TimeStatus_0_25',NULL,'2',NULL),(199,'2_TimeStatus_0_26',NULL,'2',NULL),(200,'2_TimeStatus_0_27',NULL,'2',NULL),(201,'2_TimeStatus_0_28',NULL,'2',NULL),(202,'2_TimeStatus_0_29',NULL,'2',NULL),(203,'2_TimeStatus_0_30',NULL,'2',NULL),(204,'2_TimeStatus_0_31',NULL,'2',NULL),(205,'3_Status_0','256','3',NULL),(206,'3_Status_1',NULL,'3',NULL),(207,'3_TotalCounter',NULL,'3',NULL),(208,'3_DailyCounter',NULL,'3',NULL),(209,'3_ResetStatus_0',NULL,'3',NULL),(210,'3_ResetOutput_0',NULL,'3',NULL),(211,'3_TimeOutput_0_0',NULL,'3',NULL),(212,'3_TimeOutput_0_1',NULL,'3',NULL),(213,'3_TimeOutput_0_2',NULL,'3',NULL),(214,'3_TimeOutput_0_3',NULL,'3',NULL),(215,'3_TimeOutput_0_4',NULL,'3',NULL),(216,'3_TimeOutput_0_5',NULL,'3',NULL),(217,'3_TimeOutput_0_6',NULL,'3',NULL),(218,'3_TimeOutput_0_7',NULL,'3',NULL),(219,'3_TimeOutput_0_8',NULL,'3',NULL),(220,'3_TimeOutput_0_9',NULL,'3',NULL),(221,'3_TimeOutput_0_10',NULL,'3',NULL),(222,'3_TimeOutput_0_11',NULL,'3',NULL),(223,'3_TimeOutput_0_12',NULL,'3',NULL),(224,'3_TimeOutput_0_13',NULL,'3',NULL),(225,'3_TimeOutput_0_14',NULL,'3',NULL),(226,'3_TimeOutput_0_15',NULL,'3',NULL),(227,'3_TimeOutput_0_16',NULL,'3',NULL),(228,'3_TimeOutput_0_17',NULL,'3',NULL),(229,'3_TimeOutput_0_18',NULL,'3',NULL),(230,'3_TimeOutput_0_19',NULL,'3',NULL),(231,'3_TimeOutput_0_20',NULL,'3',NULL),(232,'3_TimeOutput_0_21',NULL,'3',NULL),(233,'3_TimeOutput_0_22',NULL,'3',NULL),(234,'3_TimeOutput_0_23',NULL,'3',NULL),(235,'3_TimeOutput_0_24',NULL,'3',NULL),(236,'3_TimeOutput_0_25',NULL,'3',NULL),(237,'3_TimeOutput_0_26',NULL,'3',NULL),(238,'3_TimeOutput_0_27',NULL,'3',NULL),(239,'3_TimeOutput_0_28',NULL,'3',NULL),(240,'3_TimeOutput_0_29',NULL,'3',NULL),(241,'3_TimeOutput_0_30',NULL,'3',NULL),(242,'3_TimeOutput_0_31',NULL,'3',NULL),(243,'3_CountOutput_0_0',NULL,'3',NULL),(244,'3_CountOutput_0_1',NULL,'3',NULL),(245,'3_CountOutput_0_2',NULL,'3',NULL),(246,'3_CountOutput_0_3',NULL,'3',NULL),(247,'3_CountOutput_0_4',NULL,'3',NULL),(248,'3_CountOutput_0_5',NULL,'3',NULL),(249,'3_CountOutput_0_6',NULL,'3',NULL),(250,'3_CountOutput_0_7',NULL,'3',NULL),(251,'3_CountOutput_0_8',NULL,'3',NULL),(252,'3_CountOutput_0_9',NULL,'3',NULL),(253,'3_CountOutput_0_10',NULL,'3',NULL),(254,'3_CountOutput_0_11',NULL,'3',NULL),(255,'3_CountOutput_0_12',NULL,'3',NULL),(256,'3_CountOutput_0_13',NULL,'3',NULL),(257,'3_CountOutput_0_14',NULL,'3',NULL),(258,'3_CountOutput_0_15',NULL,'3',NULL),(259,'3_CountOutput_0_16',NULL,'3',NULL),(260,'3_CountOutput_0_17',NULL,'3',NULL),(261,'3_CountOutput_0_18',NULL,'3',NULL),(262,'3_CountOutput_0_19',NULL,'3',NULL),(263,'3_CountOutput_0_20',NULL,'3',NULL),(264,'3_CountOutput_0_21',NULL,'3',NULL),(265,'3_CountOutput_0_22',NULL,'3',NULL),(266,'3_CountOutput_0_23',NULL,'3',NULL),(267,'3_CountOutput_0_24',NULL,'3',NULL),(268,'3_CountOutput_0_25',NULL,'3',NULL),(269,'3_CountOutput_0_26',NULL,'3',NULL),(270,'3_CountOutput_0_27',NULL,'3',NULL),(271,'3_CountOutput_0_28',NULL,'3',NULL),(272,'3_CountOutput_0_29',NULL,'3',NULL),(273,'3_CountOutput_0_30',NULL,'3',NULL),(274,'3_CountOutput_0_31',NULL,'3',NULL),(275,'3_TimeStatus_0_0',NULL,'3',NULL),(276,'3_TimeStatus_0_1',NULL,'3',NULL),(277,'3_TimeStatus_0_2',NULL,'3',NULL),(278,'3_TimeStatus_0_3',NULL,'3',NULL),(279,'3_TimeStatus_0_4',NULL,'3',NULL),(280,'3_TimeStatus_0_5',NULL,'3',NULL),(281,'3_TimeStatus_0_6',NULL,'3',NULL),(282,'3_TimeStatus_0_7',NULL,'3',NULL),(283,'3_TimeStatus_0_8',NULL,'3',NULL),(284,'3_TimeStatus_0_9',NULL,'3',NULL),(285,'3_TimeStatus_0_10',NULL,'3',NULL),(286,'3_TimeStatus_0_11',NULL,'3',NULL),(287,'3_TimeStatus_0_12',NULL,'3',NULL),(288,'3_TimeStatus_0_13',NULL,'3',NULL),(289,'3_TimeStatus_0_14',NULL,'3',NULL),(290,'3_TimeStatus_0_15',NULL,'3',NULL),(291,'3_TimeStatus_0_16',NULL,'3',NULL),(292,'3_TimeStatus_0_17',NULL,'3',NULL),(293,'3_TimeStatus_0_18',NULL,'3',NULL),(294,'3_TimeStatus_0_19',NULL,'3',NULL),(295,'3_TimeStatus_0_20',NULL,'3',NULL),(296,'3_TimeStatus_0_21',NULL,'3',NULL),(297,'3_TimeStatus_0_22',NULL,'3',NULL),(298,'3_TimeStatus_0_23',NULL,'3',NULL),(299,'3_TimeStatus_0_24',NULL,'3',NULL),(300,'3_TimeStatus_0_25',NULL,'3',NULL),(301,'3_TimeStatus_0_26',NULL,'3',NULL),(302,'3_TimeStatus_0_27',NULL,'3',NULL),(303,'3_TimeStatus_0_28',NULL,'3',NULL),(304,'3_TimeStatus_0_29',NULL,'3',NULL),(305,'3_TimeStatus_0_30',NULL,'3',NULL),(306,'3_TimeStatus_0_31',NULL,'3',NULL),(307,'4_Status_0','256','4',NULL),(308,'4_Status_1',NULL,'4',NULL),(309,'4_TotalCounter',NULL,'4',NULL),(310,'4_DailyCounter',NULL,'4',NULL),(311,'4_ResetStatus_0',NULL,'4',NULL),(312,'4_ResetOutput_0',NULL,'4',NULL),(313,'4_TimeOutput_0_0',NULL,'4',NULL),(314,'4_TimeOutput_0_1',NULL,'4',NULL),(315,'4_TimeOutput_0_2',NULL,'4',NULL),(316,'4_TimeOutput_0_3',NULL,'4',NULL),(317,'4_TimeOutput_0_4',NULL,'4',NULL),(318,'4_TimeOutput_0_5',NULL,'4',NULL),(319,'4_TimeOutput_0_6',NULL,'4',NULL),(320,'4_TimeOutput_0_7',NULL,'4',NULL),(321,'4_TimeOutput_0_8',NULL,'4',NULL),(322,'4_TimeOutput_0_9',NULL,'4',NULL),(323,'4_TimeOutput_0_10',NULL,'4',NULL),(324,'4_TimeOutput_0_11',NULL,'4',NULL),(325,'4_TimeOutput_0_12',NULL,'4',NULL),(326,'4_TimeOutput_0_13',NULL,'4',NULL),(327,'4_TimeOutput_0_14',NULL,'4',NULL),(328,'4_TimeOutput_0_15',NULL,'4',NULL),(329,'4_TimeOutput_0_16',NULL,'4',NULL),(330,'4_TimeOutput_0_17',NULL,'4',NULL),(331,'4_TimeOutput_0_18',NULL,'4',NULL),(332,'4_TimeOutput_0_19',NULL,'4',NULL),(333,'4_TimeOutput_0_20',NULL,'4',NULL),(334,'4_TimeOutput_0_21',NULL,'4',NULL),(335,'4_TimeOutput_0_22',NULL,'4',NULL),(336,'4_TimeOutput_0_23',NULL,'4',NULL),(337,'4_TimeOutput_0_24',NULL,'4',NULL),(338,'4_TimeOutput_0_25',NULL,'4',NULL),(339,'4_TimeOutput_0_26',NULL,'4',NULL),(340,'4_TimeOutput_0_27',NULL,'4',NULL),(341,'4_TimeOutput_0_28',NULL,'4',NULL),(342,'4_TimeOutput_0_29',NULL,'4',NULL),(343,'4_TimeOutput_0_30',NULL,'4',NULL),(344,'4_TimeOutput_0_31',NULL,'4',NULL),(345,'4_CountOutput_0_0',NULL,'4',NULL),(346,'4_CountOutput_0_1',NULL,'4',NULL),(347,'4_CountOutput_0_2',NULL,'4',NULL),(348,'4_CountOutput_0_3',NULL,'4',NULL),(349,'4_CountOutput_0_4',NULL,'4',NULL),(350,'4_CountOutput_0_5',NULL,'4',NULL),(351,'4_CountOutput_0_6',NULL,'4',NULL),(352,'4_CountOutput_0_7',NULL,'4',NULL),(353,'4_CountOutput_0_8',NULL,'4',NULL),(354,'4_CountOutput_0_9',NULL,'4',NULL),(355,'4_CountOutput_0_10',NULL,'4',NULL),(356,'4_CountOutput_0_11',NULL,'4',NULL),(357,'4_CountOutput_0_12',NULL,'4',NULL),(358,'4_CountOutput_0_13',NULL,'4',NULL),(359,'4_CountOutput_0_14',NULL,'4',NULL),(360,'4_CountOutput_0_15',NULL,'4',NULL),(361,'4_CountOutput_0_16',NULL,'4',NULL),(362,'4_CountOutput_0_17',NULL,'4',NULL),(363,'4_CountOutput_0_18',NULL,'4',NULL),(364,'4_CountOutput_0_19',NULL,'4',NULL),(365,'4_CountOutput_0_20',NULL,'4',NULL),(366,'4_CountOutput_0_21',NULL,'4',NULL),(367,'4_CountOutput_0_22',NULL,'4',NULL),(368,'4_CountOutput_0_23',NULL,'4',NULL),(369,'4_CountOutput_0_24',NULL,'4',NULL),(370,'4_CountOutput_0_25',NULL,'4',NULL),(371,'4_CountOutput_0_26',NULL,'4',NULL),(372,'4_CountOutput_0_27',NULL,'4',NULL),(373,'4_CountOutput_0_28',NULL,'4',NULL),(374,'4_CountOutput_0_29',NULL,'4',NULL),(375,'4_CountOutput_0_30',NULL,'4',NULL),(376,'4_CountOutput_0_31',NULL,'4',NULL),(377,'4_TimeStatus_0_0',NULL,'4',NULL),(378,'4_TimeStatus_0_1',NULL,'4',NULL),(379,'4_TimeStatus_0_2',NULL,'4',NULL),(380,'4_TimeStatus_0_3',NULL,'4',NULL),(381,'4_TimeStatus_0_4',NULL,'4',NULL),(382,'4_TimeStatus_0_5',NULL,'4',NULL),(383,'4_TimeStatus_0_6',NULL,'4',NULL),(384,'4_TimeStatus_0_7',NULL,'4',NULL),(385,'4_TimeStatus_0_8',NULL,'4',NULL),(386,'4_TimeStatus_0_9',NULL,'4',NULL),(387,'4_TimeStatus_0_10',NULL,'4',NULL),(388,'4_TimeStatus_0_11',NULL,'4',NULL),(389,'4_TimeStatus_0_12',NULL,'4',NULL),(390,'4_TimeStatus_0_13',NULL,'4',NULL),(391,'4_TimeStatus_0_14',NULL,'4',NULL),(392,'4_TimeStatus_0_15',NULL,'4',NULL),(393,'4_TimeStatus_0_16',NULL,'4',NULL),(394,'4_TimeStatus_0_17',NULL,'4',NULL),(395,'4_TimeStatus_0_18',NULL,'4',NULL),(396,'4_TimeStatus_0_19',NULL,'4',NULL),(397,'4_TimeStatus_0_20',NULL,'4',NULL),(398,'4_TimeStatus_0_21',NULL,'4',NULL),(399,'4_TimeStatus_0_22',NULL,'4',NULL),(400,'4_TimeStatus_0_23',NULL,'4',NULL),(401,'4_TimeStatus_0_24',NULL,'4',NULL),(402,'4_TimeStatus_0_25',NULL,'4',NULL),(403,'4_TimeStatus_0_26',NULL,'4',NULL),(404,'4_TimeStatus_0_27',NULL,'4',NULL),(405,'4_TimeStatus_0_28',NULL,'4',NULL),(406,'4_TimeStatus_0_29',NULL,'4',NULL),(407,'4_TimeStatus_0_30',NULL,'4',NULL),(408,'4_TimeStatus_0_31',NULL,'4',NULL),(409,'5_Status_0','256','5',NULL),(410,'5_Status_1',NULL,'5',NULL),(411,'5_TotalCounter',NULL,'5',NULL),(412,'5_DailyCounter',NULL,'5',NULL),(413,'5_ResetStatus_0',NULL,'5',NULL),(414,'5_ResetOutput_0',NULL,'5',NULL),(415,'5_TimeOutput_0_0',NULL,'5',NULL),(416,'5_TimeOutput_0_1',NULL,'5',NULL),(417,'5_TimeOutput_0_2',NULL,'5',NULL),(418,'5_TimeOutput_0_3',NULL,'5',NULL),(419,'5_TimeOutput_0_4',NULL,'5',NULL),(420,'5_TimeOutput_0_5',NULL,'5',NULL),(421,'5_TimeOutput_0_6',NULL,'5',NULL),(422,'5_TimeOutput_0_7',NULL,'5',NULL),(423,'5_TimeOutput_0_8',NULL,'5',NULL),(424,'5_TimeOutput_0_9',NULL,'5',NULL),(425,'5_TimeOutput_0_10',NULL,'5',NULL),(426,'5_TimeOutput_0_11',NULL,'5',NULL),(427,'5_TimeOutput_0_12',NULL,'5',NULL),(428,'5_TimeOutput_0_13',NULL,'5',NULL),(429,'5_TimeOutput_0_14',NULL,'5',NULL),(430,'5_TimeOutput_0_15',NULL,'5',NULL),(431,'5_TimeOutput_0_16',NULL,'5',NULL),(432,'5_TimeOutput_0_17',NULL,'5',NULL),(433,'5_TimeOutput_0_18',NULL,'5',NULL),(434,'5_TimeOutput_0_19',NULL,'5',NULL),(435,'5_TimeOutput_0_20',NULL,'5',NULL),(436,'5_TimeOutput_0_21',NULL,'5',NULL),(437,'5_TimeOutput_0_22',NULL,'5',NULL),(438,'5_TimeOutput_0_23',NULL,'5',NULL),(439,'5_TimeOutput_0_24',NULL,'5',NULL),(440,'5_TimeOutput_0_25',NULL,'5',NULL),(441,'5_TimeOutput_0_26',NULL,'5',NULL),(442,'5_TimeOutput_0_27',NULL,'5',NULL),(443,'5_TimeOutput_0_28',NULL,'5',NULL),(444,'5_TimeOutput_0_29',NULL,'5',NULL),(445,'5_TimeOutput_0_30',NULL,'5',NULL),(446,'5_TimeOutput_0_31',NULL,'5',NULL),(447,'5_CountOutput_0_0',NULL,'5',NULL),(448,'5_CountOutput_0_1',NULL,'5',NULL),(449,'5_CountOutput_0_2',NULL,'5',NULL),(450,'5_CountOutput_0_3',NULL,'5',NULL),(451,'5_CountOutput_0_4',NULL,'5',NULL),(452,'5_CountOutput_0_5',NULL,'5',NULL),(453,'5_CountOutput_0_6',NULL,'5',NULL),(454,'5_CountOutput_0_7',NULL,'5',NULL),(455,'5_CountOutput_0_8',NULL,'5',NULL),(456,'5_CountOutput_0_9',NULL,'5',NULL),(457,'5_CountOutput_0_10',NULL,'5',NULL),(458,'5_CountOutput_0_11',NULL,'5',NULL),(459,'5_CountOutput_0_12',NULL,'5',NULL),(460,'5_CountOutput_0_13',NULL,'5',NULL),(461,'5_CountOutput_0_14',NULL,'5',NULL),(462,'5_CountOutput_0_15',NULL,'5',NULL),(463,'5_CountOutput_0_16',NULL,'5',NULL),(464,'5_CountOutput_0_17',NULL,'5',NULL),(465,'5_CountOutput_0_18',NULL,'5',NULL),(466,'5_CountOutput_0_19',NULL,'5',NULL),(467,'5_CountOutput_0_20',NULL,'5',NULL),(468,'5_CountOutput_0_21',NULL,'5',NULL),(469,'5_CountOutput_0_22',NULL,'5',NULL),(470,'5_CountOutput_0_23',NULL,'5',NULL),(471,'5_CountOutput_0_24',NULL,'5',NULL),(472,'5_CountOutput_0_25',NULL,'5',NULL),(473,'5_CountOutput_0_26',NULL,'5',NULL),(474,'5_CountOutput_0_27',NULL,'5',NULL),(475,'5_CountOutput_0_28',NULL,'5',NULL),(476,'5_CountOutput_0_29',NULL,'5',NULL),(477,'5_CountOutput_0_30',NULL,'5',NULL),(478,'5_CountOutput_0_31',NULL,'5',NULL),(479,'5_TimeStatus_0_0',NULL,'5',NULL),(480,'5_TimeStatus_0_1',NULL,'5',NULL),(481,'5_TimeStatus_0_2',NULL,'5',NULL),(482,'5_TimeStatus_0_3',NULL,'5',NULL),(483,'5_TimeStatus_0_4',NULL,'5',NULL),(484,'5_TimeStatus_0_5',NULL,'5',NULL),(485,'5_TimeStatus_0_6',NULL,'5',NULL),(486,'5_TimeStatus_0_7',NULL,'5',NULL),(487,'5_TimeStatus_0_8',NULL,'5',NULL),(488,'5_TimeStatus_0_9',NULL,'5',NULL),(489,'5_TimeStatus_0_10',NULL,'5',NULL),(490,'5_TimeStatus_0_11',NULL,'5',NULL),(491,'5_TimeStatus_0_12',NULL,'5',NULL),(492,'5_TimeStatus_0_13',NULL,'5',NULL),(493,'5_TimeStatus_0_14',NULL,'5',NULL),(494,'5_TimeStatus_0_15',NULL,'5',NULL),(495,'5_TimeStatus_0_16',NULL,'5',NULL),(496,'5_TimeStatus_0_17',NULL,'5',NULL),(497,'5_TimeStatus_0_18',NULL,'5',NULL),(498,'5_TimeStatus_0_19',NULL,'5',NULL),(499,'5_TimeStatus_0_20',NULL,'5',NULL),(500,'5_TimeStatus_0_21',NULL,'5',NULL),(501,'5_TimeStatus_0_22',NULL,'5',NULL),(502,'5_TimeStatus_0_23',NULL,'5',NULL),(503,'5_TimeStatus_0_24',NULL,'5',NULL),(504,'5_TimeStatus_0_25',NULL,'5',NULL),(505,'5_TimeStatus_0_26',NULL,'5',NULL),(506,'5_TimeStatus_0_27',NULL,'5',NULL),(507,'5_TimeStatus_0_28',NULL,'5',NULL),(508,'5_TimeStatus_0_29',NULL,'5',NULL),(509,'5_TimeStatus_0_30',NULL,'5',NULL),(510,'5_TimeStatus_0_31',NULL,'5',NULL),(511,'6_Output_0',NULL,'6',NULL),(512,'6_CountOutput_0_0',NULL,'6',NULL),(513,'6_TimeOutput_0_0',NULL,'6',NULL),(514,'6_ResetOutput_0',NULL,'6',NULL),(515,'6_DailyCounter',NULL,'6',NULL),(516,'6_TotalCounter',NULL,'6',NULL),(517,'6_Status_0','256','6',NULL),(518,'6_TimeStatus_0_0',NULL,'6',NULL),(519,'6_TimeStatus_0_1',NULL,'6',NULL),(520,'6_TimeStatus_0_3',NULL,'6',NULL),(521,'6_TimeStatus_0_4',NULL,'6',NULL),(522,'6_TimeStatus_0_5',NULL,'6',NULL),(523,'6_TimeStatus_0_6',NULL,'6',NULL),(524,'6_TimeStatus_0_7',NULL,'6',NULL),(525,'6_TimeStatus_0_8',NULL,'6',NULL),(526,'6_TimeStatus_0_9',NULL,'6',NULL),(527,'6_TimeStatus_0_10',NULL,'6',NULL),(528,'6_TimeStatus_0_11',NULL,'6',NULL),(529,'6_TimeStatus_0_12',NULL,'6',NULL),(530,'6_TimeStatus_0_13',NULL,'6',NULL),(531,'6_TimeStatus_0_14',NULL,'6',NULL),(532,'6_TimeStatus_0_15',NULL,'6',NULL),(533,'6_TimeStatus_0_2',NULL,'6',NULL),(534,'6_TimeOutput_0_2',NULL,'6',NULL),(535,'6_TimeOutput_0_3',NULL,'6',NULL),(536,'6_TimeOutput_0_4',NULL,'6',NULL),(537,'6_TimeOutput_0_5',NULL,'6',NULL),(538,'6_TimeOutput_0_6',NULL,'6',NULL),(539,'6_TimeOutput_0_7',NULL,'6',NULL),(540,'6_TimeOutput_0_1',NULL,'6',NULL),(541,'6_TimeOutput_0_8',NULL,'6',NULL),(542,'6_TimeOutput_0_10',NULL,'6',NULL),(543,'6_TimeOutput_0_11',NULL,'6',NULL),(544,'6_TimeOutput_0_12',NULL,'6',NULL),(545,'6_TimeOutput_0_13',NULL,'6',NULL),(546,'6_TimeOutput_0_14',NULL,'6',NULL),(547,'6_TimeOutput_0_15',NULL,'6',NULL),(548,'6_TimeOutput_0_9',NULL,'6',NULL),(549,'6_CountOutput_0_1',NULL,'6',NULL),(550,'6_CountOutput_0_3',NULL,'6',NULL),(551,'6_CountOutput_0_4',NULL,'6',NULL),(552,'6_CountOutput_0_5',NULL,'6',NULL),(553,'6_CountOutput_0_6',NULL,'6',NULL),(554,'6_CountOutput_0_7',NULL,'6',NULL),(555,'6_CountOutput_0_8',NULL,'6',NULL),(556,'6_CountOutput_0_9',NULL,'6',NULL),(557,'6_CountOutput_0_10',NULL,'6',NULL),(558,'6_CountOutput_0_11',NULL,'6',NULL),(559,'6_CountOutput_0_12',NULL,'6',NULL),(560,'6_CountOutput_0_13',NULL,'6',NULL),(561,'6_CountOutput_0_14',NULL,'6',NULL),(562,'6_CountOutput_0_15',NULL,'6',NULL),(563,'6_CountOutput_0_2',NULL,'6',NULL),(564,'6_ResetStatus_0',NULL,'6',NULL),(565,'6_Input_0',NULL,'6',NULL),(566,'6_Status_1',NULL,'6',NULL),(567,'6_TimeOutput_0_16',NULL,'6',NULL),(568,'6_TimeOutput_0_17',NULL,'6',NULL),(569,'6_TimeOutput_0_18',NULL,'6',NULL),(570,'6_TimeOutput_0_19',NULL,'6',NULL),(571,'6_TimeOutput_0_20',NULL,'6',NULL),(572,'6_TimeOutput_0_21',NULL,'6',NULL),(573,'6_TimeOutput_0_22',NULL,'6',NULL),(574,'6_TimeOutput_0_23',NULL,'6',NULL),(575,'6_TimeOutput_0_24',NULL,'6',NULL),(576,'6_TimeOutput_0_25',NULL,'6',NULL),(577,'6_TimeOutput_0_26',NULL,'6',NULL),(578,'6_TimeOutput_0_27',NULL,'6',NULL),(579,'6_TimeOutput_0_28',NULL,'6',NULL),(580,'6_TimeOutput_0_29',NULL,'6',NULL),(581,'6_TimeOutput_0_30',NULL,'6',NULL),(582,'6_TimeOutput_0_31',NULL,'6',NULL),(583,'6_CountOutput_0_16',NULL,'6',NULL),(584,'6_CountOutput_0_17',NULL,'6',NULL),(585,'6_CountOutput_0_18',NULL,'6',NULL),(586,'6_CountOutput_0_19',NULL,'6',NULL),(587,'6_CountOutput_0_20',NULL,'6',NULL),(588,'6_CountOutput_0_21',NULL,'6',NULL),(589,'6_CountOutput_0_22',NULL,'6',NULL),(590,'6_CountOutput_0_23',NULL,'6',NULL),(591,'6_CountOutput_0_24',NULL,'6',NULL),(592,'6_CountOutput_0_25',NULL,'6',NULL),(593,'6_CountOutput_0_26',NULL,'6',NULL),(594,'6_CountOutput_0_27',NULL,'6',NULL),(595,'6_CountOutput_0_28',NULL,'6',NULL),(596,'6_CountOutput_0_29',NULL,'6',NULL),(597,'6_CountOutput_0_30',NULL,'6',NULL),(598,'6_CountOutput_0_31',NULL,'6',NULL),(599,'6_TimeStatus_0_16',NULL,'6',NULL),(600,'6_TimeStatus_0_17',NULL,'6',NULL),(601,'6_TimeStatus_0_18',NULL,'6',NULL),(602,'6_TimeStatus_0_19',NULL,'6',NULL),(603,'6_TimeStatus_0_20',NULL,'6',NULL),(604,'6_TimeStatus_0_21',NULL,'6',NULL),(605,'6_TimeStatus_0_22',NULL,'6',NULL),(606,'6_TimeStatus_0_23',NULL,'6',NULL),(607,'6_TimeStatus_0_24',NULL,'6',NULL),(608,'6_TimeStatus_0_25',NULL,'6',NULL),(609,'6_TimeStatus_0_26',NULL,'6',NULL),(610,'6_TimeStatus_0_27',NULL,'6',NULL),(611,'6_TimeStatus_0_28',NULL,'6',NULL),(612,'6_TimeStatus_0_29',NULL,'6',NULL),(613,'6_TimeStatus_0_30',NULL,'6',NULL),(614,'6_TimeStatus_0_31',NULL,'6',NULL),(615,'7_Status_0','256','7',NULL),(616,'7_Status_1',NULL,'7',NULL),(617,'7_TotalCounter',NULL,'7',NULL),(618,'7_DailyCounter',NULL,'7',NULL),(619,'7_ResetStatus_0',NULL,'7',NULL),(620,'7_ResetOutput_0',NULL,'7',NULL),(621,'7_TimeOutput_0_0',NULL,'7',NULL),(622,'7_TimeOutput_0_1',NULL,'7',NULL),(623,'7_TimeOutput_0_2',NULL,'7',NULL),(624,'7_TimeOutput_0_3',NULL,'7',NULL),(625,'7_TimeOutput_0_4',NULL,'7',NULL),(626,'7_TimeOutput_0_5',NULL,'7',NULL),(627,'7_TimeOutput_0_6',NULL,'7',NULL),(628,'7_TimeOutput_0_7',NULL,'7',NULL),(629,'7_TimeOutput_0_8',NULL,'7',NULL),(630,'7_TimeOutput_0_9',NULL,'7',NULL),(631,'7_TimeOutput_0_10',NULL,'7',NULL),(632,'7_TimeOutput_0_11',NULL,'7',NULL),(633,'7_TimeOutput_0_12',NULL,'7',NULL),(634,'7_TimeOutput_0_13',NULL,'7',NULL),(635,'7_TimeOutput_0_14',NULL,'7',NULL),(636,'7_TimeOutput_0_15',NULL,'7',NULL),(637,'7_TimeOutput_0_16',NULL,'7',NULL),(638,'7_TimeOutput_0_17',NULL,'7',NULL),(639,'7_TimeOutput_0_18',NULL,'7',NULL),(640,'7_TimeOutput_0_19',NULL,'7',NULL),(641,'7_TimeOutput_0_20',NULL,'7',NULL),(642,'7_TimeOutput_0_21',NULL,'7',NULL),(643,'7_TimeOutput_0_22',NULL,'7',NULL),(644,'7_TimeOutput_0_23',NULL,'7',NULL),(645,'7_TimeOutput_0_24',NULL,'7',NULL),(646,'7_TimeOutput_0_25',NULL,'7',NULL),(647,'7_TimeOutput_0_26',NULL,'7',NULL),(648,'7_TimeOutput_0_27',NULL,'7',NULL),(649,'7_TimeOutput_0_28',NULL,'7',NULL),(650,'7_TimeOutput_0_29',NULL,'7',NULL),(651,'7_TimeOutput_0_30',NULL,'7',NULL),(652,'7_TimeOutput_0_31',NULL,'7',NULL),(653,'7_CountOutput_0_0',NULL,'7',NULL),(654,'7_CountOutput_0_1',NULL,'7',NULL),(655,'7_CountOutput_0_2',NULL,'7',NULL),(656,'7_CountOutput_0_3',NULL,'7',NULL),(657,'7_CountOutput_0_4',NULL,'7',NULL),(658,'7_CountOutput_0_5',NULL,'7',NULL),(659,'7_CountOutput_0_6',NULL,'7',NULL),(660,'7_CountOutput_0_7',NULL,'7',NULL),(661,'7_CountOutput_0_8',NULL,'7',NULL),(662,'7_CountOutput_0_9',NULL,'7',NULL),(663,'7_CountOutput_0_10',NULL,'7',NULL),(664,'7_CountOutput_0_11',NULL,'7',NULL),(665,'7_CountOutput_0_12',NULL,'7',NULL),(666,'7_CountOutput_0_13',NULL,'7',NULL),(667,'7_CountOutput_0_14',NULL,'7',NULL),(668,'7_CountOutput_0_15',NULL,'7',NULL),(669,'7_CountOutput_0_16',NULL,'7',NULL),(670,'7_CountOutput_0_17',NULL,'7',NULL),(671,'7_CountOutput_0_18',NULL,'7',NULL),(672,'7_CountOutput_0_19',NULL,'7',NULL),(673,'7_CountOutput_0_20',NULL,'7',NULL),(674,'7_CountOutput_0_21',NULL,'7',NULL),(675,'7_CountOutput_0_22',NULL,'7',NULL),(676,'7_CountOutput_0_23',NULL,'7',NULL),(677,'7_CountOutput_0_24',NULL,'7',NULL),(678,'7_CountOutput_0_25',NULL,'7',NULL),(679,'7_CountOutput_0_26',NULL,'7',NULL),(680,'7_CountOutput_0_27',NULL,'7',NULL),(681,'7_CountOutput_0_28',NULL,'7',NULL),(682,'7_CountOutput_0_29',NULL,'7',NULL),(683,'7_CountOutput_0_30',NULL,'7',NULL),(684,'7_CountOutput_0_31',NULL,'7',NULL),(685,'7_TimeStatus_0_0',NULL,'7',NULL),(686,'7_TimeStatus_0_1',NULL,'7',NULL),(687,'7_TimeStatus_0_2',NULL,'7',NULL),(688,'7_TimeStatus_0_3',NULL,'7',NULL),(689,'7_TimeStatus_0_4',NULL,'7',NULL),(690,'7_TimeStatus_0_5',NULL,'7',NULL),(691,'7_TimeStatus_0_6',NULL,'7',NULL),(692,'7_TimeStatus_0_7',NULL,'7',NULL),(693,'7_TimeStatus_0_8',NULL,'7',NULL),(694,'7_TimeStatus_0_9',NULL,'7',NULL),(695,'7_TimeStatus_0_10',NULL,'7',NULL),(696,'7_TimeStatus_0_11',NULL,'7',NULL),(697,'7_TimeStatus_0_12',NULL,'7',NULL),(698,'7_TimeStatus_0_13',NULL,'7',NULL),(699,'7_TimeStatus_0_14',NULL,'7',NULL),(700,'7_TimeStatus_0_15',NULL,'7',NULL),(701,'7_TimeStatus_0_16',NULL,'7',NULL),(702,'7_TimeStatus_0_17',NULL,'7',NULL),(703,'7_TimeStatus_0_18',NULL,'7',NULL),(704,'7_TimeStatus_0_19',NULL,'7',NULL),(705,'7_TimeStatus_0_20',NULL,'7',NULL),(706,'7_TimeStatus_0_21',NULL,'7',NULL),(707,'7_TimeStatus_0_22',NULL,'7',NULL),(708,'7_TimeStatus_0_23',NULL,'7',NULL),(709,'7_TimeStatus_0_24',NULL,'7',NULL),(710,'7_TimeStatus_0_25',NULL,'7',NULL),(711,'7_TimeStatus_0_26',NULL,'7',NULL),(712,'7_TimeStatus_0_27',NULL,'7',NULL),(713,'7_TimeStatus_0_28',NULL,'7',NULL),(714,'7_TimeStatus_0_29',NULL,'7',NULL),(715,'7_TimeStatus_0_30',NULL,'7',NULL),(716,'7_TimeStatus_0_31',NULL,'7',NULL),(717,'8_Status_0','256','8',NULL),(718,'8_Status_1',NULL,'8',NULL),(719,'8_TotalCounter',NULL,'8',NULL),(720,'8_DailyCounter',NULL,'8',NULL),(721,'8_ResetStatus_0',NULL,'8',NULL),(722,'8_ResetOutput_0',NULL,'8',NULL),(723,'8_TimeOutput_0_0',NULL,'8',NULL),(724,'8_TimeOutput_0_1',NULL,'8',NULL),(725,'8_TimeOutput_0_2',NULL,'8',NULL),(726,'8_TimeOutput_0_3',NULL,'8',NULL),(727,'8_TimeOutput_0_4',NULL,'8',NULL),(728,'8_TimeOutput_0_5',NULL,'8',NULL),(729,'8_TimeOutput_0_6',NULL,'8',NULL),(730,'8_TimeOutput_0_7',NULL,'8',NULL),(731,'8_TimeOutput_0_8',NULL,'8',NULL),(732,'8_TimeOutput_0_9',NULL,'8',NULL),(733,'8_TimeOutput_0_10',NULL,'8',NULL),(734,'8_TimeOutput_0_11',NULL,'8',NULL),(735,'8_TimeOutput_0_12',NULL,'8',NULL),(736,'8_TimeOutput_0_13',NULL,'8',NULL),(737,'8_TimeOutput_0_14',NULL,'8',NULL),(738,'8_TimeOutput_0_15',NULL,'8',NULL),(739,'8_TimeOutput_0_16',NULL,'8',NULL),(740,'8_TimeOutput_0_17',NULL,'8',NULL),(741,'8_TimeOutput_0_18',NULL,'8',NULL),(742,'8_TimeOutput_0_19',NULL,'8',NULL),(743,'8_TimeOutput_0_20',NULL,'8',NULL),(744,'8_TimeOutput_0_21',NULL,'8',NULL),(745,'8_TimeOutput_0_22',NULL,'8',NULL),(746,'8_TimeOutput_0_23',NULL,'8',NULL),(747,'8_TimeOutput_0_24',NULL,'8',NULL),(748,'8_TimeOutput_0_25',NULL,'8',NULL),(749,'8_TimeOutput_0_26',NULL,'8',NULL),(750,'8_TimeOutput_0_27',NULL,'8',NULL),(751,'8_TimeOutput_0_28',NULL,'8',NULL),(752,'8_TimeOutput_0_29',NULL,'8',NULL),(753,'8_TimeOutput_0_30',NULL,'8',NULL),(754,'8_TimeOutput_0_31',NULL,'8',NULL),(755,'8_CountOutput_0_0',NULL,'8',NULL),(756,'8_CountOutput_0_1',NULL,'8',NULL),(757,'8_CountOutput_0_2',NULL,'8',NULL),(758,'8_CountOutput_0_3',NULL,'8',NULL),(759,'8_CountOutput_0_4',NULL,'8',NULL),(760,'8_CountOutput_0_5',NULL,'8',NULL),(761,'8_CountOutput_0_6',NULL,'8',NULL),(762,'8_CountOutput_0_7',NULL,'8',NULL),(763,'8_CountOutput_0_8',NULL,'8',NULL),(764,'8_CountOutput_0_9',NULL,'8',NULL),(765,'8_CountOutput_0_10',NULL,'8',NULL),(766,'8_CountOutput_0_11',NULL,'8',NULL),(767,'8_CountOutput_0_12',NULL,'8',NULL),(768,'8_CountOutput_0_13',NULL,'8',NULL),(769,'8_CountOutput_0_14',NULL,'8',NULL),(770,'8_CountOutput_0_15',NULL,'8',NULL),(771,'8_CountOutput_0_16',NULL,'8',NULL),(772,'8_CountOutput_0_17',NULL,'8',NULL),(773,'8_CountOutput_0_18',NULL,'8',NULL),(774,'8_CountOutput_0_19',NULL,'8',NULL),(775,'8_CountOutput_0_20',NULL,'8',NULL),(776,'8_CountOutput_0_21',NULL,'8',NULL),(777,'8_CountOutput_0_22',NULL,'8',NULL),(778,'8_CountOutput_0_23',NULL,'8',NULL),(779,'8_CountOutput_0_24',NULL,'8',NULL),(780,'8_CountOutput_0_25',NULL,'8',NULL),(781,'8_CountOutput_0_26',NULL,'8',NULL),(782,'8_CountOutput_0_27',NULL,'8',NULL),(783,'8_CountOutput_0_28',NULL,'8',NULL),(784,'8_CountOutput_0_29',NULL,'8',NULL),(785,'8_CountOutput_0_30',NULL,'8',NULL),(786,'8_CountOutput_0_31',NULL,'8',NULL),(787,'8_TimeStatus_0_0',NULL,'8',NULL),(788,'8_TimeStatus_0_1',NULL,'8',NULL),(789,'8_TimeStatus_0_2',NULL,'8',NULL),(790,'8_TimeStatus_0_3',NULL,'8',NULL),(791,'8_TimeStatus_0_4',NULL,'8',NULL),(792,'8_TimeStatus_0_5',NULL,'8',NULL),(793,'8_TimeStatus_0_6',NULL,'8',NULL),(794,'8_TimeStatus_0_7',NULL,'8',NULL),(795,'8_TimeStatus_0_8',NULL,'8',NULL),(796,'8_TimeStatus_0_9',NULL,'8',NULL),(797,'8_TimeStatus_0_10',NULL,'8',NULL),(798,'8_TimeStatus_0_11',NULL,'8',NULL),(799,'8_TimeStatus_0_12',NULL,'8',NULL),(800,'8_TimeStatus_0_13',NULL,'8',NULL),(801,'8_TimeStatus_0_14',NULL,'8',NULL),(802,'8_TimeStatus_0_15',NULL,'8',NULL),(803,'8_TimeStatus_0_16',NULL,'8',NULL),(804,'8_TimeStatus_0_17',NULL,'8',NULL),(805,'8_TimeStatus_0_18',NULL,'8',NULL),(806,'8_TimeStatus_0_19',NULL,'8',NULL),(807,'8_TimeStatus_0_20',NULL,'8',NULL),(808,'8_TimeStatus_0_21',NULL,'8',NULL),(809,'8_TimeStatus_0_22',NULL,'8',NULL),(810,'8_TimeStatus_0_23',NULL,'8',NULL),(811,'8_TimeStatus_0_24',NULL,'8',NULL),(812,'8_TimeStatus_0_25',NULL,'8',NULL),(813,'8_TimeStatus_0_26',NULL,'8',NULL),(814,'8_TimeStatus_0_27',NULL,'8',NULL),(815,'8_TimeStatus_0_28',NULL,'8',NULL),(816,'8_TimeStatus_0_29',NULL,'8',NULL),(817,'8_TimeStatus_0_30',NULL,'8',NULL),(818,'8_TimeStatus_0_31',NULL,'8',NULL),(819,'9_Status_0','256','9',NULL),(820,'9_Status_1',NULL,'9',NULL),(821,'9_TotalCounter',NULL,'9',NULL),(822,'9_DailyCounter',NULL,'9',NULL),(823,'9_ResetStatus_0',NULL,'9',NULL),(824,'9_ResetOutput_0',NULL,'9',NULL),(825,'9_TimeOutput_0_0',NULL,'9',NULL),(826,'9_TimeOutput_0_1',NULL,'9',NULL),(827,'9_TimeOutput_0_2',NULL,'9',NULL),(828,'9_TimeOutput_0_3',NULL,'9',NULL),(829,'9_TimeOutput_0_4',NULL,'9',NULL),(830,'9_TimeOutput_0_5',NULL,'9',NULL),(831,'9_TimeOutput_0_6',NULL,'9',NULL),(832,'9_TimeOutput_0_7',NULL,'9',NULL),(833,'9_TimeOutput_0_8',NULL,'9',NULL),(834,'9_TimeOutput_0_9',NULL,'9',NULL),(835,'9_TimeOutput_0_10',NULL,'9',NULL),(836,'9_TimeOutput_0_11',NULL,'9',NULL),(837,'9_TimeOutput_0_12',NULL,'9',NULL),(838,'9_TimeOutput_0_13',NULL,'9',NULL),(839,'9_TimeOutput_0_14',NULL,'9',NULL),(840,'9_TimeOutput_0_15',NULL,'9',NULL),(841,'9_TimeOutput_0_16',NULL,'9',NULL),(842,'9_TimeOutput_0_17',NULL,'9',NULL),(843,'9_TimeOutput_0_18',NULL,'9',NULL),(844,'9_TimeOutput_0_19',NULL,'9',NULL),(845,'9_TimeOutput_0_20',NULL,'9',NULL),(846,'9_TimeOutput_0_21',NULL,'9',NULL),(847,'9_TimeOutput_0_22',NULL,'9',NULL),(848,'9_TimeOutput_0_23',NULL,'9',NULL),(849,'9_TimeOutput_0_24',NULL,'9',NULL),(850,'9_TimeOutput_0_25',NULL,'9',NULL),(851,'9_TimeOutput_0_26',NULL,'9',NULL),(852,'9_TimeOutput_0_27',NULL,'9',NULL),(853,'9_TimeOutput_0_28',NULL,'9',NULL),(854,'9_TimeOutput_0_29',NULL,'9',NULL),(855,'9_TimeOutput_0_30',NULL,'9',NULL),(856,'9_TimeOutput_0_31',NULL,'9',NULL),(857,'9_CountOutput_0_0',NULL,'9',NULL),(858,'9_CountOutput_0_1',NULL,'9',NULL),(859,'9_CountOutput_0_2',NULL,'9',NULL),(860,'9_CountOutput_0_3',NULL,'9',NULL),(861,'9_CountOutput_0_4',NULL,'9',NULL),(862,'9_CountOutput_0_5',NULL,'9',NULL),(863,'9_CountOutput_0_6',NULL,'9',NULL),(864,'9_CountOutput_0_7',NULL,'9',NULL),(865,'9_CountOutput_0_8',NULL,'9',NULL),(866,'9_CountOutput_0_9',NULL,'9',NULL),(867,'9_CountOutput_0_10',NULL,'9',NULL),(868,'9_CountOutput_0_11',NULL,'9',NULL),(869,'9_CountOutput_0_12',NULL,'9',NULL),(870,'9_CountOutput_0_13',NULL,'9',NULL),(871,'9_CountOutput_0_14',NULL,'9',NULL),(872,'9_CountOutput_0_15',NULL,'9',NULL),(873,'9_CountOutput_0_16',NULL,'9',NULL),(874,'9_CountOutput_0_17',NULL,'9',NULL),(875,'9_CountOutput_0_18',NULL,'9',NULL),(876,'9_CountOutput_0_19',NULL,'9',NULL),(877,'9_CountOutput_0_20',NULL,'9',NULL),(878,'9_CountOutput_0_21',NULL,'9',NULL),(879,'9_CountOutput_0_22',NULL,'9',NULL),(880,'9_CountOutput_0_23',NULL,'9',NULL),(881,'9_CountOutput_0_24',NULL,'9',NULL),(882,'9_CountOutput_0_25',NULL,'9',NULL),(883,'9_CountOutput_0_26',NULL,'9',NULL),(884,'9_CountOutput_0_27',NULL,'9',NULL),(885,'9_CountOutput_0_28',NULL,'9',NULL),(886,'9_CountOutput_0_29',NULL,'9',NULL),(887,'9_CountOutput_0_30',NULL,'9',NULL),(888,'9_CountOutput_0_31',NULL,'9',NULL),(889,'9_TimeStatus_0_0',NULL,'9',NULL),(890,'9_TimeStatus_0_1',NULL,'9',NULL),(891,'9_TimeStatus_0_2',NULL,'9',NULL),(892,'9_TimeStatus_0_3',NULL,'9',NULL),(893,'9_TimeStatus_0_4',NULL,'9',NULL),(894,'9_TimeStatus_0_5',NULL,'9',NULL),(895,'9_TimeStatus_0_6',NULL,'9',NULL),(896,'9_TimeStatus_0_7',NULL,'9',NULL),(897,'9_TimeStatus_0_8',NULL,'9',NULL),(898,'9_TimeStatus_0_9',NULL,'9',NULL),(899,'9_TimeStatus_0_10',NULL,'9',NULL),(900,'9_TimeStatus_0_11',NULL,'9',NULL),(901,'9_TimeStatus_0_12',NULL,'9',NULL),(902,'9_TimeStatus_0_13',NULL,'9',NULL),(903,'9_TimeStatus_0_14',NULL,'9',NULL),(904,'9_TimeStatus_0_15',NULL,'9',NULL),(905,'9_TimeStatus_0_16',NULL,'9',NULL),(906,'9_TimeStatus_0_17',NULL,'9',NULL),(907,'9_TimeStatus_0_18',NULL,'9',NULL),(908,'9_TimeStatus_0_19',NULL,'9',NULL),(909,'9_TimeStatus_0_20',NULL,'9',NULL),(910,'9_TimeStatus_0_21',NULL,'9',NULL),(911,'9_TimeStatus_0_22',NULL,'9',NULL),(912,'9_TimeStatus_0_23',NULL,'9',NULL),(913,'9_TimeStatus_0_24',NULL,'9',NULL),(914,'9_TimeStatus_0_25',NULL,'9',NULL),(915,'9_TimeStatus_0_26',NULL,'9',NULL),(916,'9_TimeStatus_0_27',NULL,'9',NULL),(917,'9_TimeStatus_0_28',NULL,'9',NULL),(918,'9_TimeStatus_0_29',NULL,'9',NULL),(919,'9_TimeStatus_0_30',NULL,'9',NULL),(920,'9_TimeStatus_0_31',NULL,'9',NULL);
/*!40000 ALTER TABLE `wincc_statistics` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:40:59