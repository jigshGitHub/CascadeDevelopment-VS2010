--Executed 03/24
EXEC [dbo].[aspnet_Roles_CreateRole] 'CascadeWeb','mediaAdmin';

/****** Object:  Table [dbo].[MSI_MessageNotification]    Script Date: 03/21/2013 23:38:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MessageNotification]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MessageNotification]
GO

/****** Object:  Table [dbo].[MSI_MessageNotification]    Script Date: 03/21/2013 23:38:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_MessageNotification](
	[Id] [uniqueidentifier] NOT NULL,
	[Body] [varchar](max) NULL,
	[Subject] [varchar](150) NULL,
	[RecipientUserId] [uniqueidentifier] NULL,
	[SentOn] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MSI_MediaRequestMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaTracker]') AND type in (N'U'))
	DELETE FROM [dbo].[MSI_MediaTracker] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestTypes]') AND type in (N'U'))
	DELETE FROM [dbo].[MSI_MediaRequestTypes]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestResponse]') AND type in (N'U'))
	DELETE FROM [dbo].[MSI_MediaRequestResponse]
GO

/****** Object:  Table [dbo].[MSI_MediaRequestStatus]    Script Date: 03/22/2013 12:26:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaRequestStatus]') AND type in (N'U'))
	DELETE FROM [dbo].[MSI_MediaRequestStatus]
GO
SET IDENTITY_INSERT [dbo].[MSI_MediaRequestStatus] ON
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (1, N'Sent to Owner')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (2, N'Request In Progress')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (3, N'Originator Researching')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (4, N'Fulfilled');--Request Fulfillment changed
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (5, N'Owner Update Requested')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (6, N'Request Complete')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (7, N'Sent to Originator')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (8, N'Originator Update Requested')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (9, N'Originator in Process')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (10, N'No Originator Media Available')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (11, N'Uploaded')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (12, N'Originator Fulfilled')
INSERT [dbo].[MSI_MediaRequestStatus] ([Id], [Name]) VALUES (13, N'Downloaded')
SET IDENTITY_INSERT [dbo].[MSI_MediaRequestStatus] OFF

