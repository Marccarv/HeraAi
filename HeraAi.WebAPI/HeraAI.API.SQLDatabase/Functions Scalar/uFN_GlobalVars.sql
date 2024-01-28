/*
	USAGE EXAMPLE
	SELECT [dbo].[uFN_GlobalVars]('WeighingBaseUnit');
*/

/*
-- DROP SP IF EXISTS
IF OBJECT_ID('[dbo].[uFN_GlobalVars]','FN') IS NOT NULL
   DROP FUNCTION [dbo].[uFN_GlobalVars]
GO
*/


CREATE FUNCTION [dbo].[uFN_GlobalVars]
(
	@tokenType NVARCHAR(25)
)
RETURNS NVARCHAR(40)

AS
BEGIN

	-- VARIABLES
	DECLARE @source NVARCHAR(100)				= 'HeraAI.uFN_GlobalVars';
	DECLARE @result NVARCHAR(10)				= '';
	DECLARE @error NVARCHAR(256)				= '';


	-- NULL VALIDATIONS
	IF @tokenType IS NULL 
	   SET @error = @error + 'input argument tokenType is required;';


	-- CONTENT VALIDATIONS
	IF UPPER(@tokenType) NOT IN ('ADMINUSER','BASECOUNTRY', 'BASELANGUAGE')
	   SET @error = @error + 'input argument tokenType has an unexpected content (' + @tokenType + ');'; 


	-- RETRIEVE THE NEXT TOKEN
	IF UPPER(@tokenType) = 'ADMINUSER'
	   SET @result = '000000';
	ELSE IF UPPER(@tokenType) = 'BASECOUNTRY'
	   SET @result = 'US';
	ELSE IF UPPER(@tokenType) = 'BASELANGUAGE'
	   SET @result = 'en-US';
	ELSE
		SET @error = @error + 'unexpected error'

	-- THROW ERROR IF NEEDED
	IF @error <> ''
	   SET @result = CAST(@source + ' ' + @error AS INT);


	-- END
	RETURN @result;


END
