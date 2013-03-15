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
-- Table structure for table `routing_conditions`
--

DROP TABLE IF EXISTS `routing_conditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `routing_conditions` (
  `idrouting_conditions` int(11) NOT NULL AUTO_INCREMENT,
  `RoutingPositionEntry_ID` int(11) DEFAULT NULL,
  `ConditionList_ID` int(11) DEFAULT NULL,
  `ValueName` varchar(256) DEFAULT NULL,
  `Operation_ID` int(11) DEFAULT '-1',
  `Value` varchar(256) DEFAULT NULL,
  `Condition_comply` tinyint(1) DEFAULT '0',
  `Description` tinytext,
  `ActualValue` varchar(255) DEFAULT NULL,
  `SortOrder` int(11) DEFAULT NULL,
  PRIMARY KEY (`idrouting_conditions`),
  KEY `routing_conditions` (`ConditionList_ID`,`RoutingPositionEntry_ID`,`Condition_comply`)
) ENGINE=InnoDB AUTO_INCREMENT=261 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routing_conditions`
--

LOCK TABLES `routing_conditions` WRITE;
/*!40000 ALTER TABLE `routing_conditions` DISABLE KEYS */;
INSERT INTO `routing_conditions` VALUES (91,1,10,'24',1,'true',0,NULL,'',NULL),(92,1,7,'350',1,'occupied',0,NULL,'',NULL),(93,1,3,'2002',1,'sample free',1,NULL,'',NULL),(95,2,10,'24',1,'true',0,NULL,'',NULL),(96,2,2,'5',NULL,'',0,NULL,'0',NULL),(97,3,10,'24',1,'true',0,NULL,'',NULL),(98,3,2,'3',NULL,'',0,NULL,'0',NULL),(99,4,3,'6',1,'sample free',0,NULL,'',NULL),(101,4,2,'2',NULL,'',0,NULL,'0',NULL),(102,4,3,'2002',1,'sample free',0,NULL,'',NULL),(105,5,2,'4',NULL,'',0,NULL,'0',NULL),(106,6,10,'24',1,'true',0,NULL,'',NULL),(107,6,2,'6',NULL,'',0,NULL,'0',NULL),(108,5,10,'98',1,'true',0,NULL,'',NULL),(109,7,3,'2002',1,'sample free',0,NULL,'',NULL),(110,7,10,'48',1,'false',0,NULL,'',NULL),(111,7,2,'6',NULL,'',0,NULL,'0',NULL),(116,9,2,'4',NULL,'',0,NULL,'0',NULL),(117,9,10,'99',1,'true',0,NULL,'',NULL),(118,10,10,'108',1,'false',0,NULL,'',NULL),(119,10,2,'4',NULL,'',0,NULL,'0',NULL),(120,11,10,'24',1,'false',0,NULL,'',NULL),(121,11,2,'7',NULL,'',0,NULL,'0',NULL),(122,12,7,'62',1,'not occupied',0,NULL,'',NULL),(123,12,3,'2002',1,'sample free',0,NULL,'',NULL),(124,12,10,'24',1,'true',0,NULL,'',NULL),(125,12,2,'4',NULL,'',0,NULL,'0',NULL),(126,13,10,'24',1,'true',0,NULL,'',NULL),(127,13,10,'129',1,'false',0,NULL,'',NULL),(128,13,10,'24',1,'true',0,NULL,'',NULL),(129,13,10,'129',1,'false',0,NULL,'',NULL),(132,3,6,'PREPARATION',2,'OK',0,NULL,'',NULL),(133,16,6,'PREPARATION',1,'OK',0,NULL,'',NULL),(134,16,2,'4',NULL,'',0,NULL,'0',NULL),(135,16,10,'99',1,'true',0,NULL,'',NULL),(136,18,10,'99',1,'true',0,NULL,'',NULL),(137,18,2,'2',NULL,'',0,NULL,'0',NULL),(141,19,10,'63',1,'true',0,NULL,'',NULL),(142,19,10,'64',1,'false',0,NULL,'',NULL),(143,19,10,'65',1,'false',0,NULL,'',NULL),(144,19,2,'6',NULL,'',0,NULL,'0',NULL),(146,20,3,'2001',1,'sample free',0,NULL,'',NULL),(147,20,2,'4',NULL,'',0,NULL,'0',NULL),(148,21,1,'2',NULL,'',0,NULL,'0',NULL),(152,22,3,'6',1,'sample free',0,NULL,'',NULL),(153,22,10,'48',1,'false',0,NULL,'',NULL),(155,23,10,'24',1,'true',0,NULL,'0',NULL),(156,21,11,'',1,'free',0,NULL,'',NULL),(157,25,3,'2002',1,'sample free',0,NULL,'',NULL),(158,25,3,'9',1,'sample free',0,NULL,'',NULL),(159,25,7,'269',1,'not occupied',0,NULL,'',NULL),(160,24,3,'9',1,'sample free',0,NULL,'',NULL),(161,24,3,'2002',1,'sample free',0,NULL,'',NULL),(162,24,10,'129',1,'true',0,NULL,'',NULL),(163,24,10,'394',1,'true',0,NULL,'',NULL),(164,26,10,'394',1,'true',0,NULL,'',NULL),(165,26,10,'129',1,'true',0,NULL,'',NULL),(166,26,10,'129',1,'true',0,NULL,'',NULL),(168,26,6,'ScanAttempts',1,'0',0,NULL,'',NULL),(169,27,10,'394',1,'true',0,NULL,'',NULL),(170,27,10,'412',1,'true',0,NULL,'',NULL),(172,27,6,'ScanAttempts',1,'1',0,NULL,'',NULL),(178,29,10,'394',1,'true',0,NULL,'',NULL),(179,29,10,'413',1,'true',0,NULL,'',NULL),(181,29,6,'ScanAttempts',1,'1',0,NULL,'',NULL),(183,28,3,'9',1,'sample free',0,NULL,'',NULL),(184,28,3,'2002',1,'sample free',0,NULL,'',NULL),(185,28,10,'129',1,'true',0,NULL,'',NULL),(186,28,10,'394',1,'true',0,NULL,'',NULL),(187,28,10,'410',1,'true',0,NULL,'',NULL),(188,28,6,'SetRobotAccess',1,'1',0,NULL,'',NULL),(191,24,10,'187',1,'false',0,NULL,'',NULL),(196,31,10,'129',1,'true',0,NULL,'',NULL),(197,31,10,'24',1,'true',0,NULL,'',NULL),(198,31,10,'147',1,'true',0,NULL,'',NULL),(199,32,10,'129',1,'true',0,NULL,'',NULL),(200,32,6,'SetRobotAccess',2,'1',0,NULL,'',NULL),(201,32,10,'187',1,'false',0,NULL,'',NULL),(202,33,10,'129',1,'true',0,NULL,'',NULL),(203,33,6,'SetRobotAccess',1,'1',0,NULL,'',NULL),(204,33,10,'186',1,'true',0,NULL,'',NULL),(205,34,10,'24',1,'true',0,NULL,'',1),(216,1,10,'129',1,'true',0,NULL,'',NULL),(217,1,10,'266',1,'true',0,NULL,'',NULL),(218,1,10,'283',1,'false',0,NULL,'',NULL),(219,1,10,'284',1,'true',0,NULL,'',NULL),(220,36,10,'24',1,'true',0,NULL,'',1),(221,36,7,'350',1,'occupied',0,NULL,'',2),(222,36,3,'2002',1,'sample free',0,NULL,'',4),(223,36,10,'129',1,'true',0,NULL,'',5),(224,36,10,'266',1,'true',0,NULL,'',6),(225,36,10,'282',1,'true',0,NULL,'',7),(226,36,10,'284',1,'true',0,NULL,'',8),(227,36,6,'SetRobotAccess',1,'1',0,NULL,'',3),(228,38,10,'129',1,'true',0,NULL,'',NULL),(229,37,7,'55',1,'occupied',0,NULL,'',NULL),(231,34,7,'353',1,'occupied',0,NULL,'',2),(232,34,8,'353',1,'2',0,NULL,'',3),(233,18,5,'6_BalanceWeight',1,'0.0',0,NULL,'',NULL),(240,21,10,'63',1,'true',0,NULL,'',NULL),(241,21,10,'64',1,'false',0,NULL,'',NULL),(242,21,10,'65',1,'false',0,NULL,'',NULL),(243,21,2,'8',NULL,'',0,NULL,'0',NULL),(244,39,3,'2001',1,'sample free',0,NULL,'',1),(245,39,5,'*Tim8',1,'dfs',0,'','',2),(246,42,7,'55',1,'occupied',0,NULL,'',NULL),(257,43,10,'24',1,'true',0,'','\"\"',2),(258,43,7,'353',1,'occupied',0,'','\"\"',3),(259,43,8,'353',1,'2',0,'','\"\"',1),(260,39,4,'RunMode',1,'dfhg',0,NULL,'',2);
/*!40000 ALTER TABLE `routing_conditions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:25
