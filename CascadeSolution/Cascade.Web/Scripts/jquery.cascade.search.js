/****
imagedir
-- set this to the location of the images that you will be using
-- for your ajax loader and your ascending/descending hints 
-- i.e. on Windows XP, this default corresponds to http://localhost/MvcContribJQuery/Content/Images
-- LEADING slash only
*****/
/****
applicationname
-- set this to your application subdirectory
-- if you do not have an application subdirectory, leave completely blank
-- i.e. on Windows XP, this default corresponds to http://localhost/MvcContribJQuery
-- LEADING slash only
*****/
//var applicationname = "/Cascade";
var applicationname = "";
var advanceGrids = {};
var basicGrids = {};

function fixGrids(griddiv) {
    // Fix Firefox 3 bug where scrolling table rows fill tbody height if thereare not enough rows to scroll.
    if (navigator.userAgent.indexOf("Firefox/3") > -1) {
        var tbodies = $(griddiv + " tbody");
        tbodies.each(function () {
            var trows = $(this).children("tr");
            var tbodyHeight = $(this).height();
            var rowsHeight = 0;
            trows.each(function () {
                rowsHeight += $(this).height();
                if (rowsHeight >= tbodyHeight) return false;
            });
            if (rowsHeight <= (tbodyHeight + 1)) $(this).height("auto");
        });
    }
    else {
        if (navigator.userAgent.indexOf("MSIE") > -1) {
            var tbodies = $(griddiv + " tbody");
            tbodies.each(function () {
                $(this).height("auto");
            });
        }
    }
}

function UpdateAdvanceSearchResults(griddiv, controller, gridview, col, page, account, originator, seller,investor) {
    // when sorting, page is undefined or the same
    // when paging, column is the same
    var mvcgridvals = advanceGrids[griddiv];
    if (mvcgridvals == null) {
        //log(originator);
        mvcgridvals = {};
        mvcgridvals["currentpage"] = 1;
        mvcgridvals["direction"] = "ASC";
        mvcgridvals["sortcolumn"] = col;
        mvcgridvals['account'] = account;
        mvcgridvals['originator'] = originator;
        mvcgridvals['seller'] = seller;
        mvcgridvals['investor'] = investor;
    }
    else {
        if (page !== "" && page !== undefined && mvcgridvals["currentpage"] !== page) {
            // changing page
            mvcgridvals["currentpage"] = page;
        }
        else {
            // page being the same means we're sorting
            if (mvcgridvals["sortcolumn"] == col) {
                // same column means we need to switch ascending and descending
                if (mvcgridvals["direction"] == "ASC") {
                    mvcgridvals["direction"] = "DESC";
                }
                else {
                    mvcgridvals["direction"] = "ASC";
                }
            }
            else {
                // different column means we're starting with ascending
                mvcgridvals["direction"] = "ASC";
            }
            mvcgridvals["sortcolumn"] = col;
        }
    }
    // persist grid variables
    advanceGrids[griddiv] = mvcgridvals;
    // correcting for trailing slash -- no VirtualPathUtility here.
    $("#loading").dialog('open');
    $("#loading").html("<img src=\"" + baseUrl + imagedir + "/ajax-loader.gif\" />");
    log('Search' + baseUrl);
    $.ajax({
        type: "POST",
        url: baseUrl + controller,
        data: ({ account:mvcgridvals['account'], originator:mvcgridvals['originator'], seller:mvcgridvals['seller'], investor:mvcgridvals['investor'], page: mvcgridvals["currentpage"], columnToSort: mvcgridvals["sortcolumn"], sortDirection: mvcgridvals["direction"] }),
        success: function (msg) {
            $(griddiv).html(msg);
            $(griddiv).advanceSearch(controller, gridview, mvcgridvals['account'], mvcgridvals['originator'],mvcgridvals['seller'], mvcgridvals['investor'], { databinding: true });
            fixGrids(griddiv);
            $("#loading").html("&nbsp;");
            $("#loading").dialog('close');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Data Failed to Sort: " + XMLHttpRequest.statusText);
            $("#loading").html("&nbsp;");
        }
    });
}

