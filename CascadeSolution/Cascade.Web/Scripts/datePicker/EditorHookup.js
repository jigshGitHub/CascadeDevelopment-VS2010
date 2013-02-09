/// <reference path="jquery-1.4.4.js" />
/// <reference path="jquery-ui.js" />

$(document).ready(function () {
    //alert("hello I am here");
    function getDateYymmdd(value) {
        if (value == null)
            return null;
        alert($.datepicker.parseDate("yy/mm/dd", value));
        alert($.datepicker.parseDate("mm/dd/yy", value));
        return $.datepicker.parseDate("yy/mm/dd", value);
    }

    //alert($('.date').length);

    $('.date').each(function () {
        //var minDate = getDateYymmdd($(this).data("val-rangedate-min"));
        //var maxDate = getDateYymmdd($(this).data("val-rangedate-max"));
        $(this).datepicker(
        {
            //dateFormat: "dd/mm/yy",  // hard-coding uk date format, but could embed this as an attribute server-side (based on the current culture)
            //minDate: minDate,
            //maxDate: maxDate
        });
    });
});