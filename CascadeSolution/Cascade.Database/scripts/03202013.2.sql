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

SET ANSI_PADDING OFF
GO


Update MSI_MediaRequestStatus SET Name = 'Sent to Owner' Where Id = 1;
Update MSI_MediaRequestStatus SET Name = 'Originator Researching' Where Id = 3;
Update MSI_MediaRequestStatus SET Name = 'Owner Update Requested' Where Id = 5;
Insert INTO MSI_MediaRequestStatus (Name) Values('Sent to Originator');
Insert INTO MSI_MediaRequestStatus (Name) Values('Originator Update Requested');
Insert INTO MSI_MediaRequestStatus (Name) Values('Originator in Process');
Insert INTO MSI_MediaRequestStatus (Name) Values('No Originator Media Available');
Insert INTO MSI_MediaRequestStatus (Name) Values('Uploaded');
Insert INTO MSI_MediaRequestStatus (Name) Values('Originator Fulfilled');