function UpdateBasicSearchResults(griddiv, controller, gridview, col, page, basicSearchVal) {
    // when sorting, page is undefined or the same
    // when paging, column is the same
    var mvcgridvals = basicGrids[griddiv];
    if (mvcgridvals == null) {
        mvcgridvals = {};
        mvcgridvals["currentpage"] = 1;
        mvcgridvals["direction"] = "ASC";
        mvcgridvals["sortcolumn"] = col;
        mvcgridvals['basicSearchVal'] = basicSearchVal;
    }
    else {
        if (page !== "" && page !== undefined && mvcgridvals["currentpage"] !== page) {
            // changing page
            mvcgridvals["currentpage"] = page;
        }
        else {
            // page being the same means we're sorting
            if (mvcgridvals["sortcolumn"] == col) {
                // same column means we need to switch ascending and descending
                if (mvcgridvals["direction"] == "ASC") {
                    mvcgridvals["direction"] = "DESC";
                }
                else {
                    mvcgridvals["direction"] = "ASC";
                }
            }
            else {
                // different column means we're starting with ascending
                mvcgridvals["direction"] = "ASC";
            }
            mvcgridvals["sortcolumn"] = col;
        }
    }
    // persist grid variables
    basicGrids[griddiv] = mvcgridvals;
    // correcting for trailing slash -- no VirtualPathUtility here.
    $("#loading").dialog('open');
    $("#loading").html("<img src=\"" + baseUrl + imagedir + "/ajax-loader.gif\" />");

    log('here ' + basicSearchVal);
    $.ajax({
        type: "POST",
        url: baseUrl + controller,
        data: ({ basicSearchVal: mvcgridvals['basicSearchVal'], page: mvcgridvals["currentpage"], columnToSort: mvcgridvals["sortcolumn"], sortDirection: mvcgridvals["direction"] }),
        success: function (msg) {
            $(griddiv).html(msg);
            $(griddiv).basicSearch(controller, gridview, mvcgridvals['basicSearchVal'], { databinding: true });
            fixGrids(griddiv);
            $("#loading").html("&nbsp;");
            $("#loading").dialog('close');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Data Failed to Sort: " + XMLHttpRequest.statusText);
            $("#loading").html("&nbsp;");
        }
    });
}

(function ($) {
    $.fn.extend({
        advanceSearch: function (controller, gridview, account, originator, seller, investor, options) {
            var defaults = {
                defaultsort: '',
                databinding: false
            };

            var settings = $.extend(defaults, options);
            //log(originator);
            // which element are we applying this to?
            var selector = $(this).selector;
            if (!settings.databinding) {
                advanceGrids = {};
                UpdateAdvanceSearchResults(selector, controller, gridview, settings.defaultsort, undefined, account, originator,seller,investor);
                return false;
            }

            // helper function to parse querystring
            getPage = function (path) {
                var pagenum = 0;
                $.each(path.split("?")[1].split("&"),
                    function (key, val) {
                        var params = val.split("=");
                        pagenum = params[1];
                        return (params[0] !== "page");
                    }
                );
                return pagenum;
            };

            return this.each(function () {
                // step 1 -- manipulate pager
                // step 1.a -- retrieve pager links
                var anchors = $(selector + " .pagination").children(".paginationRight").children("a");
                var pagenum;
                var updatelink = "javascript:UpdateAdvanceSearchResults('#0', '#1', '#2', '#3', '#4');";
                updatelink = updatelink
                        .replace("#0", selector)
                        .replace("#1", controller)
                        .replace("#2", gridview);
                anchors.each(function () {
                    // step 1.b -- for each pager link, retrieve page linked to
                    pagenum = getPage($(this).attr("href"));
                    // step 1.c -- change page link to match controller 
                    $(this).attr("href",
                      updatelink
                        .replace("#3", '')
                        .replace("#4", pagenum)
                    );
                });
                // step 2 -- manipulate header
                // step 2.a -- retrieve header items
                var header = $(selector + " .grid-style").children("thead").children("tr").children("th");
                var anchor;
                var sortcolumn;
                header.each(function () {
                    // step 2.b -- manage links to javascript function
                    anchor = $(this).children("a");
                    // if there's no pre-existing link on the header 
                    // (from a header override, maybe), we create a new one
                    if (anchor.length == 0) {
                        $(this).wrapInner("<a></a>");
                    }
                    anchor = $(this).children("a");
                    sortcolumn = $(this).attr("sortcolumn");
                    anchor.attr("href",
                      updatelink
                        .replace("#3", sortcolumn)
                        .replace("#4", '')
                    );
                    // step 3 -- add span for ASC/DESC image
                    $(this).append("<span id='" + sortcolumn + "_Sort'></span>");
                    //alert($(this).html());
                });

                // step 3 -- manipulate page dropdown
                // step 3.a -- retrieve page dropdown items
                //var dropdown = $(selector + " .pagedropdown").children("select");
                //dropdown.each(function () {
                //    $(this).change(function () {
                //        UpdateAdvanceSearchResults(selector, controller, gridview, '', $(this).val());
                //    });
                //});
            });
        }
    });
})(jQuery);

