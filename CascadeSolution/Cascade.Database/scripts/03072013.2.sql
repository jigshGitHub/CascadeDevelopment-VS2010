--ALTER TABLE [dbo].[MSI_Port_Acq_Original] Drop Column CreatedBy 
--ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD CreatedBy NVARCHAR(256) NULL
--ALTER TABLE [dbo].[MSI_Port_Acq_Original] Drop Column UpdatedBy 
--ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD UpdatedBy NVARCHAR(256) NULL

--ALTER TABLE [dbo].[MSI_Port_Acq_Edited] Drop Column CreatedBy 
--ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD CreatedBy NVARCHAR(256) NULL
--ALTER TABLE [dbo].[MSI_Port_Acq_Edited] Drop Column UpdatedBy 
--ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD UpdatedBy NVARCHAR(256) NULL

--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] Drop Column CreatedBy 
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD CreatedBy NVARCHAR(256) NULL
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] Drop Column UpdatedBy 
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD UpdatedBy NVARCHAR(256) NULL

--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] Drop Column CreatedBy 
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD CreatedBy NVARCHAR(256) NULL
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] Drop Column UpdatedBy 
--ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD UpdatedBy NVARCHAR(256) NULL

ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD CreatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD UpdatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD CreatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Original] ADD UpdatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD CreatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD UpdatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD CreatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_Acq_Edited] ADD UpdatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD CreatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD UpdatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD CreatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Original] ADD UpdatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD CreatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD UpdatedBy NVARCHAR(256)  NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD CreatedDate Datetime NULL
GO
ALTER TABLE [dbo].[MSI_Port_SalesTrans_Edited] ADD UpdatedDate Datetime NULL
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioPurchaseSummary]    Script Date: 03/07/2013 15:34:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioPurchaseSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioPurchaseSummary]
GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioSalesSummary]    Script Date: 03/07/2013 15:34:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioSalesSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioSalesSummary]
GO


/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioPurchaseSummary]    Script Date: 03/07/2013 15:34:18 ******/
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
	
	SELECT 
	Count(Account) #OfAccounts,PRODUCT_CODE,PortfolioOwner,Seller,PurchasePrice As CostBasis,SUM(OriginalBalance) As FaceValue,PurchaseDate
	INTO #tmpPortfolioData
	FROM vwAccounts WHERE PRODUCT_CODE = @productCode
	Group by PRODUCT_CODE,PortfolioOwner,Seller,PurchasePrice,PurchaseDate;

	Select PRODUCT_CODE AS [Portfolio#],PurchaseDate AS [Cut-OffDate],'' As [ClosingDate], '' As [Lender/FileDescription],Seller,Avg(CostBasis)As  CostBasis,SUM(FaceValue) As Face, SUM(FaceValue) * Avg(CostBasis) As [PurchasePrice],Sum(#OfAccounts) As [#ofAccts],'' As PutBackTerm ,'' As[PutbackDeadline],'' As Notes,'' As [ResaleRestrictionId],PortfolioOwner As [Company],'' AS [CreatedBy], '' As [UpdatedBy], GetDate() AS [CreatedDate], GetdAte() AS [UpdatedDate]
	From #tmpPortfolioData
	Group By PRODUCT_CODE,PortfolioOwner,Seller,PurchaseDate;
	DROP Table #tmpPortfolioData;
END



GO

/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioSalesSummary]    Script Date: 03/07/2013 15:34:18 ******/
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
	INSERT INTO MSI_Port_SalesTrans_Original(Portfolio#,[Cut-OffDate],Buyer,SalesPrice,SalesBasis,FaceValue,#ofAccts, CreatedBy, UpdatedBy,CreatedDate, UpdatedDate)
	SELECT PRODUCT_CODE, SoldDate,RESPONSIBILITY,(Sum(OriginalBalance) * AVG(SalesPrice)), AVG(SalesPrice),Sum(OriginalBalance),Count(Account), @userId, @userId,GETDATE(),GETDATE()
	FROM vwAccounts
	WHERE PRODUCT_CODE = @productCode
	Group By PRODUCT_CODE,RESPONSIBILITY,Seller,SalesPrice,SoldDate
END

SELECT [ID] ,[Portfolio#] ,[Cut-OffDate] ,[ClosingDate] ,[Lender] ,[Buyer] ,[SalesPrice] ,[SalesBasis] ,[FaceValue] ,[#ofAccts] ,[PutbackTerm(days)] As PutBackTerm ,[PutbackDeadline] ,[Notes], [CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_SalesTrans_Original]
WHERE [Portfolio#] = @productCode


GO



BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE [dbo].[TMP_MSI_Port_SalesTrans_Edited](
	[ID] [int] NOT NULL,
	[Portfolio#] [nvarchar](255) NULL,
	[Cut-OffDate] [datetime] NULL,
	[ClosingDate] [datetime] NULL,
	[Lender] [nvarchar](255) NULL,
	[Buyer] [nvarchar](255) NULL,
	[SalesPrice] [money] NULL,
	[SalesBasis] [float] NULL,
	[FaceValue] [money] NULL,
	[#ofAccts] [float] NULL,
	[PutbackTerm(days)] [float] NULL,
	[PutbackDeadline] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[UpdatedBy] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.MSI_Port_SalesTrans_Edited)
EXEC('INSERT INTO dbo.Tmp_MSI_Port_SalesTrans_Edited
SELECT * FROM dbo.MSI_Port_SalesTrans_Edited WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.MSI_Port_SalesTrans_Edited
GO
EXECUTE sp_rename N'dbo.Tmp_MSI_Port_SalesTrans_Edited', N'MSI_Port_SalesTrans_Edited', 'OBJECT'
GO
COMMIT




GO

