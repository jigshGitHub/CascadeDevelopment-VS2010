/* File Created: February 15, 2013 */

/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />
if ($('#hdnUserRole').val() == 'user' || $('#hdnUserRole').val() == "mediaAdmin") {
    var pimsAccountFocusString = 'PIMS Account Number';
}
else {
    var pimsAccountFocusString = 'Client Account Number';
}
var originalAccountFocusString = 'Original Account Number';
var fOrlNameFocusString = 'First Or Last Name';

function mediaReuqestType(id, reqId, typeId, documents, reqDt, reqUser, respDt, respUser, typeConstraints) {
    this.Id = id;
    this.RequestedId = reqId;
    this.TypeId = typeId;
    this.RespondedDocuments = documents;
    this.RequestedDate = reqDt;
    this.RequestedUserID = reqUser;
    this.RespondedDate = respDt;
    this.RespondedUserID = respUser;
    this.TypeConstraints = typeConstraints;
}

function mediaType(text, value, enable, checked) {
    var self = this;
    self.text = ko.observable(text);
    self.value = ko.observable(value);
    self.enable = ko.observable(enable);
    self.checked = ko.observable(checked);
    self.typeConstraints = ko.observable('');
    self.documents = ko.observable('');
    self.docUrl = ko.observable('');
    self.typeConstraintsTxt = ko.observable('');
}
function pageViewModel(userId, userAgency, userRole, id, account) {
    log(userId + ' ' + userAgency + ' ' + userRole);
    var self = this;
    self.userId = ko.observable(userId);
    self.agency = ko.observable(userAgency);
    self.userRole = ko.observable(userRole);
    self.validationErrors = ko.observableArray([]);
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.setFocus = ko.observable(false);
    
    self.agenciesDisable = ko.computed(function () { return (userAgency != '' && userAgency != undefined); }, self);
    self.agencies = ko.computed(function () {
        var localAgencies = [];
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'Agencies' },
            dataType: 'json',
            async: false,
            success: function (data) {
                //log(data.length);
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        //log(item.Text);
                        localAgencies.push(item);
                    });
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
        return localAgencies;
    }, self);
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
    self.showMediaTypeSelection = ko.observable(false);
    self.id = ko.observable(id);
    self.pimsAccountNumber = ko.observable((account == '') ? pimsAccountFocusString : account);
    self.pimsAccountNumberRequired = ko.observable('*');
    self.pimsAccountNumberRequiredMsg = ko.observable('');
    self.pimsAccountNumberRequiredHasError = ko.observable(false);

    self.originalAccountNumber = ko.observable(originalAccountFocusString);
    self.originalAccountNumberRequired = ko.observable('*');
    self.originalAccountNumberRequiredMsg = ko.observable('');
    self.originalAccountNumberRequiredHasError = ko.observable(false);

    self.clientName = ko.observable(fOrlNameFocusString);

    self.pimsRecords = ko.observableArray([]);
    self.portfolio = ko.observable('');
    self.lender = ko.observable('');
    self.ssn = ko.observable('');
    self.name = ko.observable('');
    self.openDate = ko.observable('');
    self.coDate = ko.observable('');
    self.seller = ko.observable('');

    self.mediaTypes = ko.observableArray([]);
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
        if (data.SSN.indexOf('-') > 0) {
            var n = data.SSN.split("-");
            self.ssn(n[2]);
        }
        else {
            self.ssn(data.SSN);
        }
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

        if (self.agency() == '' || self.agency() == undefined) {
            self.setShowMessagePanel(true, 'Agency is required');
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
            window.open(baseUrl + '/Recourse/Media/PIMSDataSearch?nameSearch=' + self.clientName(), '_self', '', '');
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
                if (data == null) {
                    log('Getting from RAccount');
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
                            self.showMediaTypeSelection(false);
                            self.loadMediaTypes();
                        },
                        error: function (xhr, status, error) {
                            self.setShowMessagePanel(true, xhr.responseText);
                            $("#loading").html("&nbsp;");
                            $("#loading").dialog('close');
                        }
                    });
                }
                else {
//                    if ($('#hdnUserRole').val() == 'user' || $('#hdnUserRole').val() == "mediaAdmin") {
//                        window.open(baseUrl + '/Recourse/Media/UpdateMediaRequest/' + data.Id, '_self', '', '');
//                    }
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');
                    self.pimsDataAvailable(true);
                    self.setPimsData(data);
                    self.showMediaTypeSelection(false);
                    self.loadMediaTypes();
                    self.setSelectedMediaRequested(data.MSI_MediaRequestTypes);
                }
            },
            error: function (xhr, status, error) {

            }
        });
    }
    self.viewMedia = function () {
        self.showMediaTypeSelection(true);
    }
    //Search for Another Account - Basically reloading the page
    self.searchForAnother = function () {
        window.open(baseUrl + '/Recourse/Media/Create', '_self', '', '');
    }
    self.setSelectedMediaRequested = function (data) {

        $.each(data, function (i, item) {
            $.each(self.mediaTypes(), function (i, mediaItem) {
                if (mediaItem.value() == item.TypeId) {                    
                    mediaItem.enable(false);
                    mediaItem.checked(true);
                    if (item.RespondedDocuments != null) {
                        mediaItem.docUrl(baseUrl + '/Recourse/Media/DownloadDoc?fileName=' + item.RespondedDocuments);
                        mediaItem.documents(getFileName(item.RespondedDocuments));
                    }
                    self.selectedMediaTypes.push(mediaItem);
                }
            });

        });
        if (self.selectedMediaTypes.length == self.mediaTypes.length)
            self.selectedMediaTypes([]);
    }


    self.loadExisitingRequest = function () {
        //log(self.id());
        self.showSearchCriteria(false);

        $.ajax({
            url: baseUrl + '/api/MediaRequest/Details',
            type: "GET",
            data: { id: self.id() },
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                self.pimsDataAvailable(true);
                self.setPimsData(response);
                self.showMediaTypeSelection(true);
                self.loadMediaTypes();
                self.setSelectedMediaRequested(response.MSI_MediaRequestTypes);
            },
            error: function (response, errorText) {
                return false;
            }
        });

    }

    self.loadMediaTypes = function () {
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'MediaTypes' },
            dataType: 'json',
            async: false,
            success: function (data) {
                //log(data.length);
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        //log(item.Text);
                        self.mediaTypes.push(new mediaType(item.Text, item.Value, true, false));
                    });
                    // log(self.mediaTypes());
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }

    //look for logic here http://jsfiddle.net/cjgaudin/Dp7Br/

    self.selectedMediaTypes = ko.observableArray([]);
    self.mediaChecked = function (media, elem) {
        var $checkBox = $(elem.srcElement);
        var isChecked = $checkBox.is(':checked');
        //If it is checked and not in the array, add it
        if (isChecked && self.selectedMediaTypes.indexOf(media) < 0) {
            self.selectedMediaTypes.push(media);
            if (media.value() == '5') {
                $("#statementMediaTypeConstraint").dialog('open');
            }
        }
        //If it is in the array and not checked remove it                
        else if (!isChecked && self.selectedMediaTypes.indexOf(media) >= 0) {
            self.selectedMediaTypes.remove(media);
            if (media.value() == '5') {
                media.typeConstraints('');
                if (self.mediaTypes().indexOf(media) >= 0) {
                    var mediaStatement = $.grep(self.mediaTypes(), function (item) { return item.value() == '5'; });
                    mediaStatement[0].typeConstraintsTxt('');
                }
            }
        }
        //Need to return to to allow the Checkbox to process checked/unchecked
        //log(self.selectedMediaTypes());
        return true;
    }


    removeStatementSelectedConstraint = function () {
        $.each(self.selectedMediaTypes(), function (i, item) {
            var media = item;
            if (media.value() == '5') {
                self.selectedMediaTypes.remove(media);
                media.typeConstraints('');
                if (self.mediaTypes().indexOf(media) >= 0) {
                    var mediaStatement = $.grep(self.mediaTypes(), function (item) { return item.value() == '5'; });
                    mediaStatement[0].typeConstraintsTxt('');
                    mediaStatement[0].checked(false);
                }
            }
        });
    }
    checkMediaTypeChecked = function (isChecked, media) {
        if (isChecked && self.selectedMediaTypes.indexOf(media) < 0) {
            log(media.enable());
            self.selectedMediaTypes.push(media);
            if (media.value() == '5') {
                $("#statementMediaTypeConstraint").dialog('open');
            }
        }
        //If it is in the array and not checked remove it                
        else if (!isChecked && self.selectedMediaTypes.indexOf(media) >= 0) {
            self.selectedMediaTypes.remove(media);
            media.typeConstraintsTxt('');
        }
    }
    self.selectAll = ko.computed({
        read: function () {
            var firstUnchecked = ko.utils.arrayFirst(self.mediaTypes(), function (item) {
                return item.checked() == false;
            });
            return firstUnchecked == null;
        },
        write: function (value) {

            ko.utils.arrayForEach(self.mediaTypes(), function (item) {
                if (item.enable()) {
                    item.checked(value);
                    checkMediaTypeChecked(value, item);
                }
            });
        }
    });
    self.statementStDt = ko.observable();
    self.statementEndDt = ko.observable();
    self.submitStatementDateRange = function () {
        $.each(self.selectedMediaTypes(), function (i, item) {
            var media = item;
            if (media.value() == '5') {
                if (self.statementStDt() == undefined || self.statementEndDt() == undefined || self.statementStDt() == '' || self.statementEndDt() == '') {
                    alert('From and to dates are required');
                    removeStatementSelectedConstraint();
                }
                else {
                    media.typeConstraints(self.statementStDt() + ';' + self.statementEndDt());
                    if (self.mediaTypes().indexOf(media) >= 0) {
                        var mediaStatement = $.grep(self.mediaTypes(), function (item) { return item.value() == '5'; });
                        mediaStatement[0].typeConstraintsTxt('From ' + self.statementStDt() + ' to ' + self.statementEndDt());
                    }
                }
            }
        });

        $("#statementMediaTypeConstraint").dialog('close');
    }
    self.showSubmit = ko.computed(function () { return (self.selectedMediaTypes().length > 0); }, self);
    function getSelectedMediaRequested() {
        var localSelectedMediaTypes = [];
        $.each(self.selectedMediaTypes(), function (i, item) {
            localSelectedMediaTypes.push(new mediaReuqestType(undefined, undefined, item.value(), undefined, new Date(), self.userId(), undefined, undefined, item.typeConstraints()));
        });
        return localSelectedMediaTypes;
    }
    self.saveData = function () {

        var json = JSON.stringify({
            Id: self.id(),
            AgencyId: self.agency(),
            Account: self.pimsAccountNumber(),
            OriginalAccount: self.originalAccountNumber(),
            Portfolio: self.portfolio(),
            Originator: self.lender(),
            SSN: self.ssn(),
            Name: self.name(),
            OpenDate: self.openDate(),
            ChargeOffDate: self.coDate(),
            Seller: self.seller(),
            RequestedDate: new Date(),
            RequestedByUserId: self.userId(),
            MSI_MediaRequestTypes: getSelectedMediaRequested()
        });

        $.ajax({
            url: baseUrl + '/api/MediaRequest/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                self.setShowMessagePanel(true, 'Media request submitted');
            },
            error: function (response, errorText) {
                return false;
            }
        });

    }
    self.submit = function () {
        self.saveData();
        window.open(baseUrl + '/Recourse/Home', '_self', '', '');
    }

    self.submitRequestMore = function () {
        self.submit();
        window.open(baseUrl + '/Recourse/Media/Create', '_self', '', '');
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