(function ($) {
    $.fn.extend({
        basicSearch: function (controller, gridview, basicSearchVal, options) {
            var defaults = {
                defaultsort: '',
                databinding: false
            };

            var settings = $.extend(defaults, options);
            //log(originator);
            // which element are we applying this to?
            var selector = $(this).selector;
            if (!settings.databinding) {
                basicGrids = {};
                UpdateBasicSearchResults(selector, controller, gridview, settings.defaultsort, undefined, basicSearchVal);
                return false;
            }

            // helper function to parse querystring
            getPage = function (path) {
                var pagenum = 0;
                $.each(path.split("?")[1].split("&"),
                    function (key, val) {
                        var params = val.split("=");
                        pagenum = params[1];
                        return (params[0] !== "page");
                    }
                );
                return pagenum;
            };

            return this.each(function () {
                // step 1 -- manipulate pager
                // step 1.a -- retrieve pager links
                var anchors = $(selector + " .pagination").children(".paginationRight").children("a");
                var pagenum;
                var updatelink = "javascript:UpdateBasicSearchResults('#0', '#1', '#2', '#3', '#4');";
                updatelink = updatelink
                        .replace("#0", selector)
                        .replace("#1", controller)
                        .replace("#2", gridview);
                anchors.each(function () {
                    // step 1.b -- for each pager link, retrieve page linked to
                    pagenum = getPage($(this).attr("href"));
                    // step 1.c -- change page link to match controller 
                    $(this).attr("href",
                      updatelink
                        .replace("#3", '')
                        .replace("#4", pagenum)
                    );
                });
                // step 2 -- manipulate header
                // step 2.a -- retrieve header items
                var header = $(selector + " .grid-style").children("thead").children("tr").children("th");
                var anchor;
                var sortcolumn;
                header.each(function () {
                    // step 2.b -- manage links to javascript function
                    anchor = $(this).children("a");
                    // if there's no pre-existing link on the header 
                    // (from a header override, maybe), we create a new one
                    if (anchor.length == 0) {
                        $(this).wrapInner("<a></a>");
                    }
                    anchor = $(this).children("a");
                    sortcolumn = $(this).attr("sortcolumn");
                    anchor.attr("href",
                      updatelink
                        .replace("#3", sortcolumn)
                        .replace("#4", '')
                    );
                    // step 3 -- add span for ASC/DESC image
                    $(this).append("<span id='" + sortcolumn + "_Sort'></span>");
                    //alert($(this).html());
                });

                // step 3 -- manipulate page dropdown
                // step 3.a -- retrieve page dropdown items
                //var dropdown = $(selector + " .pagedropdown").children("select");
                //dropdown.each(function () {
                //    $(this).change(function () {
                //        UpdateBasicSearchResults(selector, controller, gridview, '', $(this).val());
                //    });
                //});
            });
        }
    });
})(jQuery);