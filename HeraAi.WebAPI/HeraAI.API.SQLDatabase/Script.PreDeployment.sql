﻿-- GENERATES A NEW DATABASE IF DOES NOT EXISTS YET
IF NOT EXISTS(SELECT [Name] FROM [SYS].[DATABASES] WHERE [Name] = 'HeraAI')
BEGIN
	CREATE DATABASE HeraAI COLLATE LATIN1_GENERAL_CI_AS 
	-- RECOMENDATION: SET ALSO THIS PROPERTIES OF THIS PROJECT TO THE SAME COLLATION.
END		