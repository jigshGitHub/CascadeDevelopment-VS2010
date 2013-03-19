--Executed DBPCS 0317 2:30PM
/****** Object:  Table [dbo].[MSI_PutBackForm]    Script Date: 03/15/2013 23:15:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_PutBackForm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[OrigAcct] [varchar](255) NOT NULL,
	[AcctName] [varchar](255) NULL,
	[Portfolio] [varchar](50) NULL,
	[CurrentResp] [varchar](150) NULL,
	[PutBackReason] [varchar](255) NULL,
	[DateNoteSent] [datetime] NULL,
	[PIMSAcct] [varchar](150) NOT NULL,
	[NewStatus] [varchar](150) NULL,
	[NewResp] [varchar](150) NULL,
	[UploadedDate] [datetime] NULL,
	[CheckNumber] [varchar](50) NULL,
	[Invoice] [varchar](50) NULL,
	[Explanation] [varchar](max) NULL,
	[FaceValueofAcct] [money] NULL,
	[CostBasis] [decimal](8, 6) NULL,
	[SalesBasis] [decimal](8, 6) NULL,
	[SellerCheck] [varchar](50) NULL,
	[DateAcctClosed] [datetime] NULL,
	[AmtReceivable] [money] NULL,
	[AmtPayable] [money] NULL,
	[Seller] [varchar](150) NULL,
	[GUID] [varchar](255) NOT NULL,
	[CheckDocuments] [varchar](max) NULL,
	[UploadedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[UploadedBy] [varchar](50) NULL,
	[PutBackInitiatedBy] [varchar](50) NULL,
	[ClientName] [varchar](150) NULL,
 CONSTRAINT [PK_MSI_PutBackForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallViewEditRecords]    Script Date: 03/15/2013 21:17:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallViewEditRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallViewEditRecords]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallViewEditRecords]    Script Date: 03/15/2013 21:17:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[MSI_spGetRecallViewEditRecords]
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
	ID,Portfolio,OrigAcct
    ,AcctName ,FaceValueofAcct
    ,PIMSAcct, Isnull(DateAcctClosed,'''') as DateAcctClosed , Isnull(DateNoteSent,'''') as DateNoteSent,GUID,RecallInitiatedBy FROM MSI_RecallForm search '
	SET @whereClause = 'Where ';

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
		
		SET @whereClause = @whereClause + ' search.DateAcctClosed between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO



/****** Object:  StoredProcedure [dbo].[MSI_spGetPutBackViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetPutBackViewEditRecordsExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetPutBackViewEditRecordsExport]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetPutBackViewEditRecords]    Script Date: 03/15/2013 23:20:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetPutBackViewEditRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetPutBackViewEditRecords]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallViewEditRecordsExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallViewEditRecordsExport]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetMediaViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetMediaViewEditRecordsExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetMediaViewEditRecordsExport]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetPutBackViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[MSI_spGetPutBackViewEditRecordsExport]
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@PortfolioOwner VARCHAR(255) = NULL,
	@Responsibility VARCHAR(255) = NULL,
	@Account VARCHAR(255) = NULL,
	@GUID VARCHAR(255) = NULL
	
AS
	DECLARE @sqlQry VARCHAR(MAX);
	DECLARE @selectClause VARCHAR(1500);
	DECLARE @whereClause VARCHAR(MAX);
	DECLARE @isANDReq bit;
	
	SET @isANDReq = 0;

	--SET @selectClause = 'Select * FROM MSI_vwSearch search '
	SET @selectClause = 'Select 
	  ID
      ,Isnull(Date,'''') as Date
      ,OrigAcct
      ,AcctName
      ,Portfolio
      ,CurrentResp
      ,PutBackReason
      ,Isnull(DateNoteSent,'''') as DateNoteSent 
      ,PIMSAcct
      ,NewStatus
      ,NewResp
      ,Isnull(UploadedDate,'''') as UploadedDate 
      ,CheckNumber
      ,Invoice
      ,Explanation
      ,FaceValueofAcct
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Isnull(DateAcctClosed,'''') as DateAcctClosed
      ,AmtReceivable
      ,AmtPayable
      ,Seller
      ,GUID,
      PutBackInitiatedBy, 
      CheckDocuments
      FROM MSI_PutBackForm search '
	SET @whereClause = 'Where ';

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
		
		SET @whereClause = @whereClause + ' search.DateAcctClosed between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetPutBackViewEditRecords]    Script Date: 03/15/2013 23:20:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[MSI_spGetPutBackViewEditRecords]
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
	ID,Portfolio,OrigAcct
    ,AcctName ,FaceValueofAcct
    ,PIMSAcct, Isnull(DateAcctClosed,'''') as DateAcctClosed , Isnull(DateNoteSent,'''') as DateNoteSent,GUID,PutBackInitiatedBy, CheckDocuments FROM MSI_PutBackForm search '
	SET @whereClause = 'Where ';

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
		
		SET @whereClause = @whereClause + ' search.DateAcctClosed between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[MSI_spGetRecallViewEditRecordsExport]
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@PortfolioOwner VARCHAR(255) = NULL,
	@Responsibility VARCHAR(255) = NULL,
	@Account VARCHAR(255) = NULL,
	@GUID VARCHAR(255) = NULL
	
AS
	DECLARE @sqlQry VARCHAR(MAX);
	DECLARE @selectClause VARCHAR(1500);
	DECLARE @whereClause VARCHAR(MAX);
	DECLARE @isANDReq bit;
	
	SET @isANDReq = 0;

	--SET @selectClause = 'Select * FROM MSI_vwSearch search '
	SET @selectClause = 'Select 
	  ID
      ,Isnull(Date,'''') as Date
      ,OrigAcct
      ,AcctName
      ,Portfolio
      ,CurrentResp
      ,RecallReason
      ,Isnull(DateNoteSent,'''') as DateNoteSent 
      ,PIMSAcct
      ,NewStatus
      ,NewResp
      ,Isnull(UploadedDate,'''') as UploadedDate 
      ,CheckNumber
      ,Invoice
      ,Explanation
      ,FaceValueofAcct
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'''') as DateAcctClosed
      ,AmtReceivable
      ,AmtPayable
      ,Seller
      ,GUID,
      RecallInitiatedBy, 
      CheckDocuments
      FROM MSI_RecallForm search '
	SET @whereClause = 'Where ';

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
		
		SET @whereClause = @whereClause + ' search.DateAcctClosed between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetMediaViewEditRecordsExport]    Script Date: 03/15/2013 23:20:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_spGetMediaViewEditRecordsExport]
	@StartDate datetime = NULL,
	@EndDate datetime = NULL,
	@PortfolioOwner VARCHAR(255) = NULL,
	@Responsibility VARCHAR(255) = NULL,
	@Account VARCHAR(255) = NULL,
	@GUID VARCHAR(255) = NULL
	
AS
	DECLARE @sqlQry VARCHAR(MAX);
	DECLARE @selectClause VARCHAR(1500);
	DECLARE @whereClause VARCHAR(MAX);
	DECLARE @isANDReq bit;
	
	SET @isANDReq = 0;

	--SET @selectClause = 'Select * FROM MSI_vwSearch search '
	SET @selectClause = 'Select 
	  ID
      ,CompanyRequesting
      ,Portfolio
      ,OriginalLender
      ,OrigAcct
      ,SSN
      ,AcctName
      ,IsApplication
      ,IsAffidavitIssuer
      ,IsAffidavitSeller
      ,Isnull(StmtsFrom,'''') as StmtsFrom 
      ,Isnull(StmtsTo,'''') as StmtsTo 
      ,Isnull(OpenDate,'''') as OpenDate
      ,Isnull(OrderDate,'''') as OrderDate
      ,Isnull(CODate,'''') as CODate
      ,Explanation
      ,Isnull(DateSubmitted,'''') as DateSubmitted 
      ,Isnull(DateConfirmed,'''') as DateConfirmed  
      ,SellerFee
      ,Isnull(DateSellerPaid,'''') as DateSellerPaid  
      ,OurFee
      ,Isnull(DateReceived,'''') as DateReceived  
      ,Isnull(DateForwarded,'''') as DateForwarded  
      ,MediaTypeReceived
      ,Notes
      ,IsUnavailable
      ,IsClosed
      ,Isnull(UnavailableDate,'''') as UnavailableDate  
      ,SellerInvoice
      ,Isnull(DatePayRec,'''') as DatePayRec  
      ,BuyerCheck
      ,OurCheck
      ,PIMSAcct
      ,Seller
      ,OrderNumber
      ,OurInvoice
      ,GUID, FileName
      FROM MSI_MediaForm search '
	SET @whereClause = 'Where ';

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
		
		SET @whereClause = @whereClause + ' search.CompanyRequesting = ''' + @Responsibility + ''''
		SET @isANDReq = 1
	END
	
	
	IF @StartDate IS NOT NULL And @EndDate IS NOT NULL
	BEGIN
		IF (@isANDReq = 1)
		BEGIN
			SET @whereClause = @whereClause + ' AND '
			SET @isANDReq = 0
		ENd
		
		SET @whereClause = @whereClause + ' search.OpenDate between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''
		SET @isANDReq = 1
	END
		
	
	SET @sqlQry = @selectClause + @whereClause;

	PRINT @sqlQry;

	EXECUTE(@sqlQry);

GO


/****** Object: StoredProcedure [dbo].[MSI_spGetRecallViewEditRecords] Script Date: 03/17/2013 16:22:17 ******/

