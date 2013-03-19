/* File Created: February 25, 2013 */

/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />

function pageViewModel(userId, userAgency, userRole, id) {
    log(userId + ' ' + userAgency + ' ' + userRole);
    var self = this;

    //Set apiUrl for Lookup table
    var apiUrl = baseUrl + '/api/Lookup/';

    self.userId = ko.observable(userId);
    self.agency = ko.observable(userAgency);
    self.userRole = ko.observable(userRole);
    self.validationErrors = ko.observableArray([]);
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.setFocus = ko.observable(false);
    function getFormatedDate(data) {
        if (data != null && data != '' && data != undefined) {
            return $.datepicker.formatDate('mm/dd/yy', new Date(data));
        }
        else
            return '';
    }

    //self.showMediaTypeSelection = ko.observable(false);
    self.id = ko.observable(id);
    //For PIMS Account Number
    self.pimsAccountNumber = ko.observable('');
    self.pimsAccountNumberRequired = ko.observable('*');
    self.pimsAccountNumberRequiredMsg = ko.observable('');
    self.pimsAccountNumberRequiredHasError = ko.observable(false);
    //For Original Account Number
    self.originalAccountNumber = ko.observable('');
    self.originalAccountNumberRequired = ko.observable('*');
    self.originalAccountNumberRequiredMsg = ko.observable('');
    self.originalAccountNumberRequiredHasError = ko.observable(false);
    //For Client Name
    self.clientName = ko.observable('');

    self.pimsRecords = ko.observableArray([]);
    self.portfolio = ko.observable('');
    self.lender = ko.observable('');
    self.ssn = ko.observable('');
    self.name = ko.observable('');
    self.openDate = ko.observable('');
    self.coDate = ko.observable('');
    self.seller = ko.observable('');

    self.CurrentResponsibility = ko.observable('');
    self.FaceValueOfAcct = ko.observable('');

    //Recall Initiated By
    self.recallByOption = ko.observable('');
    self.recallByOptions = ko.observableArray([]);

    //Reason For Recall
    self.recallreason = ko.observable('');
    self.recallreasons = ko.observableArray([]);
    //New Status
    self.status = ko.observable('');
    self.statuses = ko.observableArray([]);
    //For New Responsibility
    self.responsibility = ko.observable('');
    self.responsibilities = ko.observableArray([]);

    //Property to control Invoice and Seller check fields
    self.isenable = ko.observable(false);

    //For Date
    self.newDateRec = ko.observable('');
       
    //Follow-up Data
    self.newExplanation = ko.observable('');
    self.newDateNotificationSent = ko.observable('');
    self.newDateAcctClosed = ko.observable('');
    self.newUploadedDate = ko.observable('');
    self.newInvoice = ko.observable('');
    self.newSellerCheck = ko.observable('');
    self.newSalesBasis = ko.observable('');
    self.newCheckNo = ko.observable('');
    self.amtPayableComputed = ko.observable('');
    self.amtReceivableComputed = ko.observable('');
    self.newCostBasis = ko.observable('');
    self.newGUID = ko.observable('');
    
    //For Putback Initiated By - Lookup Table 
    $.ajax({
        url: apiUrl,
        type: 'GET',
        contentType: 'application/json',
        data: { id: 'RecallInitiatedBy' },
        success: function (data) {
            $.each(data, function (i, item) {
                self.recallByOptions.push(item);
            });
        },
        error: function (xhr, status, somthing) {
            log(status);
        }
    });

    //For Putback Reasons
    $.ajax({
        url: apiUrl,
        type: 'GET',
        contentType: 'application/json',
        data: { id: 'Reason' },
        success: function (data) {
            $.each(data, function (i, item) {
                self.recallreasons.push(item);
            });
        },
        error: function (xhr, status, somthing) {
            log(status);
        }
    });

    //For New Responsibility
    $.ajax({
        url: apiUrl,
        type: 'GET',
        contentType: 'application/json',
        data: { id: 'Responsibility' },
        success: function (data) {
            $.each(data, function (i, item) {
                self.responsibilities.push(item);
            });
        },
        error: function (xhr, status, somthing) {
            log(status);
        }
    });

    //For Status
    $.ajax({
        url: apiUrl,
        type: 'GET',
        contentType: 'application/json',
        data: { id: 'Status' },
        success: function (data) {
            $.each(data, function (i, item) {
                self.statuses.push(item);
            });
        },
        error: function (xhr, status, somthing) {
            log(status);
        }
    });


    //For Page Data
    $.ajax({
        url: baseUrl + '/Recall/GetPutbackData/',
        type: 'GET',
        contentType: 'application/json',
        data: { id: $('#hdnId').val() },
        dataType: 'json',
        success: function (response) {
            //Set Selected options                     
            self.name(response.AcctName);
            self.clientName(response.AcctName);
            self.portfolio(response.Portfolio);
            self.CurrentResponsibility(response.CurrentResp);
            self.originalAccountNumber(response.OrigAcct);
            self.pimsAccountNumber(response.PIMSAcct);
            self.FaceValueOfAcct(response.FaceValueofAcct);
            self.seller(response.Seller);
            self.clientName(response.AcctName);
            self.recallByOption(response.PutBackInitiatedBy);
            self.newSalesBasis(response.SalesBasis);
            self.newCostBasis(response.CostBasis);
            self.amtPayableComputed(response.AmtPayable);
            self.amtReceivableComputed(response.AmtReceivable);
            self.newDateRec(JSONDate(response.Date));
            self.recallreason(response.PutBackReason);
            self.newDateNotificationSent(JSONDate(response.DateNoteSent));
            self.newExplanation(response.Explanation);
            self.newDateAcctClosed(JSONDate(response.DateAcctClosed));
            self.newUploadedDate(JSONDate(response.UploadedDate));
            self.status(response.NewStatus);
            self.responsibility(response.NewResp);
            self.newCheckNo(response.CheckNumber);
            self.newInvoice(response.Invoice);
            self.newSellerCheck(response.SellerCheck);
            self.newGUID(response.GUID);
            if (response.PutBackInitiatedBy != 'Cascade') {
                self.isenable(true);
            }

        },
        error: function (xhr, status, somthing) {
            log(status);
        }
    });

    //This is used to format Date from Json response - We get date like this from Json "\/Date(1283219926108)\/"
    function JSONDate(dateStr) {
        if (dateStr != null) {
            var m, day;
            jsonDate = dateStr;
            var d = new Date(parseInt(jsonDate.substr(6)));
            m = d.getMonth() + 1;
            if (m < 10)
                m = '0' + m
            if (d.getDate() < 10)
                day = '0' + d.getDate()
            else
                day = d.getDate();
            return (m + '/' + day + '/' + d.getFullYear())
        }
        else {
            return ''
        }
    }
    //For Date with Time
    function JSONDateWithTime(dateStr) {
        if (dateStr != null) {
            jsonDate = dateStr;
            var d = new Date(parseInt(jsonDate.substr(6)));
            var m, day;
            m = d.getMonth() + 1;
            if (m < 10)
                m = '0' + m
            if (d.getDate() < 10)
                day = '0' + d.getDate()
            else
                day = d.getDate();
            var formattedDate = m + "/" + day + "/" + d.getFullYear();
            var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
            var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
            var formattedTime = hours + ":" + minutes + ":" + d.getSeconds();
            formattedDate = formattedDate + " " + formattedTime;
            return formattedDate;
        }
        else {
            return ''
        }
    }

        
    //Save or Add button click event
    self.save = function () {
                
            //alert("we are here");
            var json = JSON.stringify({
                ID: $('#hdnId').val(),
                Date: self.newDateRec(),
                OrigAcct: self.originalAccountNumber(),
                PIMSAcct: self.pimsAccountNumber(),
                PutBackReason: self.recallreason(),
                NewStatus: self.status(),
                NewResp: self.responsibility(),
                FaceValueOfAcct: self.FaceValueOfAcct(),
                AcctName: self.clientName(),
                Seller: self.seller(),
                CurrentResp: self.CurrentResponsibility(),
                Portfolio: self.portfolio(),
                UploadedDate: self.newUploadedDate(),
                Explanation: self.newExplanation(),
                DateNoteSent: self.newDateNotificationSent(),
                DateAcctClosed: self.newDateAcctClosed(),
                CostBasis: self.newCostBasis(),
                SalesBasis: self.newSalesBasis(),
                Invoice: self.newInvoice(),
                SellerCheck: self.newSellerCheck(),
                CheckNumber: self.newCheckNo(),
                AmtPayable: self.amtPayableComputed(),
                AmtReceivable: self.amtReceivableComputed(),
                GUID: self.newGUID(),
                PutBackInitiatedBy: self.recallByOption(),
                ClientName: self.clientName()
                
            });

            $.ajax({
                url: baseUrl + "/Recall/AddPutback/",
                type: "POST",
                data: json,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    //log(response.ID);
                    //$('#resultSummary ul').append('<li>Coach with username ' + response.UserName + ' has been created.</li>');
                    $('#resultSummaryData ul').append('<li>Putback Record updated successfully.</li>');
                    $('#saveNewBtn').hide();
                    //Now display the File Upload Div if user wants to upload
                    if (self.newUploaded() == true) {
                        //log(self.uploadChecked());
                        //alert(response.ID);
                        hdnRecallRecordID.value = response.ID;
                        $('#fileUploadContainer').show();
                        $('#fileUploadContainer').dialog('open');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                    console.log(textStatus, errorThrown);
                    $('#resultSummaryData ul').append('<li>We have some issue processing your request. Please try again later.</li>');
                }

            });
        }
        
    };
        
ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        var value = valueAccessor();
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).fadeIn(3000, function () {
            $(element).fadeOut(5000);
            value(false)
        }) : $(element).fadeOut();
    }
};
