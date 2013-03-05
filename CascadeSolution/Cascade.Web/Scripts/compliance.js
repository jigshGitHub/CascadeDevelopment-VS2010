function getFormatedDate(data) {
    if (data != null && data != '' && data != undefined) {
        return $.datepicker.formatDate('mm/dd/yy', new Date(data));
    }
    else
        return '';
}
function complianceVM(userId, userAgency) {
    var self = this;
    self.account = ko.observable('');
    self.agency = ko.observable(userAgency);
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
    self.moreInfoReqdFromDebtor_YesNo = ko.observable('');
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
    self.moreInfoFromAgencyReceived_YesNo = ko.observable('');
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

    self.moreInfoReqdFromDebtor_YesNo.subscribe(function (value) {
        if (value == 'true') {
            var todaysDate = new Date();
            todaysDate.setDate(todaysDate.getDate());
            self.moreInfoRequestedDate($.datepicker.formatDate('mm/dd/yy', todaysDate));
        }
        else {
            self.moreInfoRequestedDate('');
        }
    }, self);

    self.moreInfoReceivedFromDebtor.subscribe(function (value) {
        if (value == 'true') {
            var todaysDate = new Date();
            todaysDate.setDate(todaysDate.getDate());
            self.moreInfoReceivedDate($.datepicker.formatDate('mm/dd/yy', todaysDate));
        }
        else {
            self.moreInfoReceivedDate('');
        }
    }, self);

    self.complaintSubmittedToOwner.subscribe(function (value) {
        if (value == 'true') {
            var todaysDate = new Date();
            todaysDate.setDate(todaysDate.getDate());
            self.complaintSubmittedDate($.datepicker.formatDate('mm/dd/yy', todaysDate));
        }
        else {
            self.complaintSubmittedDate('');
        }
    }, self);

    self.moreInfoFromAgency.subscribe(function (value) {
        if (value == 'true') {
            var todaysDate = new Date();
            todaysDate.setDate(todaysDate.getDate());
            self.moreInfoFromAgencyRequestedDate($.datepicker.formatDate('mm/dd/yy', todaysDate));
        }
        else {
            self.moreInfoFromAgencyRequestedDate('');
        }
    }, self);

    self.moreInfoFromAgencyReceived_YesNo.subscribe(function (value) {
        if (value == 'true') {
            var todaysDate = new Date();
            todaysDate.setDate(todaysDate.getDate());
            self.moreInfoFromAgencyReceivedDate($.datepicker.formatDate('mm/dd/yy', todaysDate));
        }
        else {
            self.moreInfoFromAgencyReceivedDate('');
        }
    }, self);

    self.save = function () {
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
            MoreInfoReqdFromDebtor: self.moreInfoReqdFromDebtor_YesNo(),
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

        function setComplaint(data) {
            self.populateComplaint(data.Account, data.FirstName, data.LastName, data.DOB, data.Address, data.City, data.State, data.Zip, data.LastFourSSN, data.MobilePhone, data.HomePhone, data.WorkPhone, data.DebtCurrentBalance, data.DebtPurchaseBalance, data.CreditorName, data.DebtProductId, data.DebtPurchaseBalance, data.DebtCurrentBalance, data.DisputeDebt, data.DisputeDebtAmount, data.DisputeDebtDueDate, data.ComplaintID, data.ComplaintDate, data.ComplaintReceivedByMethodId, data.ComplaintIssueId, data.ComplaintNotes, data.ComplaintSubmitedToAgency, data.ComplaintSubmitedToAgencyDate, data.MoreInfoReqdFromDebtor, data.MoreInfoRequestedDate, data.MoreInfoRequested, data.MoreInfoReceivedFromDebtor, data.MoreInfoReceivedDate, data.MoreInfoReceived, data.ComplaintSubmittedToOwner, data.ComplaintSubmittedDate, data.TimeToSubmitDays, data.MoreInfoFromAgency, data.MoreInfoFromAgencyRequestedDate, data.MoreInfoFromAgencyRequested, data.MoreInfoFromAgencyReceived, data.MoreInfoFromAgencyReceivedDate, data.OwnerResponseId, data.OwnerResponseDate, data.OwnerResponseDays, data.AgencyResponseToDebtorDate, data.TotalResponseTimeDays, data.DebtorAgree, data.NeedFurtherAction, data.FinalActionStepId, data.CreatedBy);
        };

        $.ajax({
            url: baseUrl + '/api/Compliance/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                setComplaint(response);
            },
            error: function (response, errorText) {
                return false;
            }
        });
    }

    self.populateComplaint = function (account, firstName, lastName, dob, address, city, state, zip, ssn, phoneCell, phoneHome, phoneWork, debtCurrentBalance, debtorPurchaseBalance, creditorName, debtProductId, debtPurchaseBalance, debtCurrentBalance, disputeDebt, disputeDebtAmount, disputeDebtDueDate, complaintID, complaintDate, complaintMethodId, complaintIssueId, complaintNotes, complaintSubmitedToAgency, complaintSubmitedToAgencyDate, moreInfoReqdFromDebtor_YesNo, moreInfoRequestedDate, moreInfoRequested, moreInfoReceivedFromDebtor, moreInfoReceivedDate, moreInfoReceived, complaintSubmittedToOwner, complaintSubmittedDate, timeToSubmitDays, moreInfoFromAgency, moreInfoFromAgencyRequestedDate, moreInfoFromAgencyRequested, moreInfoFromAgencyReceived, moreInfoFromAgencyReceivedDate, ownerResponseId, ownerResponseDate, ownerResponseDays, agencyResponseToDebtorDate, totalResponseTimeDays, debtorAgree, needFurtherAction, finalActionStepId, createdBy) {
        self.account(account);
        self.firstName(firstName);
        self.lastName(lastName);
        self.dob(dob);
        self.address(address);
        self.city(city);
        self.state(state);
        self.zip(zip);
        self.lastFourSSN(ssn);
        self.mobilePhone(phoneCell);
        self.homePhone(phoneHome);
        self.workPhone(phoneWork);
        self.debtCurrentBalance(debtCurrentBalance);
        self.debtPurchaseBalance(debtorPurchaseBalance);
        self.creditorName(creditorName);
        self.debtProductId(debtProductId);
        self.debtPurchaseBalance(((debtPurchaseBalance == '') ? '' : formatCurrency(debtPurchaseBalance)));
        self.debtCurrentBalance(((debtCurrentBalance == '') ? '' : formatCurrency(debtCurrentBalance)));
        self.disputeDebt(disputeDebt);
        self.disputeDebtAmount(disputeDebtAmount);
        self.disputeDebtDueDate(disputeDebtDueDate);
        self.complaintID(complaintID);
        self.complaintDate(getFormatedDate(complaintDate));
        self.complaintReceivedByMethodId(complaintMethodId);
        self.complaintIssueId(complaintIssueId);
        self.complaintNotes(complaintNotes);
        self.complaintSubmitedToAgency(complaintSubmitedToAgency);
        self.complaintSubmitedToAgencyDate(getFormatedDate(complaintSubmitedToAgencyDate));
        self.moreInfoReqdFromDebtor_YesNo(moreInfoReqdFromDebtor_YesNo);
        self.moreInfoRequestedDate(getFormatedDate(moreInfoRequestedDate));
        self.moreInfoRequested(moreInfoRequested);
        self.moreInfoReceivedFromDebtor(moreInfoReceivedFromDebtor);
        self.moreInfoReceivedDate(getFormatedDate(moreInfoReceivedDate));
        self.moreInfoReceived(moreInfoReceived);
        self.complaintSubmittedToOwner();
        self.complaintSubmittedDate(getFormatedDate(complaintSubmittedDate));
        self.timeToSubmitDays(timeToSubmitDays);
        self.moreInfoFromAgency(moreInfoFromAgency);
        self.moreInfoFromAgencyRequestedDate(getFormatedDate(moreInfoFromAgencyRequestedDate));
        self.moreInfoFromAgencyRequested(moreInfoFromAgencyRequested);
        self.moreInfoFromAgencyReceived(moreInfoFromAgencyReceived);
        self.moreInfoFromAgencyReceivedDate(getFormatedDate(moreInfoFromAgencyReceivedDate));
        self.ownerResponseId(ownerResponseId);
        self.ownerResponseDate(getFormatedDate(ownerResponseDate));
        self.ownerResponseDays(ownerResponseDays);
        self.agencyResponseToDebtorDate(getFormatedDate(agencyResponseToDebtorDate));
        self.totalResponseTimeDays(totalResponseTimeDays);
        self.debtorAgree(debtorAgree);
        self.needFurtherAction(needFurtherAction);
        self.finalActionStepId(finalActionStepId);
        //self.createdBy(createdBy)
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