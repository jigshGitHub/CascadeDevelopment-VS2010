--Executed DBPCS 02/22/2013

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsReceivableReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsReceivableReportData]
GO

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[MSI_spGetRecallsReceivableReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
    Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(AmtReceivable),0)   FROM MSI_RecallForm mf
	      where mf.RecallReason is not null;
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID	  
	  ,Isnull(Date,'') as Date
      ,OrigAcct as OrigAcct
      ,AcctName as AcctName
      ,Isnull(Portfolio,'') as Portfolio
      ,Isnull(CurrentResp,'') as CurrentResp
      ,Isnull(RecallReason,'') as RecallReason 
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,Isnull(PIMSAcct,'') as PIMSAcct
      ,Isnull(NewStatus,'') as NewStatus
      ,Isnull(NewResp,'') as NewResp 
       ,Isnull(UploadedDate,'') as UploadedDate 
      ,Isnull(CheckNumber,'') as CheckNumber 
      ,Isnull(Invoice,'') as Invoice
      ,Isnull(Explanation,'') as Explanation
      ,FaceValueofAcct 
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,Isnull(AmtReceivable,'') as AmtReceivable        
      ,Isnull(AmtPayable,'') as AmtPayable        
            ,Isnull(Seller,'') as Seller
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_RecallForm mf where mf.RecallReason is not null
      order by OrigAcct;
	End
	
	if @Export IS NULL
	Begin
	  Select ID,  OrigAcct,Isnull(Date,'') as Date,AcctName,Portfolio,PIMSAcct,FaceValueofAcct,CostBasis ,
	    AmtReceivable,Invoice,@TotalAmount as TotalAmount	   
	     FROM MSI_RecallForm mf
	      where mf.RecallReason is not null
	    order by OrigAcct;
	End
    
  End
  
GO
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsInvoiceLookupReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsInvoiceLookupReportData]
GO
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetRecallsInvoiceLookupReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
    Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(AmtReceivable),0)   FROM MSI_RecallForm mf
	      where mf.Invoice is not null;
    
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID	  
	  ,Isnull(Date,'') as Date
      ,OrigAcct as OrigAcct
      ,AcctName as AcctName
      ,Isnull(Portfolio,'') as Portfolio
      ,Isnull(CurrentResp,'') as CurrentResp
      ,Isnull(RecallReason,'') as RecallReason 
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,Isnull(PIMSAcct,'') as PIMSAcct
      ,Isnull(NewStatus,'') as NewStatus
      ,Isnull(NewResp,'') as NewResp 
       ,Isnull(UploadedDate,'') as UploadedDate 
      ,Isnull(CheckNumber,'') as CheckNumber 
      ,Isnull(Invoice,'') as Invoice
      ,Isnull(Explanation,'') as Explanation
      ,FaceValueofAcct 
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,Isnull(AmtReceivable,'') as AmtReceivable        
      ,Isnull(AmtPayable,'') as AmtPayable        
            ,Isnull(Seller,'') as Seller
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_RecallForm mf where mf.Invoice is not null;
	End
	
	if @Export IS NULL
	Begin
	  Select ID,  OrigAcct,Isnull(Date,'') as Date,AcctName,Portfolio,PIMSAcct,FaceValueofAcct,CostBasis ,
	    AmtReceivable,Invoice,@TotalAmount as TotalAmount FROM MSI_RecallForm mf where mf.Invoice is not null;
	End
    
  End
  

  GO
  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsSellerCheckLookupReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsSellerCheckLookupReportData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetRecallsSellerCheckLookupReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
     Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(AmtReceivable),0)   FROM MSI_RecallForm mf
	      where mf.SellerCheck is not null;
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID	  
	  ,Isnull(Date,'') as Date
      ,OrigAcct as OrigAcct
      ,AcctName as AcctName
      ,Isnull(Portfolio,'') as Portfolio
      ,Isnull(CurrentResp,'') as CurrentResp
      ,Isnull(RecallReason,'') as RecallReason 
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,Isnull(PIMSAcct,'') as PIMSAcct
      ,Isnull(NewStatus,'') as NewStatus
      ,Isnull(NewResp,'') as NewResp 
       ,Isnull(UploadedDate,'') as UploadedDate 
      ,Isnull(CheckNumber,'') as CheckNumber 
      ,Isnull(Invoice,'') as Invoice
      ,Isnull(Explanation,'') as Explanation
      ,FaceValueofAcct 
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,Isnull(AmtReceivable,'') as AmtReceivable        
      ,Isnull(AmtPayable,'') as AmtPayable        
            ,Isnull(Seller,'') as Seller
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_RecallForm mf where mf.SellerCheck is not null;
	End
	
	if @Export IS NULL
	Begin
	  Select ID,  OrigAcct,Isnull(Date,'') as Date,AcctName,Portfolio,PIMSAcct,FaceValueofAcct,CostBasis ,
	    AmtReceivable,Invoice,SellerCheck,@TotalAmount as TotalAmount FROM MSI_RecallForm mf where mf.SellerCheck is not null;
	End
    
  End
  
