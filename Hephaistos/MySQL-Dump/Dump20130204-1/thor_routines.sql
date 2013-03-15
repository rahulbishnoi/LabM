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
-- Temporary table structure for view `samplehistory`
--

DROP TABLE IF EXISTS `samplehistory`;
/*!50001 DROP VIEW IF EXISTS `samplehistory`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `samplehistory` (
  `name` varchar(303)
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `samplehistory`
--

/*!50001 DROP TABLE IF EXISTS `samplehistory`*/;
/*!50001 DROP VIEW IF EXISTS `samplehistory`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `samplehistory` AS select concat(`machines`.`Name`,_utf8' / ',`machines`.`Description`) AS `name` from `machines` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Dumping events for database 'thor'
--

--
-- Dumping routines for database 'thor'
--
/*!50003 DROP FUNCTION IF EXISTS `CheckIfAllMagazinePositionsAreOccupied` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `CheckIfAllMagazinePositionsAreOccupied`(nMagazine_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE v_nPositionsOccupied INT;
 DECLARE v_nDimensionX     INT;
 DECLARE v_nDimensionY     INT;
 DECLARE v_nMaxPos    INT;


   SELECT Count(idactive_samples) INTO v_nPositionsOccupied FROM sample_active 
   WHERE Magazine=nMagazine_ID;
   
   
   SELECT Dimension_X,Dimension_Y INTO v_nDimensionX,v_nDimensionY FROM magazine_configuration WHERE idmagazine_configuration=nMagazine_ID;
   
   SET v_nMaxPos = v_nDimensionX * v_nDimensionY;
   
   IF v_nPositionsOccupied >= v_nMaxPos THEN
            RETURN TRUE;
        ELSE
          RETURN FALSE;   
   END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `CheckIfAllOtherRoutingConditionsAreTrue` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `CheckIfAllOtherRoutingConditionsAreTrue`(nRoutingPositionEntry_ID int,  nRoutingCondition_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE v_nConditionsTrue INT;
 DECLARE v_nCountConditions     INT;
 DECLARE EXIT HANDLER FOR NOT FOUND RETURN NULL;

   SELECT Count(idrouting_conditions) INTO v_nConditionsTrue FROM routing_conditions 
   WHERE RoutingPositionEntry_ID=nRoutingPositionEntry_ID AND NOT idrouting_conditions=nRoutingCondition_ID;
   
   
   SELECT COUNT(Condition_comply) INTO v_nCountConditions FROM routing_conditions WHERE Condition_comply=1 
   AND RoutingPositionEntry_ID=nRoutingPositionEntry_ID AND NOT idrouting_conditions=nRoutingCondition_ID;
   
   IF v_nConditionsTrue > 0 THEN
       IF v_nConditionsTrue = v_nCountConditions THEN
            RETURN TRUE;
        ELSE
          RETURN FALSE;
       END IF;
   END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `CheckIfCommandForMachineAllreadyExist` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `CheckIfCommandForMachineAllreadyExist`(nMachine_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE done INT DEFAULT FALSE;
 DECLARE v_nCommands     INT;
 DECLARE v_nCommandsTotal     INT;
 DECLARE v_idcommand_active    INT;
 DECLARE curCommands CURSOR FOR SELECT idmachine_commands FROM machine_commands WHERE Machine_ID=nMachine_ID;
 DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
 
 SET v_nCommands = 0;  
 SET v_nCommandsTotal = 0;
  
  #SELECT idmachine_commands FROM machine_commands WHERE Machine_ID=9;
  
  # get all commands for the machine that was given by "nMachine_ID"
 OPEN curCommands;

read_loop: LOOP
    FETCH curCommands INTO v_idcommand_active;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
   SELECT COUNT(idcommand_active) INTO v_nCommands FROM command_active WHERE MachineCommand_ID=v_idcommand_active;
     SET v_nCommandsTotal = v_nCommandsTotal + v_nCommands;
     
  END LOOP;
  
    Close curCommands; 
    
  # SELECT COUNT(idcommand_active) INTO v_nCommands FROM command_active WHERE Machine_ID=nMachine_ID;
   
   IF v_nCommandsTotal > 0 THEN
       
            RETURN TRUE;
       
     ELSE
          RETURN FALSE;   
   END IF;
  

 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `CheckIfMachineIsSampleFree` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `CheckIfMachineIsSampleFree`(nMachine_ID int, nSample_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE done INT DEFAULT FALSE;
 DECLARE v_idPosition    INT;
 DECLARE v_nPosFound   INT;
 DECLARE v_nPosTotalFound   INT;
 DECLARE curPos CURSOR FOR SELECT idmachine_positions FROM machine_positions WHERE Machine_ID=nMachine_ID;
 DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
 
 SET v_nPosFound = 0;  
 SET v_nPosTotalFound = 0;
  
 
  # get all positions for the machine that was given by "nMachine_ID"
 OPEN curPos;

read_loop: LOOP
    FETCH curPos INTO v_idPosition;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
   SELECT COUNT(idactive_samples) INTO v_nPosFound FROM sample_active WHERE ActualSamplePosition_ID=v_idPosition;
     SET v_nPosTotalFound = v_nPosTotalFound + v_nPosFound;
     if nSample_ID >=0 THEN
         SELECT COUNT(idsample_reservation) INTO v_nPosFound FROM sample_reservation WHERE ActualSamplePosition_ID=v_idPosition AND NOT ActiveSample_ID=nSample_ID;
       ELSE 
         SELECT COUNT(idsample_reservation) INTO v_nPosFound FROM sample_reservation WHERE ActualSamplePosition_ID=v_idPosition;
       END IF;
     SET v_nPosTotalFound = v_nPosTotalFound + v_nPosFound;
     
  END LOOP;
  
    Close curPos; 
    
   
   IF v_nPosTotalFound > 0 THEN
            RETURN FALSE;
     ELSE
          RETURN TRUE;   
   END IF;
  

 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `CheckIfMagazinePositionIsReadyToOutput` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `CheckIfMagazinePositionIsReadyToOutput`(nPosition_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE done INT DEFAULT FALSE;
 
 DECLARE v_nSamples       INT;
 DECLARE v_nSamplesTotal     INT;
 DECLARE v_idsample_active    INT;
 DECLARE curSamples CURSOR FOR SELECT idactive_samples FROM sample_active WHERE ActualSamplePosition_ID=nPosition_ID;
 DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
 

 SET v_nSamplesTotal = 0;
 SET v_nSamples = 0;
 
 OPEN curSamples;

read_loop: LOOP
    FETCH curSamples INTO v_idsample_active;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
     SET v_nSamplesTotal = v_nSamplesTotal + 1;
     
  END LOOP;
  
    Close curSamples; 
    
  
   
   IF v_nSamplesTotal > 0 THEN
       
            RETURN FALSE;
       
     ELSE
          RETURN TRUE;   
   END IF;
  

 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `DeleteReservationOnSample_Active` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `DeleteReservationOnSample_Active`(nPosition_ID int) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
 DECLARE v_nPositionsfound INT;


   SELECT Count(idactive_samples) INTO v_nPositionsfound FROM sample_active WHERE ActualSamplePosition_ID = nPosition_ID AND SampleProgramType_ID=0;

   
   IF v_nPositionsfound >0 THEN
            DELETE FROM sample_active WHERE ActualSamplePosition_ID = nPosition_ID AND SampleProgramType_ID=0;
            RETURN TRUE;
        ELSE
          RETURN FALSE;   
   END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GetPreparationStep` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 FUNCTION `GetPreparationStep`(nRoutingPositionEntry_ID int , nNewRoutingPositionEntry_ID int) RETURNS int(11)
BEGIN
DECLARE done_Steps INT DEFAULT FALSE;
DECLARE n_TotalSeconds int(32);
DECLARE n_bitNumber int(11);
DECLARE t_Timestamp Timestamp;
DECLARE t_LastTimestamp Timestamp;
DECLARE t_TimeSpan int(32);
DECLARE cur_Steps CURSOR FOR SELECT ConditionList_ID, Operation_ID,ValueName,Value,Description FROM routing_conditions WHERE RoutingPositionEntry_ID=nRoutingPositionEntry_ID;
DECLARE CONTINUE HANDLER FOR NOT FOUND SET done_Steps = TRUE;

 SET n_TotalSeconds=0;

Insert INTO routing_conditions (ConditionList_ID, Operation_ID,ValueName,Value,Description,RoutingPositionEntry_ID) 
select ConditionList_ID, Operation_ID,ValueName,Value,Description,nNewRoutingPositionEntry_ID
from routing_conditions 
WHERE RoutingPositionEntry_ID=nRoutingPositionEntry_ID;

OPEN cur_Steps;

#read_loop: LOOP
    
       
 # END LOOP;
  
    Close cur_Steps; 
    
       return n_TotalSeconds;    
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AlarmMessageStatistic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `AlarmMessageStatistic`(nAlarm_Message_ID int, cState char)
BEGIN
DECLARE v_MachineName text;
DECLARE v_Type int;
DECLARE v_Timestamp timestamp;
DECLARE v_MessageNumber int;
DECLARE v_AlarmText tinytext;

SELECT MachineName,Type,Timestamp,MessageNumber,AlarmText INTO v_MachineName,v_Type,v_Timestamp,v_MessageNumber,v_AlarmText FROM alarm_message WHERE idalarm_message = nAlarm_Message_ID;
IF cState='+' THEN
  INSERT INTO alarm_message_statistic (MachineName,Type,Timestamp,MessageNumber,AlarmText,State) VALUES(v_MachineName,v_Type,v_Timestamp,v_MessageNumber,v_AlarmText,cState);
ELSE
  INSERT INTO alarm_message_statistic (MachineName,Type,Timestamp,MessageNumber,AlarmText,State) VALUES(v_MachineName,v_Type,now(),v_MessageNumber,v_AlarmText,cState);
END IF;

#INSERT INTO alarm_message_statistic (SELECT * FROM alarm_message WHERE idalarm_message = nAlarm_Message_ID);
#UPDATE alarm_message_statistic SET State=cState WHERE idalarm_message = nAlarm_Message_ID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CalledEverySecond` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `CalledEverySecond`()
BEGIN
# Call the procedure to set the magazine status values ('actual_time')

-- old
#CALL SetMagazineStatus();
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ChangeSampleIDBySample_ID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `ChangeSampleIDBySample_ID`(nSample_ID int, strNewSampleID char(255))
BEGIN

UPDATE sample_active SET SampleID=strNewSampleID WHERE idactive_samples=nSample_ID;
UPDATE sample_values SET SampleID=strNewSampleID WHERE ActiveSample_ID=nSample_ID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CopyConditionsAndCommandsByRoutingPositionEntry_ID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `CopyConditionsAndCommandsByRoutingPositionEntry_ID`(nRoutingPositionEntry_ID int , nNewRoutingPositionEntry_ID int)
BEGIN
#copy Conditions
Insert INTO routing_conditions (ConditionList_ID, Operation_ID,ValueName,Value,Description,ActualValue,SortOrder,RoutingPositionEntry_ID) 
select ConditionList_ID, Operation_ID,ValueName,Value,Description,ActualValue,SortOrder,nNewRoutingPositionEntry_ID
from routing_conditions 
WHERE RoutingPositionEntry_ID=nRoutingPositionEntry_ID;

# copy Commands
Insert INTO routing_commands (Command_ID,CommandValue0,CommandValue1,CommandValue2,CommandValue3,Description,SortOrder,RoutingPositionEntry_ID) 
select Command_ID,CommandValue0,CommandValue1,CommandValue2,CommandValue3,Description,SortOrder,nNewRoutingPositionEntry_ID
from routing_commands 
WHERE RoutingPositionEntry_ID=nRoutingPositionEntry_ID;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateChildSample` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `CreateChildSample`(nSample_ID int, strNewSampleID varchar(128), n_ActualSamplePosition_ID INT)
    READS SQL DATA
BEGIN
DECLARE done INT DEFAULT FALSE;
DECLARE n_NewSample_ID INT;
DECLARE strOldSampleID char(255);
DECLARE n_ActiveSample_ID  int(11);
DECLARE strName char(255);
DECLARE strValue char(255);
DECLARE n_SampleProgramType_ID int(11);
DECLARE n_Priority int(11);
DECLARE n_Magazine int(11);
DECLARE n_MagazinePos int(11);

# AND Name != 'Magazine' AND Name != 'Magazine_Pos'
DECLARE cur1 CURSOR FOR SELECT ActiveSample_ID,`Name`,`Value`,SampleID FROM sample_values WHERE ActiveSample_ID = nSample_ID;


DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

SELECT SampleProgramType_ID,Priority,Magazine,MagazinePos INTO n_SampleProgramType_ID,n_Priority,n_Magazine,n_MagazinePos FROM sample_active WHERE idactive_samples = nSample_ID;
INSERT INTO sample_active (SampleProgramType_ID,SampleID,ActualSamplePosition_ID,Priority,StartPosition_ID,Magazine,MagazinePos) VALUES( n_SampleProgramType_ID,strNewSampleID,n_ActualSamplePosition_ID,n_Priority,n_ActualSamplePosition_ID,n_Magazine,n_MagazinePos);

SELECT last_insert_id() INTO n_NewSample_ID;

# copy the values 
 OPEN cur1;

read_loop: LOOP
    FETCH cur1 INTO  n_ActiveSample_ID,strName,strValue,strOldSampleID;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    # fh
    IF  strName = 'MAGAZINE' Then
        INSERT INTO sample_values (ActiveSample_ID,`Name`,`Value`,SampleID) VALUES  (n_NewSample_ID,'PARENTMAGAZINE',strValue,strNewSampleID);
    ELSE IF strName = 'MAGAZINE_POS' Then
        INSERT INTO sample_values (ActiveSample_ID,`Name`,`Value`,SampleID) VALUES  (n_NewSample_ID,'PARENTMAGAZINE_POS',strValue,strNewSampleID);
    ELSE 
        INSERT INTO sample_values (ActiveSample_ID,`Name`,`Value`,SampleID) VALUES  (n_NewSample_ID,strName,strValue,strNewSampleID);
    END IF;
    END IF;
    
  END LOOP;
  
   # insert ParentID
     INSERT INTO sample_values (ActiveSample_ID,`Name`,`Value`,SampleID) VALUES  (n_NewSample_ID,"ParentID",strOldSampleID,strNewSampleID);
   
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteCommand` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteCommand`(IN nMachineNumber INT)
BEGIN
Insert INTO command_done (command_done.MachineCommand_ID,command_done.Program_ID,command_done.Sample_ID,command_done.InsertCommandTimestamp) 
select command_active.MachineCommand_ID,command_active.Program_ID,command_active.Sample_ID,command_active.InsertCommandTimestamp 
from command_active 
INNER JOIN machine_commands ca
ON (ca.idmachine_commands = command_active.MachineCommand_ID)
where machine_ID=nMachineNumber;

DELETE FROM command_active 
using machine_commands 
INNER JOIN command_active 
ON (machine_commands.idmachine_commands = command_active.MachineCommand_ID) 
where machine_commands.Machine_ID=nMachineNumber;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteMachine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteMachine`(nMachine_ID int)
BEGIN
DELETE FROM machines WHERE idmachines = nMachine_ID; 
DELETE FROM machine_commands WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_IN_OUT_Signals WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_positions WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_program_parameter_names WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_program_parameters WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_programs WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_services WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_statistic WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_status_bits WHERE Machine_ID = nMachine_ID; 
DELETE FROM machine_tags WHERE Machine_ID = nMachine_ID; 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteReservationOnSample_Active` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteReservationOnSample_Active`(nPosition_ID int)
    READS SQL DATA
BEGIN
 DECLARE v_nPositionsfound INT;


   SELECT Count(idactive_samples) INTO v_nPositionsfound FROM sample_active WHERE ActualSamplePosition_ID = nPosition_ID AND SampleProgramType_ID=0;

   
   IF v_nPositionsfound >0 THEN
        
        DELETE FROM sample_active WHERE ActualSamplePosition_ID = nPosition_ID AND SampleProgramType_ID=0;
          
   END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteReservationOnSample_Reservation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteReservationOnSample_Reservation`(nSample_ID int)
    READS SQL DATA
BEGIN


        DELETE FROM sample_reservation WHERE ActiveSample_ID = nSample_ID;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteSample` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteSample`(nSample_ID int)
    READS SQL DATA
BEGIN
DECLARE done INT DEFAULT FALSE;
DECLARE n_idsample_values int(11);
DECLARE n_ActiveSample_ID  int(11);
DECLARE strName char(255);
DECLARE strValue char(255);
DECLARE strSampleID char(255);
DECLARE num_rows  int(11);

DECLARE n_SampleProgramType_ID int(11);
DECLARE n_TimestampInserted timestamp;
DECLARE n_TimestampFinished timestamp;
DECLARE v_SampleID varchar(256);
DECLARE n_ActualSamplePosition_ID int(11);
DECLARE n_Priority int(11);
DECLARE n_StartPosition_ID int(11);
DECLARE n_Magazine int(11);
DECLARE n_MagazinePos int(11);

#DECLARE cur1 CURSOR FOR SELECT idsample_values,ActiveSample_ID,Name,Value FROM sample_values WHERE ActiveSample_ID = nSample_ID;
DECLARE cur1 CURSOR FOR SELECT ActiveSample_ID,`Name`,`Value`,SampleID FROM sample_values WHERE ActiveSample_ID = nSample_ID;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

UPDATE sample_active SET TimestampFinished=now() WHERE idactive_samples = nSample_ID;

# geht nur so einfach, da beide Tabellen exakt den gleichen Aufbau haben!

#INSERT INTO sample_done (SELECT * FROM sample_active WHERE idactive_samples = nSample_ID);

SELECT SampleProgramType_ID,TimestampInserted,TimestampFinished,SampleID,ActualSamplePosition_ID,Priority,StartPosition_ID,Magazine,MagazinePos INTO n_SampleProgramType_ID,n_TimestampInserted,n_TimestampFinished,v_SampleID,n_ActualSamplePosition_ID,n_Priority,n_StartPosition_ID,n_Magazine,n_MagazinePos FROM sample_active WHERE idactive_samples = nSample_ID;
INSERT INTO sample_done (SampleProgramType_ID,TimestampInserted,TimestampFinished,SampleID,ActualSamplePosition_ID,Priority,StartPosition_ID,Magazine,MagazinePos,idactive_samples) VALUES( n_SampleProgramType_ID,n_TimestampInserted,n_TimestampFinished,v_SampleID,n_ActualSamplePosition_ID,n_Priority,n_StartPosition_ID,n_Magazine,n_MagazinePos,nSample_ID);


DELETE FROM sample_active WHERE idactive_samples = nSample_ID; 

 # copy the values to "sample_values_done" and delete the values afterwards
 OPEN cur1;

read_loop: LOOP
    FETCH cur1 INTO  n_ActiveSample_ID,strName,strValue,strSampleID;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
    INSERT INTO sample_values_done (ActiveSample_ID,`Name`,`Value`,SampleID) VALUES  (n_ActiveSample_ID,strName,strValue,strSampleID);
    
     
  END LOOP;
  
    Close cur1; 
    
    # delete the entries from "sample_values" 
    DELETE FROM sample_values WHERE ActiveSample_ID=nSample_ID;
    
    #delete the reservations
    DELETE FROM sample_reservation WHERE ActiveSample_ID=nSample_ID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteSampleOutOfMagazine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `DeleteSampleOutOfMagazine`(IN nSample_ID int )
    READS SQL DATA
BEGIN

Delete from sample_active Where idmagazine=nSample_ID;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ForceSortOrderSampleOutOfMagazine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `ForceSortOrderSampleOutOfMagazine`(nSample_ID int, nValue int)
BEGIN

UPDATE sample_active SET MagazineOrderForce=nValue WHERE idactive_samples=nSample_ID;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertSampleInMagazine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `InsertSampleInMagazine`( IN nMagazine int, IN nSample_ID int , IN bForceFIFO bool, IN nPos int, IN nMachine_ID int, IN bPutBack bool)
    READS SQL DATA
BEGIN

DECLARE n_idmagazine int(11);
DECLARE n_LookUpPos int;
DECLARE n_FreePos  INT;
DECLARE nInsertPos INT;
DECLARE n_Machine_Position INT;

 IF bPutBack = true THEN 
     UPDATE sample_active SET MagazineDoneFlag=1 WHERE idactive_samples=nSample_ID;
   ELSE
     UPDATE sample_active SET MagazineDoneFlag=0 WHERE idactive_samples=nSample_ID;
 END IF;
   
SELECT idmachine_positions INTO n_Machine_Position FROM machine_positions WHERE machine_ID=nMachine_ID AND PosNumber=nPos;

 IF bForceFIFO = true THEN  # if FIFO get free pos and insert into the free position
  
   CALL ShiftSamplesInMagazine(nMagazine);
  # SELECT MagazinePos INTO n_FreePos FROM sample_active WHERE Magazine=nMagazine ORDER BY MagazinePos ASC;
  # SET n_FreePos = n_FreePos-1;
   
   UPDATE sample_active SET MagazinePos=1,Magazine=nMagazine,ActualSamplePosition_ID=n_Machine_Position WHERE idactive_samples=nSample_ID;
   SET nInsertPos = n_FreePos;
 
 ELSE  # insert into given pos
  
   Select Count(MagazinePos) INTO n_LookUpPos FROM sample_active Where MagazinePos=nPos AND Magazine=nMagazine;
   
    IF n_LookUpPos = 0 THEN  # if return value is 0 
       UPDATE sample_active SET MagazinePos=nPos,Magazine=nMagazine,ActualSamplePosition_ID=n_Machine_Position WHERE idactive_samples=nSample_ID;
       SET nInsertPos = nPos;
    ELSE
      SET nInsertPos = -1; # found allready sample on pos
    END IF;
    
  END IF;
  
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `MachineStateStatistics` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `MachineStateStatistics`()
BEGIN
DECLARE done INT DEFAULT FALSE;
DECLARE v_Reset     varchar(45);
DECLARE n_machine_State_Signals_ID  int(11);
DECLARE n_actual_count  int(11);
DECLARE n_actual_time  bigint(20);
DECLARE machine_state_reset CURSOR FOR SELECT reset_tag FROM `thor`.`machine_state_signals` where reset_tag is not NULL;
DECLARE machine_state_TimeCount CURSOR FOR SELECT idmachine_state_signals,actual_count,actual_time FROM `thor`.`machine_state_signals` ;



DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

 INSERT INTO machine_state_signals_statistic (name, machine_ID, signal_type,actual_time,bit_number,actual_count,signal_number) select name, machine_ID, signal_type,actual_time,bit_number,actual_count,signal_number from machine_state_signals WHERE actual_count>0 OR actual_Time>0;

OPEN machine_state_TimeCount;

count_loop: LOOP
    FETCH machine_state_TimeCount INTO n_machine_State_Signals_ID,n_actual_count,n_actual_time;
    
  IF done THEN
     LEAVE count_loop;
    END IF;
    
    UPDATE machine_state_signals SET count_since_last_reset = count_since_last_reset + n_actual_count,time_since_last_reset = time_since_last_reset + n_actual_time WHERE idmachine_state_signals=n_machine_State_Signals_ID;
    UPDATE machine_state_signals SET actual_Count = 0, actual_time = 0 WHERE idmachine_state_signals=n_machine_State_Signals_ID;
  END LOOP;
  
    Close machine_state_TimeCount; 
    
 set done = false;
 
 OPEN machine_state_reset;

read_loop: LOOP
    FETCH machine_state_reset INTO v_Reset;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
    insert into write_wincc_data (value,name,type) values('true',v_Reset,1);
    
  END LOOP;
  
    Close machine_state_reset; 
    
   
    
    
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `MoveSampleFromMagazineToActiveSamples` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `MoveSampleFromMagazineToActiveSamples`(nActiveSample_ID int, nMagazineOutputPos_ID int)
BEGIN

DECLARE nSampleProgramType_ID int(11);
DECLARE strSampleID char(128);
DECLARE nPriority int(11);
DECLARE nMagazine int(11);
DECLARE nMagazinePos int(11);
DECLARE nRobotMagazinePos int(11);
DECLARE nTimeStamp timestamp;

DECLARE nRecordFound int(11);
SET nRecordFound=-1;

SELECT  Magazine,MagazinePos,SampleID INTO nMagazine,nMagazinePos,strSampleID FROM sample_active 
   WHERE idactive_samples=nActiveSample_ID;

SELECT RobotMagazinePosition  INTO nRobotMagazinePos FROM magazine_configuration 
   WHERE idmagazine_configuration=nMagazine;
 
 IF nMagazine > 0  AND nMagazinePos>0  THEN
   
     UPDATE sample_active SET Magazine=0,MagazinePos=0,ActualSamplePosition_ID=nMagazineOutputPos_ID,MagazineDoneFlag=1 WHERE idactive_samples=nActiveSample_ID;
     
     SELECT Count(ActiveSample_ID) INTO nRecordFound FROM sample_values WHERE `Name` LIKE 'MAGAZINE_POS' AND ActiveSample_ID=nActiveSample_ID AND SampleID=strSampleID;
     
     If nRecordFound>0 THEN
        UPDATE sample_Values SET Value=nMagazinePos WHERE `Name` LIKE 'MAGAZINE_POS' AND ActiveSample_ID=nActiveSample_ID AND SampleID=strSampleID;
        UPDATE sample_Values SET Value=nRobotMagazinePos WHERE `Name` LIKE 'MAGAZINE' AND ActiveSample_ID=nActiveSample_ID AND SampleID=strSampleID;
     ELSE
       INSERT INTO sample_Values (`ActiveSample_ID`, `Name`, `Value`,`SampleID`) Values(nActiveSample_ID,'MAGAZINE_POS', nMagazinePos,strSampleID);
       INSERT INTO sample_Values (`ActiveSample_ID`, `Name`, `Value`,`SampleID`) Values(nActiveSample_ID,'MAGAZINE', nRobotMagazinePos,strSampleID);
     END IF;
 END IF;
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SetCommandActiveOnSampleActive` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SetCommandActiveOnSampleActive`(nSample_ID int)
BEGIN
 UPDATE sample_active SET Command_Active=0 WHERE idactive_samples = nSample_ID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SetMachineStateStatistic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SetMachineStateStatistic`(nMachine_ID int)
BEGIN
DECLARE done INT DEFAULT FALSE;
DECLARE v_Reset     varchar(45);
DECLARE n_machine_State_Signals_ID  int(11);
DECLARE n_actual_count  int(11);
DECLARE n_actual_time  bigint(20);
-- DECLARE machine_state_reset CURSOR FOR SELECT reset_tag FROM `thor`.`machine_state_signals` where Machine_ID=nMachine_ID AND reset_tag is not NULL;
-- DECLARE machine_state_TimeCount CURSOR FOR SELECT idmachine_state_signals,actual_count,actual_time FROM `thor`.`machine_state_signals` where Machine_ID=nMachine_ID;



-- DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

 INSERT INTO machine_state_signals_statistic (name, machine_ID, signal_type,bit_number,signal_number,actual_count, actual_time) select name, machine_ID, signal_type,bit_number,signal_number,actual_count, actual_time from machine_state_signals WHERE Machine_ID=nMachine_ID AND value=1 AND signal_number=0;

/*OPEN machine_state_TimeCount;

count_loop: LOOP
    FETCH machine_state_TimeCount INTO n_machine_State_Signals_ID,n_actual_count,n_actual_time;
    
  IF done THEN
     LEAVE count_loop;
    END IF;
    
    UPDATE machine_state_signals SET count_since_last_reset = count_since_last_reset + n_actual_count,time_since_last_reset = time_since_last_reset + n_actual_time WHERE idmachine_state_signals=n_machine_State_Signals_ID;
    UPDATE machine_state_signals SET actual_Count = 0, actual_time = 0 WHERE idmachine_state_signals=n_machine_State_Signals_ID;
  END LOOP;
  
    Close machine_state_TimeCount; 
    
 set done = false;
 
 OPEN machine_state_reset;

read_loop: LOOP
    FETCH machine_state_reset INTO v_Reset;
    
    IF done THEN
      LEAVE read_loop;
    END IF;
    
    insert into write_wincc_data (value,name,type) values('true',v_Reset,1);
    
  END LOOP;
  
    Close machine_state_reset; 
    
   */
    
    
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SetStatusBitsForMagazine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SetStatusBitsForMagazine`(nMachine_ID int,  bBit0 bool, bBit1 bool, bBit2 bool, bBit3 bool)
BEGIN
-- routine to set the 4 status bits of a magazine driver

         -- ready
         IF  (bBit0) THEN
            UPDATE machine_state_signals SET `Value`=1 WHERE Machine_ID=nMachine_ID AND Bit_number=1;     
	       ELSE
	          UPDATE machine_state_signals SET `Value`=0 WHERE Machine_ID=nMachine_ID AND Bit_number=1; 
	       END  IF;
           
        -- no sample to output (stop mode)     
        IF  (bBit1) THEN
            UPDATE machine_state_signals SET `Value`=1 WHERE Machine_ID=nMachine_ID AND Bit_number=9;     
	       ELSE
	          UPDATE machine_state_signals SET `Value`=0 WHERE Machine_ID=nMachine_ID AND Bit_number=9; 
	       END  IF;
           
        -- magazine full
        IF  (bBit2) THEN
            UPDATE machine_state_signals SET `Value`=1 WHERE Machine_ID=nMachine_ID AND Bit_number=12;     
	       ELSE
	          UPDATE machine_state_signals SET `Value`=0 WHERE Machine_ID=nMachine_ID AND Bit_number=12; 
	       END  IF;
        
        -- spare
        IF  (bBit3) THEN
            UPDATE machine_state_signals SET `Value`=1 WHERE Machine_ID=nMachine_ID AND Bit_number=13;     
	       ELSE
	          UPDATE machine_state_signals SET `Value`=0 WHERE Machine_ID=nMachine_ID AND Bit_number=13; 
	       END  IF;
           
        -- state changed so put an entry to the machine_state_signals_statistic table
        CALL SetMachineStateStatistic(nMachine_ID);
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SetStatusBitsFromStateValue` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `SetStatusBitsFromStateValue`()
BEGIN
DECLARE done_Machines INT DEFAULT FALSE;
DECLARE n_idmachines int(11);
DECLARE n_StateValue  bigint(20);
#DECLARE n_StateValue  BIGINT;
DECLARE x  INT;
DECLARE nFilter  BIGINT;


