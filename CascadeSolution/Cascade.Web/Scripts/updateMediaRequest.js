/* File Created: February 15, 2013 */

/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />


function mediaReuqestType(id, reqId, typeId, documents, reqDt, reqUser, respDt, respUser, typeConstraints, statusId) {
    this.Id = id;
    this.RequestedId = reqId;
    this.TypeId = typeId;
    this.RespondedDocuments = documents;
    this.RequestedDate = reqDt;
    this.RequestedUserID = reqUser;
    this.RespondedDate = respDt;
    this.RespondedUserID = respUser;
    this.TypeConstraints = typeConstraints;
    this.RequestStatusId = statusId;// 6;  //RequestComplete
}

function mediaType(id, text, value, visible, checked) {
    var self = this;
    self.id = id;
    self.text = ko.observable(text);
    self.value = ko.observable(value);
    self.visible = ko.observable(visible);
    self.checked = ko.observable(checked);
    self.typeConstraints = ko.observable('');
    self.documents = ko.observable('');
    self.docUrl = ko.observable('');
    self.statusId = ko.observable('');
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

    self.clientName = ko.observable('');

    self.pimsRecords = ko.observableArray([]);
    self.portfolio = ko.observable('');
    self.lender = ko.observable('');
    self.ssn = ko.observable('');
    self.name = ko.observable('');
    self.openDate = ko.observable('');
    self.coDate = ko.observable('');
    self.seller = ko.observable('');

    self.mediaTypes = ko.observableArray([]);
    
    self.statusList = ko.computed(function () {
        var availableStatus = [];
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'MediaRequestStatus' },
            dataType: 'json',
            async: false,
            success: function (data) {
                //log(data.length);
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        //log(item.Text);
                        availableStatus.push(item);
                    });
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
        return availableStatus;
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
                    log('Metching media type ' + mediaItem.text());
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
        self.showSearchCriteria(true);

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
                self.loadMediaTypes(response.MSI_MediaRequestTypes);
            },
            error: function (response, errorText) {
                return false;
            }
        });

    }

    self.loadMediaTypes = function (requestedTypes) {
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
                            if (item.Value == requestedItem.TypeId && requestedItem.RequestStatusId != 6) {
                                requestedMedia = new mediaType(requestedItem.Id, item.Text, item.Value, true, false);
                                requestedMedia.statusId(requestedItem.RequestStatusId);
                                self.mediaTypes.push(requestedMedia);
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

    checkMediaTypeChecked = function (isChecked, media) {
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
                item.checked(value);
                checkMediaTypeChecked(value, item);
            });
        }
    });
    self.statementStDt = ko.observable();
    self.statementEndDt = ko.observable();
    
    self.showSubmit = ko.computed(function () { return (self.selectedMediaTypes().length > 0); }, self);
    function getSelectedMediaRequested() {
        var localSelectedMediaTypes = [];
        $.each(self.selectedMediaTypes(), function (i, item) {
            log(item.statusId());
            localSelectedMediaTypes.push(new mediaReuqestType(item.id, undefined, item.value(), undefined, new Date(), self.userId(), new Date(), self.userId(), undefined, item.statusId()));
        });
        return localSelectedMediaTypes;
    }
    self.saveData = function () {

        var json = JSON.stringify({
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
        //window.open(baseUrl + '/Recourse/Home', '_self', '', '');
    }

    self.submitRequestMore = function () {
        self.submit();
        location.reload();
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