GO


  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsPayableReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsPayableReportData]
GO

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetRecallsPayableReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
     Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(AmtReceivable),0)   FROM MSI_RecallForm mf
	     where mf.AmtPayable > 0;
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID	  
	  ,Isnull(Date,'') as Date
      ,OrigAcct as OrigAcct
      ,AcctName as AcctName
      ,Isnull(Portfolio,'') as Portfolio
      ,Isnull(CurrentResp,'') as CurrentResp
      ,Isnull(RecallReason,'') as RecallReason 
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,Isnull(PIMSAcct,'') as PIMSAcct
      ,Isnull(NewStatus,'') as NewStatus
      ,Isnull(NewResp,'') as NewResp 
       ,Isnull(UploadedDate,'') as UploadedDate 
      ,Isnull(CheckNumber,'') as CheckNumber 
      ,Isnull(Invoice,'') as Invoice
      ,Isnull(Explanation,'') as Explanation
      ,FaceValueofAcct 
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,Isnull(AmtReceivable,'') as AmtReceivable        
      ,Isnull(AmtPayable,'') as AmtPayable        
            ,Isnull(Seller,'') as Seller
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_RecallForm mf where mf.AmtPayable > 0;
	End
	
	if @Export IS NULL
	Begin
	  Select ID,  OrigAcct,Isnull(Date,'') as Date,AcctName,Portfolio,PIMSAcct,FaceValueofAcct,CostBasis ,
	    AmtReceivable,Invoice,SalesBasis,Isnull(AmtPayable,'') as AmtPayable,
	     Isnull(CheckNumber,'') as CheckNumber,@TotalAmount as TotalAmount   FROM MSI_RecallForm mf where mf.AmtPayable >0;
	End
    
  End
  
GO
  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetRecallsPaidByOurCheckReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetRecallsPaidByOurCheckReportData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetRecallsPaidByOurCheckReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
     Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(AmtReceivable),0)   FROM MSI_RecallForm mf
	     where mf.CheckNumber is not null;
    -- get data
	if @Export IS NOT NULL
	Begin
	Select 
	  ID	  
	  ,Isnull(Date,'') as Date
      ,OrigAcct as OrigAcct
      ,AcctName as AcctName
      ,Isnull(Portfolio,'') as Portfolio
      ,Isnull(CurrentResp,'') as CurrentResp
      ,Isnull(RecallReason,'') as RecallReason 
      ,Isnull(DateNoteSent,'') as DateNoteSent 
      ,Isnull(PIMSAcct,'') as PIMSAcct
      ,Isnull(NewStatus,'') as NewStatus
      ,Isnull(NewResp,'') as NewResp 
       ,Isnull(UploadedDate,'') as UploadedDate 
      ,Isnull(CheckNumber,'') as CheckNumber 
      ,Isnull(Invoice,'') as Invoice
      ,Isnull(Explanation,'') as Explanation
      ,FaceValueofAcct 
      ,CostBasis
      ,SalesBasis
      ,SellerCheck
      ,Putback
      ,Isnull(DateAcctClosed,'') as DateAcctClosed
      ,Isnull(AmtReceivable,'') as AmtReceivable        
      ,Isnull(AmtPayable,'') as AmtPayable        
            ,Isnull(Seller,'') as Seller
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_RecallForm mf where mf.CheckNumber is not null;
	End
	
	if @Export IS NULL
	Begin
	  Select ID,  OrigAcct,Isnull(Date,'') as Date,AcctName,Portfolio,PIMSAcct,FaceValueofAcct,CostBasis ,
	    AmtReceivable,Invoice,SalesBasis,Isnull(AmtPayable,'') as AmtPayable,
	     Isnull(CheckNumber,'') as CheckNumber,@TotalAmount as TotalAmount   FROM MSI_RecallForm mf where mf.CheckNumber is not null;
	End
    
  End
  