#DECLARE cur_Machines CURSOR FOR SELECT idmachines,State_Value FROM machines;
DECLARE cur_Machines CURSOR FOR SELECT machines.idmachines,machines.State_Value FROM machines INNER JOIN machine_list ON machines.Machine_list_ID = machine_list.idmachine_list WHERE machine_list.Connection_type_list_ID = 1;
DECLARE CONTINUE HANDLER FOR NOT FOUND SET done_Machines = TRUE;


 # copy the values to "Statusbits" 
 OPEN cur_Machines;

read_loop: LOOP
    FETCH cur_Machines INTO n_idmachines, n_StateValue;
    
    IF done_Machines THEN
      LEAVE read_loop;
    END IF;
    SET x = 0;
    SET nFilter = 1;
    
    WHILE x  <= 31 DO 
    
    # Bit 24 - 31
     if x < 8 THEN
         IF  (nFilter & n_StateValue) THEN
            UPDATE machine_status_bits SET `Value`=1 WHERE Machine_ID=n_idmachines AND Bit_number=x+24;     
	       ELSE
	          UPDATE machine_status_bits SET `Value`=0 WHERE Machine_ID=n_idmachines AND Bit_number=x+24; 
	       END  IF;
      END if;
      
      # Bit 16 - 23
      if x < 16 AND x >= 8 THEN
         IF  (nFilter & n_StateValue) THEN
            UPDATE machine_status_bits SET `Value`=1 WHERE Machine_ID=n_idmachines AND Bit_number=x+8;     
	       ELSE
	         UPDATE machine_status_bits SET `Value`=0 WHERE Machine_ID=n_idmachines AND Bit_number=x+8; 
	       END  IF;
      END if;
      
      # Bit 8 - 15
      if x < 24 AND x >= 16 THEN
         IF  (nFilter & n_StateValue) THEN
            UPDATE machine_status_bits SET `Value`=1 WHERE Machine_ID=n_idmachines AND Bit_number=x-8;     
	       ELSE
	          UPDATE machine_status_bits SET `Value`=0 WHERE Machine_ID=n_idmachines AND Bit_number=x-8; 
	       END  IF;
      END if;
      # Bit 0 - 7
      if x < 32 AND x >= 24 THEN
         IF  (nFilter & n_StateValue) THEN
            UPDATE machine_status_bits SET `Value`=1 WHERE Machine_ID=n_idmachines AND Bit_number=x-24;     
	       ELSE
	          UPDATE machine_status_bits SET `Value`=0 WHERE Machine_ID=n_idmachines AND Bit_number=x-24; 
	       END  IF;
      END if;
        SET  x = x + 1; 
        SET nFilter = nFilter*2;
        
    END WHILE;
   
     
  END LOOP;
  
    Close cur_Machines; 
    
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ShiftSamplesInMagazine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `ShiftSamplesInMagazine`(IN nMagazine_ID int)
    READS SQL DATA
