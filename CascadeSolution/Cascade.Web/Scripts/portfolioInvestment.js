function investmentRecord(id, portfolioNumber, investor, prftShareBfr, prftShareAftr, contribution, interestRate, investmentTransType, notes) {
    this.id = id;
    this.portfolioNumber = ko.observable(portfolioNumber);
    this.investor = ko.observable(investor);
    this.prftShareBfr = ko.observable(prftShareBfr);
    this.prftShareAftr = ko.observable(prftShareAftr);
    this.contribution = ko.observable(contribution);
    this.interestRate = ko.observable(interestRate);
    this.investmentTransType = ko.observable(investmentTransType);
    this.notes = ko.observable(notes);
}

function investmentsTransVM(portfolioNumber) {
    var self = this;
    self.portfolioNumber = ko.observable('');
    self.totalContributions = ko.observable('');
    self.investmentRecords = ko.computed(function () {
        var investmentRecords = [];
        var totalContributions = 0;
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/PortfolioTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), transType: 'Investment' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    //log(data);
                    if (data.length > 0) {
                        self.currentRecordIndex(0);
                        $.each(data, function (i, item) {
                            totalContributions += item.SalesPrice;
                            investmentRecords.push(new investmentRecord(item.ID, self.portfolioNumber(), item.Inv_AgencyName, item.ProfitShare_before, item.ProfitShare_after,formatCurrency(item.SalesPrice), item.C_Interest, item.TransType,item.Notes));
                        });
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        self.totalContributions(formatCurrency(totalContributions));
        return investmentRecords;
    }, self);
    self.currentRecordIndex = ko.observable(0);
    self.currentInvestmentRecord = ko.computed(function () {
        if (self.investmentRecords.length > 0)
            return self.investmentRecords()[self.currentRecordIndex()];
        else
            return new investmentRecord('', '', '', '','','', '', '','');
    }, self);

    self.investors = ko.observableArray([]);
    $.each(portfolioViewModels.investors(), function (i, item) {
        self.investors.push(item);
    });
    self.investmentTransTypes = [{ Text: 'Sale', Value: 'Sale' }, { Text: 'Collection', Value: 'Collection' }, { Text: 'Distribution', Value: 'Distribution' }, { Text: 'Interest', Value: 'Interest' }, { Text: 'Investment', Value: 'Investment' }];
    self.visibleNext = ko.computed(function () {
        if (self.currentRecordIndex() + 1 != self.investmentRecords().length && self.investmentRecords().length > 1)
            return true;
        else
            return false;
    }, self);
    self.visiblePrevious = ko.computed(function () {
        if (self.currentRecordIndex() + 1 > 1 && self.investmentRecords().length > 1)
            return true;
        else
            return false;
    }, self);

    self.recordCounts = ko.computed(function () {
        return 'Records ' + (self.currentRecordIndex() + 1) + ' of ' + self.investmentRecords().length;
    }, self);

    self.nextRecord = function () {
        self.currentRecordIndex(self.currentRecordIndex() + 1);
    }
    self.previousRecord = function () {
        self.currentRecordIndex(self.currentRecordIndex() - 1);
    }
    self.saveRecord = function () {
        //log(self.currentInvestmentRecord().contribution() + ',' + self.currentInvestmentRecord().investor());
        var json = JSON.stringify({
            ProfitShare_after: self.currentInvestmentRecord().prftShareAftr(),
            ProfitShare_before: self.currentInvestmentRecord().prftShareBfr(),
            SalesPrice: self.currentInvestmentRecord().contribution().replace(/[^0-9.]/ig, ''),
            InterestRate: self.currentInvestmentRecord().interestRate(),
            Notes: self.currentInvestmentRecord().notes(),
            ID: self.currentInvestmentRecord().id,
            Inv_AgencyName: self.currentInvestmentRecord().investor(),
            TransType: 'Investment'
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
