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
    self.distributionRecords = ko.computed(function () {
        var distributionRecords = [];
        var totalDistributions = 0;
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/PortfolioTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), transType: 'Distribution' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        self.currentRecordIndex(0);
                        $.each(data, function (i, item) {
                            totalDistributions += item.SalesPrice;
                            distributionRecords.push(new distributionRecord(item.ID,self.portfolioNumber(),item.Inv_AgencyName,item.Check_,item.ClosingDate,item.TransType,formatCurrency(item.SalesPrice),item.Notes));
                        });
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        self.totalDistributions(formatCurrency(totalDistributions));
        return distributionRecords;
    }, self);
    self.currentRecordIndex = ko.observable(0);
    self.currentDistributionRecord = ko.computed(function () {
        if (self.distributionRecords().length > 0)
            return self.distributionRecords()[self.currentRecordIndex()];
        else
            return new distributionRecord('', '', '', '', '', '', '','');
    }, self);
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
    self.saveRecord = function () {
        //log(self.currentdistributionRecord().contribution() + ',' + self.currentdistributionRecord().investor());
        var json = JSON.stringify({
            Check_:self.currentDistributionRecord().checkNumber(),
            SalesPrice: self.currentDistributionRecord().distribution().replace(/[^0-9.]/ig, ''),
            Notes: self.currentDistributionRecord().notes(),
            ID: self.currentDistributionRecord().id,
            Inv_AgencyName: self.currentDistributionRecord().investor(),
            ClosingDate:self.currentDistributionRecord().closingDt(),
            TransType: 'Distribution'
        });

        $.ajax({
            url: baseUrl + '/api/PortfolioTransactions/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                log(response);
            },
            error: function (response, errorText) {
            }
        });
    }
};
