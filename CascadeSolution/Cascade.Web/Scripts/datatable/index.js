

$(document).ready(function () {


    //View Company Records
    if ($("#companyRecords").length != 0) {
        $('#companyRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }

    //View DPS Records
    if ($("#dpsRecords").length != 0) {
        $('#dpsRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    { "sType": "currency" },
                    null,
                    null,
                    null,
                    null,
                    null

                ]
            });
    }


    //View People Records
    if ($("#peopleRecords").length != 0) {
        $('#peopleRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null

                ]
            });
    }

    //View Money Records
    if ($("#moneyRecords").length != 0) {
        $('#moneyRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                     { "sType": "currency" }

                ]
            });
    }

    //View Recall Records
    if ($("#recallRecords").length != 0) {
        $('#recallRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    { "sType": "currency" },
                    null,
                    null,
                    null,
                    null,
                    null

                ]
            });
    }

    //Recalls Upload Report
    if ($("#recallsUpload").length != 0) {
        $('#recallsUpload').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null

                ]
            });
    }

    //Recalls Not Closed Report
    if ($("#recallsNotClosed").length != 0) {
        $('#recallsNotClosed').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    { "sType": "currency" },
                    null
                 ]
            });
    }


    //Add Seller Check Report
    if ($("#addSellerCheck").length != 0) {
        $('#addSellerCheck').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    { "sType": "currency" },
                    null
                ]
            });
    }

    //Recalls No Note Sent
    if ($("#recallsNoNoteSent").length != 0) {
        $('#recallsNoNoteSent').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    { "sType": "currency" },
                    null
                ]
            });
    }

    //Recalls Not Uploaded
    if ($("#recallsNotUploaded").length != 0) {
        $('#recallsNotUploaded').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    { "sType": "currency" },
                    null
                ]
            });
    }

    //Recalls Receivable
    if ($("#recallsReceivable").length != 0) {
        $('#recallsReceivable').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                       null,
                         { "sType": "currency" },
                         null
                ]
            });
    }

    if ($("#recallsInvoiceLookup").length != 0) {
        $('#recallsInvoiceLookup').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                       null,
                         { "sType": "currency" },
                         null
                ]
            });
    }

    if ($("#recallsSellerCheckLookup").length != 0) {
        $('#recallsSellerCheckLookup').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                       null,
                         { "sType": "currency" },
                         null,
                         null
                ]
            });
    }


    if ($("#recallsPayable").length != 0) {
        $('#recallsPayable').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                       null,
                         { "sType": "currency" },
                         null
                ]
            });
    }

    if ($("#dpsPayable").length != 0) {
        $('#dpsPayable').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    null


                ]
            });
    }


    if ($("#recallsPaidByOurCheck").length != 0) {
        $('#recallsPaidByOurCheck').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                       null,
                         { "sType": "currency" },
                         null,
                         null,
                         null,
                ]
            });
    }

    if ($("#DPSCheckDetails").length != 0) {
        $('#DPSCheckDetails').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                     null,
                      { "sType": "currency" },
                         { "sType": "currency" },
                          null,
                         null,
                ]
            });
    }



    //Media Not Submitted
    if ($("#mediaNotSubmitted").length != 0) {
        $('#mediaNotSubmitted').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null
                ]
            });
    }

    //Media Not Received
    if ($("#mediaNotReceived").length != 0) {
        $('#mediaNotReceived').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null
                ]
            });
    }

    //Media Not Forwarded
    if ($("#mediaNotForwarded").length != 0) {
        $('#mediaNotForwarded').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null
                ]
            });
    }

    //Media Not Confirmed
    if ($("#mediaNotConfirmed").length != 0) {
        $('#mediaNotConfirmed').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null
                ]
            });
    }

    //ComplianceNCRA
    if ($("#tblComplianceNCRA").length != 0) {
        $('#tblComplianceNCRA').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    //tblComplianceAAI
    if ($("#tblComplianceAAI").length != 0) {
        $('#tblComplianceAAI').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    // tblComplianceNCP
    if ($("#tblComplianceNCP").length != 0) {
        $('#tblComplianceNCP').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    //tblComplianceORP
    if ($("#tblComplianceORP").length != 0) {
        $('#tblComplianceORP').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    //tblComplianceRC
    if ($("#tblComplianceRC").length != 0) {
        $('#tblComplianceRC').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    //tblComplianceSOA
    if ($("#tblComplianceSOA").length != 0) {
        $('#tblComplianceSOA').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
                ]
            });
    }
    //View Media Records
    if ($("#mediaRecords").length != 0) {
        $('#mediaRecords').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    {
                        "bSearchable": false,
                        "bSortable": false

                    },
                    null,
                    { "sType": "currency" },
                    { "sType": "currency" },
                    null,
                    null,
                    null,
                    null,
                    null

                ]
            });
    }

    //For Portfolio Cash Flow Report
    if ($("#portCashFlow").length != 0) {
        $('#portCashFlow').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    null
                ]
            });

    }
    //For Portfolio Cash Position Report
    if ($("#portCashPosition").length != 0) {
        $('#portCashPosition').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    { "sType": "currency" },
                    { "sType": "currency" },
                    { "sType": "currency" },
                    null,
                    { "sType": "percent" }
                ]
            });

    }
    //For Purchases Report
    if ($("#purchases").length != 0) {
        $('#purchases').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    { "sType": "currency" }
                ]
            });

    }
    //For Sales Report
    if ($("#sales").length != 0) {
        $('#sales').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    { "sType": "currency" }
                ]
            });

    }

    //For CollectionRecon Report
    if ($("#CollectionRecon").length != 0) {
        $('#CollectionRecon').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    { "sType": "currency" },
                ]
            });
    }
    //For Portfolio Summary Report
    if ($("#portSummary").length != 0) {
        $('#portSummary').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    null,
                    null,
                    null
                 ]
            });
    }
    //For Portfolio Transactions
    if ($("#portTransaction").length != 0) {
        $('#portTransaction').dataTable(
            {
                "bFilter": false,
                "aoColumns": [
                    null,
                    null,
                    null,
                    null,
                    { "sType": "currency" },
                    null,
                    null
                ]
            });
    }
    //For Add DPS Check - Report
    if ($("#adddpscheck").length != 0) {
        $('#adddpscheck').dataTable(
             {
                 "bFilter": false,
                 "aoColumns": [
                     null,
                     null,
                     { "sType": "currency" },
                     null,
                     null,
                     null,
                     null
                 ]
             });
    }

    //For All Reports where Company Name is not required
    //if ($('#reportType').val() == 'Purchases' || $('#reportType').val() == 'Sales') {
    //  $('#companytr').hide();
    //}

    //For All Reports where StartDate and EndDate is not required
    if ($('#reportType').val() == 'CashPosition'
        || $('#reportType').val() == 'PortfolioSummary'
        || $('#reportType').val() == 'AddDPSCheck'
        || $('#reportType').val() == 'PortTransactions') {
        $('#startdatetr').hide();
        $('#enddatetr').hide();
    }


    /*globals $  Used for Sorting*/
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['currency-asc'] = function (a, b) {
            'use strict';
            //alert("currency asc test");
            var x, y;

            /* Remove any commas (assumes that if present all strings will have a fixed number of d.p) */
            x = (a === "-" || a === "--" || a === '' || a.toLowerCase().replace('/', '') === 'na') ? -1 : a.replace(/,/g, "");
            y = (b === "-" || b === "--" || b === '' || b.toLowerCase().replace('/', '') === 'na') ? -1 : b.replace(/,/g, "");

            /* Remove the currency sign */
            if (typeof x === "string" && isNaN(x.substr(0, 1), 10)) {
                x = x.substring(1);
            }
            if (typeof y === "string" && isNaN(y.substr(0, 1), 10)) {
                y = y.substring(1);
            }

            /* Parse and return */
            x = parseFloat(x, 10);
            y = parseFloat(y, 10);

            return x - y;
        };
    }
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['currency-desc'] = function (a, b) {
            'use strict';
            //alert("currency desc test");
            var x, y;

            /* Remove any commas (assumes that if present all strings will have a fixed number of d.p) */
            x = (a === "-" || a === "--" || a === '' || a.toLowerCase().replace('/', '') === 'na') ? -1 : a.replace(/,/g, "");
            y = (b === "-" || b === "--" || b === '' || b.toLowerCase().replace('/', '') === 'na') ? -1 : b.replace(/,/g, "");

            /* Remove the currency sign */
            if (typeof x === "string" && isNaN(x.substr(0, 1), 10)) {
                x = x.substring(1);
            }
            if (typeof y === "string" && isNaN(y.substr(0, 1), 10)) {
                y = y.substring(1);
            }

            /* Parse and return */
            x = parseFloat(x, 10);
            y = parseFloat(y, 10);

            return y - x;
        };
    }
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['numeric-comma-asc'] = function (a, b) {
            var x = (a == "-") ? 0 : a.replace(/,/, ".");
            var y = (b == "-") ? 0 : b.replace(/,/, ".");
            x = parseFloat(x);
            y = parseFloat(y);
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        };
    }
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['numeric-comma-desc'] = function (a, b) {
            var x = (a == "-") ? 0 : a.replace(/,/, ".");
            var y = (b == "-") ? 0 : b.replace(/,/, ".");
            x = parseFloat(x);
            y = parseFloat(y);
            return ((x < y) ? 1 : ((x > y) ? -1 : 0));
        };
    }
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['percent-asc'] = function (a, b) {
            var x = (a == "-") ? 0 : a.replace(/%/, "");
            var y = (b == "-") ? 0 : b.replace(/%/, "");
            x = parseFloat(x); y = parseFloat(y);
            if (isNaN(x)) { if (isNaN(y)) return 0; else return 1; }
            else if (isNaN(y))
                return -1;
            return ((x < y) ? -1 : ((x > y) ? 1 : 0)
            );
        };
    }
    if ($.fn.dataTableExt !== undefined) {
        $.fn.dataTableExt.oSort['percent-desc'] = function (a, b) {
            var x = (a == "-") ? 0 : a.replace(/%/, "");
            var y = (b == "-") ? 0 : b.replace(/%/, "");
            x = parseFloat(x); y = parseFloat(y);
            if (isNaN(x)) { if (isNaN(y)) return 0; else return 1; }
            else if (isNaN(y)) return -1;
            return ((x < y) ? 1 : ((x > y) ? -1 : 0));
        };
    }

    //Used for Search Results - Left side navigation
    if ($("#searchRecords").length != 0) {
        $('#searchRecords').dataTable();
    }

    if ($("#mediaRequestRecords").length != 0) {
        $('#mediaRequestRecords').dataTable();
    }
    //"bFilter": false 

    //alert("here beginning");
    //get Window Location
    //var windlowLocation = window.location.href.split('/');
    //Get BaseURL Info
    //var baseUrl = windlowLocation[0] + '//' + windlowLocation[2] + '/' + windlowLocation[3];

    //var baseUrlColl = windlowLocation[0] + '//' + windlowLocation[2] + '/' + windlowLocation[3] + '/' + windlowLocation[4];
    //alert(baseUrl);
    //alert(baseUrlColl);

    //Create Ajax Source - For CRM Company
    //var _ajaxSource = baseUrl + "/Company/AjaxHandler";
    //For Collections
    //var _ajaxSourceColl = baseUrl + "/Home/AjaxHandler";

    //alert(_ajaxSourceColl);
    //if ($("#myCompanyTable").length != 0) {
    //    var oTable = $('#myCompanyTable').dataTable({
    //        "bServerSide": true,
    //        "sAjaxSource": _ajaxSource,
    //        "bProcessing": true,
    //        "aoColumns": [
    //                        {
    //                            "sName": "COMPANIESID",
    //                            "bSearchable": false,
    //                            "bSortable": false,
    //                            "fnRender": function (oObj) {
    //                                return '<a href=\"Company/Details/' + oObj.aData[0] + '\">View</a>';
    //                            }
    //                        },
    //                        { "sName": "COMPANYNAME" },
    //                        { "sName": "POCLastName" },
    //                        { "sName": "POCFirstName" },
    //                        { "sName": "CITY" },
    //                        { "sName": "Country" }
    //        ]
    //    });

    //}

    //Used for Collections - myCollectionsTable
    //if ($("#myCollectionsTable").length != 0) {
    //    var oCollTable = $('#myCollectionsTable').dataTable({
    //        "bServerSide": true,
    //        "sAjaxSource": _ajaxSourceColl,
    //        "bProcessing": true,
    //        "aoColumns": [
    //                        {
    //                            "sName": "ACCOUNT",
    //                            "bSearchable": false,
    //                            "bSortable": false,
    //                            "fnRender": function (oObj) {
    //                                return '<a href=\"Home/Details/' + oObj.aData[0] + '\">View</a>';
    //                            }
    //                        },
    //                        { "sName": "ACCOUNT" },
    //                        { "sName": "PortfolioName" },
    //                        { "sName": "PortfolioOwner" },
    //                        { "sName": "NAME" },
    //                        { "sName": "LAST_PAY_DATE" },
    //                        { "sName": "LAST_PAY_AMT" },
    //                        { "sName": "DaysSinceLastPay" },
    //                        { "sName": "Balance" },
    //                        { "sName": "AccruedInterest" },
    //                        { "sName": "TotalInterest" },
    //                        { "sName": "ADDRESS1" },
    //                        { "sName": "CITY" },
    //                        { "sName": "STATE" },
    //                        { "sName": "ZIP_CODE" },
    //                        { "sName": "WorkStatusDescription" },
    //                        { "sName": "WorkGroup" }
    //        ]
    //    });
    //}

    //Used for Collections - myDPSTable
    //if ($("#myDPSTable").length != 0) {
    //    //alert("let us start processing");
    //    var oCollTable = $('#myDPSTable').dataTable({
    //        "bServerSide": true,
    //        "sAjaxSource": _ajaxSourceColl,
    //        "bProcessing": true,
    //        "aoColumns": [
    //                        {
    //                            "sName": "ID",
    //                            "bSearchable": false,
    //                            "bSortable": false,
    //                            "fnRender": function (oObj) {
    //                                return '<a href=\"Home/Details/' + oObj.aData[0] + '\">View</a>';
    //                            }
    //                        },
    //                        { "sName": "Portfolio" },
    //                        { "sName": "Amount", "sType": "currency" },
    //                        { "sName": "DateRec" },
    //                        { "sName": "TranDate"},
    //                        { "sName": "Name" },
    //                        { "sName": "OriginalAccount" },
    //                        { "sName": "PIMSAccount" }

    //        ]
    //    });
    //}


    //Use for State DDL
    //$(function () {
    //    if ($("#stateList").length != 0) {
    //        //alert("I am here");
    //        $.getJSON("DPS/GetStateList", null, function (data) {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#stateList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#stateList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});


    //Use for TransCodes DDL 
    //$(function ()
    //{
    //    if ($("#transCodesList").length != 0)
    //    {
    //        //alert("I am here inside");
    //        $.getJSON("DPS/GetTransCodeList", null, function (data)
    //        {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#transCodesList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#transCodesList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});

    //Use for TransSources DDL  
    //$(function () {
    //    if ($("#transSourcesList").length != 0) {
    //        //alert("I am here inside");
    //        $.getJSON("DPS/GetTransSourceList", null, function (data) {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#transSourcesList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#transSourcesList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});
    //Use for PmtTypes DDL  
    //$(function () {
    //    if ($("#pmtTypesList").length != 0) {
    //        //alert("I am here inside");
    //        $.getJSON("DPS/GetPmtTypesList", null, function (data) {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#pmtTypesList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#pmtTypesList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});

    //Use for Portfolio DDL  
    //$(function () {
    //    if ($("#portfoliosList").length != 0) {
    //        //alert("I am here inside");
    //        $.getJSON("DPS/GetPortfoliosList", null, function (data) {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#portfoliosList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#portfoliosList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});

    //Use for Responsibilites DDL  
    //$(function () {
    //    if ($("#responsibilitiesList").length != 0) {
    //        //alert("I am here inside");
    //        $.getJSON("DPS/GetResponsibilitesList", null, function (data) {

    //            //alert(data.length);

    //            // clear dropdown 
    //            $("#responsibilitiesList").html("");
    //            //add data
    //            for (var i = 0; i < data.length; i++) {
    //                //alert("process started");
    //                var item = data[i];
    //                //alert(item);
    //                $("#responsibilitiesList").append(
    //                      $("<option></option>").val(item.Value).html(item.Text)
    //                );
    //            }
    //        });
    //    }
    //});

});

