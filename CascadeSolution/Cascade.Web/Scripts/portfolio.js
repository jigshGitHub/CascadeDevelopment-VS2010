///
// known issue when you select add new, if portfolio number has '-' then it will mess up currentRecordIndex of sales in self.salesBatchSelected.subscribe
///
ko.bindingHandlers.not = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        //expects a single, truthy/falsy binding, 
        //    such as checked, visible, enable, etc.
        var binding = valueAccessor();
        var observable = binding[Object.keys(binding)[0]];
        var notComputed = ko.computed({
            read: function () {
                return !observable();
            },
            write: function (newValue) {
                observable(!newValue);
            }
        });
        var newBinding = {};
        newBinding[Object.keys(binding)[0]] = notComputed;
        ko.applyBindingsToNode(element, newBinding, viewModel);
    }
};


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

function portfolioVM(userId) {
    var self = this;
    self.portfolios = ko.observableArray([]);

    $.each(portfolioViewModels.portfolios(), function (i, item) {
        self.portfolios.push(item);
    });

    self.portfolioNumber = ko.observable();
    self.portfolioListVisible = ko.observable(true);
    self.portfolioEditableFields = ko.observable(false);
    self.portfolioNumber.subscribe(function (number) {
        if (number != undefined) {
            //log(number);
            if (number == '') {
                self.portfolioListVisible(false);
                self.portfolioEditableFields(true);
                self.purchaseSummarySectionVM.detailsVisible(true);
                self.purchaseSummarySectionVM.saveVisible(true);
                return;
            }
            if (!self.portfolioBlankScenario()) {
                //load acqusitions portfolio data
                $("#loading").dialog('open');
                $("#loading").html("<img src=\"" + absoluteapp + imagedir + "/ajax-loader.gif\" />");

                $.ajax({
                    //                url: baseUrl + '/api/Portfolio/',
                    url: baseUrl + '/api/MSIPortfolioOriginal/',
                    type: 'GET',
                    contentType: 'application/json',
                    data: { portfolioNumber: number },
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
                        self.purchaseSummarySectionVM.saveVisible(true);
                        self.collectionsTabVM.loadCollectionRecords();
                        $("#loading").html("&nbsp;");
                        $("#loading").dialog('close');
                    },
                    error: function (xhr, status, somthing) {
                        log(status);
                    }
                });
                self.salesTabVM.portfolioNumber(self.portfolioNumber());
                self.salesTabVM.salesBatchSelected(self.portfolioNumber() + '-1');
                self.salesTabVM.saveVisible(false);
                self.collectionsTabVM.portfolioNumber(self.portfolioNumber());
            }
        }
        else {
            self.salesTabVM.resetFields();
            self.purchaseSummarySectionVM.resetFields();
            self.purchaseSummarySectionVM.saveVisible(false);
            self.salesTabVM.saveVisible(false);
        }

    } .bind(self));

    self.salesTabVM = new salesTransVM(userId);
    self.purchaseSummarySectionVM = new purchaseSummaryVM(userId, self.salesTabVM);
    self.portfolioBlankScenario = ko.computed(function () {
        return self.purchaseSummarySectionVM.detailsVisible();
    }, self);
    self.collectionsTabVM = new collectionsTransVM();
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
//portfolioViewModels.interestVM = function () { return $(document).data('interestVM') };
//portfolioViewModels.distributionsTransVM = function () { return $(document).data('distributionsTransVM') };
//portfolioViewModels.investmentsTransVM = function () { return $(document).data('investmentsTransVM') };
portfolioViewModels.collectionsTransVM = function () { return $(document).data('collectionsTransVM') };



portfolioViewModels.inittializeVMS = function (portfolioNumber) {
    //log('inittializeVMS');
    //$(document).data('interestVM', new interestVM(portfolioNumber));
    //$(document).data('distributionsTransVM', new distributionsTransVM(portfolioNumber));
    //$(document).data('investmentsTransVM', new investmentsTransVM(portfolioNumber));
    $(document).data('collectionsTransVM', new collectionsTransVM(portfolioNumber));
    $(document).data('salesTransVM', new salesTransVM(portfolioNumber));
    $(document).data('purchaseSummaryVM', new purchaseSummaryVM(portfolioNumber));
}

portfolioViewModels.resetVMS = function () {
    $(document).data('purchaseSummaryVM', '');
    //$(document).data('interestVM', '');
    //$(document).data('distributionsTransVM', '');
    //$(document).data('investmentsTransVM', '');
    $(document).data('collectionsTransVM', '');
    $(document).data('salesTransVM', '');
}

