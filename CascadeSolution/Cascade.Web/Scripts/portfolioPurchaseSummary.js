function purchaseSummaryVM(userId, salesVM) {
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
    self.save = function () {
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
            url: baseUrl + '/api/MSIPortfolioOriginal/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
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
    }
};
