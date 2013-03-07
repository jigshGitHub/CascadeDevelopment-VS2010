--Executed 0307 6:51AM DBPCS
ALTER TABLE [dbo].[MSI_DPSForm] ADD [CheckDocuments] [varchar](max) NULL
GO
ALTER TABLE [dbo].[MSI_DPSForm] ADD [UploadedOn] [datetime] NULL
GO
ALTER TABLE [dbo].[MSI_DPSForm] ADD [IsActive] [bit] NULL
GO
ALTER TABLE [dbo].[MSI_DPSForm] ADD [UploadedBy] [varchar](50) NULL
GO


/****** Object:  Table [dbo].[MSI_ComplaintMain]    Script Date: 03/07/2013 05:04:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_ComplaintMain]') AND type in (N'U'))
DROP TABLE [dbo].[MSI_ComplaintMain]
GO


/****** Object:  Table [dbo].[MSI_ComplaintMain]    Script Date: 03/07/2013 05:04:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MSI_ComplaintMain](
	[AgencyId] [varchar](20) NULL,
	[Account] [varchar](20) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[Address] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](10) NULL,
	[HomePhone] [varchar](50) NULL,
	[WorkPhone] [varchar](50) NULL,
	[MobilePhone] [varchar](50) NULL,
	[LastFourSSN] [varchar](4) NOT NULL,
	[DebtorIdentityVerifiedYN] [bit] NULL,
	[ContactMethodId] [int] NULL,
	[ContactTimeId] [int] NULL,
	[CreditorName] [varchar](500) NULL,
	[DebtProductId] [int] NULL,
	[DebtPurchaseBalance] [varchar](50) NULL,
	[DebtCurrentBalance] [varchar](50) NULL,
	[DisputeDebtYN] [bit] NULL,
	[DisputeDebtAmountYN] [bit] NULL,
	[DisputeDebtDueDateYN] [bit] NULL,
	[ComplaintID] [varchar](50) NULL,
	[ComplaintDate] [datetime] NOT NULL,
	[ComplaintReceivedByMethodId] [int] NULL,
	[ComplaintIssueId] [int] NOT NULL,
	[ComplaintNotes] [varchar](1000) NULL,
	[ComplaintSubmitedToAgencyYN] [bit] NULL,
	[ComplaintSubmitedToAgencyDate] [datetime] NULL,
	[MoreInfoReqdFromDebtorYN] [bit] NULL,
	[MoreInfoRequestedDate] [datetime] NULL,
	[MoreInfoRequested] [varchar](1000) NULL,
	[MoreInfoReceivedFromDebtorYN] [bit] NULL,
	[MoreInfoReceivedDate] [datetime] NULL,
	[MoreInfoReceived] [varchar](1000) NULL,
	[ComplaintSubmittedToOwnerYN] [bit] NULL,
	[ComplaintSubmittedDate] [datetime] NULL,
	[TimeToSubmitDays] [int] NULL,
	[MoreInfoFromAgencyYN] [bit] NULL,
	[MoreInfoFromAgencyRequestedDate] [datetime] NULL,
	[MoreInfoFromAgencyRequested] [varchar](1000) NULL,
	[MoreInfoFromAgencyReceivedYN] [bit] NULL,
	[MoreInfoFromAgencyReceived] [varchar](1000) NULL,
	[MoreInfoFromAgencyReceivedDate] [datetime] NULL,
	[OwnerResponseId] [int] NULL,
	[OwnerResponseDate] [datetime] NULL,
	[OwnerResponseDays] [int] NULL,
	[AgencyResponseToDebtorDate] [datetime] NULL,
	[TotalResponseTimeDays] [int] NULL,
	[DebtorAgreeYN] [bit] NULL,
	[NeedFurtherActionYN] [bit] NULL,
	[FinalActionStepId] [int] NULL,
	[IsViewedByOwner] [bit] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[IsViewedByAgency] [bit] NULL,
	[ComplaintDocument] [varchar](500) NULL,
	[DebtOwnerProcessDocument] [varchar](500) NULL,
 CONSTRAINT [PK_MSI_ComplaintMain] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