GO
  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetDPSCheckDetailReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetDPSCheckDetailReportData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetDPSCheckDetailReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
     Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(Amount),0)   FROM MSI_DPSForm mf
	    where mf.OurCheckNumber is not null;
    -- get data
	
	Begin
	Select 
	  ID	  
	  ,Isnull(PIMSAcct,'') as PIMSAcct	  
	  ,Amount
	  ,NetPayment	  
	  ,Isnull(TransCode,'') as TransCode
	  ,Isnull(TransDate,'') as TransDate
	  ,Isnull(CheckNumber,'') as CheckNumber 
	  ,PmtType
	  ,TransSource
	  ,Isnull(Portfolio,'') as Portfolio	  
      ,AcctName as AcctName
      ,Isnull(DateRec,'') as DateRec 
      ,Isnull(CurrentResp,'') as CurrentResp      
      ,OriginalAcct
      ,Isnull(OurCheckNumber,'') as OurCheckNumber 
      ,Uploaded
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_DPSForm mf where mf.OurCheckNumber is not null;
	End
	
	
    
  End
  
GO
  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetDPSPayableReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetDPSPayableReportData]
GO
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetDPSPayableReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
   Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(Amount),0) from  MSI_DPSForm mf;
    
    -- get data
	
	Begin
	Select 
	  ID	  
	  ,Isnull(PIMSAcct,'') as PIMSAcct	  
	  ,Amount
	  ,NetPayment	  
	  ,Isnull(TransCode,'') as TransCode
	  ,Isnull(TransDate,'') as TransDate
	  ,Isnull(CheckNumber,'') as CheckNumber 
	  ,PmtType
	  ,TransSource
	  ,Isnull(Portfolio,'') as Portfolio	  
      ,AcctName as AcctName
      ,Isnull(DateRec,'') as DateRec 
      ,Isnull(CurrentResp,'') as CurrentResp      
      ,OriginalAcct
      ,Isnull(OurCheckNumber,'') as OurCheckNumber 
      ,Uploaded
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_DPSForm mf ;
	
    END
  End
  
GO
  GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MSI_spGetDPSPaidByOurCheckReportData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MSI_spGetDPSPaidByOurCheckReportData]
GO
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[MSI_spGetDPSPaidByOurCheckReportData]
  @Export VARCHAR(255) = NULL
AS
  
  Begin
     Declare @TotalAmount varchar(100);
    select @TotalAmount =  ISNULL(SUM(Amount),0)  from MSI_DPSForm mf where mf.OurCheckNumber is not null;
    -- get data
	
	Begin
	Select 
	  ID	  
	  ,Isnull(PIMSAcct,'') as PIMSAcct	  
	  ,Amount
	  ,NetPayment	  
	  ,Isnull(TransCode,'') as TransCode
	  ,Isnull(TransDate,'') as TransDate
	  ,Isnull(CheckNumber,'') as CheckNumber 
	  ,PmtType
	  ,TransSource
	  ,Isnull(Portfolio,'') as Portfolio	  
      ,AcctName as AcctName
      ,Isnull(DateRec,'') as DateRec 
      ,Isnull(CurrentResp,'') as CurrentResp      
      ,OriginalAcct
      ,Isnull(OurCheckNumber,'') as OurCheckNumber 
      ,Uploaded
      ,GUID,@TotalAmount as TotalAmount
      FROM MSI_DPSForm mf where mf.OurCheckNumber is not null;
	End
	
    
  End
  
