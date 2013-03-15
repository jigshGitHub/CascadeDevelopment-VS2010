Alter table MSI_RecallForm Add [CheckDocuments] [varchar](max) NULL;
Alter table MSI_RecallForm Add [UploadedOn] [datetime] NULL;
Alter table MSI_RecallForm Add [IsActive] [bit] NULL;
Alter table MSI_RecallForm Add [UploadedBy] [varchar](50) NULL;
Alter table MSI_RecallForm Add [RecallInitiatedBy] [varchar](50) NULL;
Alter table MSI_RecallForm Add [ClientName] [varchar](150) NULL;