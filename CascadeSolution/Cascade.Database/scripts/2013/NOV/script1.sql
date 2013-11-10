/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioPurchaseSummary]    Script Date: 11/06/2013 00:26:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioPurchaseSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioPurchaseSummary]
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioPurchaseSummary]    Script Date: 11/06/2013 00:26:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_sp_GetPortfolioPurchaseSummary](
@productCode VARCHAR(6)
)
AS

If Exists(Select [Portfolio#] From [MSI_Port_Acq_Original] Where [Portfolio#] = @productCode)
BEGIN
	Select [Portfolio#],[Cut-OffDate],[ClosingDate] ,[Lender/FileDescription] ,[Seller] ,[CostBasis] ,[Face] ,[PurchasePrice] ,[#ofAccts] ,[PutbackTerm (days)] As PutBackTerm ,[PutbackDeadline] ,[Notes] ,[ResaleRestrictionId] ,[Company],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
	FROM [CascadeDB].[dbo].[MSI_Port_Acq_Original]
	Where [Portfolio#] = @productCode;
END
ELSE
BEGIN
	
	--SELECT 
	--Count(Account) #OfAccounts,PRODUCT_CODE,PortfolioOwner,Seller,PurchasePrice As CostBasis,SUM(OriginalBalance) As FaceValue,PurchaseDate
	--INTO #tmpPortfolioData
	--FROM vwAccounts WHERE PRODUCT_CODE = @productCode
	--Group by PRODUCT_CODE,PortfolioOwner,Seller,PurchasePrice,PurchaseDate;

	--Select PRODUCT_CODE AS [Portfolio#],PurchaseDate AS [Cut-OffDate],'' As [ClosingDate], '' As [Lender/FileDescription],Seller,Avg(CostBasis)As  CostBasis,SUM(FaceValue) As Face, SUM(FaceValue) * Avg(CostBasis) As [PurchasePrice],Sum(#OfAccounts) As [#ofAccts],'' As PutBackTerm ,'' As[PutbackDeadline],'' As Notes,'' As [ResaleRestrictionId],PortfolioOwner As [Company],'' AS [CreatedBy], '' As [UpdatedBy], GetDate() AS [CreatedDate], GetdAte() AS [UpdatedDate]
	--From #tmpPortfolioData
	--Group By PRODUCT_CODE,PortfolioOwner,Seller,PurchaseDate;
	--DROP Table #tmpPortfolioData;
	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT  [Portfolio#],[Cut-OffDate] ,[ClosingDate] ,[Lender/FileDescription] ,[Seller] ,[CostBasis] ,[Face] ,[PurchasePrice] ,[#ofAccts] ,[PutbackTerm (days)]  AS PutBackTerm,[PutbackDeadline] ,[Notes] ,[Company],'' As [ResaleRestrictionId],'' AS [CreatedBy], '' As [UpdatedBy], GetDate() AS [CreatedDate], GetdAte() AS [UpdatedDate]
	FROM [CascadeDB].[dbo].[Port_Acq]
	 WHERE Portfolio#=@productCode;
END

GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioSalesSummary]    Script Date: 11/06/2013 00:26:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioSalesSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioSalesSummary]
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioSalesSummary]    Script Date: 11/06/2013 00:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_sp_GetPortfolioSalesSummary](
@productCode VARCHAR(6),
@userId NVARCHAR(256)
)
AS

If Not Exists(Select Top 1 [Portfolio#] From [MSI_Port_SalesTrans_Original] Where [Portfolio#] = @productCode)
BEGIN
	INSERT INTO MSI_Port_SalesTrans_Original(Portfolio#,[Cut-OffDate],Buyer,SalesPrice,SalesBasis,FaceValue,#ofAccts, [PutbackTerm(days)],PutbackDeadline,Notes,CreatedBy, UpdatedBy,CreatedDate, UpdatedDate)
	--SELECT PRODUCT_CODE, SoldDate,RESPONSIBILITY,(Sum(OriginalBalance) * AVG(SalesPrice)), AVG(SalesPrice),Sum(OriginalBalance),Count(Account), 
	--FROM vwAccounts
	--WHERE PRODUCT_CODE = @productCode
	--Group By PRODUCT_CODE,RESPONSIBILITY,Seller,SalesPrice,SoldDate
	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT  [Portfolio#] ,[Cut-OffDate] ,[Buyer],[SalesPrice],[SalesBasis] ,[FaceValue] ,[#ofAccts],[PutbackTerm(days)],[PutbackDeadline],Notes ,@userId, @userId,GETDATE(),GETDATE()
	FROM [CascadeDB].[dbo].[Port_Trans]
	WHERE [Portfolio#] = @productCode AND TransType = 'SALE';
END

SELECT [ID] ,[Portfolio#] ,[Cut-OffDate] ,[ClosingDate] ,[Lender] ,[Buyer] ,[SalesPrice] ,[SalesBasis] ,[FaceValue] ,[#ofAccts] ,[PutbackTerm(days)] As PutBackTerm ,[PutbackDeadline] ,[Notes], [CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_SalesTrans_Original]
WHERE [Portfolio#] = @productCode



GO


/****** Object:  Table [dbo].[MSI_Port_CollectionsTrans]    Script Date: 11/06/2013 00:32:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_Port_CollectionsTrans]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_Port_CollectionsTrans]
GO


/****** Object:  Table [dbo].[MSI_Port_CollectionsTrans]    Script Date: 11/06/2013 00:32:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MSI_Port_CollectionsTrans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Portfolio#] [nvarchar](255) NULL,
	[ClosingDate] [datetime] NULL,
	[SalesPrice] [money] NULL,
	[FaceValue] [money] NULL,
	[Inv_AgencyName] [nvarchar](255) NULL,
	[TransType] [nvarchar](50) NULL,
	[IsOriginal] bit,
	[CreatedBy] [nvarchar](256) NULL,
	[UpdatedBy] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioCollectionsSummary]    Script Date: 11/06/2013 00:26:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioCollectionsSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioCollectionsSummary]
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioCollectionsSummary]    Script Date: 11/06/2013 00:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_sp_GetPortfolioCollectionsSummary](
@productCode VARCHAR(6),
@userId NVARCHAR(256),
@isOriginal bit
)
AS

If Not Exists(Select Top 1 [Portfolio#] From [MSI_Port_CollectionsTrans] Where [Portfolio#] = @productCode)
BEGIN
	INSERT INTO [dbo].[MSI_Port_CollectionsTrans]([Portfolio#],[ClosingDate],[SalesPrice],[FaceValue],[Inv_AgencyName],[TransType],[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate])
	SELECT  [Portfolio#] ,[ClosingDate],[SalesPrice],[FaceValue],[Inv_AgencyName],[TransType],1,@userId, @userId,GETDATE(),GETDATE()
	FROM [dbo].[Port_Trans]
	WHERE [Portfolio#] = @productCode AND TransType = 'COLLECTION';
END

SELECT [ID],[Portfolio#],[ClosingDate],[SalesPrice],[FaceValue],[Inv_AgencyName],[TransType],[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_CollectionsTrans]
WHERE [Portfolio#] = @productCode AND IsOriginal = @isOriginal;

GO

/****** Object:  Table [dbo].[MSI_Port_InvestmentsTrans]    Script Date: 11/06/2013 00:32:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_Port_InvestmentsTrans]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_Port_InvestmentsTrans]
GO


/****** Object:  Table [dbo].[MSI_Port_InvestmentsTrans]    Script Date: 11/06/2013 00:32:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MSI_Port_InvestmentsTrans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Portfolio#] [nvarchar](255) NULL,
	[SalesPrice] [money] NULL,
	[ProfitShare_before] [float] NULL,
	[ProfitShare_after] [float] NULL,
	[Interest] [float] NULL,
	[Inv_AgencyName] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[TransType] [nvarchar](50) NULL,
	[IsOriginal] bit,
	[CreatedBy] [nvarchar](256) NULL,
	[UpdatedBy] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioInvestmentsSummary]    Script Date: 11/06/2013 00:26:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioInvestmentsSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioInvestmentsSummary]
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioInvestmentsSummary]    Script Date: 11/06/2013 00:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_sp_GetPortfolioInvestmentsSummary](
@productCode VARCHAR(6),
@userId NVARCHAR(256),
@isOriginal bit
)
AS

If Not Exists(Select Top 1 [Portfolio#] From [MSI_Port_InvestmentsTrans] Where [Portfolio#] = @productCode)
BEGIN
	INSERT INTO [dbo].[MSI_Port_InvestmentsTrans]([Portfolio#],[SalesPrice],[Inv_AgencyName],[Interest],TransType,ProfitShare_before,ProfitShare_after,Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate])
	SELECT Portfolio#, SalesPrice,Inv_AgencyName,[$Interest],TransType,ProfitShare_after,ProfitShare_before,Notes,1,@userId, @userId,GETDATE(),GETDATE()
	FROM [dbo].Port_Trans
	WHERE [Portfolio#] = @productCode AND TransType = 'INVESTMENT';
END

SELECT [ID],[Portfolio#],[SalesPrice],[Inv_AgencyName],[Interest],ProfitShare_before,ProfitShare_after,TransType,Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_InvestmentsTrans]
WHERE [Portfolio#] = @productCode AND IsOriginal = @isOriginal;

GO