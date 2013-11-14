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
    self.investmentRecords = ko.observableArray([]);
    self.loadInvestments = function () {
        //var investmentRecords = [];
        var totalContributions = 0;
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/MSIPortfolioInvestmentsTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), isOriginal: 'true' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data != undefined) {
                        if (data.length > 0) {
                            self.currentRecordIndex(0);
                            $.each(data, function (i, item) {
                                totalContributions += item.SalesPrice;
                                self.investmentRecords.push(new investmentRecord(item.ID, self.portfolioNumber(), item.Inv_AgencyName, item.ProfitShare_before, item.ProfitShare_after, formatCurrency(item.SalesPrice), item.Interest, item.TransType, item.Notes));
                            });
                        }
                    }
                    else {
                        if (self.investmentRecords().length == 1) 
                            self.investmentRecords()[0].portfolioNumber(self.portfolioNumber());
                    }

                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        self.totalContributions(formatCurrency(totalContributions));
        //return investmentRecords;
    };

    self.currentRecordIndex = ko.observable(-1);
    self.currentInvestmentRecord = ko.computed(function () {
        if (self.currentRecordIndex() == -1) {
            return new investmentRecord('0', '', '', '', '', '', '', '', '');
        }
        return self.investmentRecords()[self.currentRecordIndex()];
    }, self);

    self.addNewInvestment = function () {
        self.investmentRecords().push(new investmentRecord('0', self.portfolioNumber(), '', '', '', '', '', '', ''));
        self.currentRecordIndex(self.investmentRecords().length-1);
    };

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
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.saveRecord = function () {
        //log(self.currentInvestmentRecord().contribution() + ',' + self.currentInvestmentRecord().investor());
        var json = JSON.stringify({
            Portfolio_: self.currentInvestmentRecord().portfolioNumber(),
            ProfitShare_after: self.currentInvestmentRecord().prftShareAftr(),
            ProfitShare_before: self.currentInvestmentRecord().prftShareBfr(),
            SalesPrice: self.currentInvestmentRecord().contribution().replace(/[^0-9.]/ig, ''),
            Interest: self.currentInvestmentRecord().interestRate(),
            Notes: self.currentInvestmentRecord().notes(),
            ID: self.currentInvestmentRecord().id,
            Inv_AgencyName: self.currentInvestmentRecord().investor(),
            TransType: 'Investment',
            IsOriginal: 'true'
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioInvestmentsTransactions/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                self.showMessage(true);
                self.message('Data saved successfully!');
                self.currentInvestmentRecord().id = response.ID;
            },
            error: function (response, errorText) {
            }
        });
    }
    self.resetFields = function () {
        self.investmentRecords([]);
    };
};
