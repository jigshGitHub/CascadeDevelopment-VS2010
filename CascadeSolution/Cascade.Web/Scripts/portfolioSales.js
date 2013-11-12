function salesRecord(id, portfolioNumber, lender, buyer, cutoffDt, closingDt, putbackTerms, putbackDeadline, salesBasis, salesPrice, faceValue, accounts, salesBatch, notes) {

    var self = this;
    self.Id = id;
    self.portfolioNumber = ko.observable(portfolioNumber);
    self.lender = ko.observable(lender);
    self.buyer = ko.observable(buyer);
    self.cutoffDt = ko.observable(cutoffDt);
    self.closingDt = ko.observable(closingDt);
    self.putbackTerm = ko.observable(putbackTerms);
    self.putbackDeadline = ko.observable(putbackDeadline);
    self.salesBasis = ko.observable(salesBasis);
    self.salesPrice = ko.observable(salesPrice);
    self.faceValue = ko.observable(faceValue);
    self.accounts = ko.observable(accounts);
    self.salesBatch = ko.observable(salesBatch);
    self.notes = ko.observable(notes);

    self.putbackTerm.subscribe(function (termValue) {
        if (termValue != undefined) {
            self.setPutbackDeadline(termValue, self.cutoffDt());
        }
        else {
            self.putbackDeadline('');
        }
    } .bind(self));

    self.cutoffDt.subscribe(function (value) {
        self.setPutbackDeadline(self.putbackTerm(), value);
    } .bind(self));
    self.setPutbackDeadline = function (termValue,cutOffDt) {
        log(cutOffDt);
        if (cutOffDt != undefined && cutOffDt != '') {
            var putbackDeadline = new Date(cutOffDt);
            putbackDeadline.setDate(putbackDeadline.getDate() + Number(termValue));
            self.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
        }
    }
}


