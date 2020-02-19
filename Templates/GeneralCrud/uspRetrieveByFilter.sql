----------------------------------------------------------------------------------------------------------------
--Object Name	: [{!AREA}].[uspRetrieve{!FIELDS}ByFilter]
--Description	: {!AREA} Management, retrieve the records and the total of records
--				  to populate the table or grid of utb{!FIELDS}
--Author		: Jose Vargas
--Create Date	: {!DATE}
--Returns		: The total count and records of utb{!FIELDS} by filter
--Parameters	:
--	+ In		:
--		+ @pFilter : Contains the string to apply the filter
--		+ @pIsActive : Contains a list of states values
--		+ @pLanguageName : Contains the current culture language of a user
--		+ @pPageSize : Contains the number of records to retrieve
--		+ @pPageOffset : Contains the current page value
--	+ Out
--		+ None
----------------------------------------------------
--Change History
----------------------------------------------------
--ChangeID	Date		Author				Description
----------------------------------------------------------------------------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [{!AREA}].[uspRetrieve{!FIELDS}ByFilter]
(
	@pFilter nvarchar(50) = '',
	@pIsActive nvarchar(10) = '',
	@pLanguageName nvarchar(10),
	@pPageSize int,
	@pPageOffset int
)
AS
BEGIN

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL SNAPSHOT

BEGIN TRY
           
	DECLARE @lBaseStatusMasterId INT = [Utility].[ufnRetrieveStatusMasterId]('Base Status')

	SET @pFilter = '%' + @pFilter + '%'

	SELECT COUNT(1) AS Total 
	FROM [{!AREA}].[utb{!FIELDS}] x
	WHERE 
		(
			@pIsActive = '' OR @pIsActive = CONVERT(nvarchar(10), ISNULL(x.IsActive, 0))	
		) AND
		x.{!FIELD}Name like @pFilter
		

	;WITH cte AS (
		SELECT x.{!FIELD}Id as EntityId, x.{!FIELD}Name as Name, x.IsActive, tsd.StatusDetailName AS Status, x.IsSystemDefault
		FROM
			[{!AREA}].[utb{!FIELDS}] AS x
		JOIN [Utility].[utbStatusDetails] sd on sd.StatusDetailValue = x.IsActive
		JOIN [Utility].[utbTranslationsByStatusDetail] tsd on tsd.StatusDetailId = sd.StatusDetailId
		JOIN [Utility].[utbLanguages] l on l.LanguageId = tsd.LanguageId
		WHERE
			(
				sd.StatusMasterId = @lBaseStatusMasterId AND
				l.LanguageName = @pLanguageName
			) AND
			(
				@pIsActive = '' OR @pIsActive = CONVERT(nvarchar(10), ISNULL(x.IsActive, 0))
			) AND
			x.{!FIELD}Name like @pFilter
		ORDER BY x.IsSystemDefault desc
		OFFSET @pPageSize * @pPageOffset ROWS
		FETCH FIRST @pPageSize ROWS ONLY
	)
	
	SELECT * FROM 
	(
		SELECT * FROM ( SELECT * FROM cte WHERE cte.IsSystemDefault = 1 ORDER BY cte.Name asc OFFSET 0 ROWS ) first
		UNION ALL
		SELECT * FROM ( SELECT * FROM cte WHERE cte.IsSystemDefault = 0 ORDER BY cte.EntityId desc OFFSET 0 ROWS ) last
	) as unionized

END TRY
BEGIN CATCH

	EXEC Utility.uspErrorsTrace;
	THROW;

END CATCH

END
