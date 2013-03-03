--executed in dbpcs 03/02 & 03/03
INSERT INTO MSI_MediaRequestStatus (Name) VALUES('Re Submit');
INSERT INTO MSI_MediaRequestStatus (Name) VALUES('Request Complete');


/****** Object:  StoredProcedure [dbo].[MSI_spGetMediaTrackerByAccountNumber]    Script Date: 03/01/2013 15:18:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetMediaTrackerByAccountNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetMediaTrackerByAccountNumber]
GO


/****** Object:  StoredProcedure [dbo].[MSI_spGetMediaTrackerByAccountNumber]    Script Date: 03/01/2013 15:18:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_spGetMediaTrackerByAccountNumber](@account VARCHAR(25))
As
select Id, Account, OriginalAccount, MediaTypeId,MediaDocuments, TypeConstraints  from MSI_MediaTracker where Account = @account


GO