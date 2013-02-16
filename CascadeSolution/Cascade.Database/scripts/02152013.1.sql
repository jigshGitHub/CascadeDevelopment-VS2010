IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MSI_MediaTypes_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MSI_MediaTypes] DROP CONSTRAINT [DF_MSI_MediaTypes_IsActive]
END

GO

/****** Object:  Table [dbo].[MSI_MediaTypes]    Script Date: 02/15/2013 10:25:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaTypes]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaTypes]
GO

/****** Object:  Table [dbo].[MSI_MediaTypes]    Script Date: 02/15/2013 10:25:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_MediaTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MSI_MediaTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MSI_MediaTypes] ADD  CONSTRAINT [DF_MSI_MediaTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO



INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Affidavit (Issuer)');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Affidavit (Seller)');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Application');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Bill of Sale');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Charge-off Statement');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Deed of Trust');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Deficiency Balance Breakdown');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Notice to Cure');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Promissory Note');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Statement of Charges');
INSERT INTO [dbo].[MSI_MediaTypes]([Name]) VALUES('Terms and Conditions');

GO


GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MSI_MediaRequestResponse_RequestedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MSI_MediaRequestResponse] DROP CONSTRAINT [DF_MSI_MediaRequestResponse_RequestedDate]
END

GO


GO

/****** Object:  Table [dbo].[MSI_MediaRequestResponse]    Script Date: 02/15/2013 11:05:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestResponse]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaRequestResponse]
GO


GO

/****** Object:  Table [dbo].[MSI_MediaRequestResponse]    Script Date: 02/15/2013 11:05:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_MediaRequestResponse](
	[Id] [uniqueidentifier] NOT NULL,
	[AgencyId] [varchar](20) NOT NULL,
	[Account] [varchar](20) NULL,
	[OriginalAccount] [varchar](255) NULL,
	[Portfolio] [varchar](255) NULL,
	[Lender] [varchar](255) NULL,
	[SSN] [varchar](11) NULL,
	[AccountName] [varchar](100) NULL,
	[OpenDate] [datetime] NULL,
	[CODate] [datetime] NULL,
	[Seller] [varchar](255) NULL,
	[RequestedDate] [datetime] NOT NULL,
	[RespondedDate] [datetime] NULL,
	[RequestedByUserId] [uniqueidentifier] NOT NULL,
	[RespondedByUserId] [uniqueidentifier] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MSI_MediaRequestResponse] ADD  CONSTRAINT [DF_MSI_MediaRequestResponse_RequestedDate]  DEFAULT (getdate()) FOR [RequestedDate]
GO