function salesTransVM(userId) {
    var self = this;
    self.userId = userId;
    self.portfolioNumber = ko.observable('');
    self.salesRecords = ko.observableArray();
    self.loadSalesRecords = function () {
        var batchIndex;
        var salesBatch;
        var cutOffDate;
        if (self.portfolioNumber() != '') {
            $.ajax({
                url: baseUrl + '/api/MSIPortfolioSalesTransactionsOriginal/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), userId: self.userId },
                dataType: 'json',
                async: false,
                success: function (data) {
                    //log(data.length);
                    if (data != null || data != undefined) {
                        if (data.length > 0) {
                            self.currentRecordIndex(0);
                            $.each(data, function (i, item) {
                                batchIndex = i + 1
                                salesBatch = self.portfolioNumber() + '-' + batchIndex;
                                cutOffDate = new Date(item.Cut_OffDate);
                                cutOffDate.setDate(cutOffDate.getDate() + 1);
                                self.salesRecords.push(new salesRecord(item.ID, self.portfolioNumber(), item.Lender, item.Buyer, ((item.Cut_OffDate == undefined) ? '' : $.datepicker.formatDate('mm/dd/yy', cutOffDate)), ((item.ClosingDate == undefined) ? '' : $.datepicker.formatDate('mm/dd/yy', new Date(item.ClosingDate))), item.PutbackTerm_days_, ((item.PutbackDeadline == undefined) ? '' : $.datepicker.formatDate('mm/dd/yy', new Date(item.PutbackDeadline))), item.SalesBasis, ((item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice)), ((item.FaceValue == undefined) ? '' : formatCurrency(item.FaceValue)), item.C_ofAccts, salesBatch, item.Notes));
                            });
                        }
                    }
                    else {
                        log('Creating new sales record for:' + self.portfolioNumber());
                        self.currentRecordIndex(0);
                        salesBatch = self.portfolioNumber() + '-1';
                        salesRecords.push(new salesRecord('', self.portfolioNumber(), '', '', '', '', '', '', '', '', '', '', salesBatch, ''));
                    }
                    //                    log(salesRecords);
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
    };

    self.putbackTerms = ko.observableArray([]);
    $.each(portfolioViewModels.putbackTerms(), function (i, item) {
        self.putbackTerms.push(item);
    });
    self.salesBatchSelected = ko.observable();
    self.salesBatchSelected.subscribe(function (value) {
        if (value != undefined) {
            var startIndex = value.indexOf('-');
            var totalChars = value.length - startIndex;
            var index = value.substr(startIndex + 1, totalChars);
            self.currentRecordIndex(index - 1);
        }
    } .bind(self));
    self.getsalesBatchSelected = function (index) {
        //log(index);
        return self.portfolioNumber() + '-' + index;
    }
    self.portfolioSalesEditableFields = ko.observable(false);
    self.addNewSales = function () {
        self.portfolioSalesEditableFields(true);
        var batchIndex = self.salesRecords().length + 1;
        var salesBatch = self.portfolioNumber() + '-' + batchIndex;
        log('salesBatch:' + salesBatch);
        self.salesRecords().push(new salesRecord('', self.portfolioNumber(), '', '', '', '', '', '', '', '', '', '', salesBatch, ''));
        self.currentRecordIndex(self.salesRecords().length - 1);
    };
    self.currentRecordIndex = ko.observable(0);
    self.currentSalesRecord = ko.computed(function () {
        log(self.currentRecordIndex()); 
        log(self.salesRecords());
        if (self.salesRecords().length == 0)
            return new salesRecord('0', '', '', '', '', '', '', '', '', '', '', '', '', '');
        return self.salesRecords()[self.currentRecordIndex()];
    }, self);
    self.buyers = ko.observableArray([]);

    $.each(portfolioViewModels.buyers(), function (i, item) {
        self.buyers.push(item);
    });
    self.visibleNext = ko.computed(function () {
        if (self.currentRecordIndex() + 1 != self.salesRecords().length && self.salesRecords().length > 1)
            return true;
        else
            return false;
    }, self);
    self.visiblePrevious = ko.computed(function () {
        if (self.currentRecordIndex() + 1 > 1 && self.salesRecords().length > 1)
            return true;
        else
            return false;
    }, self);

    self.recordCounts = ko.computed(function () {
        return 'Portfolio Sales ' + (self.currentRecordIndex() + 1) + ' of ' + self.salesRecords().length;
    }, self);

    self.nextRecord = function () {
        self.currentRecordIndex(self.currentRecordIndex() + 1);
        self.salesBatchSelected(self.getsalesBatchSelected(self.currentRecordIndex() + 1));
    }
    self.previousRecord = function () {
        var setIndex = self.currentRecordIndex();
        self.currentRecordIndex(self.currentRecordIndex() - 1);
        self.salesBatchSelected(self.getsalesBatchSelected(setIndex));
    }
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.saveRecord = function () {
        //log(self.currentSalesRecord().accounts());
        //log(self.currentSalesRecord().lender());
        //log(self.currentSalesRecord().buyer());
        //log(self.currentSalesRecord().cutoffDt());
        //log(self.currentSalesRecord().closingDt());
        //log(self.currentSalesRecord().salesBasis());
        //log(self.currentSalesRecord().faceValue());
        //log(self.currentSalesRecord().putbackTerm());
        //log(self.currentSalesRecord().putbackDeadline());
        //log(self.currentSalesRecord().salesPrice());
        var json = JSON.stringify({
            Portfolio_: self.portfolioNumber(),
            PutbackDeadline: self.currentSalesRecord().putbackDeadline(),
            PutbackTerm_days_: self.currentSalesRecord().putbackTerm(),
            C_ofAccts: self.currentSalesRecord().accounts(),
            FaceValue: Number(self.currentSalesRecord().faceValue().replace(/[^0-9\.]+/g, "")),
            SalesBasis: self.currentSalesRecord().salesBasis(),
            SalesPrice: Number(self.currentSalesRecord().salesPrice().replace(/[^0-9\.]+/g, "")),
            Buyer: self.currentSalesRecord().buyer(),
            Lender: self.currentSalesRecord().lender(),
            ClosingDate: self.currentSalesRecord().closingDt(),
            Cut_OffDate: self.currentSalesRecord().cutoffDt(),
            ID: self.currentSalesRecord().Id,
            Notes: self.currentSalesRecord().notes(),
            CreatedBy: self.userId,
            UpdatedBy: self.userId
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioSalesTransactionsOriginal/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                log(response);
                self.showMessage(true);
                self.message('Data saved successfully!');
                self.currentSalesRecord().Id = response.ID;
                //                if (self.currentSalesRecord().Id == '') {
                //                    self.currentSalesRecord().lender('');
                //                    self.currentSalesRecord().buyer(undefined);
                //                    self.currentSalesRecord().cutoffDt('');
                //                    self.currentSalesRecord().closingDt('');
                //                    self.currentSalesRecord().putbackTerm(undefined);
                //                    self.currentSalesRecord().putbackDeadline('');
                //                    self.currentSalesRecord().salesBasis('');
                //                    self.currentSalesRecord().salesPrice('');
                //                    self.currentSalesRecord().faceValue('');
                //                    self.currentSalesRecord().accounts('');
                //                    self.currentSalesRecord().notes('');
                //                }
            },
            error: function (response, errorText) {
            }
        });
    }
    self.resetFields = function () {
        self.portfolioNumber('');
        self.currentSalesRecord().Id = undefined;
        self.currentSalesRecord().portfolioNumber('');
        self.currentSalesRecord().lender('');
        self.currentSalesRecord().buyer(undefined);
        self.currentSalesRecord().cutoffDt('');
        self.currentSalesRecord().closingDt('');
        self.currentSalesRecord().putbackTerm(undefined);
        self.currentSalesRecord().putbackDeadline('');
        self.currentSalesRecord().salesBasis('');
        self.currentSalesRecord().salesPrice('');
        self.currentSalesRecord().faceValue('');
        self.currentSalesRecord().accounts('');
        self.currentSalesRecord().salesBatch('');
        self.currentSalesRecord().notes('');
        self.currentRecordIndex(0);
        self.salesBatchSelected(undefined);
    }
};

