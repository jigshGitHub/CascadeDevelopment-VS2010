--Executed in DPCS on 3/20

EXEC [dbo].[aspnet_Roles_CreateRole] 'CascadeWeb','buyer';
/****** Object:  StoredProcedure [dbo].[MSI_spGetDPSViewEditRecordsExport]    Script Date: 03/19/2013 22:29:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetDPSViewEditRecordsExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetDPSViewEditRecordsExport]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetDPSViewEditRecords]    Script Date: 03/19/2013 22:29:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetDPSViewEditRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetDPSViewEditRecords]
GO


/****** Object:  StoredProcedure [dbo].[MSI_spGetDPSViewEditRecordsExport]    Script Date: 03/19/2013 22:29:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_spGetDPSViewEditRecordsExport]
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@PortfolioOwner VARCHAR(255) = NULL,
	@Responsibility VARCHAR(255) = NULL,
	@Account VARCHAR(255) = NULL,
	@GUID VARCHAR(255) = NULL
	
AS
	DECLARE @sqlQry VARCHAR(MAX);
	DECLARE @selectClause VARCHAR(1000);
	DECLARE @whereClause VARCHAR(MAX);
	DECLARE @isANDReq bit;
	
	SET @isANDReq = 0;

	--SET @selectClause = 'Select * FROM MSI_vwSearch search '
	SET @selectClause = 'Select 
	ID
      ,PIMSAcct
      ,Amount
      ,NetPayment
      ,Isnull(TransDate,'''') as TransDate
      ,CheckNumber
      ,PmtType
      ,TransSource
      ,Portfolio
      ,AcctName
      ,Isnull(DateRec,'''') as DateRec
      ,CurrentResp
      ,OriginalAcct
      ,OurCheckNumber
      ,Uploaded
      ,GUID
	  FROM MSI_DPSForm search '
	SET @whereClause = 'Where search.IsActive=1 and ';

	IF @PortfolioOwner IS NOT NULL
	BEGIN
		SET @whereClause = @whereClause + ' search.Portfolio = ''' + @PortfolioOwner + ''''
		SET @isANDReq = 1
	END
	
	IF @Account IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.PIMSAcct = ' + @Account
		SET @isANDReq = 1
	END

	IF @GUID IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.GUID = ''' + @GUID + ''''
		SET @isANDReq = 1
	END
		
	IF @Responsibility IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.CurrentResp = ''' + @Responsibility + ''''
		SET @isANDReq = 1
	END
	
	
	IF @StartDate IS NOT NULL And @EndDate IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.TransDate between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetDPSViewEditRecords]    Script Date: 03/19/2013 22:29:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_spGetDPSViewEditRecords]
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@PortfolioOwner VARCHAR(255) = NULL,
	@Responsibility VARCHAR(255) = NULL,
	@Account VARCHAR(255) = NULL,
	@GUID VARCHAR(255) = NULL
	
AS
	DECLARE @sqlQry VARCHAR(MAX);
	DECLARE @selectClause VARCHAR(1000);
	DECLARE @whereClause VARCHAR(MAX);
	DECLARE @isANDReq bit;
	
	SET @isANDReq = 0;

	--SET @selectClause = 'Select * FROM MSI_vwSearch search '
	SET @selectClause = 'Select ID ,PIMSAcct,Amount,
	Isnull(TransDate,'''') as TransDate,
	Portfolio,AcctName,
	Isnull(DateRec,'''') as DateRec, 
	OriginalAcct,GUID,CurrentResp FROM MSI_DPSForm search '
	SET @whereClause = 'Where search.IsActive=1 and ';

	IF @PortfolioOwner IS NOT NULL
	BEGIN
		SET @whereClause = @whereClause + ' search.Portfolio = ''' + @PortfolioOwner + ''''
		SET @isANDReq = 1
	END
	
	IF @Account IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.PIMSAcct = ' + @Account
		SET @isANDReq = 1
	END

	IF @GUID IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.GUID = ''' + @GUID + ''''
		SET @isANDReq = 1
	END
		
	IF @Responsibility IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.CurrentResp = ''' + @Responsibility + ''''
		SET @isANDReq = 1
	END
	
	
	IF @StartDate IS NOT NULL And @EndDate IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.TransDate between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO


