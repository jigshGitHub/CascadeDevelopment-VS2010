/* File Created: February 25, 2013 */

/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />
var pimsAccountFocusString = 'PIMS Account Number';
var originalAccountFocusString = 'Original Account Number';
var fOrlNameFocusString = 'First Or Last Name';

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

    self.showSearchCriteria = ko.observable(true);
    self.pimsDataAvailable = ko.observable(false);
    
    //self.showMediaTypeSelection = ko.observable(false);
    self.id = ko.observable(id);
    //For PIMS Account Number
    self.pimsAccountNumber = ko.observable(pimsAccountFocusString);
    self.pimsAccountNumberRequired = ko.observable('*');
    self.pimsAccountNumberRequiredMsg = ko.observable('');
    self.pimsAccountNumberRequiredHasError = ko.observable(false);
    //For Original Account Number
    self.originalAccountNumber = ko.observable(originalAccountFocusString);
    self.originalAccountNumberRequired = ko.observable('*');
    self.originalAccountNumberRequiredMsg = ko.observable('');
    self.originalAccountNumberRequiredHasError = ko.observable(false);
    //For Client Name
    self.clientName = ko.observable(fOrlNameFocusString);

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

    //Property to control Invoice and Seller check fields
    self.isenable = ko.observable(false);
    self.recallByOption.subscribe(function (Selected) {
        if (Selected == 'Cascade') {
            self.newInvoice('');
            self.newSellerCheck('');
            self.newCheckNo('');
            self.isenable(false);
        }
        else {
            self.newInvoice('');
            self.newSellerCheck('');
            self.newCheckNo('');
            self.isenable(true);
        }
    } .bind(self));

    //Reason For Recall
    self.recallreason = ko.observable('');
    self.recallreasons = ko.observableArray([]);
    //New Status
    self.status = ko.observable('');
    self.statuses = ko.observableArray([]);
    //For New Responsibility
    self.responsibility = ko.observable('');
    self.responsibilities = ko.observableArray([]);

    //For Date
    self.newDateRec = ko.computed(function () {
        return dateFormat(Date.parse(new Date()), 'mm/dd/yyyy');
    });

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
    //Weather to upload required or not
    self.newUploaded = ko.observable('');

    //For Media types
    self.checkDocuments = ko.observableArray([]);
    
    //For Recall Initiated By - Lookup Table 
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

    //For Recall Reasons
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

    //Only show Upload button if files are selected for the upload
    self.showUpload = ko.computed(function () {
        return (self.checkDocuments().length > 0);
    }, self);

    
    self.setShowMessagePanel = function (isVisible, message) {
        self.showMessage(isVisible);
        self.message(message);
    };

    //This function is used to set the PIMS Data on the Page
    self.setPimsData = function (data) {
        //log(data);
        self.id(data.Id);
        self.pimsAccountNumber(data.ACCOUNT);
        self.originalAccountNumber(data.OriginalAccount);
        self.portfolio(data.Portfolio);
        self.lender(data.Originator);
        //alert(data.SSN);
        var n = data.SSN.split("-");
        //alert(n.length);
        //alert(n[2]);
        self.ssn(n[2]);
        self.name(data.NAME);
        self.clientName(data.NAME);
        self.openDate(getFormatedDate(data.OpenDate));
        self.coDate(getFormatedDate(data.ChargeOffDate));
        self.seller(data.Seller);
        //For Original Data
        self.CurrentResponsibility(data.RESPONSIBILITY);
        self.FaceValueOfAcct(data.PrincipalBalance);
        //For Followup Data
        self.newSalesBasis(data.SalesPrice);
        self.newCostBasis(data.PurchasePrice);
        self.amtPayableComputed(data.PrincipalBalance * data.SalesPrice);
        self.amtReceivableComputed(data.PrincipalBalance * data.PurchasePrice);
        self.newGUID(data.GUID);

    };

    self.searchedIdnetity = '';
    self.searchedType = 'pims'; //default

    self.selectedAccount = function (record) {
        $("#pimsResults").dialog('close');
        self.searchedIdnetity = record.ACCOUNT;
        self.searchedType = 'pims';
        self.getPimsDetails();
    }

    self.search = function () {
        self.setShowMessagePanel(false, '');
        if (self.pimsAccountNumber() == pimsAccountFocusString && self.originalAccountNumber() == originalAccountFocusString && self.clientName() == fOrlNameFocusString) {
            self.setShowMessagePanel(true, pimsAccountFocusString + ' OR ' + originalAccountFocusString + ' OR ' + fOrlNameFocusString + ' is required for searching');
            self.setFocus(true);
            return;
        }
        self.searchedIdnetity = self.pimsAccountNumber(); //default
        if (self.originalAccountNumber() != originalAccountFocusString) {
            self.searchedIdnetity = self.originalAccountNumber();
            self.searchedType = 'original';
        }
        if (self.clientName() != fOrlNameFocusString) {
            self.searchedIdnetity = self.clientName();
            self.searchedType = 'name';
        }
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
        $("#loading").dialog('open');

        
        if (self.searchedType == 'name') {
            $.ajax({
                url: baseUrl + '/api/RAccount/',
                type: "GET",
                data: { nameSearch: self.clientName() },
                dataType: 'json',
                async: true,
                success: function (data) {
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');
                    if (data != null) {
                        $.each(data, function (i, item) {
                            item.OpenDate = getFormatedDate(item.OpenDate);
                            item.ChargeOffDate = getFormatedDate(item.ChargeOffDate);
                            self.pimsRecords.push(item);
                        });
                        $("#pimsResults").dialog('open');
                    }
                },
                error: function (xhr, status, error) {

                }
            });
        }
        else {
            self.getPimsDetails();
        }

    };

    self.getPimsDetails = function () {
        log('Getting from RAccount');
        //alert("Gettomg from RAccount");
        $.ajax({
            url: baseUrl + '/api/RAccount/Details/',
            type: 'GET',
            contentType: 'application/json',
            data: { accountNumber: self.searchedIdnetity, searchType: self.searchedType },
            dataType: 'json',
            async: true,
            success: function (data) {
                $("#loading").html("&nbsp;");
                $("#loading").dialog('close');
                //log(data);
                self.pimsDataAvailable(true);
                self.setPimsData(data);

            },
            error: function (xhr, status, error) {
                self.setShowMessagePanel(true, xhr.responseText);
                $("#loading").html("&nbsp;");
                $("#loading").dialog('close');
            }
        });
    }

    //Save or Add button click event
    self.save = function () {
        //alert("starting to save");
        self.validationErrors([]);
        //alert("ready for validation checks");

        $('#resultSummaryData ul').empty();

        //Original Account
        if (self.originalAccountNumber() == '') {
            self.originalAccountNumberRequiredHasError(true);
            self.originalAccountNumberRequiredMsg('Original Account Number is required.');
            self.validationErrors.push(self.originalAccountNumberRequiredMsg());
        }
        //PIMS Acccount
        if (self.pimsAccountNumber() == '') {
            self.pimsAccountNumberRequiredHasError(true);
            self.pimsAccountNumberRequiredMsg('PIMS Account Number is required.');
            self.validationErrors.push(self.pimsAccountNumberRequiredMsg());
        }

        if (self.validationErrors() == '') {
            //alert("we are here");
            var json = JSON.stringify({
                Date: self.newDateRec(),
                OrigAcct: self.originalAccountNumber(),
                PIMSAcct: self.pimsAccountNumber(),
                RecallReason: self.recallreason(),
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
                RecallInitiatedBy: self.recallByOption(),
                ClientName: self.clientName()

            });

            $.ajax({
                url: baseUrl + "/Recall/Add/",
                type: "POST",
                data: json,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    //log(response.ID);
                    //$('#resultSummary ul').append('<li>Coach with username ' + response.UserName + ' has been created.</li>');
                    $('#resultSummaryData ul').append('<li>Recall Record created successfully.</li>');
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
        else {
            $.each(self.validationErrors(), function (i, item) {
                log(item);
                $('#resultSummaryData ul').append('<li>' + item + '</li>');
            });
        }
    };
    
}

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
