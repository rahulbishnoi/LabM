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
-- Table structure for table `machine_positions`
--

DROP TABLE IF EXISTS `machine_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `machine_positions` (
  `idmachine_positions` int(11) NOT NULL AUTO_INCREMENT,
  `PosNumber` int(11) DEFAULT NULL,
  `Machine_ID` int(11) DEFAULT NULL,
  `InternalPos` bit(1) DEFAULT b'0',
  `Description` tinytext,
  `Name` varchar(45) DEFAULT NULL,
  `Registration_Point` bit(1) DEFAULT b'0',
  `Moving_Point` bit(1) DEFAULT b'0',
  `IsRobot` bit(1) DEFAULT b'0',
  PRIMARY KEY (`idmachine_positions`)
) ENGINE=InnoDB AUTO_INCREMENT=604 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_positions`
--

LOCK TABLES `machine_positions` WRITE;
/*!40000 ALTER TABLE `machine_positions` DISABLE KEYS */;
INSERT INTO `machine_positions` VALUES (43,1,1001,'',NULL,'Pos_1','\0','\0','\0'),(44,2,1001,'',NULL,'Pos2','\0','\0','\0'),(45,3,1001,'',NULL,'Pos3','\0','\0','\0'),(46,4,1001,'',NULL,'Pos4','\0','\0','\0'),(47,5,1001,'',NULL,'Pos5','\0','\0','\0'),(48,6,1001,'',NULL,'Pos6','\0','\0','\0'),(49,7,1001,'',NULL,'Pos7','\0','\0','\0'),(50,8,1001,'',NULL,'Pos8','\0','\0','\0'),(51,9,1001,'',NULL,'Pos9','\0','\0','\0'),(52,10,1001,'',NULL,'Pos10','\0','\0','\0'),(53,101,1001,'\0','Position to register the sample','Input','\0','','\0'),(54,102,1001,'\0',NULL,'Output','\0','\0','\0'),(55,1,6,'\0','','Start dosing','\0','','\0'),(56,2,6,'\0',NULL,'Flux predosing','\0','\0','\0'),(57,4,6,'\0',NULL,'Material dosing','\0','\0','\0'),(58,5,6,'\0',NULL,'Material dosing failed','\0','','\0'),(59,6,6,'\0',NULL,'Flux fine dosing','\0','\0','\0'),(60,3,6,'\0',NULL,'Flux dosing failed','\0','','\0'),(62,7,6,'\0',NULL,'Dosing done','\0','','\0'),(79,1,1002,'',NULL,'Pos1','\0','\0','\0'),(80,2,1002,'',NULL,'Pos2','\0','\0','\0'),(81,3,1002,'',NULL,'Pos3','\0','\0','\0'),(82,4,1002,'',NULL,'Pos4','\0','\0','\0'),(83,5,1002,'',NULL,'Pos5','\0','\0','\0'),(84,6,1002,'',NULL,'Pos6','\0','\0','\0'),(85,7,1002,'',NULL,'Pos7','\0','\0','\0'),(86,8,1002,'',NULL,'Pos8','\0','\0','\0'),(87,9,1002,'',NULL,'Pos9','\0','\0','\0'),(88,10,1002,'',NULL,'Pos10','\0','\0','\0'),(89,101,1002,'\0',NULL,'Input','\0','\0','\0'),(90,102,1002,'\0',NULL,'Output','\0','\0','\0'),(92,2,1003,'',NULL,'Pos2','\0','\0','\0'),(93,3,1003,'',NULL,'Pos3','\0','\0','\0'),(94,4,1003,'',NULL,'Pos4','\0','\0','\0'),(95,5,1003,'',NULL,'Pos5','\0','\0','\0'),(96,6,1003,'',NULL,'Pos6','\0','\0','\0'),(97,7,1003,'',NULL,'Pos7','\0','\0','\0'),(98,8,1003,'',NULL,'Pos8','\0','\0','\0'),(99,9,1003,'',NULL,'Pos9','\0','\0','\0'),(100,10,1003,'',NULL,'Pos10','\0','\0','\0'),(101,101,1003,'\0',NULL,'Input','\0','\0','\0'),(102,102,1003,'\0',NULL,'Output','\0','\0','\0'),(103,1,1003,'\0',NULL,'Pos1','\0','\0','\0'),(104,2,1004,'',NULL,'Pos2','\0','\0','\0'),(105,3,1004,'',NULL,'Pos3','\0','\0','\0'),(106,4,1004,'',NULL,'Pos4','\0','\0','\0'),(107,5,1004,'',NULL,'Pos5','\0','\0','\0'),(108,6,1004,'',NULL,'Pos6','\0','\0','\0'),(109,7,1004,'',NULL,'Pos7','\0','\0','\0'),(110,8,1004,'',NULL,'Pos8','\0','\0','\0'),(111,9,1004,'',NULL,'Pos9','\0','\0','\0'),(112,10,1004,'',NULL,'Pos10','\0','\0','\0'),(113,101,1004,'\0',NULL,'Input','\0','\0','\0'),(114,102,1004,'\0',NULL,'Output','\0','\0','\0'),(115,1,1005,'',NULL,'Pos1','\0','\0','\0'),(116,2,1005,'',NULL,'Pos2','\0','\0','\0'),(117,3,1005,'',NULL,'Pos3','\0','\0','\0'),(118,4,1005,'',NULL,'Pos4','\0','\0','\0'),(119,5,1005,'',NULL,'Pos5','\0','\0','\0'),(120,6,1005,'',NULL,'Pos6','\0','\0','\0'),(121,7,1005,'',NULL,'Pos7','\0','\0','\0'),(122,8,1005,'',NULL,'Pos8','\0','\0','\0'),(123,9,1005,'',NULL,'Pos9','\0','\0','\0'),(124,10,1005,'',NULL,'Pos10','\0','\0','\0'),(125,101,1005,'\0',NULL,'Input','\0','\0','\0'),(126,102,1005,'\0',NULL,'Output','\0','\0','\0'),(127,1,1006,'\0',NULL,'Pos1','\0','\0','\0'),(128,2,1006,'',NULL,'Pos2','\0','\0','\0'),(129,3,1006,'',NULL,'Pos3','\0','\0','\0'),(130,4,1006,'',NULL,'Pos4','\0','\0','\0'),(131,5,1006,'',NULL,'Pos5','\0','\0','\0'),(132,6,1006,'',NULL,'Pos6','\0','\0','\0'),(133,7,1006,'',NULL,'Pos7','\0','\0','\0'),(134,8,1006,'',NULL,'Pos8','\0','\0','\0'),(135,9,1006,'',NULL,'Pos9','\0','\0','\0'),(136,10,1006,'',NULL,'Pos10','\0','\0','\0'),(137,11,1007,'\0',NULL,'Input','\0','\0','\0'),(138,12,1007,'\0',NULL,'Output','\0','\0','\0'),(139,11,1008,'\0',NULL,'Input','\0','','\0'),(140,12,1008,'\0',NULL,'Output','\0','\0','\0'),(141,1,2001,'\0',NULL,'TGA_Input','\0','','\0'),(142,2,2001,'\0',NULL,'TGA_Output','\0','','\0'),(151,11,1001,'',NULL,'Pos11','\0','\0','\0'),(152,12,1001,'',NULL,'Pos12','\0','\0','\0'),(153,13,1001,'',NULL,'Pos13','\0','\0','\0'),(154,14,1001,'',NULL,'Pos14','\0','\0','\0'),(155,15,1001,'',NULL,'Pos15','\0','\0','\0'),(156,16,1001,'',NULL,'Pos16','\0','\0','\0'),(157,17,1001,'',NULL,'Pos17','\0','\0','\0'),(158,18,1001,'',NULL,'Pos18','\0','\0','\0'),(159,19,1001,'',NULL,'Pos19','\0','\0','\0'),(160,20,1001,'',NULL,'Pos20','\0','\0','\0'),(161,21,1001,'',NULL,'Pos21','\0','\0','\0'),(162,22,1001,'',NULL,'Pos22','\0','\0','\0'),(163,23,1001,'',NULL,'Pos23','\0','\0','\0'),(164,24,1001,'',NULL,'Pos24','\0','\0','\0'),(165,25,1001,'',NULL,'Pos25','\0','\0','\0'),(166,26,1001,'',NULL,'Pos26','\0','\0','\0'),(167,27,1001,'',NULL,'Pos27','\0','\0','\0'),(168,28,1001,'',NULL,'Pos28','\0','\0','\0'),(169,29,1001,'',NULL,'Pos29','\0','\0','\0'),(170,30,1001,'',NULL,'Pos30','\0','\0','\0'),(171,31,1001,'',NULL,'Pos31','\0','\0','\0'),(172,32,1001,'',NULL,'Pos32','\0','\0','\0'),(173,33,1001,'',NULL,'Pos33','\0','\0','\0'),(174,34,1001,'',NULL,'Pos34','\0','\0','\0'),(175,35,1001,'',NULL,'Pos35','\0','\0','\0'),(176,36,1001,'',NULL,'Pos36','\0','\0','\0'),(177,37,1001,'',NULL,'Pos37','\0','\0','\0'),(178,38,1001,'',NULL,'Pos38','\0','\0','\0'),(179,39,1001,'',NULL,'Pos39','\0','\0','\0'),(180,40,1001,'',NULL,'Pos40','\0','\0','\0'),(181,41,1001,'',NULL,'Pos41','\0','\0','\0'),(182,42,1001,'',NULL,'Pos42','\0','\0','\0'),(183,43,1001,'',NULL,'Pos43','\0','\0','\0'),(184,44,1001,'',NULL,'Pos44','\0','\0','\0'),(185,45,1001,'',NULL,'Pos45','\0','\0','\0'),(186,46,1001,'',NULL,'Pos46','\0','\0','\0'),(187,47,1001,'',NULL,'Pos47','\0','\0','\0'),(188,48,1001,'',NULL,'Pos48','\0','\0','\0'),(189,49,1001,'',NULL,'Pos49','\0','\0','\0'),(190,50,1001,'',NULL,'Pos50','\0','\0','\0'),(191,51,1001,'',NULL,'Pos51','\0','\0','\0'),(192,52,1001,'',NULL,'Pos52','\0','\0','\0'),(193,53,1001,'',NULL,'Pos53','\0','\0','\0'),(194,54,1001,'',NULL,'Pos54','\0','\0','\0'),(195,55,1001,'',NULL,'Pos55','\0','\0','\0'),(196,56,1001,'',NULL,'Pos56','\0','\0','\0'),(197,57,1001,'',NULL,'Pos57','\0','\0','\0'),(198,58,1001,'',NULL,'Pos58','\0','\0','\0'),(199,59,1001,'',NULL,'Pos59','\0','\0','\0'),(200,60,1001,'',NULL,'Pos60','\0','\0','\0'),(201,61,1001,'',NULL,'Pos61','\0','\0','\0'),(202,62,1001,'',NULL,'Pos62','\0','\0','\0'),(203,63,1001,'',NULL,'Pos63','\0','\0','\0'),(204,64,1001,'',NULL,'Pos64','\0','\0','\0'),(205,65,1001,'',NULL,'Pos65','\0','\0','\0'),(206,66,1001,'',NULL,'Pos66','\0','\0','\0'),(207,67,1001,'',NULL,'Pos67','\0','\0','\0'),(208,68,1001,'',NULL,'Pos68','\0','\0','\0'),(209,69,1001,'',NULL,'Pos69','\0','\0','\0'),(210,70,1001,'',NULL,'Pos70','\0','\0','\0'),(211,71,1001,'',NULL,'Pos71','\0','\0','\0'),(212,72,1001,'',NULL,'Pos72','\0','\0','\0'),(213,73,1001,'',NULL,'Pos73','\0','\0','\0'),(214,74,1001,'',NULL,'Pos74','\0','\0','\0'),(215,75,1001,'',NULL,'Pos75','\0','\0','\0'),(216,76,1001,'',NULL,'Pos76','\0','\0','\0'),(217,77,1001,'',NULL,'Pos77','\0','\0','\0'),(218,78,1001,'',NULL,'Pos78','\0','\0','\0'),(219,79,1001,'',NULL,'Pos79','\0','\0','\0'),(220,80,1001,'',NULL,'Pos80','\0','\0','\0'),(221,81,1001,'',NULL,'Pos81','\0','\0','\0'),(222,82,1001,'',NULL,'Pos82','\0','\0','\0'),(223,83,1001,'',NULL,'Pos83','\0','\0','\0'),(224,84,1001,'',NULL,'Pos84','\0','\0','\0'),(225,85,1001,'',NULL,'Pos85','\0','\0','\0'),(226,86,1001,'',NULL,'Pos86','\0','\0','\0'),(227,87,1001,'',NULL,'Pos87','\0','\0','\0'),(228,88,1001,'',NULL,'Pos88','\0','\0','\0'),(229,89,1001,'',NULL,'Pos89','\0','\0','\0'),(230,90,1001,'',NULL,'Pos90','\0','\0','\0'),(231,91,1001,'',NULL,'Pos91','\0','\0','\0'),(232,92,1001,'',NULL,'Pos92','\0','\0','\0'),(233,93,1001,'',NULL,'Pos93','\0','\0','\0'),(234,94,1001,'',NULL,'Pos94','\0','\0','\0'),(235,95,1001,'',NULL,'Pos95','\0','\0','\0'),(236,96,1001,'',NULL,'Pos96','\0','\0','\0'),(237,97,1001,'',NULL,'Pos97','\0','\0','\0'),(238,98,1001,'',NULL,'Pos98','\0','\0','\0'),(239,99,1001,'',NULL,'Pos99','\0','\0','\0'),(240,100,1001,'',NULL,'Pos100','\0','\0','\0'),(244,1,1008,'',NULL,'Pos1','\0','\0','\0'),(245,2,1008,'',NULL,'Pos2','\0','\0','\0'),(246,3,1008,'',NULL,'Pos3','\0','\0','\0'),(247,4,1008,'',NULL,'Pos4','\0','\0','\0'),(248,5,1008,'',NULL,'Pos5','\0','\0','\0'),(249,6,1008,'',NULL,'Pos6','\0','\0','\0'),(250,7,1008,'',NULL,'Pos7','\0','\0','\0'),(251,8,1008,'',NULL,'Pos8','\0','\0','\0'),(252,9,1008,'',NULL,'Pos9','\0','\0','\0'),(253,10,1008,'',NULL,'Pos10','\0','\0','\0'),(254,1,1007,'',NULL,'Pos1','\0','\0','\0'),(255,2,1007,'',NULL,'Pos2','\0','\0','\0'),(256,3,1007,'',NULL,'Pos3','\0','\0','\0'),(257,4,1007,'',NULL,'Pos4','\0','\0','\0'),(258,5,1007,'',NULL,'Pos5','\0','\0','\0'),(259,6,1007,'',NULL,'Pos6','\0','\0','\0'),(260,7,1007,'',NULL,'Pos7','\0','\0','\0'),(261,8,1007,'',NULL,'Pos8','\0','\0','\0'),(262,9,1007,'',NULL,'Pos9','\0','\0','\0'),(263,10,1007,'',NULL,'Pos10','\0','\0','\0'),(264,20,1006,'\0',NULL,'Input','\0','\0','\0'),(265,21,1006,'\0',NULL,'Output','\0','\0','\0'),(267,1,5,'\0',NULL,'Input','\0','\0','\0'),(268,2,5,'\0',NULL,'Finished mixing','\0','\0','\0'),(269,1,9,'\0',NULL,'Input','\0','\0','\0'),(270,2,9,'\0',NULL,'Scan finished successfull','\0','\0','\0'),(271,3,9,'\0',NULL,'Scan finished no barcode','\0','\0','\0'),(273,11,1003,'',NULL,'Pos11','\0','\0','\0'),(274,12,1003,'',NULL,'Pos12','\0','\0','\0'),(275,13,1003,'',NULL,'Pos13','\0','\0','\0'),(276,14,1003,'',NULL,'Pos14','\0','\0','\0'),(277,15,1003,'',NULL,'Pos15','\0','\0','\0'),(278,16,1003,'',NULL,'Pos16','\0','\0','\0'),(279,17,1003,'',NULL,'Pos17','\0','\0','\0'),(280,18,1003,'',NULL,'Pos18','\0','\0','\0'),(281,19,1003,'',NULL,'Pos19','\0','\0','\0'),(282,20,1003,'',NULL,'Pos20','\0','\0','\0'),(283,21,1003,'',NULL,'Pos21','\0','\0','\0'),(284,22,1003,'',NULL,'Pos22','\0','\0','\0'),(285,23,1003,'',NULL,'Pos23','\0','\0','\0'),(286,24,1003,'',NULL,'Pos24','\0','\0','\0'),(287,25,1003,'',NULL,'Pos25','\0','\0','\0'),(288,26,1003,'',NULL,'Pos26','\0','\0','\0'),(289,27,1003,'',NULL,'Pos27','\0','\0','\0'),(290,28,1003,'',NULL,'Pos28','\0','\0','\0'),(291,29,1003,'',NULL,'Pos29','\0','\0','\0'),(292,30,1003,'',NULL,'Pos30','\0','\0','\0'),(293,31,1003,'',NULL,'Pos31','\0','\0','\0'),(294,32,1003,'',NULL,'Pos32','\0','\0','\0'),(295,33,1003,'',NULL,'Pos33','\0','\0','\0'),(296,34,1003,'',NULL,'Pos34','\0','\0','\0'),(297,35,1003,'',NULL,'Pos35','\0','\0','\0'),(298,36,1003,'',NULL,'Pos36','\0','\0','\0'),(299,37,1003,'',NULL,'Pos37','\0','\0','\0'),(300,38,1003,'',NULL,'Pos38','\0','\0','\0'),(301,39,1003,'',NULL,'Pos39','\0','\0','\0'),(305,99,2002,'\0',NULL,'InRobot','\0','\0','\0'),(306,1,2002,'\0',NULL,'Dosing1Vial','\0','',''),(307,2,2002,'\0',NULL,'Dosing2Crucible','\0','',''),(308,3,2002,'\0',NULL,'Dosing2Vial','\0','',''),(309,4,2002,'\0',NULL,'OutputTranspBelt','\0','',''),(310,5,2002,'\0',NULL,'Mixer','\0','',''),(311,6,2002,'\0',NULL,'BarcodeReader','\0','',''),(312,7,2002,'\0',NULL,'QCMagazine','\0','','\0'),(313,8,2002,'\0',NULL,'QCMagazine_2','\0','\0','\0'),(314,9,2002,'\0',NULL,'QCMagazine_3','\0','\0','\0'),(315,10,2002,'\0',NULL,'QCMagazine_4','\0','\0','\0'),(316,11,2002,'\0',NULL,'MagInputVial1','\0','','\0'),(317,12,2002,'\0',NULL,'MagInputVial1_2','\0','\0','\0'),(318,13,2002,'\0',NULL,'MagInputVial1_3','\0','\0','\0'),(319,15,2002,'\0',NULL,'MagInputVial1_5','\0','\0','\0'),(320,16,2002,'\0',NULL,'MagInputVial1_6','\0','\0','\0'),(321,17,2002,'\0',NULL,'MagInputVial1_7','\0','\0','\0'),(322,18,2002,'\0',NULL,'MagInputVial1_8','\0','\0','\0'),(323,19,2002,'\0',NULL,'MagInputVial2','\0','','\0'),(324,20,2002,'\0',NULL,'MagInputVial2_2','\0','\0','\0'),(325,21,2002,'\0',NULL,'MagInputVial2_3','\0','\0','\0'),(326,22,2002,'\0',NULL,'MagInputVial2_4','\0','\0','\0'),(327,23,2002,'\0',NULL,'MagInputVial2_5','\0','\0','\0'),(328,24,2002,'\0',NULL,'MagInputVial2_6','\0','\0','\0'),(329,25,2002,'\0',NULL,'MagInputVial2_7','\0','\0','\0'),(330,26,2002,'\0',NULL,'MagInputVial2_8','\0','\0','\0'),(331,27,2002,'\0',NULL,'TurnTableVial','\0','','\0'),(332,28,2002,'\0',NULL,'TurnTableVial_2','\0','\0','\0'),(333,29,2002,'\0',NULL,'TurnTableVial_3','\0','\0','\0'),(334,30,2002,'\0',NULL,'TurnTableVial_4','\0','\0','\0'),(335,31,2002,'\0',NULL,'Oven','\0','',''),(336,32,2002,'\0',NULL,'Cooling','\0','','\0'),(337,33,2002,'\0',NULL,'Cooling_2','\0','\0','\0'),(338,34,2002,'\0',NULL,'Cooling_3','\0','\0','\0'),(339,35,2002,'\0',NULL,'Cooling_4','\0','\0','\0'),(340,36,2002,'\0',NULL,'TurnTableTube','\0','','\0'),(341,37,2002,'\0',NULL,'TurnTableTube_2','\0','\0','\0'),(342,38,2002,'\0',NULL,'TurnTableTube_3','\0','\0','\0'),(343,39,2002,'\0',NULL,'TurnTableTube_4','\0','\0','\0'),(349,14,2002,'\0',NULL,'MagInputVial1_4','\0','\0','\0'),(350,50,6,'\0',NULL,'Input - vial','\0','','\0'),(351,201,2002,'\0',NULL,'ScanSuccessful','\0','','\0'),(352,202,2002,'\0',NULL,'ScanNotSuccessful','\0','','\0'),(353,51,6,'\0',NULL,'Input - crucible','\0','','\0'),(354,0,2002,'\0',NULL,'Dosing1Crucible','\0','',''),(355,40,1003,'',NULL,'Pos40','\0','\0','\0'),(356,41,1003,'',NULL,'Pos41','\0','\0','\0'),(357,42,1003,'',NULL,'Pos42','\0','\0','\0'),(358,43,1003,'',NULL,'Pos43','\0','\0','\0'),(359,44,1003,'',NULL,'Pos44','\0','\0','\0'),(360,45,1003,'',NULL,'Pos45','\0','\0','\0'),(361,46,1003,'',NULL,'Pos46','\0','\0','\0'),(362,47,1003,'',NULL,'Pos47','\0','\0','\0'),(363,48,1003,'',NULL,'Pos48','\0','\0','\0'),(364,49,1003,'',NULL,'Pos49','\0','\0','\0'),(365,50,1003,'',NULL,'Pos50','\0','\0','\0'),(366,51,1003,'',NULL,'Pos51','\0','\0','\0'),(367,52,1003,'',NULL,'Pos52','\0','\0','\0'),(368,53,1003,'',NULL,'Pos53','\0','\0','\0'),(369,54,1003,'',NULL,'Pos54','\0','\0','\0'),(370,55,1003,'',NULL,'Pos55','\0','\0','\0'),(371,56,1003,'',NULL,'Pos56','\0','\0','\0'),(372,57,1003,'',NULL,'Pos57','\0','\0','\0'),(373,58,1003,'',NULL,'Pos58','\0','\0','\0'),(374,59,1003,'',NULL,'Pos59','\0','\0','\0'),(375,60,1003,'',NULL,'Pos60','\0','\0','\0'),(376,61,1003,'',NULL,'Pos61','\0','\0','\0'),(377,62,1003,'',NULL,'Pos62','\0','\0','\0'),(378,63,1003,'',NULL,'Pos63','\0','\0','\0'),(379,64,1003,'',NULL,'Pos64','\0','\0','\0'),(380,65,1003,'',NULL,'Pos65','\0','\0','\0'),(381,66,1003,'',NULL,'Pos66','\0','\0','\0'),(382,67,1003,'',NULL,'Pos67','\0','\0','\0'),(383,68,1003,'',NULL,'Pos68','\0','\0','\0'),(384,69,1003,'',NULL,'Pos69','\0','\0','\0'),(385,70,1003,'',NULL,'Pos70','\0','\0','\0'),(386,71,1003,'',NULL,'Pos71','\0','\0','\0'),(387,72,1003,'',NULL,'Pos72','\0','\0','\0'),(388,73,1003,'',NULL,'Pos73','\0','\0','\0'),(389,74,1003,'',NULL,'Pos74','\0','\0','\0'),(390,75,1003,'',NULL,'Pos75','\0','\0','\0'),(391,76,1003,'',NULL,'Pos76','\0','\0','\0'),(392,77,1003,'',NULL,'Pos77','\0','\0','\0'),(393,78,1003,'',NULL,'Pos78','\0','\0','\0'),(394,79,1003,'',NULL,'Pos79','\0','\0','\0'),(395,80,1003,'',NULL,'Pos80','\0','\0','\0'),(396,81,1003,'',NULL,'Pos81','\0','\0','\0'),(397,82,1003,'',NULL,'Pos82','\0','\0','\0'),(398,83,1003,'',NULL,'Pos83','\0','\0','\0'),(399,84,1003,'',NULL,'Pos84','\0','\0','\0'),(400,85,1003,'',NULL,'Pos85','\0','\0','\0'),(401,86,1003,'',NULL,'Pos86','\0','\0','\0'),(402,87,1003,'',NULL,'Pos87','\0','\0','\0'),(403,88,1003,'',NULL,'Pos88','\0','\0','\0'),(404,89,1003,'',NULL,'Pos89','\0','\0','\0'),(405,90,1003,'',NULL,'Pos90','\0','\0','\0'),(406,91,1003,'',NULL,'Pos91','\0','\0','\0'),(407,92,1003,'',NULL,'Pos92','\0','\0','\0'),(408,93,1003,'',NULL,'Pos93','\0','\0','\0'),(409,94,1003,'',NULL,'Pos94','\0','\0','\0'),(410,95,1003,'',NULL,'Pos95','\0','\0','\0'),(411,96,1003,'',NULL,'Pos96','\0','\0','\0'),(412,97,1003,'',NULL,'Pos97','\0','\0','\0'),(413,98,1003,'',NULL,'Pos98','\0','\0','\0'),(414,99,1003,'',NULL,'Pos99','\0','\0','\0'),(415,100,1003,'',NULL,'Pos100','\0','\0','\0'),(416,11,1002,'',NULL,'Pos11','\0','\0','\0'),(417,12,1002,'',NULL,'Pos12','\0','\0','\0'),(418,13,1002,'',NULL,'Pos13','\0','\0','\0'),(419,14,1002,'',NULL,'Pos14','\0','\0','\0'),(420,15,1002,'',NULL,'Pos15','\0','\0','\0'),(421,16,1002,'',NULL,'Pos16','\0','\0','\0'),(422,17,1002,'',NULL,'Pos17','\0','\0','\0'),(423,18,1002,'',NULL,'Pos18','\0','\0','\0'),(424,19,1002,'',NULL,'Pos19','\0','\0','\0'),(425,20,1002,'',NULL,'Pos20','\0','\0','\0'),(426,21,1002,'',NULL,'Pos21','\0','\0','\0'),(427,22,1002,'',NULL,'Pos22','\0','\0','\0'),(428,23,1002,'',NULL,'Pos23','\0','\0','\0'),(429,24,1002,'',NULL,'Pos24','\0','\0','\0'),(430,25,1002,'',NULL,'Pos25','\0','\0','\0'),(431,26,1002,'',NULL,'Pos26','\0','\0','\0'),(432,27,1002,'',NULL,'Pos27','\0','\0','\0'),(433,28,1002,'',NULL,'Pos28','\0','\0','\0'),(434,29,1002,'',NULL,'Pos29','\0','\0','\0'),(435,30,1002,'',NULL,'Pos30','\0','\0','\0'),(436,31,1002,'',NULL,'Pos31','\0','\0','\0'),(437,32,1002,'',NULL,'Pos32','\0','\0','\0'),(438,33,1002,'',NULL,'Pos33','\0','\0','\0'),(439,34,1002,'',NULL,'Pos34','\0','\0','\0'),(440,35,1002,'',NULL,'Pos35','\0','\0','\0'),(441,36,1002,'',NULL,'Pos36','\0','\0','\0'),(442,37,1002,'',NULL,'Pos37','\0','\0','\0'),(443,38,1002,'',NULL,'Pos38','\0','\0','\0'),(444,39,1002,'',NULL,'Pos39','\0','\0','\0'),(445,40,1002,'',NULL,'Pos40','\0','\0','\0'),(446,41,1002,'',NULL,'Pos41','\0','\0','\0'),(447,42,1002,'',NULL,'Pos42','\0','\0','\0'),(448,43,1002,'',NULL,'Pos43','\0','\0','\0'),(449,44,1002,'',NULL,'Pos44','\0','\0','\0'),(450,45,1002,'',NULL,'Pos45','\0','\0','\0'),(451,46,1002,'',NULL,'Pos46','\0','\0','\0'),(452,47,1002,'',NULL,'Pos47','\0','\0','\0'),(453,48,1002,'',NULL,'Pos48','\0','\0','\0'),(454,49,1002,'',NULL,'Pos49','\0','\0','\0'),(455,50,1002,'',NULL,'Pos50','\0','\0','\0'),(456,51,1002,'',NULL,'Pos51','\0','\0','\0'),(457,52,1002,'',NULL,'Pos52','\0','\0','\0'),(458,53,1002,'',NULL,'Pos53','\0','\0','\0'),(459,54,1002,'',NULL,'Pos54','\0','\0','\0'),(460,55,1002,'',NULL,'Pos55','\0','\0','\0'),(461,56,1002,'',NULL,'Pos56','\0','\0','\0'),(462,57,1002,'',NULL,'Pos57','\0','\0','\0'),(463,58,1002,'',NULL,'Pos58','\0','\0','\0'),(464,59,1002,'',NULL,'Pos59','\0','\0','\0'),(465,60,1002,'',NULL,'Pos60','\0','\0','\0'),(466,61,1002,'',NULL,'Pos61','\0','\0','\0'),(467,62,1002,'',NULL,'Pos62','\0','\0','\0'),(468,63,1002,'',NULL,'Pos63','\0','\0','\0'),(469,64,1002,'',NULL,'Pos64','\0','\0','\0'),(470,65,1002,'',NULL,'Pos65','\0','\0','\0'),(471,66,1002,'',NULL,'Pos66','\0','\0','\0'),(472,67,1002,'',NULL,'Pos67','\0','\0','\0'),(473,68,1002,'',NULL,'Pos68','\0','\0','\0'),(474,69,1002,'',NULL,'Pos69','\0','\0','\0'),(475,70,1002,'',NULL,'Pos70','\0','\0','\0'),(476,71,1002,'',NULL,'Pos71','\0','\0','\0'),(477,72,1002,'',NULL,'Pos72','\0','\0','\0'),(478,73,1002,'',NULL,'Pos73','\0','\0','\0'),(479,74,1002,'',NULL,'Pos74','\0','\0','\0'),(480,75,1002,'',NULL,'Pos75','\0','\0','\0'),(481,76,1002,'',NULL,'Pos76','\0','\0','\0'),(482,77,1002,'',NULL,'Pos77','\0','\0','\0'),(483,78,1002,'',NULL,'Pos78','\0','\0','\0'),(484,79,1002,'',NULL,'Pos79','\0','\0','\0'),(485,80,1002,'',NULL,'Pos80','\0','\0','\0'),(486,81,1002,'',NULL,'Pos81','\0','\0','\0'),(487,82,1002,'',NULL,'Pos82','\0','\0','\0'),(488,83,1002,'',NULL,'Pos83','\0','\0','\0'),(489,84,1002,'',NULL,'Pos84','\0','\0','\0'),(490,85,1002,'',NULL,'Pos85','\0','\0','\0'),(491,86,1002,'',NULL,'Pos86','\0','\0','\0'),(492,87,1002,'',NULL,'Pos87','\0','\0','\0'),(493,88,1002,'',NULL,'Pos88','\0','\0','\0'),(494,89,1002,'',NULL,'Pos89','\0','\0','\0'),(495,90,1002,'',NULL,'Pos90','\0','\0','\0'),(496,91,1002,'',NULL,'Pos91','\0','\0','\0'),(497,92,1002,'',NULL,'Pos92','\0','\0','\0'),(498,93,1002,'',NULL,'Pos93','\0','\0','\0'),(499,94,1002,'',NULL,'Pos94','\0','\0','\0'),(500,95,1002,'',NULL,'Pos95','\0','\0','\0'),(501,96,1002,'',NULL,'Pos96','\0','\0','\0'),(502,97,1002,'',NULL,'Pos97','\0','\0','\0'),(503,98,1002,'',NULL,'Pos98','\0','\0','\0'),(504,99,1002,'',NULL,'Pos99','\0','\0','\0'),(505,100,1002,'',NULL,'Pos100','\0','\0','\0'),(506,11,1006,'',NULL,'Pos11','\0','\0','\0'),(507,12,1006,'',NULL,'Pos12','\0','\0','\0'),(508,13,1006,'',NULL,'Pos13','\0','\0','\0'),(509,14,1006,'',NULL,'Pos14','\0','\0','\0'),(510,15,1006,'',NULL,'Pos15','\0','\0','\0'),(511,16,1006,'',NULL,'Pos16','\0','\0','\0'),(512,90,1004,'',NULL,'Pos90','\0','\0','\0'),(513,91,1004,'',NULL,'Pos91','\0','\0','\0'),(514,92,1004,'',NULL,'Pos92','\0','\0','\0'),(515,93,1004,'',NULL,'Pos93','\0','\0','\0'),(516,94,1004,'',NULL,'Pos94','\0','\0','\0'),(517,95,1004,'',NULL,'Pos95','\0','\0','\0'),(518,96,1004,'',NULL,'Pos96','\0','\0','\0'),(519,97,1004,'',NULL,'Pos97','\0','\0','\0'),(520,98,1004,'',NULL,'Pos98','\0','\0','\0'),(521,99,1004,'',NULL,'Pos99','\0','\0','\0'),(522,100,1004,'',NULL,'Pos100','\0','\0','\0'),(523,70,1004,'',NULL,'Pos70','\0','\0','\0'),(524,71,1004,'',NULL,'Pos71','\0','\0','\0'),(525,72,1004,'',NULL,'Pos72','\0','\0','\0'),(526,73,1004,'',NULL,'Pos73','\0','\0','\0'),(527,74,1004,'',NULL,'Pos74','\0','\0','\0'),(528,75,1004,'',NULL,'Pos75','\0','\0','\0'),(529,76,1004,'',NULL,'Pos76','\0','\0','\0'),(530,77,1004,'',NULL,'Pos77','\0','\0','\0'),(531,78,1004,'',NULL,'Pos78','\0','\0','\0'),(532,79,1004,'',NULL,'Pos79','\0','\0','\0'),(533,80,1004,'',NULL,'Pos80','\0','\0','\0'),(534,81,1004,'',NULL,'Pos81','\0','\0','\0'),(535,82,1004,'',NULL,'Pos82','\0','\0','\0'),(536,83,1004,'',NULL,'Pos83','\0','\0','\0'),(537,84,1004,'',NULL,'Pos84','\0','\0','\0'),(538,85,1004,'',NULL,'Pos85','\0','\0','\0'),(539,86,1004,'',NULL,'Pos86','\0','\0','\0'),(540,87,1004,'',NULL,'Pos87','\0','\0','\0'),(541,88,1004,'',NULL,'Pos88','\0','\0','\0'),(542,89,1004,'',NULL,'Pos89','\0','\0','\0'),(543,50,1004,'',NULL,'Pos50','\0','\0','\0'),(544,51,1004,'',NULL,'Pos51','\0','\0','\0'),(545,52,1004,'',NULL,'Pos52','\0','\0','\0'),(546,53,1004,'',NULL,'Pos53','\0','\0','\0'),(547,54,1004,'',NULL,'Pos54','\0','\0','\0'),(548,55,1004,'',NULL,'Pos55','\0','\0','\0'),(549,56,1004,'',NULL,'Pos56','\0','\0','\0'),(550,57,1004,'',NULL,'Pos57','\0','\0','\0'),(551,58,1004,'',NULL,'Pos58','\0','\0','\0'),(552,59,1004,'',NULL,'Pos59','\0','\0','\0'),(553,60,1004,'',NULL,'Pos60','\0','\0','\0'),(554,61,1004,'',NULL,'Pos61','\0','\0','\0'),(555,62,1004,'',NULL,'Pos62','\0','\0','\0'),(556,63,1004,'',NULL,'Pos63','\0','\0','\0'),(557,64,1004,'',NULL,'Pos64','\0','\0','\0'),(558,65,1004,'',NULL,'Pos65','\0','\0','\0'),(559,66,1004,'',NULL,'Pos66','\0','\0','\0'),(560,67,1004,'',NULL,'Pos67','\0','\0','\0'),(561,68,1004,'',NULL,'Pos68','\0','\0','\0'),(562,69,1004,'',NULL,'Pos69','\0','\0','\0'),(563,30,1004,'',NULL,'Pos30','\0','\0','\0'),(564,31,1004,'',NULL,'Pos31','\0','\0','\0'),(565,32,1004,'',NULL,'Pos32','\0','\0','\0'),(566,33,1004,'',NULL,'Pos33','\0','\0','\0'),(567,34,1004,'',NULL,'Pos34','\0','\0','\0'),(568,35,1004,'',NULL,'Pos35','\0','\0','\0'),(569,36,1004,'',NULL,'Pos36','\0','\0','\0'),(570,37,1004,'',NULL,'Pos37','\0','\0','\0'),(571,38,1004,'',NULL,'Pos38','\0','\0','\0'),(572,39,1004,'',NULL,'Pos39','\0','\0','\0'),(573,40,1004,'',NULL,'Pos40','\0','\0','\0'),(574,41,1004,'',NULL,'Pos41','\0','\0','\0'),(575,42,1004,'',NULL,'Pos42','\0','\0','\0'),(576,43,1004,'',NULL,'Pos43','\0','\0','\0'),(577,44,1004,'',NULL,'Pos44','\0','\0','\0'),(578,45,1004,'',NULL,'Pos45','\0','\0','\0'),(579,46,1004,'',NULL,'Pos46','\0','\0','\0'),(580,47,1004,'',NULL,'Pos47','\0','\0','\0'),(581,48,1004,'',NULL,'Pos48','\0','\0','\0'),(582,49,1004,'',NULL,'Pos49','\0','\0','\0'),(583,20,1004,'',NULL,'Pos20','\0','\0','\0'),(584,21,1004,'',NULL,'Pos21','\0','\0','\0'),(585,22,1004,'',NULL,'Pos22','\0','\0','\0'),(586,23,1004,'',NULL,'Pos23','\0','\0','\0'),(587,24,1004,'',NULL,'Pos24','\0','\0','\0'),(588,25,1004,'',NULL,'Pos25','\0','\0','\0'),(589,26,1004,'',NULL,'Pos26','\0','\0','\0'),(590,27,1004,'',NULL,'Pos27','\0','\0','\0'),(591,28,1004,'',NULL,'Pos28','\0','\0','\0'),(592,29,1004,'',NULL,'Pos29','\0','\0','\0'),(593,11,1004,'',NULL,'Pos11','\0','\0','\0'),(594,12,1004,'',NULL,'Pos12','\0','\0','\0'),(595,13,1004,'',NULL,'Pos13','\0','\0','\0'),(596,14,1004,'',NULL,'Pos14','\0','\0','\0'),(597,15,1004,'',NULL,'Pos15','\0','\0','\0'),(598,16,1004,'',NULL,'Pos16','\0','\0','\0'),(599,17,1004,'',NULL,'Pos17','\0','\0','\0'),(600,18,1004,'',NULL,'Pos18','\0','\0','\0'),(601,19,1004,'',NULL,'Pos19','\0','\0','\0'),(602,100,1009,'\0',NULL,'Input','\0','\0','\0'),(603,101,1009,'\0',NULL,'Output','\0','\0','\0');
/*!40000 ALTER TABLE `machine_positions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-02-04 12:41:18