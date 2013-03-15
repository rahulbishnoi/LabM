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
-- Table structure for table `editor_table`
--

DROP TABLE IF EXISTS `editor_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `editor_table` (
  `ideditor_table` int(11) NOT NULL AUTO_INCREMENT,
  `EditorTableListType` varchar(45) DEFAULT NULL,
  `TableNameReference` varchar(127) DEFAULT NULL,
  `TableColumn` varchar(60) DEFAULT NULL,
  `CommaSeparatedListValues` text,
  `CommaSeparatedListNames` text,
  `SQL_Command` text,
  `Description` varchar(127) DEFAULT NULL,
  PRIMARY KEY (`ideditor_table`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `editor_table`
--

LOCK TABLES `editor_table` WRITE;
/*!40000 ALTER TABLE `editor_table` DISABLE KEYS */;
INSERT INTO `editor_table` VALUES (1,'StyleBool','tcpip_configuration','Activate',NULL,NULL,NULL,'Test bool'),(2,'ListDictionary','tcpip_configuration','Machine_ID','0','Please select','SELECT  machines.idmachines,machines.Name FROM   machine_list INNER JOIN machines ON machine_list.idmachine_list = machines.Machine_list_ID  WHERE (machine_list.Connection_type_list_ID = 4)','Tedgss'),(3,'Hidden','tcpip_configuration','ID',NULL,NULL,NULL,NULL),(4,'ListDictionary','editor_table','EditorTableListType','','','SELECT  Name,Name As Value FROM   editor_table_list',NULL),(5,'Hidden','editor_table','ID',NULL,NULL,NULL,NULL),(6,'ListDictionary','editor_table','TableNameReference',NULL,NULL,'SELECT TableName,TableName AS Value FROM configuration_tables',NULL),(7,'ListDictionary','configuration_tables','TableName',NULL,NULL,'SHOW TABLES',NULL),(8,'ListDictionary','tcpip_configuration','Type','0,1','Server,Client','',NULL),(9,'ListDictionary','tcpip_configuration','TerminationString',',\\r,\\r\\n,\\0','not terminated, CR, CR + LF,null terminated','',NULL),(11,'StyleNumeric','tcpip_configuration','Port',NULL,NULL,NULL,NULL),(12,'ListDictionary','tcpip_configuration','AnalyseType_ID','1,2,3,4,5','LabManager,TGA,LIMS,Robot,RobotRemoteControl','',NULL),(13,'NotEditable','user_administration','access',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `editor_table` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:38
