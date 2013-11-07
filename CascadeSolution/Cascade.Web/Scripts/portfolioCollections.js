function collectionRecord(id, portfolioNumber, agency, closingDt, faceValue, netColls, collectionsTransType) {
    this.id = id;
    this.portfolioNumber = ko.observable(portfolioNumber);
    this.collectionAgencyName = ko.observable(agency);
    //this.agencies = ko.observableArray([]);
    this.closingDt = ko.observable(closingDt)
    this.faceValue = ko.observable(faceValue)
    this.netColls = ko.observable(netColls)
    this.collectionsTransType = ko.observable(collectionsTransType);
}

function collectionsTransVM() {
    var self = this;
    self.portfolioNumber = ko.observable();
    self.editMode = ko.observable(false);
    self.collectionRecords = ko.computed(function () {
        var collectionRecords = [];
        log(self.portfolioNumber());
        if (self.portfolioNumber() != undefined) {
            $.ajax({
                url: baseUrl + '/api/MSIPortfolioCollectionsTransactions/',
                type: 'GET',
                contentType: 'application/json',
                data: { portfolioNumber: self.portfolioNumber(), isOriginal: 'true' },
                dataType: 'json',
                async: false,
                success: function (data) {
                    if (data != undefined) {
                        if (data.length > 0) {
                            $.each(data, function (i, item) {
                                collectionRecords.push(new collectionRecord(item.ID, item.Portfolio_,
                                    item.Inv_AgencyName,
                                    dateFormat(Date.parse(item.ClosingDate), 'mm/dd/yyyy'),
                                    (item.FaceValue == undefined) ? '' : formatCurrency(item.FaceValue),
                                    (item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice),
                                    item.TransType));
                            });
                        }
                    }
                    else {
                        collectionRecords.push(new collectionRecord('', '', '', '', '', ''));
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        return collectionRecords;
    }, self);
    self.currentRecordIndex = ko.observable(0);
    self.selectedRecordIndexChanged = function (item) {
        self.currentCollectionRecord(item);
        self.selectedAgency(item.collectionAgencyName());
    };
    self.selectedAgency = ko.observable();
    self.currentCollectionRecord = ko.observable();
    self.agencies = ko.observableArray([]);
    self.collectionsTransTypes = [{ Text: 'Sale', Value: 'Sale' }, { Text: 'Collection', Value: 'Collection' }, { Text: 'Distribution', Value: 'Distribution' }, { Text: 'Interest', Value: 'Interest' }, { Text: 'Investment', Value: 'Investment' }];
    self.totalCollections = ko.computed(function () {
        var salesTotal = 0;
        var val;
        $.each(self.collectionRecords(), function (i, item) {
            val = item.netColls().replace(/[^0-9.]/ig, '');
            salesTotal += parseFloat(val) || 0;
        });
        return formatCurrency(salesTotal);
    }, self);
    $.each(portfolioViewModels.supCompanies(), function (i, item) {
        self.agencies.push(item);
    });

    self.showMessage = ko.observable(false);
    self.message = ko.observable('');
    self.saveRecord = function () {
        //log(self.currentCollectionRecord().faceValue());
        //log(self.currentCollectionRecord().netColls());
        //log(self.currentCollectionRecord().closingDt());
        //log(self.currentCollectionRecord().collectionAgencyName());
        //log(self.currentCollectionRecord().id);
        var json = JSON.stringify({
            FaceValue: self.currentCollectionRecord().faceValue(),
            SalesPrice: self.currentCollectionRecord().netColls(),
            ClosingDate: self.currentCollectionRecord().closingDt(),
            Inv_AgencyName:self.selectedAgency(),
            ID: self.currentCollectionRecord().id,
            TransType: 'Collection',
            IsOriginal:'true'
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioCollectionsTransactions/',
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

        self.showMessage(true);
        self.message('Data saved successfully!');
    }

};