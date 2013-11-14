/****** Object:  Table [dbo].[MSI_Port_DistributionTrans]    Script Date: 11/14/2013 03:37:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_Port_DistributionTrans]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_Port_DistributionTrans]
GO


/****** Object:  Table [dbo].[MSI_Port_DistributionTrans]    Script Date: 11/14/2013 03:37:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MSI_Port_DistributionTrans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Portfolio#] [nvarchar](255) NULL,
	[ClosingDate] [datetime] NULL,
	[SalesPrice] [money] NULL,
	[Inv_AgencyName] [nvarchar](256) NULL,
	[IsOriginal] bit null,
	[Notes] [nvarchar](max) NULL,
	[CheckNo] [nvarchar](50) NULL,
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

/****** Object:  Table [dbo].[MSI_Port_InterestTrans]    Script Date: 11/14/2013 03:37:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_Port_InterestTrans]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_Port_InterestTrans]
GO


/****** Object:  Table [dbo].[MSI_Port_InterestTrans]    Script Date: 11/14/2013 03:37:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MSI_Port_InterestTrans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Portfolio#] [nvarchar](255) NULL,
	[ClosingDate] [datetime] NULL,
	[SalesPrice] [money] NULL,
	[Inv_AgencyName] [nvarchar](256) NULL,
	[Notes] [nvarchar](max) NULL,
	[CheckNo] [nvarchar](50) NULL,
	[IsOriginal] bit null,
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



/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioDistributionSummary]    Script Date: 11/14/2013 03:49:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioDistributionSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioDistributionSummary]
GO



/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioDistributionSummary]    Script Date: 11/14/2013 03:49:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_sp_GetPortfolioDistributionSummary](
@productCode VARCHAR(6),
@userId NVARCHAR(256),
@isOriginal bit
)
AS

If Not Exists(Select Top 1 [Portfolio#] From [MSI_Port_DistributionTrans] Where [Portfolio#] = @productCode)
BEGIN
	INSERT INTO [dbo].[MSI_Port_DistributionTrans]([Portfolio#],[ClosingDate],[SalesPrice],[Inv_AgencyName],[CheckNo],Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate])
	SELECT Portfolio#, ClosingDate,SalesPrice,Inv_AgencyName,[Check#],Notes,1,@userId, @userId,GETDATE(),GETDATE()
	FROM [dbo].Port_Trans
	WHERE [Portfolio#] = @productCode AND TransType = 'DISTRIBUTION';
END

SELECT [ID],[Portfolio#],ClosingDate,[SalesPrice],[Inv_AgencyName],[CheckNo],Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_DistributionTrans]
WHERE [Portfolio#] = @productCode AND IsOriginal = @isOriginal;


GO


/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioInterestSummary]    Script Date: 11/14/2013 03:49:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_sp_GetPortfolioInterestSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_sp_GetPortfolioInterestSummary]
GO



/****** Object:  StoredProcedure [dbo].[MSI_sp_GetPortfolioInterestSummary]    Script Date: 11/14/2013 03:49:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_sp_GetPortfolioInterestSummary](
@productCode VARCHAR(6),
@userId NVARCHAR(256),
@isOriginal bit
)
AS

If Not Exists(Select Top 1 [Portfolio#] From [MSI_Port_InterestTrans] Where [Portfolio#] = @productCode)
BEGIN
	INSERT INTO [dbo].[MSI_Port_InterestTrans]([Portfolio#],[ClosingDate],[SalesPrice],[Inv_AgencyName],[CheckNo],Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate])
	SELECT Portfolio#, ClosingDate,SalesPrice,Inv_AgencyName,[Check#],Notes,1,@userId, @userId,GETDATE(),GETDATE()
	FROM [dbo].Port_Trans
	WHERE [Portfolio#] = @productCode AND TransType = 'INTEREST';
END

SELECT [ID],[Portfolio#],ClosingDate,[SalesPrice],[Inv_AgencyName],[CheckNo],Notes,[IsOriginal],[CreatedBy],[UpdatedBy],[CreatedDate],[UpdatedDate]
FROM [dbo].[MSI_Port_InterestTrans]
WHERE [Portfolio#] = @productCode AND IsOriginal = @isOriginal;


GO