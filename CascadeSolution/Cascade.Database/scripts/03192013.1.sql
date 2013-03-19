--Executed DBPCS 3/19 6:15AM
/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallsUploadReportData]    Script Date: 03/18/2013 22:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsUploadReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsUploadReportData]
GO

/****** Object:  StoredProcedure [dbo].[MSI_spGetRecallsUploadReportData]    Script Date: 03/18/2013 22:16:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[MSI_spGetRecallsUploadReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID
      ,Isnull(Date,'') as Date
      ,OrigAcct
      ,AcctName
      ,Portfolio
      ,CurrentResp
      ,RecallReason
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,PIMSAcct
      ,NewStatus
      ,NewResp
      ,Isnull(UploadedDate,'') as UploadedDate 
      ,CheckNumber
      ,Invoice
      ,Explanation
      ,FaceValueofAcct
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,AmtReceivable
      ,AmtPayable
      ,Seller
      ,GUID,RecallInitiatedBy FROM MSI_RecallForm rf where rf.NewStatus  is not null and rf.NewResp is not null;
	End
	
	if @Export IS NULL
	Begin
	  Select ID, PIMSAcct, NewStatus, NewResp FROM MSI_RecallForm rf where rf.NewStatus  is not null and rf.NewResp is not null; 
	End
	
  End
  

GO


