ALTER TABLE [dbo].[MSI_MediaRequestResponse] ADD  CONSTRAINT [DF_MSI_MediaRequestResponse_RequestedDate]  DEFAULT (getdate()) FOR [RequestedDate]
GO


/****** Object:  Table [dbo].[MSI_MediaRequestStatus]    Script Date: 02/22/2013 11:58:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestStatus]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaRequestStatus]
GO
/****** Object:  Table [dbo].[MSI_MediaRequestStatus]    Script Date: 02/22/2013 11:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MSI_MediaRequestStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MSI_MediaRequestStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MSI_MediaRequestStatus] ON
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (1, N'Request Received')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (2, N'Request In Progress')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (3, N'Pending Research')
SET IDENTITY_INSERT [dbo].[MSI_MediaRequestStatus] OFF


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaRequestedTypes_MSI_MediaRequestResponse]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestedTypes]'))
ALTER TABLE [dbo].[MSI_MediaRequestedTypes] DROP CONSTRAINT [FK_MSI_MediaRequestedTypes_MSI_MediaRequestResponse]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaRequestedTypes_MSI_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestedTypes]'))
ALTER TABLE [dbo].[MSI_MediaRequestedTypes] DROP CONSTRAINT [FK_MSI_MediaRequestedTypes_MSI_MediaTypes]
GO

/****** Object:  Table [dbo].[MSI_MediaRequestedTypes]    Script Date: 02/16/2013 06:16:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestedTypes]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaRequestedTypes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaRequestTypes_MSI_MediaRequestResponse]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestTypes]'))
ALTER TABLE [dbo].[MSI_MediaRequestTypes] DROP CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestResponse]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaRequestTypes_MSI_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestTypes]'))
ALTER TABLE [dbo].[MSI_MediaRequestTypes] DROP CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaTypes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaRequestTypes_MSI_MediaRequestStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestTypes]'))
ALTER TABLE [dbo].[MSI_MediaRequestTypes] DROP CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestStatus]
GO


/****** Object:  Table [dbo].[MSI_MediaRequestTypes]    Script Date: 02/16/2013 06:16:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestTypes]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaRequestTypes]
GO

CREATE TABLE [dbo].[MSI_MediaRequestTypes](
	[Id] [varchar](50) NOT NULL,
	[RequestedId] [varchar](50) NOT NULL,
	[TypeId] [int] NOT NULL,
	[RespondedDocuments] [varchar](max) NULL,
	[RequestedDate] [datetime] NULL,
	[RequestedUserID] [uniqueidentifier] NULL,
	[RespondedUserID] [uniqueidentifier] NULL,
	[RespondedDate] [datetime] NULL,
	[MediaDownloaded] [bit] NULL,
	[MediaUploaded] [bit] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[LastUpdatedBy] [uniqueidentifier] NULL,
	[RequestStatusId] [int] NULL,
	[ReSubmittedDate] [datetime] NULL,
	[ReSubmittedBy] [uniqueidentifier] NULL,
	[TypeConstraints] [varchar](1000) NULL,
 CONSTRAINT [PK_MSI_MediaRequestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes]  WITH CHECK ADD  CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestResponse] FOREIGN KEY([RequestedId])
REFERENCES [dbo].[MSI_MediaRequestResponse] ([Id])
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes] CHECK CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestResponse]
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes]  WITH CHECK ADD  CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[MSI_MediaTypes] ([ID])
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes] CHECK CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaTypes]
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes]  WITH CHECK ADD  CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestStatus] FOREIGN KEY([RequestStatusId])
REFERENCES [dbo].[MSI_MediaRequestStatus] ([ID])
GO

ALTER TABLE [dbo].[MSI_MediaRequestTypes] CHECK CONSTRAINT [FK_MSI_MediaRequestTypes_MSI_MediaRequestStatus]
GO

Update MSI_MediaTypes Set Name = 'Card Holder Agreement' WHERE ID = 10;
Update MSI_MediaTypes Set Name = 'Notice of Intent to Sell' WHERE ID = 9;
Update MSI_MediaTypes Set Name = 'Balance Deficiency Letter' WHERE ID = 8;
Update MSI_MediaTypes Set Name = 'Right to Cure' WHERE ID = 7;
Update MSI_MediaTypes Set Name = 'Charge off Statement' WHERE ID = 6;
Update MSI_MediaTypes Set Name = 'Statement' WHERE ID = 5;
Update MSI_MediaTypes Set Name = 'Contract' WHERE ID = 4;
