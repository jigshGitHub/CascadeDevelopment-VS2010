function interestRecord(id, portfolioNumber, agencyName, checkNumber, closingDt, salesPrice, transType) {
    this.id = id;
    this.portfolioNumber = ko.observable(portfolioNumber);
    this.agencyName = ko.observable(agencyName);
    this.checkNumber = ko.observable(checkNumber);
    this.closingDt = ko.observable(closingDt);
    this.salesPrice = ko.observable(salesPrice);
    this.interestTransType = ko.observable(transType);
};

function interestTransVM() {
    var self = this;
    self.portfolioNumber = ko.observable();
    self.editMode = ko.observable(false);
    self.interestRecords = ko.observableArray([]);
    self.loadInterest = function () {
        if (self.interestRecords().length > 0)
            self.interestRecords([]);
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/MSIPortfolioInterestTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), isOriginal: 'true' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data != undefined) {
                        if (data.length > 0) {
                            $.each(data, function (i, item) {
                                self.interestRecords.push(new interestRecord(item.ID, item.Portfolio_,
                                item.Inv_AgencyName, item.Check_,
                                dateFormat(Date.parse(item.ClosingDate), 'mm/dd/yyyy'),
                                (item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice),
                                'Interest'));
                            });
                        }
                    }                    
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
    };

    self.currentRecordIndex = ko.observable(0);
    self.addNewInterest = function () {
        self.currentInterestRecord(new interestRecord('0', self.portfolioNumber(), '', '', '', '','interest'));
    };
    self.selectedRecordIndexChanged = function (item) {
        self.currentInterestRecord(item);
        $.each(self.agencies(), function (i, agencyRecord) {
            if(agencyRecord.Text === item.agencyName())
                self.selectedAgency(agencyRecord.Value);
        });
    };
    self.selectedAgency = ko.observable();
    self.currentInterestRecord = ko.observable();
    self.agencies = ko.observableArray([]);
    self.interestTransTypes = [{ Text: 'Sale', Value: 'Sale' }, { Text: 'Collection', Value: 'Collection' }, { Text: 'Distribution', Value: 'Distribution' }, { Text: 'Interest', Value: 'Interest' }, { Text: 'Investment', Value: 'Investment' }];
    self.totalInterest = ko.computed(function () {
        var salesTotal = 0;
        var val;
        $.each(self.interestRecords(), function (i, item) {
            val = item.salesPrice().replace(/[^0-9.]/ig, '');
            salesTotal += parseFloat(val) || 0;
        });
        return formatCurrency(salesTotal);
    }, self);
    $.each(portfolioViewModels.agencies(), function (i, item) {
        self.agencies.push(item);
    });

    self.saveRecord = function () {
        //log(self.currentInterestRecord().faceValue());
        //log(self.currentInterestRecord().netColls());
        //log(self.currentInterestRecord().closingDt());
        //log(self.currentInterestRecord().interestAgencyName());
        //log(self.currentInterestRecord().id);
        //log(self.selectedAgency());
        var agency = '';
        $.each(self.agencies(), function (i, agencyRecord) {
            if (agencyRecord.Value === self.selectedAgency())
                agency = agencyRecord.Text;
        });
        log(agency);
        var json = JSON.stringify({
            Portfolio_: self.currentInterestRecord().portfolioNumber(),
            SalesPrice: self.currentInterestRecord().salesPrice().replace(/[^0-9.]/ig, ''),
            ClosingDate: self.currentInterestRecord().closingDt(),
            Inv_AgencyName: agency,
            Check_:self.currentInterestRecord().checkNumber(),
            ID: self.currentInterestRecord().id,
            TransType: 'Interest',
            IsOriginal: 'true'
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioInterestTransactions/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                self.loadInterest();
                self.resetFields();
            },
            error: function (response, errorText) {
            }
        });
    }
    self.resetFields = function () {
        //self.currentCollectionRecord().portfolioNumber(''),
        if (self.currentInterestRecord() != undefined) {
            self.currentInterestRecord().salesPrice('');
            self.currentInterestRecord().closingDt('');
            self.currentInterestRecord().id=0;
            self.selectedAgency('');
        }
    };
};