IF

EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallViewEditRecords]') AND type in (N'P', N'PC'))

DROP

PROCEDURE [dbo].[MSI_spGetRecallViewEditRecords]

GO

/****** Object: StoredProcedure [dbo].[MSI_spGetRecallViewEditRecords] Script Date: 03/17/2013 16:22:17 ******/

SET

ANSI_NULLS ON

GO

SET

QUOTED_IDENTIFIER ON

GO



CREATE

Procedure [dbo].[MSI_spGetRecallViewEditRecords]

@StartDate 
datetime = NULL,

@EndDate 
datetime = NULL,

@PortfolioOwner 
VARCHAR(255) = NULL,

@Responsibility 
VARCHAR(255) = NULL,

@Account 
VARCHAR(255) = NULL,

@GUID 
VARCHAR(255) = NULL



AS


DECLARE @sqlQry VARCHAR(MAX);


DECLARE @selectClause VARCHAR(1000);


DECLARE @whereClause VARCHAR(MAX);


DECLARE @isANDReq bit;




SET @isANDReq = 0;


--SET @selectClause = 'Select * FROM MSI_vwSearch search '


SET @selectClause = 'Select 

ID,Portfolio,OrigAcct

,AcctName ,FaceValueofAcct

,PIMSAcct, Isnull(DateAcctClosed,'''') as DateAcctClosed , Isnull(DateNoteSent,'''') as DateNoteSent,GUID,RecallInitiatedBy, CheckDocuments FROM MSI_RecallForm search '


SET @whereClause = 'Where ';


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




SET @whereClause = @whereClause + ' search.DateAcctClosed between ''' + Convert(Varchar,@StartDate,101) + ''' and ''' + Convert(Varchar,@EndDate,101) + ''''


SET @isANDReq = 1


END






SET @sqlQry = @selectClause + @whereClause;


PRINT @sqlQry;


EXECUTE(@sqlQry);

GO