BEGIN
DECLARE done INT DEFAULT FALSE;
DECLARE n_idactive_samples int(11);
DECLARE n_Magazine  int(11);
DECLARE n_MagazinePos int(11);
DECLARE n_Row  INT;
DECLARE nMaxPos  INT;
#, OUT nMaxPos int
DECLARE cur1 CURSOR FOR SELECT idactive_samples,Magazine,MagazinePos FROM sample_active WHERE Magazine = nMagazine_ID ORDER BY MagazinePos DESC;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
SET n_Row=0;

 
 OPEN cur1;

read_loop: LOOP
    FETCH cur1 INTO n_idactive_samples, n_Magazine,n_MagazinePos;
    
   SET n_Row =  n_Row + 1;
   
   # copy the highest number +1 into the return value "nMaxPos"
    IF  n_Row = 1 THEN
        SET nMaxPos =  n_MagazinePos +1;
    END IF;
    
    IF done THEN
      LEAVE read_loop;
    END IF;

    UPDATE sample_active SET MagazinePos=n_MagazinePos+1 WHERE idactive_samples=n_idactive_samples;

     
  END LOOP;
  
    Close cur1; 
    
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateSamplePosition` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `UpdateSamplePosition`(nMachine_ID int, nInternalPosition int,nSample_ID int)
BEGIN

DECLARE nPosition_ID int(11);
IF nMachine_ID>0 AND nSample_ID>0 AND nInternalPosition>=0 THEN
SELECT idmachine_positions INTO nPosition_ID FROM machine_positions WHERE Machine_ID=nMachine_ID AND PosNumber=nInternalPosition;
   
 UPDATE sample_Active SET `ActualSamplePosition_ID`=nPosition_ID WHERE idactive_samples=nSample_ID ;
 END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `User_Administrator_Statistics_Update` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50020 DEFINER=`root`@`localhost`*/ /*!50003 PROCEDURE `User_Administrator_Statistics_Update`(InputUserName VARCHAR(255))
BEGIN

DECLARE v_UserCommunication varchar(255);

   SELECT UserName INTO v_UserCommunication FROM communication;
   
   IF v_UserCommunication != InputUserName THEN
    INSERT INTO user_administration_statistics  (Username,Timestamp) Values (InputUserName,now());
    UPDATE communication SET UserName=InputUserName;
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

-- Dump completed on 2013-02-04 12:42:00
