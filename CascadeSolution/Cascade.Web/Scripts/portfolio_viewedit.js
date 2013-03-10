var portfolioViewModels = namespace("cascade.viewModels.portfolio");
//var applicationname = "/Cascade";
var applicationname = "";
var myhost = window.location.protocol + "//" + window.location.host
var absoluteapp = myhost + applicationname;
var imagedir = "/Content/Images";

function salesRecord(id, portfolioNumber, lender, buyer, cutoffDt, closingDt, putbackTerms, putbackDeadline, salesBasis, salesPrice, faceValue, accounts, salesBatch, notes, createdBy, updatedBy) {

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
    self.recordCreatedBy = ko.observable(createdBy);
    self.recordUpdatedBy = ko.observable(updatedBy);
    self.putbackTerm.subscribe(function (termValue) {
        if (termValue != undefined && self.cutoffDt() != undefined && self.cutoffDt() != '') {
            var putbackDeadline = new Date(self.cutoffDt());
            putbackDeadline.setDate(putbackDeadline.getDate() + Number(termValue));
            self.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
        }
        else {
            self.putbackDeadline('');
        }
    } .bind(self));
}

function salesTransVM(userId) {
    var self = this;
    self.userId = userId;
    self.portfolioNumber = ko.observable('');
    self.salesRecords = ko.computed(function () {
        var salesRecords = [];
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
                    if (data.length > 0) {
                        self.currentRecordIndex(0);
                        $.each(data, function (i, item) {
                            batchIndex = i + 1
                            salesBatch = self.portfolioNumber() + '-' + batchIndex;
                            cutOffDate = new Date(item.Cut_OffDate);
                            cutOffDate.setDate(cutOffDate.getDate() + 1);
                            salesRecords.push(new salesRecord(item.ID, self.portfolioNumber(), item.Lender, item.Buyer, ((item.Cut_OffDate == undefined) ? '' : $.datepicker.formatDate('mm/dd/yy', cutOffDate)), ((item.ClosingDate == undefined) ? '' : $.datepicker.formatDate('mm/dd/yy', new Date(item.ClosingDate))), item.PutbackTerm_days_, ((item.PutbackDeadline == undefined || item.PutbackDeadline == null) ? '' : $.datepicker.formatDate('mm/dd/yy', new Date(item.PutbackDeadline))), item.SalesBasis, ((item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice)), ((item.FaceValue == undefined) ? '' : formatCurrency(item.FaceValue)), item.C_ofAccts, salesBatch, item.Notes, item.CreatedBy, item.UpdatedBy));
                        });
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        return salesRecords;
    }, self);
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
            self.originalData(true);
            self.updatedData(false);
        }
    } .bind(self));
    self.getsalesBatchSelected = function (index) {
        //log(index);
        return self.portfolioNumber() + '-' + index;
    }
    self.currentRecordIndex = ko.observable(0);
    self.currentSalesRecord = ko.computed(function () {
        if (self.portfolioNumber() != '')
            return self.salesRecords()[self.currentRecordIndex()];
        else
            return new salesRecord('', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '');
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
            url: baseUrl + '/api/MSIPortfolioSalesTransactionsEdited/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                //log(response);
                self.getUpdated();
                self.saveVisible(false);
                self.showMessage(true);
                self.editVisible(true);
                self.message('Data saved successfully!');
            },
            error: function (response, errorText) {
            }
        });
    }
    self.saveVisible = ko.observable(false);
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
    self.saveVisible = ko.observable(false);
    self.makeFieldsEditable = ko.observable(false);
    self.editVisible = ko.observable(false);
    self.editRecord = function () {
        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioSalesTransactionsEdited/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: self.currentSalesRecord().Id },
            dataType: 'json',
            async: false,
            success: function (data) {
                //log(data);
                loadSalesData(data);

                self.makeFieldsEditable(true);
                self.saveVisible(true);
                self.editVisible(false);
                self.originalData(false);
                self.updatedData(false);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    self.originalData = ko.observable(false);
    self.updatedData = ko.observable(false);

    self.radiosVisible = ko.observable(false);
    self.lenderEditedValue = ko.observable();
    self.buyerEditedValue = ko.observable();
    self.cuttOfDateEditedValue = ko.observable();
    self.putbackTermEditedValue = ko.observable();
    self.putbackDeadlineEditedValue = ko.observable();
    self.notesEditedValue = ko.observable();

    loadSalesData = function (data) {
        self.currentSalesRecord().portfolioNumber(data.Portfolio_);
        self.currentSalesRecord().buyer(data.Buyer);
        self.currentSalesRecord().lender(data.Lender);
        if (data.Cut_OffDate != null) {
            var cutOffDate = new Date(data.Cut_OffDate);
            cutOffDate.setDate(cutOffDate.getDate() + 1);
            self.currentSalesRecord().cutoffDt($.datepicker.formatDate('mm/dd/yy', cutOffDate));
        }
        self.currentSalesRecord().salesBasis(data.SalesBasis);
        log('data.PutbackTerm__days_=' + data.PutbackTerm_days_);
        self.currentSalesRecord().putbackTerm(data.PutbackTerm_days_);
        if (data.PutbackDeadline == undefined) {
        }
        else {
            var putbackDeadline = new Date(data.PutbackDeadline);
            putbackDeadline.setDate(putbackDeadline.getDate() + 1);
            self.currentSalesRecord().putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
        }
        if (data.Face != undefined)
            self.currentSalesRecord().faceValue(formatCurrency(data.FaceValue));
        self.currentSalesRecord().accounts(data.C_ofAccts);
        if (data.PurchasePrice != undefined)
            self.currentSalesRecord().salesPrice(formatCurrency(data.SalesPrice)); //formatCurrency({ colorize: true, negativeFormat: '(%s%n)' });
        self.currentSalesRecord().Id = data.ID;
        self.currentSalesRecord().notes(data.Notes);
        self.currentSalesRecord().recordCreatedBy(data.CreatedBy);
        self.currentSalesRecord().recordUpdatedBy(data.UpdatedBy);
    }

    self.getOriginal = function () {
        log('getting original');
        self.radiosVisible(false);
        $("#loading").dialog('open');
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");

        self.buyerEditedValue('');
        self.lenderEditedValue('');
        self.cuttOfDateEditedValue('');
        self.putbackTermEditedValue('');
        self.notesEditedValue('')

        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioSalesTransactionsOriginal/Details/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: self.currentSalesRecord().Id },
            dataType: 'json',
            async: true,
            success: function (data) {
                log(data);
                loadSalesData(data);
                self.updatedData(false);
                self.originalData(true);
                $("#loading").html("&nbsp;");
                $("#loading").dialog('close');
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    self.getUpdated = function () {
        log('getting updated');
        self.radiosVisible(true);
        $("#loading").dialog('open');
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioSalesTransactionsEdited/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: self.currentSalesRecord().Id },
            dataType: 'json',
            async: true,
            success: function (data) {
                //log(data);
                if (data == null) {
                    self.message('Record has not been updated yet');
                    self.showMessage(true);
                }
                else {
                    if (self.currentSalesRecord().buyer() != data.Buyer)
                        self.buyerEditedValue('buyer');
                    if (self.currentSalesRecord().lender() != data.Lender)
                        self.lenderEditedValue('lender');
                    var cutOffDate = new Date(data.Cut_OffDate);
                    cutOffDate.setDate(cutOffDate.getDate() + 1);
                    //log(self.currentSalesRecord().cutoffDt());
                    //log($.datepicker.formatDate('mm/dd/yy', cutOffDate))
                    if (self.currentSalesRecord().cutoffDt() != $.datepicker.formatDate('mm/dd/yy', cutOffDate))
                        self.cuttOfDateEditedValue('cutOffDt');
                    if (self.currentSalesRecord().putbackTerm() != data.PutbackTerm_days_)
                        self.putbackTermEditedValue('putbackTerm');
                    if (self.currentSalesRecord().notes() != data.Notes)
                        self.notesEditedValue('notesEdited')

                    loadSalesData(data);
                    self.updatedData(true);
                    self.originalData(false);
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
};

function purchaseSummaryVM(userId) {
    var self = this;
    self.userId = userId;
    self.portfolioNumber = ko.observable('');
    self.company = ko.observable('');
    self.resaleRestrictions = ko.observableArray([]);
    $.each(portfolioViewModels.resaleRestrictions(), function (i, item) {
        self.resaleRestrictions.push(item);
    });
    self.resaleRestriction = ko.observable('');
    self.companies = ko.observableArray([]);
    self.seller = ko.observable('');
    self.lender = ko.observable('');
    self.purchaseDate = ko.observable('');
    //self.sellers = ko.observableArray([]);
    self.cutOffDt = ko.observable('');
    self.closingDt = ko.observable('');
    self.costBasis = ko.observable('');
    self.putbackTerms = ko.observableArray([]);
    $.each(portfolioViewModels.putbackTerms(), function (i, item) {
        self.putbackTerms.push(item);
    });
    self.putbackTerm = ko.observable('');
    self.putbackDeadline = ko.observable('');
    //self.putbackDeadline = ko.computed(function () {
    //    log(self.putbackTerm());
    //    if (self.putbackTerm() == undefined) {
    //        //log(self.purchaseDate());
    //    }
    //    else {
    //        var putbackDeadline = new Date(self.purchaseDate());
    //        putbackDeadline.setDate(putbackDeadline.getDate() + self.putbackTerm());
    //        log(putbackDeadline);
    //        //self.purchaseSummarySectionVM.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
    //    }
    //}, self);
    self.putbackTerm.subscribe(function (termValue) {
        if (termValue != undefined) {
            var putbackDeadline = new Date(self.purchaseDate());
            putbackDeadline.setDate(putbackDeadline.getDate() + Number(termValue));
            self.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
        }
        else {
            self.putbackDeadline('');
        }
    } .bind(self));
    self.face = ko.observable('');
    self.accounts = ko.observable('');
    self.purchasePrice = ko.observable('');
    self.notes = ko.observable('');
    self.recordCreatedBy = ko.observable('');
    self.recordUpdatedBy = ko.observable('');
    self.transType = ko.observable('');
    self.transTypes = ko.observableArray([]);
    //self.sellers = ko.observableArray([]);
    $.each(portfolioViewModels.supBrockettCompanies(), function (i, item) {
        self.companies.push(item);
    });
    //$.each(portfolioViewModels.sellers(), function (i, item) {
    //    self.sellers.push(item);
    //});
    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.saveMainRecord = function () {
        var json = JSON.stringify({
            Company: self.company(),
            PutbackDeadline: self.putbackDeadline(),
            PutbackTerm__days_: self.putbackTerm(),
            C_ofAccts: self.accounts(),
            Face: Number(self.face().replace(/[^0-9\.]+/g, "")),
            PurchasePrice: Number(self.purchasePrice().replace(/[^0-9\.]+/g, "")),
            Seller: self.seller(),
            Lender_FileDescription: self.lender(),
            ClosingDate: self.closingDt(),
            Cut_OffDate: self.cutOffDt(),
            CostBasis: self.costBasis(),
            Portfolio_: self.portfolioNumber(),
            ResaleRestrictionId: self.resaleRestriction(),
            Notes: self.notes(),
            CreatedBy: self.userId,
            UpdatedBy: self.userId
        });
        //log(json);
        $.ajax({
            //url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioEdited/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                //log(response);
                self.getUpdated();
                self.saveVisible(false);
                self.editVisible(true);
            },
            error: function (response, errorText) {
            }
        });
        self.showMessage(true);
        self.message('Data saved successfully!');
    }
    self.resetFields = function () {
        self.portfolioNumber('');
        self.company('');
        self.resaleRestriction(undefined);
        self.seller('');
        self.lender('');
        self.purchaseDate('');
        self.cutOffDt('');
        self.closingDt('');
        self.costBasis('');
        self.putbackTerm(undefined);
        self.putbackDeadline('');
        self.face('');
        self.accounts('');
        self.purchasePrice('');
        self.notes('');
        self.recordCreatedBy('');
        self.recordUpdatedBy('');
    }
    self.saveVisible = ko.observable(false);
    self.makeFieldsEditable = ko.observable(false);
    self.editVisible = ko.observable(false);
    self.editRecord = function () {
        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioEdited/',
            type: 'GET',
            contentType: 'application/json',
            data: { portfolioNumber: self.portfolioNumber() },
            dataType: 'json',
            async: false,
            success: function (data) {
                //log(data);
                loadPurchaseData(data);

                self.makeFieldsEditable(true);
                self.saveVisible(true);
                self.editVisible(false);
                self.originalData(false);
                self.updatedData(false);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    self.originalData = ko.observable(false);
    self.updatedData = ko.observable(false);

    self.radiosVisible = ko.observable(false);
    self.companyEditedValue = ko.observable();
    self.resaleEditedValue = ko.observable();
    self.lenderFileEditedValue = ko.observable();
    self.sellerEditedValue = ko.observable();
    self.cuttOfDateEditedValue = ko.observable();
    self.putbackTermEditedValue = ko.observable();
    self.putbackDeadlineEditedValue = ko.observable();
    self.notesEditedValue = ko.observable();

    loadPurchaseData = function (data) {
        self.portfolioNumber(data.Portfolio_);
        self.company(data.Company);
        self.seller(data.Seller);
        self.lender(data.Lender_FileDescription);
        var cutOffDate = new Date(data.Cut_OffDate);
        cutOffDate.setDate(cutOffDate.getDate() + 1);
        self.cutOffDt($.datepicker.formatDate('mm/dd/yy', cutOffDate));
        self.costBasis(data.CostBasis);
        self.putbackTerm(data.PutbackTerm__days_);
        if (data.PutbackDeadline == undefined) {
        }
        else {
            var putbackDeadline = new Date(data.PutbackDeadline);
            putbackDeadline.setDate(putbackDeadline.getDate() + 1);
            self.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
        }
        self.purchaseDate($.datepicker.formatDate('mm/dd/yy', cutOffDate));
        if (data.Face != undefined)
            self.face(formatCurrency(data.Face));
        self.accounts(data.C_ofAccts);
        if (data.PurchasePrice != undefined)
            self.purchasePrice(formatCurrency(data.PurchasePrice)); //formatCurrency({ colorize: true, negativeFormat: '(%s%n)' });
        self.resaleRestriction(data.ResaleRestrictionId);
        self.notes(data.Notes);
        self.recordCreatedBy(data.CreatedBy);
        self.recordUpdatedBy(data.UpdatedBy);
    }
    self.getOriginal = function () {
        log('getting original');
        self.radiosVisible(false);
        $("#loading").dialog('open');
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
        self.companyEditedValue('');
        self.resaleEditedValue('');
        self.lenderFileEditedValue('');
        self.sellerEditedValue('');
        self.cuttOfDateEditedValue('');
        self.putbackTermEditedValue('');
        self.putbackDeadlineEditedValue('');
        self.notesEditedValue('');
        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioOriginal/',
            type: 'GET',
            contentType: 'application/json',
            data: { portfolioNumber: self.portfolioNumber() },
            dataType: 'json',
            async: true,
            success: function (data) {
                //log(data);
                loadPurchaseData(data);
                self.updatedData(false);
                self.originalData(true);
                $("#loading").html("&nbsp;");
                $("#loading").dialog('close');
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    self.getUpdated = function () {
        log('getting updated');
        self.radiosVisible(true);
        $("#loading").dialog('open');
        $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");
        $.ajax({
            //                url: baseUrl + '/api/Portfolio/',
            url: baseUrl + '/api/MSIPortfolioEdited/',
            type: 'GET',
            contentType: 'application/json',
            data: { portfolioNumber: self.portfolioNumber() },
            dataType: 'json',
            async: true,
            success: function (data) {
                //log(data);
                if (data == null) {
                    self.message('Record has not been updated yet');
                    self.showMessage(true);
                }
                else {
                    if (self.company() != data.Company)
                        self.companyEditedValue('company');
                    if (self.seller() != data.Seller)
                        self.sellerEditedValue('seller');
                    if (self.lender() != data.Lender_FileDescription)
                        self.lenderFileEditedValue('lender');
                    var cutOffDate = new Date(data.Cut_OffDate);
                    cutOffDate.setDate(cutOffDate.getDate() + 1);
                    if (self.cutOffDt() != $.datepicker.formatDate('mm/dd/yy', cutOffDate))
                        self.cuttOfDateEditedValue('cutOffDt');
                    if (self.putbackTerm() != data.PutbackTerm__days_)
                        self.putbackTermEditedValue('putbackTerm');
                    if (self.resaleRestriction() != data.ResaleRestrictionId)
                        self.resaleEditedValue('reSale');
                    if (self.notes() != data.Notes)
                        self.notesEditedValue('notesEdited')

                    loadPurchaseData(data);

                    self.updatedData(true);
                    self.originalData(false);
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');
                }
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
};

function portfolioVM(userId) {
    var self = this;
    self.portfolios = ko.observableArray([]);

    $.each(portfolioViewModels.portfolios(), function (i, item) {
        self.portfolios.push(item);
    });
    self.portfolioNumber = ko.observable();
    self.portfolioNumber.subscribe(function (number) {
        if (number != undefined) {
            $("#loading").dialog('open');
            $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");

            self.portfolioNumber(number);
            //load acqusitions portfolio data

            $.ajax({
                //                url: baseUrl + '/api/Portfolio/',
                url: baseUrl + '/api/MSIPortfolioOriginal/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber() },
                dataType: 'json',
                async: true,
                success: function (data) {
                    //log(data);
                    self.purchaseSummarySectionVM.portfolioNumber(data.Portfolio_);
                    self.purchaseSummarySectionVM.company(data.Company);
                    self.purchaseSummarySectionVM.seller(data.Seller);
                    self.purchaseSummarySectionVM.lender(data.Lender_FileDescription);
                    //self.purchaseSummarySectionVM.resaleRestriction(data.ResaleRestriction);
                    //self.purchaseDate(data.PurchaseDate);
                    var cutOffDate = new Date(data.Cut_OffDate);
                    cutOffDate.setDate(cutOffDate.getDate() + 1);

                    //self.purchaseSummarySectionVM.cutOffDt($.datepicker.formatDate('mm/dd/yy', new Date(data.Cut_OffDate)));
                    self.purchaseSummarySectionVM.cutOffDt($.datepicker.formatDate('mm/dd/yy', cutOffDate));
                    //self.purchaseSummarySectionVM.closingDt($.datepicker.formatDate('mm/dd/yy', new Date(data.ClosingDate)));
                    self.purchaseSummarySectionVM.costBasis(data.CostBasis);
                    self.purchaseSummarySectionVM.putbackTerm(data.PutbackTerm__days_);
                    if (data.PutbackDeadline == undefined) {
                    }
                    else {
                        var putbackDeadline = new Date(data.PutbackDeadline);
                        putbackDeadline.setDate(putbackDeadline.getDate() + 1);
                        self.purchaseSummarySectionVM.putbackDeadline($.datepicker.formatDate('mm/dd/yy', putbackDeadline));
                    }
                    self.purchaseSummarySectionVM.purchaseDate($.datepicker.formatDate('mm/dd/yy', cutOffDate));
                    if (data.Face != undefined)
                        self.purchaseSummarySectionVM.face(formatCurrency(data.Face));
                    //self.costBasisputbackTermsputbackDeadlineface(data.Face);
                    self.purchaseSummarySectionVM.accounts(data.C_ofAccts);
                    if (data.PurchasePrice != undefined)
                        self.purchaseSummarySectionVM.purchasePrice(formatCurrency(data.PurchasePrice)); //formatCurrency({ colorize: true, negativeFormat: '(%s%n)' });
                    self.purchaseSummarySectionVM.resaleRestriction(data.ResaleRestrictionId);
                    self.purchaseSummarySectionVM.notes(data.Notes);
                    self.purchaseSummarySectionVM.recordCreatedBy(data.CreatedBy);
                    self.purchaseSummarySectionVM.recordUpdatedBy(data.UpdatedBy);

                    self.purchaseSummarySectionVM.editVisible(true);
                    self.purchaseSummarySectionVM.saveVisible(false);
                    self.purchaseSummarySectionVM.makeFieldsEditable(false);
                    self.purchaseSummarySectionVM.originalData(true);
                    self.purchaseSummarySectionVM.updatedData(false);

                    self.purchaseSummarySectionVM.companyEditedValue('');
                    self.purchaseSummarySectionVM.resaleEditedValue('');
                    self.purchaseSummarySectionVM.lenderFileEditedValue('');
                    self.purchaseSummarySectionVM.sellerEditedValue('');
                    self.purchaseSummarySectionVM.cuttOfDateEditedValue('');
                    self.purchaseSummarySectionVM.putbackTermEditedValue('');
                    self.purchaseSummarySectionVM.putbackDeadlineEditedValue('');
                    self.purchaseSummarySectionVM.notesEditedValue('');
                    $("#loading").html("&nbsp;");
                    $("#loading").dialog('close');

                    self.salesTabVM.editVisible(true);
                    self.salesTabVM.saveVisible(false);
                    self.salesTabVM.makeFieldsEditable(false);
                    self.salesTabVM.originalData(true);
                    self.salesTabVM.updatedData(false);
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
            self.salesTabVM.portfolioNumber(self.portfolioNumber());
            self.salesTabVM.salesBatchSelected(self.portfolioNumber() + '-1');
            self.salesTabVM.saveVisible(true);
        }
        else {
            self.salesTabVM.resetFields();
            self.purchaseSummarySectionVM.resetFields();
            self.purchaseSummarySectionVM.saveVisible(false);
            self.salesTabVM.saveVisible(false);
        }

    } .bind(self));

    self.purchaseSummarySectionVM = new purchaseSummaryVM(userId);
    self.salesTabVM = new salesTransVM(userId);
    //self.collectionsTabVM = new collectionsTransVM();
    //self.investmentsTabVM = new investmentsTransVM();
    //self.distributionsTabVM = new distributionsTransVM();
    //self.interestTabVM = new interestTransVM();

}

portfolioViewModels.setActiveTabViewModel = function (viewModel) {
    $(document).data("portfolioViewModels.activeTabViewmodel", viewModel);
}

portfolioViewModels.getActiveTabViewModel = function () {
    return $(document).data("portfolioViewModels.activeTabViewmodel");
}

portfolioViewModels.salesTransVM = function () { return $(document).data('salesTransVM') };
portfolioViewModels.purchaseSummaryVM = function () { return $(document).data('purchaseSummaryVM') };

portfolioViewModels.supBrockettCompanies = function () {
    if ($(document).data('supBrockettCompanies') == undefined) {
        function setCompanies(data) {
            $(document).data('supBrockettCompanies', data);
        };

        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'SupBrockettCompanies' },
            dataType: 'json',
            async: false,
            success: function (data) {
                setCompanies(data);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    return $(document).data('supBrockettCompanies');
}

portfolioViewModels.sellers = function () {
    if ($(document).data('sellers') == undefined) {

        function setSellers(data) {
            $(document).data('sellers', data);
        };

        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'Seller' },
            dataType: 'json',
            async: false,
            success: function (data) {
                setSellers(data);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    return $(document).data('sellers');
}

portfolioViewModels.resaleRestrictions = function () {
    if ($(document).data('resaleRestrictions') == undefined) {
        function setPortfolios(data) {
            $(document).data('resaleRestrictions', data);
        };
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'ResaleRestriction' },
            async: false,
            success: function (data) {
                setPortfolios(data);
            },
            error: function (xhr, status, somthing) {
                log('error:' + status);
            }
        });
    }
    return $(document).data('resaleRestrictions');
}

portfolioViewModels.putbackTerms = function () {
    if ($(document).data('putbackTerms') == undefined) {
        function setPortfolios(data) {
            $(document).data('putbackTerms', data);
        };
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'PutbackTerm' },
            async: false,
            success: function (data) {
                setPortfolios(data);
            },
            error: function (xhr, status, somthing) {
                log('error:' + status);
            }
        });
    }
    return $(document).data('putbackTerms');
}

portfolioViewModels.portfolios = function () {
    if ($(document).data('portfolios') == undefined) {
        function setPortfolios(data) {
            log('here');
            $(document).data('portfolios', data);
        };
        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'PortfolioOriginal' },
            async: false,
            success: function (data) {
                //log(data);
                setPortfolios(data);
            },
            error: function (xhr, status, somthing) {
                log('error:' + status);
            }
        });
    }
    //log($(document).data('portfolios'));
    return $(document).data('portfolios');
}

portfolioViewModels.buyers = function () {
    if ($(document).data('buyers') == undefined) {
        log('Firsttime getting buyers');
        function setBuyers(data) {
            $(document).data('buyers', data);
        };

        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'Buyer' },
            dataType: 'json',
            async: false,
            success: function (data) {
                setBuyers(data);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    return $(document).data('buyers');
}

portfolioViewModels.agencies = function () {
    if ($(document).data('agencies') == undefined) {
        log('Firsttime getting agencies');
        function setAgencies(data) {
            $(document).data('agencies', data);
        };

        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'Seller' },
            dataType: 'json',
            async: false,
            success: function (data) {
                setAgencies(data);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    return $(document).data('agencies');
}

portfolioViewModels.investors = function () {
    if ($(document).data('investors') == undefined) {
        function setAgencies(data) {
            $(document).data('investors', data);
        };

        $.ajax({
            url: baseUrl + '/api/Lookup/',
            type: 'GET',
            contentType: 'application/json',
            data: { id: 'Investor' },
            dataType: 'json',
            async: false,
            success: function (data) {
                setAgencies(data);
            },
            error: function (xhr, status, somthing) {
                log(status);
            }
        });
    }
    return $(document).data('investors');
}

portfolioViewModels.inittializeVMS = function (portfolioNumber) {
    //log('inittializeVMS');
    //$(document).data('interestVM', new interestVM(portfolioNumber));
    //$(document).data('distributionsTransVM', new distributionsTransVM(portfolioNumber));
    //$(document).data('investmentsTransVM', new investmentsTransVM(portfolioNumber));
    //$(document).data('collectionsTransVM', new collectionsTransVM(portfolioNumber));
    $(document).data('salesTransVM', new salesTransVM(portfolioNumber));
    $(document).data('purchaseSummaryVM', new purchaseSummaryVM(portfolioNumber));
}

portfolioViewModels.resetVMS = function () {
    $(document).data('purchaseSummaryVM', '');
    //$(document).data('interestVM', '');
    //$(document).data('distributionsTransVM', '');
    //$(document).data('investmentsTransVM', '');
    //$(document).data('collectionsTransVM', '');
    $(document).data('salesTransVM', '');
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
