function complianceVM(userId, userAgency) {
    var self = this;
    self.account = ko.observable('');
    self.agency = ko.observable(userAgency);
    self.makeAgencySelectionDisable = ko.computed(function () {
        if (self.agency() == '')
            return false;
        else
            return true;
    }, self);
    self.lastName = ko.observable('');
    self.firstName = ko.observable('');
    self.dob = ko.observable('');
    self.address = ko.observable('');
    self.city = ko.observable('');
    self.state = ko.observable('');
    self.zip = ko.observable('');
    self.homePhone = ko.observable('');
    self.workPhone = ko.observable('');
    self.mobilePhone = ko.observable('');
    self.lastFourSSN = ko.observable('');
    self.debtorIdentityVerified = ko.observable('');
    self.contactMethodId = ko.observable('');
    self.contactTimeId = ko.observable('');
    self.creditorName = ko.observable('');
    self.debtProductId = ko.observable('');
    self.debtPurchaseBalance = ko.observable('');
    self.debtCurrentBalance = ko.observable('');
    self.disputeDebt = ko.observable('');
    self.disputeDebtAmount = ko.observable('');
    self.disputeDebtDueDate = ko.observable('');
    //Complaint section
    self.complaintID = ko.observable('');
    var todaysDate = new Date();
    todaysDate.setDate(todaysDate.getDate());
    self.complaintDate = ko.observable($.datepicker.formatDate('mm/dd/yy', todaysDate));
    self.complaintReceivedByMethodId = ko.observable('');
    self.complaintIssueId = ko.observable('');
    self.complaintNotes = ko.observable('');
    self.complaintUploadDocument = ko.observable('');
    self.complaintSubmitedToAgency = ko.observable('');
    self.complaintSubmitedToAgencyDate = ko.observable('');
    self.moreInfoReqdFromDebtor = ko.observable('');
    self.moreInfoRequestedDate = ko.observable('');
    self.moreInfoRequested = ko.observable('');
    self.moreInfoReceivedFromDebtor = ko.observable('');
    self.moreInfoReceivedDate = ko.observable('');
    self.moreInfoReceived = ko.observable('');
    //Debt Owner Process
    self.complaintSubmittedToOwner = ko.observable('');
    self.complaintSubmittedDate = ko.observable('');
    self.timeToSubmitDays = ko.observable('');
    self.debtOwnerUploadDocument = ko.observable('');
    self.moreInfoFromAgency = ko.observable('');
    self.moreInfoFromAgencyRequestedDate = ko.observable('');
    self.moreInfoFromAgencyRequested = ko.observable('');
    self.moreInfoFromAgencyReceived = ko.observable('');
    self.moreInfoFromAgencyReceivedDate = ko.observable('');
    self.ownerResponseId = ko.observable('');
    self.ownerResponseDate = ko.observable('');
    self.ownerResponseDays = ko.observable('');
    self.agencyResponseToDebtorDate = ko.observable('');
    self.totalResponseTimeDays = ko.observable('');
    self.debtorAgree = ko.observable('');
    self.needFurtherAction = ko.observable('');
    self.finalActionStepId = ko.observable('');
    self.createdBy = ko.observable(userId);

    self.complaintinfoDocumentChecked = ko.observable(false);
    self.debtOwnerProcessDocumentChecked = ko.observable(false);
    self.complaintDocument = ko.observable('');
    self.complaintDocUrl = ko.observable('');
    self.debtOwnerProcessDocument = ko.observable('');
    self.debtOwnerProcessDocUrl = ko.observable('');

    self.save = function () {
        log('saving')
        var json = JSON.stringify({
            AgencyId: self.agency(),
            Account: self.account(),
            LastName: self.lastName(),
            FirstName: self.firstName(),
            Address: self.address(),
            City: self.city(),
            State: self.state(),
            Zip: self.zip(),
            HomePhone: self.homePhone(),
            WorkPhone: self.workPhone(),
            MobilePhone: self.mobilePhone(),
            LastFourSSN: self.lastFourSSN(),
            DebtorIdentityVerified: self.debtorIdentityVerified(),
            ContactMethodId: self.contactMethodId(),
            ContactTimeId: self.contactTimeId(),
            CreditorName: self.creditorName(),
            DebtProductId: self.debtProductId(),
            DebtPurchaseBalance: Number(self.debtPurchaseBalance().replace(/[^0-9\.]+/g, "")),
            DebtCurrentBalance: Number(self.debtCurrentBalance().replace(/[^0-9\.]+/g, "")),
            DisputeDebt: self.disputeDebt(),
            DisputeDebtAmount: self.disputeDebtAmount(),
            DisputeDebtDueDate: self.disputeDebtDueDate(),
            ComplaintID: self.complaintID(),
            ComplaintDate: self.complaintDate(),
            ComplaintReceivedByMethodId: self.complaintReceivedByMethodId(),
            ComplaintIssueId: self.complaintIssueId(),
            ComplaintNotes: self.complaintNotes(),
            ComplaintSubmitedToAgency: self.complaintSubmitedToAgency(),
            ComplaintSubmitedToAgencyDate: self.complaintSubmitedToAgencyDate(),
            MoreInfoReqdFromDebtor: self.moreInfoReqdFromDebtor(),
            MoreInfoRequestedDate: self.moreInfoRequestedDate(),
            MoreInfoRequested: self.moreInfoRequested(),
            MoreInfoReceivedFromDebtor: self.moreInfoReceivedFromDebtor(),
            MoreInfoReceivedDate: self.moreInfoReceivedDate(),
            MoreInfoReceived: self.moreInfoReceived(),
            ComplaintSubmittedToOwner: self.complaintSubmittedToOwner(),
            ComplaintSubmittedDate: self.complaintSubmittedDate(),
            TimeToSubmitDays: self.timeToSubmitDays(),
            MoreInfoFromAgency: self.moreInfoFromAgency(),
            MoreInfoFromAgencyRequestedDate: self.moreInfoFromAgencyRequestedDate(),
            MoreInfoFromAgencyRequested: self.moreInfoFromAgencyRequested(),
            MoreInfoFromAgencyReceived: self.moreInfoFromAgencyReceived(),
            MoreInfoFromAgencyReceivedDate: self.moreInfoFromAgencyReceivedDate(),
            OwnerResponseId: self.ownerResponseId(),
            OwnerResponseDate: self.ownerResponseDate(),
            OwnerResponseDays: self.ownerResponseDays(),
            AgencyResponseToDebtorDate: self.agencyResponseToDebtorDate(),
            TotalResponseTimeDays: self.totalResponseTimeDays(),
            DebtorAgree: self.debtorAgree(),
            NeedFurtherAction: self.needFurtherAction(),
            FinalActionStepId: self.finalActionStepId(),
            CreatedBy: self.createdBy()
        });

        $.ajax({
            url: baseUrl + '/api/Compliance/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
            },
            error: function (response, errorText) {
                return false;
            }
        });
    }
}

function pimsDebtor(account, firstName, lastName, dob, address1, address2, city, state, zip, ssn, phoneCell, phoneHome, phoneWork, debtCurrentBalance, debtorPurchaseBalance, creditorName) {
    var self = this;
    self.account = ko.observable(account);
    self.firstName = ko.observable(firstName);
    self.lastName = ko.observable(lastName);
    self.dob = ko.observable(dob);
    self.address1 = ko.observable(address1);
    self.address2 = ko.observable(address2);
    self.city = ko.observable(city);
    self.state = ko.observable(state);
    self.zip = ko.observable(zip);
    self.ssn = ko.observable(ssn);
    self.phoneCell = ko.observable(phoneCell);
    self.phoneHome = ko.observable(phoneHome);
    self.phoneWork = ko.observable(phoneWork);
    self.debtCurrentBalance = ko.observable(debtCurrentBalance);
    self.debtorPurchaseBalance = ko.observable(debtorPurchaseBalance);
    self.creditorName = ko.observable(creditorName);
}