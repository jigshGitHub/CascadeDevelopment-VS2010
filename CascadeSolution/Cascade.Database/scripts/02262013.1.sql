--Executed in DBPCS 02/27
INSERT INTO MSI_MediaRequestStatus(Name) Values('RequestFulfillment');


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MSI_MediaTracker_MSI_MediaTypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[MSI_MediaTracker]'))
ALTER TABLE [dbo].[MSI_MediaTracker] DROP CONSTRAINT [FK_MSI_MediaTracker_MSI_MediaTypes]
GO

/****** Object:  Table [dbo].[MSI_MediaTracker]    Script Date: 02/26/2013 12:47:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_MediaTracker]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_MediaTracker]
GO



/****** Object:  Table [dbo].[MSI_MediaTracker]    Script Date: 02/26/2013 12:47:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_MediaTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account] [varchar](20) NULL,
	[OriginalAccount] [varchar](255) NULL,
	[MediaTypeId] [int] NULL,
	[MediaDocuments] [varchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[TypeConstraints] [varchar](500) NULL,
 CONSTRAINT [PK_MSI_MediaTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MSI_MediaTracker]  WITH CHECK ADD  CONSTRAINT [FK_MSI_MediaTracker_MSI_MediaTypes] FOREIGN KEY([MediaTypeId])
REFERENCES [dbo].[MSI_MediaTypes] ([ID])
GO

ALTER TABLE [dbo].[MSI_MediaTracker] CHECK CONSTRAINT [FK_MSI_MediaTracker_MSI_MediaTypes]
GO
