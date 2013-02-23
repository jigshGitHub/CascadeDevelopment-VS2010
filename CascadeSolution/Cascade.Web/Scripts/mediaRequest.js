/* File Created: February 15, 2013 */

/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />

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
}
function pageViewModel(userId, userAgency, userRole, id) {
    log(userId + ' ' + userAgency + ' ' + userRole);
    var self = this;
    self.userId = ko.observable(userId);
    self.agency = ko.observable(userAgency);
    self.userRole = ko.observable(userRole);
    self.validationErrors = ko.observableArray([]);
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.setFocus = ko.observable(true);
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
    self.pimsAccountNumber = ko.observable('');
    self.pimsAccountNumberRequired = ko.observable('*');
    self.pimsAccountNumberRequiredMsg = ko.observable('');
    self.pimsAccountNumberRequiredHasError = ko.observable(false);

    self.originalAccountNumber = ko.observable('');
    self.originalAccountNumberRequired = ko.observable('*');
    self.originalAccountNumberRequiredMsg = ko.observable('');
    self.originalAccountNumberRequiredHasError = ko.observable(false);

    self.portfolio = ko.observable('12345');
    self.lender = ko.observable('lender 12345');
    self.ssn = ko.observable('455-565-5656');
    self.name = ko.observable('fname lname');
    self.openDate = ko.observable('05/23/1943');
    self.coDate = ko.observable('02/12/2015');
    self.seller = ko.observable('seller');

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
        self.ssn(data.SSN);
        self.name(data.NAME);
        self.openDate(getFormatedDate(data.OpenDate));
        self.coDate(getFormatedDate(data.ChargeOffDate));
        self.seller(data.Seller);
    };

    self.searchedIdnetity = '';
    self.searchedType = 'pims';

    self.search = function () {
        self.setShowMessagePanel(false, '');
        if (self.pimsAccountNumber() == '' & self.originalAccountNumber() == '') {
            self.setShowMessagePanel(true, 'PIMS Account Number OR Original Account Number is required for searching');
            self.setFocus(true);
            return;
        }
        self.searchedIdnetity = self.pimsAccountNumber();
        if (self.originalAccountNumber() != '') {
            self.searchedIdnetity = self.originalAccountNumber();
            self.searchedType = 'original';
        }
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
        $("#loading").dialog('open');

        $.ajax({
            url: baseUrl + '/api/MediaRequest/Details',
            type: "GET",
            data: { id: self.id() },
            data: { accountNumber: self.searchedIdnetity, agency: self.agency() },
            dataType: 'json',
            async: true,
            success: function (data) {
                log(data);
                if (data == null) {
                    $.ajax({
                        url: baseUrl + '/api/RAccount/',
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

    };

    self.viewMedia = function () {
        self.showMediaTypeSelection(true);
    }
    self.setSelectedMediaRequested = function (data) {

        $.each(data, function (i, item) {
            $.each(self.mediaTypes(), function (i, mediaItem) {
                if (mediaItem.value() == item.TypeId) {
                    //log(item.TypeId);
                    mediaItem.enable(false);
                    mediaItem.checked(true);
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
        }
        //Need to return to to allow the Checkbox to process checked/unchecked
        //log(self.selectedMediaTypes());
        return true;
    }

    self.statementStDt = ko.observable();
    self.statementEndDt = ko.observable();
    self.submitStatementDateRange = function () {
        $("#statementMediaTypeConstraint").dialog('close');
        $.each(self.selectedMediaTypes(), function (i, item) {
            var media = item;
            if (media.value() == '5') {
                media.typeConstraints(self.statementStDt() + ';' + self.statementEndDt());
            }
        });
    }
    self.showSubmit = ko.computed(function () { return (self.selectedMediaTypes().length > 0); }, self);
    self.submit = function () {        
        function getSelectedMediaRequested() {
            var localSelectedMediaTypes = [];
            $.each(self.selectedMediaTypes(), function (i, item) {                
                localSelectedMediaTypes.push(new mediaReuqestType(undefined, undefined, item.value(), undefined, new Date(), self.userId(), undefined, undefined, item.typeConstraints()));
            });
            return localSelectedMediaTypes;
        }
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
