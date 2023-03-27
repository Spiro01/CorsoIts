-- MySQL dump 10.19  Distrib 10.3.34-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: statistics
-- ------------------------------------------------------
-- Server version	10.3.34-MariaDB-0ubuntu0.20.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary table structure for view `COMPACT_WORKS_VIEW`
--

DROP TABLE IF EXISTS `COMPACT_WORKS_VIEW`;
/*!50001 DROP VIEW IF EXISTS `COMPACT_WORKS_VIEW`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `COMPACT_WORKS_VIEW` (
  `PROGRAM_ID` tinyint NOT NULL,
  `PROGRAM_NAME` tinyint NOT NULL,
  `PROGRAM_CODE` tinyint NOT NULL,
  `PROGRAM_STEPS_NUMBER` tinyint NOT NULL,
  `SHEET_WIDTH` tinyint NOT NULL,
  `OPERATOR_NAME` tinyint NOT NULL,
  `START` tinyint NOT NULL,
  `SCHEDULED_START` tinyint NOT NULL,
  `SCHEDULED_END` tinyint NOT NULL,
  `END` tinyint NOT NULL,
  `TIME` tinyint NOT NULL,
  `MINIMUM_TIME` tinyint NOT NULL,
  `MEDIUM_TIME` tinyint NOT NULL,
  `MACHINE_ID` tinyint NOT NULL,
  `PARTS_TO_DO` tinyint NOT NULL,
  `PARTS_DONE` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `DBINFO`
--

DROP TABLE IF EXISTS `DBINFO`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `DBINFO` (
  `ID` text DEFAULT NULL,
  `VALUE` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DBINFO`
--

LOCK TABLES `DBINFO` WRITE;
/*!40000 ALTER TABLE `DBINFO` DISABLE KEYS */;
INSERT INTO `DBINFO` VALUES ('Version','1.0');
/*!40000 ALTER TABLE `DBINFO` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MACHINE`
--

DROP TABLE IF EXISTS `MACHINE`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MACHINE` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DATE` int(11) NOT NULL,
  `OPERATOR_ID` int(11) DEFAULT NULL,
  `ON_TIME` int(11) NOT NULL DEFAULT 0,
  `PUMP_ON_TIME` int(11) NOT NULL DEFAULT 0,
  `BEND_CYCLES` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MACHINE`
--

LOCK TABLES `MACHINE` WRITE;
/*!40000 ALTER TABLE `MACHINE` DISABLE KEYS */;
INSERT INTO `MACHINE` VALUES (1,1508951486,3,480,480,0),(2,1508953978,3,0,0,0),(3,1508962347,3,1200,1200,0),(4,1525686790,3,657,0,0),(5,1525687447,3,1080,0,0),(6,1525688686,3,1200,1197,0),(7,1525703296,3,10747,10744,397),(8,1525862413,3,24462,24460,1064),(9,1525938217,3,600,597,2),(10,1525938891,3,6381,6379,238),(11,1525945429,3,27052,27049,1546),(12,1525972482,3,1031,1031,22),(13,1526021644,3,37988,37986,995),(14,1526293698,3,2880,2878,0);
/*!40000 ALTER TABLE `MACHINE` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `MACHINE_VIEW`
--

DROP TABLE IF EXISTS `MACHINE_VIEW`;
/*!50001 DROP VIEW IF EXISTS `MACHINE_VIEW`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `MACHINE_VIEW` (
  `OPERATOR` tinyint NOT NULL,
  `DATE` tinyint NOT NULL,
  `ON_TIME` tinyint NOT NULL,
  `PUMP_ON_TIME` tinyint NOT NULL,
  `MACHINE_ID` tinyint NOT NULL,
  `BEND_CYCLES` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `OPERATORS`
--

DROP TABLE IF EXISTS `OPERATORS`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OPERATORS` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` text NOT NULL,
  `PASSWORD` int(10) unsigned NOT NULL,
  `WRITING_RIGHTS` int(11) NOT NULL DEFAULT 0,
  `ACCESS_LEVEL` int(11) NOT NULL DEFAULT 0,
  `HOURLY_PAY` double NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OPERATORS`
--

LOCK TABLES `OPERATORS` WRITE;
/*!40000 ALTER TABLE `OPERATORS` DISABLE KEYS */;
INSERT INTO `OPERATORS` VALUES (1,'HARD PASSWORD',0,0,0,0),(2,'PASSWORD',0,0,0,0),(3,'ADMINISTRATOR',0,0,0,0),(4,'ARCHIVE',0,0,0,0);
/*!40000 ALTER TABLE `OPERATORS` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PROGRAMS`
--

DROP TABLE IF EXISTS `PROGRAMS`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PROGRAMS` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` text NOT NULL,
  `STEPS_NUMBER` int(11) DEFAULT NULL,
  `PARTS_TO_DO` int(11) NOT NULL,
  `SHEET_WIDTH` double DEFAULT 0,
  `PRIORITY` int(11) NOT NULL DEFAULT 100,
  `SCHEDULED_START` int(11) DEFAULT NULL,
  `SCHEDULED_END` int(11) DEFAULT NULL,
  `CODE` text DEFAULT NULL,
  `PIECE_TIME` int(11) NOT NULL DEFAULT 0,
  `SCHEDULED_TYPE` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PROGRAMS`
--

LOCK TABLES `PROGRAMS` WRITE;
/*!40000 ALTER TABLE `PROGRAMS` DISABLE KEYS */;
INSERT INTO `PROGRAMS` VALUES (3,'N060001S2142-500',2,1000000,0,100,1525689880,NULL,'',0,0),(4,'05L0448417H-1',1,4,0,0,1525704831,NULL,'PROVA',0,0),(5,'03L0448419H',1,3,0,0,1525705107,NULL,'PROVA2',0,0),(6,'NC08623',1,1,0,0,1525707932,NULL,'PROVA33',0,0),(7,'NC08615',1,152,0,0,1525709801,NULL,'PROV',0,0),(8,'STANFI90',1,271,0,0,1525862901,NULL,'PISCINE',0,0),(9,'SYWALL001',2,511,0,0,1525873843,NULL,'',0,0),(10,'IPL-951',4,150,0,0,1525946343,NULL,'JOLLY',0,0),(11,'IPL-953',1,150,0,0,1525958769,NULL,'JOLLY',0,0),(12,'STANFI90140',1,100,0,0,1525961115,NULL,'GMP',0,0),(13,'STANFI90789',1,54,0,0,1525963306,NULL,'PRO',0,0),(14,'STANFI90380',1,102,0,0,1525964482,NULL,'US PIS',0,0),(15,'IPL-957',10,151,0,0,1525966450,NULL,'ZZZZ',0,0),(16,'IPL-507',3,1000000,0,100,1526030326,NULL,'JOLLY',0,0);
/*!40000 ALTER TABLE `PROGRAMS` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `PROGRAMS_VIEW`
--

DROP TABLE IF EXISTS `PROGRAMS_VIEW`;
/*!50001 DROP VIEW IF EXISTS `PROGRAMS_VIEW`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `PROGRAMS_VIEW` (
  `PROGRAM_ID` tinyint NOT NULL,
  `PROGRAM_NAME` tinyint NOT NULL,
  `PROGRAM_CODE` tinyint NOT NULL,
  `PROGRAM_STEPS_NUMBER` tinyint NOT NULL,
  `SHEET_WIDTH` tinyint NOT NULL,
  `PRIORITY` tinyint NOT NULL,
  `SCHEDULED_START` tinyint NOT NULL,
  `SCHEDULED_END` tinyint NOT NULL,
  `PARTS_TO_DO` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `STATUS`
--

DROP TABLE IF EXISTS `STATUS`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `STATUS` (
  `START` int(11) DEFAULT 0,
  `ERROR_TEXT` text DEFAULT NULL,
  `LAST_MAINTENANCE` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `STATUS`
--

LOCK TABLES `STATUS` WRITE;
/*!40000 ALTER TABLE `STATUS` DISABLE KEYS */;
INSERT INTO `STATUS` VALUES (0,NULL,4021);
/*!40000 ALTER TABLE `STATUS` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `WORKS`
--

DROP TABLE IF EXISTS `WORKS`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `WORKS` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `MACHINE_ID` int(11) DEFAULT NULL,
  `PROGRAM_ID` int(11) DEFAULT NULL,
  `ROBOT` int(11) NOT NULL DEFAULT 0,
  `START` int(11) NOT NULL,
  `TIME` int(11) NOT NULL DEFAULT 0,
  `END` int(11) NOT NULL,
  `MINIMUM_TIME` int(11) NOT NULL DEFAULT 0,
  `PARTS_DONE` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `WORKS`
--

LOCK TABLES `WORKS` WRITE;
/*!40000 ALTER TABLE `WORKS` DISABLE KEYS */;
INSERT INTO `WORKS` VALUES (3,6,3,0,1525689880,5,1525689886,65535,0),(4,7,3,0,1525703555,190,1525703746,65535,0),(5,7,4,0,1525704853,109,1525704979,7,4),(6,7,5,0,1525705118,49,1525705173,3,3),(7,7,6,0,1525707940,1667,1525709640,3,1),(8,7,7,0,1525709815,1230,1525711103,3,152),(9,8,8,0,1525862920,5591,1525873690,7,271),(10,8,9,0,1525873843,12680,1525886876,21,392),(11,9,9,0,1525938263,25,1525938289,25,1),(12,10,9,0,1525938948,3306,1525942272,20,118),(13,11,10,0,1525946350,6986,1525953502,18,150),(14,11,11,0,1525958871,1841,1525960755,2,150),(15,11,12,0,1525961122,1806,1525962967,6,100),(16,11,13,0,1525963332,974,1525964366,5,54),(17,11,14,0,1525964488,1870,1525966385,4,102),(18,11,15,0,1525966697,5272,1525972483,48,52),(19,12,15,0,1525972611,552,1525973393,146,1),(20,13,15,0,1526021776,7658,1526029572,49,98);
/*!40000 ALTER TABLE `WORKS` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `WORKS_AND_PROGRAMS_VIEW`
--

DROP TABLE IF EXISTS `WORKS_AND_PROGRAMS_VIEW`;
/*!50001 DROP VIEW IF EXISTS `WORKS_AND_PROGRAMS_VIEW`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `WORKS_AND_PROGRAMS_VIEW` (
  `PROGRAM_ID` tinyint NOT NULL,
  `PROGRAM_NAME` tinyint NOT NULL,
  `PROGRAM_CODE` tinyint NOT NULL,
  `PROGRAM_STEPS_NUMBER` tinyint NOT NULL,
  `SHEET_WIDTH` tinyint NOT NULL,
  `SCHEDULED_START` tinyint NOT NULL,
  `SCHEDULED_END` tinyint NOT NULL,
  `PARTS_TO_DO` tinyint NOT NULL,
  `PRIORITY` tinyint NOT NULL,
  `OPERATOR_NAME` tinyint NOT NULL,
  `START` tinyint NOT NULL,
  `END` tinyint NOT NULL,
  `TIME` tinyint NOT NULL,
  `MINIMUM_TIME` tinyint NOT NULL,
  `MEDIUM_TIME` tinyint NOT NULL,
  `MACHINE_ID` tinyint NOT NULL,
  `PARTS_DONE` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Temporary table structure for view `WORKS_VIEW`
--

DROP TABLE IF EXISTS `WORKS_VIEW`;
/*!50001 DROP VIEW IF EXISTS `WORKS_VIEW`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `WORKS_VIEW` (
  `PROGRAM_ID` tinyint NOT NULL,
  `PROGRAM_NAME` tinyint NOT NULL,
  `PROGRAM_CODE` tinyint NOT NULL,
  `PROGRAM_STEPS_NUMBER` tinyint NOT NULL,
  `SHEET_WIDTH` tinyint NOT NULL,
  `OPERATOR_NAME` tinyint NOT NULL,
  `SCHEDULED_START` tinyint NOT NULL,
  `START` tinyint NOT NULL,
  `SCHEDULED_END` tinyint NOT NULL,
  `END` tinyint NOT NULL,
  `TIME` tinyint NOT NULL,
  `MINIMUM_TIME` tinyint NOT NULL,
  `MEDIUM_TIME` tinyint NOT NULL,
  `MACHINE_ID` tinyint NOT NULL,
  `PARTS_TO_DO` tinyint NOT NULL,
  `PARTS_DONE` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `COMPACT_WORKS_VIEW`
--

/*!50001 DROP TABLE IF EXISTS `COMPACT_WORKS_VIEW`*/;
/*!50001 DROP VIEW IF EXISTS `COMPACT_WORKS_VIEW`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = latin1 */;
/*!50001 SET character_set_results     = latin1 */;
/*!50001 SET collation_connection      = latin1_swedish_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `COMPACT_WORKS_VIEW` AS select `WORKS`.`PROGRAM_ID` AS `PROGRAM_ID`,`PROGRAMS`.`NAME` AS `PROGRAM_NAME`,`PROGRAMS`.`CODE` AS `PROGRAM_CODE`,`PROGRAMS`.`STEPS_NUMBER` AS `PROGRAM_STEPS_NUMBER`,`PROGRAMS`.`SHEET_WIDTH` AS `SHEET_WIDTH`,`OPERATORS`.`NAME` AS `OPERATOR_NAME`,`WORKS`.`START` AS `START`,min(`PROGRAMS`.`SCHEDULED_START`) AS `SCHEDULED_START`,`PROGRAMS`.`SCHEDULED_END` AS `SCHEDULED_END`,max(`WORKS`.`END`) AS `END`,sum(`WORKS`.`TIME`) AS `TIME`,min(`WORKS`.`MINIMUM_TIME`) AS `MINIMUM_TIME`,sum(`WORKS`.`TIME` / `WORKS`.`PARTS_DONE`) AS `MEDIUM_TIME`,`MACHINE`.`ID` AS `MACHINE_ID`,`PROGRAMS`.`PARTS_TO_DO` AS `PARTS_TO_DO`,sum(`WORKS`.`PARTS_DONE`) AS `PARTS_DONE` from (((`WORKS` join `OPERATORS`) join `MACHINE`) join `PROGRAMS`) where `PROGRAMS`.`ID` = `WORKS`.`PROGRAM_ID` and `MACHINE`.`ID` = `WORKS`.`MACHINE_ID` and `MACHINE`.`OPERATOR_ID` = `OPERATORS`.`ID` group by `WORKS`.`PROGRAM_ID` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `MACHINE_VIEW`
--

/*!50001 DROP TABLE IF EXISTS `MACHINE_VIEW`*/;
/*!50001 DROP VIEW IF EXISTS `MACHINE_VIEW`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = latin1 */;
/*!50001 SET character_set_results     = latin1 */;
/*!50001 SET collation_connection      = latin1_swedish_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `MACHINE_VIEW` AS select `OPERATORS`.`NAME` AS `OPERATOR`,`MACHINE`.`DATE` AS `DATE`,`MACHINE`.`ON_TIME` AS `ON_TIME`,`MACHINE`.`PUMP_ON_TIME` AS `PUMP_ON_TIME`,`MACHINE`.`ID` AS `MACHINE_ID`,`MACHINE`.`BEND_CYCLES` AS `BEND_CYCLES` from (`MACHINE` join `OPERATORS`) where `MACHINE`.`OPERATOR_ID` = `OPERATORS`.`ID` order by `MACHINE`.`DATE` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `PROGRAMS_VIEW`
--

/*!50001 DROP TABLE IF EXISTS `PROGRAMS_VIEW`*/;
/*!50001 DROP VIEW IF EXISTS `PROGRAMS_VIEW`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = latin1 */;
/*!50001 SET character_set_results     = latin1 */;
/*!50001 SET collation_connection      = latin1_swedish_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `PROGRAMS_VIEW` AS select `PROGRAMS`.`ID` AS `PROGRAM_ID`,`PROGRAMS`.`NAME` AS `PROGRAM_NAME`,`PROGRAMS`.`CODE` AS `PROGRAM_CODE`,`PROGRAMS`.`STEPS_NUMBER` AS `PROGRAM_STEPS_NUMBER`,`PROGRAMS`.`SHEET_WIDTH` AS `SHEET_WIDTH`,`PROGRAMS`.`PRIORITY` AS `PRIORITY`,`PROGRAMS`.`SCHEDULED_START` AS `SCHEDULED_START`,`PROGRAMS`.`SCHEDULED_END` AS `SCHEDULED_END`,`PROGRAMS`.`PARTS_TO_DO` AS `PARTS_TO_DO` from `PROGRAMS` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `WORKS_AND_PROGRAMS_VIEW`
--

/*!50001 DROP TABLE IF EXISTS `WORKS_AND_PROGRAMS_VIEW`*/;
/*!50001 DROP VIEW IF EXISTS `WORKS_AND_PROGRAMS_VIEW`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = latin1 */;
/*!50001 SET character_set_results     = latin1 */;
/*!50001 SET collation_connection      = latin1_swedish_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `WORKS_AND_PROGRAMS_VIEW` AS select `PROGRAMS_VIEW`.`PROGRAM_ID` AS `PROGRAM_ID`,`PROGRAMS_VIEW`.`PROGRAM_NAME` AS `PROGRAM_NAME`,`PROGRAMS_VIEW`.`PROGRAM_CODE` AS `PROGRAM_CODE`,`PROGRAMS_VIEW`.`PROGRAM_STEPS_NUMBER` AS `PROGRAM_STEPS_NUMBER`,`PROGRAMS_VIEW`.`SHEET_WIDTH` AS `SHEET_WIDTH`,`PROGRAMS_VIEW`.`SCHEDULED_START` AS `SCHEDULED_START`,`PROGRAMS_VIEW`.`SCHEDULED_END` AS `SCHEDULED_END`,`PROGRAMS_VIEW`.`PARTS_TO_DO` AS `PARTS_TO_DO`,`PROGRAMS_VIEW`.`PRIORITY` AS `PRIORITY`,`COMPACT_WORKS_VIEW`.`OPERATOR_NAME` AS `OPERATOR_NAME`,`COMPACT_WORKS_VIEW`.`START` AS `START`,`COMPACT_WORKS_VIEW`.`END` AS `END`,`COMPACT_WORKS_VIEW`.`TIME` AS `TIME`,`COMPACT_WORKS_VIEW`.`MINIMUM_TIME` AS `MINIMUM_TIME`,`COMPACT_WORKS_VIEW`.`MEDIUM_TIME` AS `MEDIUM_TIME`,`COMPACT_WORKS_VIEW`.`MACHINE_ID` AS `MACHINE_ID`,`COMPACT_WORKS_VIEW`.`PARTS_DONE` AS `PARTS_DONE` from (`PROGRAMS_VIEW` left join `COMPACT_WORKS_VIEW` on(`PROGRAMS_VIEW`.`PROGRAM_ID` = `COMPACT_WORKS_VIEW`.`PROGRAM_ID` and `PROGRAMS_VIEW`.`PROGRAM_NAME` = `COMPACT_WORKS_VIEW`.`PROGRAM_NAME` and `PROGRAMS_VIEW`.`PROGRAM_CODE` = `COMPACT_WORKS_VIEW`.`PROGRAM_CODE` and `PROGRAMS_VIEW`.`PROGRAM_STEPS_NUMBER` = `COMPACT_WORKS_VIEW`.`PROGRAM_STEPS_NUMBER` and `PROGRAMS_VIEW`.`SHEET_WIDTH` = `COMPACT_WORKS_VIEW`.`SHEET_WIDTH` and `PROGRAMS_VIEW`.`SCHEDULED_START` = `COMPACT_WORKS_VIEW`.`SCHEDULED_START` and `PROGRAMS_VIEW`.`PARTS_TO_DO` = `COMPACT_WORKS_VIEW`.`PARTS_TO_DO`)) order by `PROGRAMS_VIEW`.`PRIORITY`,`PROGRAMS_VIEW`.`SCHEDULED_START` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `WORKS_VIEW`
--

/*!50001 DROP TABLE IF EXISTS `WORKS_VIEW`*/;
/*!50001 DROP VIEW IF EXISTS `WORKS_VIEW`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = latin1 */;
/*!50001 SET character_set_results     = latin1 */;
/*!50001 SET collation_connection      = latin1_swedish_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `WORKS_VIEW` AS select `WORKS`.`PROGRAM_ID` AS `PROGRAM_ID`,`PROGRAMS`.`NAME` AS `PROGRAM_NAME`,`PROGRAMS`.`CODE` AS `PROGRAM_CODE`,`PROGRAMS`.`STEPS_NUMBER` AS `PROGRAM_STEPS_NUMBER`,`PROGRAMS`.`SHEET_WIDTH` AS `SHEET_WIDTH`,`OPERATORS`.`NAME` AS `OPERATOR_NAME`,`PROGRAMS`.`SCHEDULED_START` AS `SCHEDULED_START`,`WORKS`.`START` AS `START`,`PROGRAMS`.`SCHEDULED_END` AS `SCHEDULED_END`,`WORKS`.`END` AS `END`,`WORKS`.`TIME` AS `TIME`,`WORKS`.`MINIMUM_TIME` AS `MINIMUM_TIME`,`WORKS`.`TIME` / `WORKS`.`PARTS_DONE` AS `MEDIUM_TIME`,`MACHINE`.`ID` AS `MACHINE_ID`,`PROGRAMS`.`PARTS_TO_DO` AS `PARTS_TO_DO`,`WORKS`.`PARTS_DONE` AS `PARTS_DONE` from (((`WORKS` join `OPERATORS`) join `MACHINE`) join `PROGRAMS`) where `PROGRAMS`.`ID` = `WORKS`.`PROGRAM_ID` and `MACHINE`.`ID` = `WORKS`.`MACHINE_ID` and `MACHINE`.`OPERATOR_ID` = `OPERATORS`.`ID` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-24 21:40:16
