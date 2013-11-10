//Known issues
//when we edit existing record with face value or net collection ...editing will take $ sign and save will not transfer that amount in api
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
    self.collectionRecords = ko.observableArray([]);
    self.loadCollectionRecords = function () {
        //var collectionRecords = [];
        //log(self.portfolioNumber());
        if (self.collectionRecords().length > 0)
            self.collectionRecords([]);
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
                                self.collectionRecords.push(new collectionRecord(item.ID, item.Portfolio_,
                                    item.Inv_AgencyName,
                                    dateFormat(Date.parse(item.ClosingDate), 'mm/dd/yyyy'),
                                    (item.FaceValue == undefined) ? '' : formatCurrency(item.FaceValue),
                                    (item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice),
                                    item.TransType));
                            });
                        }
                    }
                    else {
                        //self.collectionRecords.push(new collectionRecord('0', self.portfolioNumber(), '', '', '', ''));
                        //self.currentCollectionRecord(self.collectionRecords[0]);
                        //self.selectedAgency('');
                    }
                },
                error: function (xhr, status, somthing) {
                    log(status);
                }
            });
        }
        //return collectionRecords;
    }
//    self.collectionRecords = ko.computed(function () {
//        var collectionRecords = [];
//        //log(self.portfolioNumber());
//        if (self.portfolioNumber() != undefined) {
//            $.ajax({
//                url: baseUrl + '/api/MSIPortfolioCollectionsTransactions/',
//                type: 'GET',
//                contentType: 'application/json',
//                data: { portfolioNumber: self.portfolioNumber(), isOriginal: 'true' },
//                dataType: 'json',
//                async: false,
//                success: function (data) {
//                    if (data != undefined) {
//                        if (data.length > 0) {
//                            $.each(data, function (i, item) {
//                                collectionRecords.push(new collectionRecord(item.ID, item.Portfolio_,
//                                    item.Inv_AgencyName,
//                                    dateFormat(Date.parse(item.ClosingDate), 'mm/dd/yyyy'),
//                                    (item.FaceValue == undefined) ? '' : formatCurrency(item.FaceValue),
//                                    (item.SalesPrice == undefined) ? '' : formatCurrency(item.SalesPrice),
//                                    item.TransType));
//                            });
//                        }
//                    }
//                    else {
//                        collectionRecords.push(new collectionRecord('0', self.portfolioNumber(), '', '', '', ''));
//                        self.currentCollectionRecord(collectionRecords[0]);
//                        self.selectedAgency('');
//                    }
//                },
//                error: function (xhr, status, somthing) {
//                    log(status);
//                }
//            });
//        }
//        return collectionRecords;
//    }, self);
    self.currentRecordIndex = ko.observable(0);
    self.addNewCollection = function () {
        self.currentCollectionRecord(new collectionRecord('0', self.portfolioNumber(), '', '', '', ''));
    };
    self.selectedRecordIndexChanged = function (item) {
        log(item);
        self.currentCollectionRecord(item);
        self.selectedAgency(item.collectionAgencyName());
    };
    self.selectedAgency = ko.observable();
//    self.selectedAgency.subscribe(function (agency) {
//        self.currentCollectionRecord().collectionAgencyName(agency);        
//    });
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
        log(self.currentCollectionRecord().faceValue());
        log(self.currentCollectionRecord().netColls());
        log(self.currentCollectionRecord().closingDt());
        log(self.currentCollectionRecord().collectionAgencyName());
        log(self.currentCollectionRecord().id);
        var json = JSON.stringify({
            Portfolio_: self.currentCollectionRecord().portfolioNumber(),
            FaceValue: Number(self.currentCollectionRecord().faceValue().replace(/[^0-9\.]+/g, "")),
            SalesPrice: Number(self.currentCollectionRecord().netColls().replace(/[^0-9\.]+/g, "")),
            ClosingDate: self.currentCollectionRecord().closingDt(),
            Inv_AgencyName: self.selectedAgency(),
            ID: self.currentCollectionRecord().id,
            TransType: 'Collection',
            IsOriginal: 'true'
        });

        $.ajax({
            url: baseUrl + '/api/MSIPortfolioCollectionsTransactions/',
            type: "POST",
            data: json,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                self.loadCollectionRecords();
                self.resetFields();
            },
            error: function (response, errorText) {
            }
        });

        self.showMessage(true);
        self.message('Data saved successfully!');
    }

    self.resetFields = function () {
        //self.currentCollectionRecord().portfolioNumber(''),
        self.currentCollectionRecord().faceValue('');
        self.currentCollectionRecord().netColls('');
        self.currentCollectionRecord().closingDt('');
        self.currentCollectionRecord().id = 0;
        self.selectedAgency('')
    };
};