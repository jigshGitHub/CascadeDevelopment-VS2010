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
    self.showPIMSData = ko.observable(true);
    self.showHidePimsData = function () {
        if (self.showPIMSData())
            self.showPIMSData(false);
        else
            self.showPIMSData(true);
    }
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

    //For Media types
    self.visible1 = ko.observable(false);
    self.docUrl1 = ko.observable('');
    self.documents1 = ko.observable('NOT UPLOADED YET');

    self.visible2 = ko.observable(false);
    self.docUrl2 = ko.observable('');
    self.documents2 = ko.observable('NOT UPLOADED YET');

    self.visible3 = ko.observable(false);
    self.docUrl3 = ko.observable('');
    self.documents3 = ko.observable('NOT UPLOADED YET');

    self.visible4 = ko.observable(false);
    self.docUrl4 = ko.observable('');
    self.documents4 = ko.observable('NOT UPLOADED YET');

    self.visible5 = ko.observable(false);
    self.docUrl5 = ko.observable('');
    self.stmtdateRanges = ko.observable('');
    self.documents5 = ko.observable('NOT UPLOADED YET');

    self.visible6 = ko.observable(false);
    self.docUrl6 = ko.observable('');
    self.documents6 = ko.observable('NOT UPLOADED YET');

    self.visible7 = ko.observable(false);
    self.docUrl7 = ko.observable('');
    self.documents7 = ko.observable('NOT UPLOADED YET');

    self.visible8 = ko.observable(false);
    self.docUrl8 = ko.observable('');
    self.documents8 = ko.observable('NOT UPLOADED YET');

    self.visible9 = ko.observable(false);
    self.docUrl9 = ko.observable('');
    self.documents9 = ko.observable('NOT UPLOADED YET');

    self.visible10 = ko.observable(false);
    self.docUrl10 = ko.observable('');
    self.documents10 = ko.observable('NOT UPLOADED YET');

    self.visible11 = ko.observable(false);
    self.docUrl11 = ko.observable('');
    self.documents11 = ko.observable('NOT UPLOADED YET');

    //For files
    self.mediaAfdvtIssuer = ko.observableArray([]);
    self.mediaAfdvtSeller = ko.observableArray([]);
    self.mediaApplication = ko.observableArray([]);
    self.mediaContact = ko.observableArray([]);
    self.mediaStatement = ko.observableArray([]);
    self.mediaChargeOffStmt = ko.observableArray([]);
    self.mediaRightOfCure = ko.observableArray([]);
    self.mediaBalanceLtr = ko.observableArray([]);
    self.mediaNoticeIntent = ko.observableArray([]);
    self.mediaCardHolderAgrmt = ko.observableArray([]);
    self.mediaTermsAndCnd = ko.observableArray([]);

    //look for logic here http://jsfiddle.net/cjgaudin/Dp7Br/
    self.statementStDt = ko.observable('');
    self.statementEndDt = ko.observable('');

    //Check if any media type file upload is selected or not
    self.showUpload = ko.computed(function () {
        //Show button if Stetement is NOT Selected and Any other Media option is selected 
        //OR Stement is Selected with necessary Start and End date combinaiton
        return ((self.mediaStatement().length == 0 &&
                (self.mediaAfdvtIssuer().length > 0
                || self.mediaAfdvtSeller().length > 0
                || self.mediaApplication().length > 0
                || self.mediaContact().length > 0
                || self.mediaChargeOffStmt().length > 0
                || self.mediaRightOfCure().length > 0
                || self.mediaBalanceLtr().length > 0
                || self.mediaNoticeIntent().length > 0
                || self.mediaCardHolderAgrmt().length > 0
                || self.mediaTermsAndCnd().length > 0)) || (self.mediaStatement().length > 0 && (self.statementStDt() != '' && self.statementEndDt() != ''))
                    );
    }, self);

    //Only show Statement Dates if Statement file is selected for upload
    self.showStatementsDate = ko.computed(function () {
        return (self.mediaStatement().length > 0);
    }, self);

    self.setShowMessagePanel = function (isVisible, message) {
        self.showMessage(isVisible);
        self.message(message);
    };

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

        //alert(self.searchedType);

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
        log('Getting from MSI_MediaRequestResponse');
        $.ajax({
            url: baseUrl + '/api/MediaRequest/Details',
            type: "GET",
            data: { accountNumber: self.searchedIdnetity, agency: self.agency(), userId: self.userId() },
            dataType: 'json',
            async: true,
            success: function (data) {
                log('Getting from RAccount');
                if (data == null) {
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
                else {
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');
                    self.pimsDataAvailable(true);
                    self.setPimsData(data);

                }
            },
            error: function (xhr, status, error) {

            }
        });
    }

    //Search for Another Account - Basically reloading the page
    self.searchForAnother = function () {
        window.open(baseUrl + '/Recourse/Media/MediaUpload', '_self', '', '');
    }
    //Initiate the Media Upload Process
    self.initiateMediaUpload = function () {
        self.getExistingMediaDetails();
        $('#fileUploadContainer').show();
        $('#fileUploadContainer').dialog('open');
    }


    self.getExistingMediaDetails = function () {
        $.ajax({
            url: baseUrl + '/Recourse/Media/GetExistingMediaDetails',
            type: "GET",
            data: { account: self.searchedIdnetity },
            dataType: 'json',
            async: true,
            success: function (data) {
                //log(data);                
                $("#loading").html("&nbsp;");
                $("#loading").dialog('close');
                self.loadExistingMediaTypes(data);
            },
            error: function (xhr, status, error) {

            }
        });
    }


    self.loadExistingMediaTypes = function (requestedTypes) {
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'MediaTypes' },
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.length > 0) {
                    var requestedMedia;
                    $.each(data, function (i, item) {
                        $.each(requestedTypes, function (i, requestedItem) {
                            if (item.Value == requestedItem.MediaTypeId) {

                                //alert(item.Value);

                                //For MT1
                                if (item.Value == "1") {
                                    self.docUrl1((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents1((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible1((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT2
                                if (item.Value == "2") {
                                    self.docUrl2((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents2((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible2((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT3
                                if (item.Value == "3") {
                                    self.docUrl3((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents3((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible3((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT4
                                if (item.Value == "4") {

                                    self.docUrl4((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents4((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible4((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT5
                                if (item.Value == "5") {
                                    self.docUrl5((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents5((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible5((requestedItem.MediaDocuments == null) ? false : true);
                                    self.stmtdateRanges('IN HOUSE -' + requestedItem.TypeConstraints);
                                }
                                //For MT6
                                if (item.Value == "6") {
                                    self.docUrl6((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents6((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible6((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT7
                                if (item.Value == "7") {
                                    self.docUrl7((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents7((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible7((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT8
                                if (item.Value == "8") {
                                    self.docUrl8((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents8((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible8((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT9
                                if (item.Value == "9") {
                                    self.docUrl9((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents9((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible9((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT10
                                if (item.Value == "10") {
                                    self.docUrl10((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents10((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible10((requestedItem.MediaDocuments == null) ? false : true);
                                }
                                //For MT11
                                if (item.Value == "11") {
                                    self.docUrl11((requestedItem.MediaDocuments == null) ? '' : baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + requestedItem.MediaDocuments);
                                    self.documents11((requestedItem.MediaDocuments == null) ? 'NOT UPLOADED YET' : getFileName(requestedItem.MediaDocuments));
                                    self.visible11((requestedItem.MediaDocuments == null) ? false : true);
                                }

                            }
                        });
                    });
                    // log(self.mediaTypes());
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }



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
