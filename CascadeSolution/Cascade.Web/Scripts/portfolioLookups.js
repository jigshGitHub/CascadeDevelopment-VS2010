var portfolioViewModels = namespace("cascade.viewModels.portfolio");
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
            data: { id: 'Portfolio' },
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