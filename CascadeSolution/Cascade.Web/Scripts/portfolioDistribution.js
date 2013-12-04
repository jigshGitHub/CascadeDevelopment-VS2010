function distributionRecord(id,portfolioNumber,investor,checkNumber,closingDt,distributionTransType,distribution,notes) {
    this.id = id,
    this.portfolioNumber = ko.observable(portfolioNumber);
    this.investor = ko.observable(investor);
    this.checkNumber = ko.observable(checkNumber);
    this.closingDt = ko.observable(closingDt);
    this.distributionTransType = ko.observable(distributionTransType);
    this.distribution = ko.observable(distribution);
    this.notes = ko.observable(notes);
}
function distributionsTransVM(portfolioNumber) {
    var self = this;
    self.portfolioNumber = ko.observable(portfolioNumber);
    self.totalDistributions = ko.observable('');
    self.distributionRecords = ko.observableArray([]);
    self.loadDistribution = function () {
        var totalDistributions = 0;
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/MSIPortfolioDistributionTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), isOriginal: 'true' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data != undefined) {
                        if (data.length > 0) {
                            $.each(data, function (i, item) {
                                totalDistributions += item.SalesPrice;
                                self.distributionRecords.push(new distributionRecord(item.ID, self.portfolioNumber(), item.Inv_AgencyName, item.Check_, item.ClosingDate, item.TransType, formatCurrency(item.SalesPrice), item.Notes));
                            }); 
                            self.currentRecordIndex(0);
                        }
                    }
                    else {
                        if (self.distributionRecords().length == 1)
                            self.distributionRecords()[0].portfolioNumber(self.portfolioNumber());
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        self.totalDistributions(formatCurrency(totalDistributions));
    };
    self.currentRecordIndex = ko.observable(-1);
    self.currentDistributionRecord = ko.computed(function () {
        if (self.currentRecordIndex() == -1) 
            return new distributionRecord('0', '', '', '', '', '', '','');
        else
            return self.distributionRecords()[self.currentRecordIndex()];

    }, self);

    self.addNewDistribution = function () {
        self.distributionRecords().push(new distributionRecord('0',self.portfolioNumber(), '', '', '', '', '',''));
        self.currentRecordIndex(self.distributionRecords().length - 1);
    };

    self.investorName = ko.observable('');
    self.investors = ko.observableArray([]);
    $.each(portfolioViewModels.investors(), function (i, item) {
        self.investors.push(item);
    });
    self.distributionTransTypes = [{ Text: 'Sale', Value: 'Sale' }, { Text: 'Collection', Value: 'Collection' }, { Text: 'Distribution', Value: 'Distribution' }, { Text: 'Interest', Value: 'Interest' }, { Text: 'Investment', Value: 'Investment' }];
    self.visibleNext = ko.computed(function () {
        if (self.currentRecordIndex() + 1 != self.distributionRecords().length && self.distributionRecords().length > 1)
            return true;
        else
            return false;
    }, self);
    self.visiblePrevious = ko.computed(function () {
        if (self.currentRecordIndex() + 1 > 1 && self.distributionRecords().length > 1)
            return true;
        else
            return false;
    }, self);

    self.recordCounts = ko.computed(function () {
        return 'Records ' + (self.currentRecordIndex() + 1) + ' of ' + self.distributionRecords().length;
    }, self);

    self.nextRecord = function () {
        self.currentRecordIndex(self.currentRecordIndex() + 1);
    }
    self.previousRecord = function () {
        self.currentRecordIndex(self.currentRecordIndex() - 1);
    }
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.saveRecord = function () {
        //log(self.currentdistributionRecord().contribution() + ',' + self.currentdistributionRecord().investor());
        var json = JSON.stringify({
            Check_:self.currentDistributionRecord().checkNumber(),
            SalesPrice: self.currentDistributionRecord().distribution().replace(/[^0-9.]/ig, ''),
            Notes: self.currentDistributionRecord().notes(),
            ID: self.currentDistributionRecord().id,
            Inv_AgencyName: self.currentDistributionRecord().investor(),
            ClosingDate:self.currentDistributionRecord().closingDt()
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioDistributionTransactions/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                self.showMessage(true);
                self.message('Data saved successfully!');
                self.currentDistributionRecord().id = response.ID;
            },
            error: function (response, errorText) {
            }
        });
    }

    self.resetFields = function () {
        self.distributionRecords([]);
        self.currentRecordIndex(-1);
    };
};
