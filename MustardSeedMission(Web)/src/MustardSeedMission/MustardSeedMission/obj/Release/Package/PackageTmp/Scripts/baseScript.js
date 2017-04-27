$(document).ready(function () {
    var baseUrl = $('input[title="baseURL"]').attr('value');
    var virtualPath = $('input[title="virtualPath"]').attr('value');
    var virtualPathUrl = baseUrl;
    var moduleID = $('input[title="module"]').attr('value');

    if (virtualPath != '/')
        virtualPathUrl += virtualPath;

    /* Set browser title */
    if ($('.content-header h1').length > 0) {
        document.title += ' - ' + $('.content-header h1').text();
    }
    
    /* LEFT SIDE NAVIGATE MENU*/
    var currentUrl = window.location.pathname;

    $('ul[class="treeview-menu"] li a').each(function () {
        var currentPage = $(this).attr('href');
        
        if (currentUrl != '/' && currentUrl != '' && currentPage != '')
        {
            if (currentUrl.indexOf(currentPage) > -1) {
                $(this).parent().addClass('active');
                $(this).parent().parent().parent().addClass('active');
                return false;
            }
        }
    });    

    /* ADVANCED-SEARCH-MODAL*/
    if ($(".advanced-search-modal").length > 0)
    {
        var jsonfile = virtualPathUrl + '/areas/' + moduleID + '/advanced-search.json';
        $.getJSON(jsonfile, function (data) {
            console.log('%c advanced-search.json', 'color:blue');
            console.log('%c $.json ' + jsonfile, 'color:red');
            var items = [];
            $.each(data, function (key, val) {
                console.log(key + ' : ' + val);
                items.push("<option value='" + key + "'>" + val + "</option>");
            });

            //remove entities exist.
            $(".baseRow .condition option").remove();
            //append new entities.
            $(".baseRow .condition").append(items.join(""));
            //clear conditions last opend and keep one.
            $('#search-condition-area [class="form-group"]').remove();
            addCondition();
        });
    }
});

//for advanced-search-modal
function addCondition() {
    var limitCount = 6;

    var optionCount = $(".baseRow #condition-keys > option").length;
    if (optionCount <= 5)
        limitCount = optionCount + 1;

    if ($("#search-condition-area .form-group").length < limitCount) {
        $("#search-condition-area").append($(".baseRow").clone(true).removeClass("baseRow").removeAttr('style'));
    }
}

//for advanced-search-modal
function removeCondition(selfObject) {
    if ($("#search-condition-area .form-group").length > 2)
        $(selfObject).parents('.form-group').remove();
}

//for advanced-search-modal
function ClearCondition() {
    $('#search-condition-area [class="form-group"]').remove();
    addCondition();
}

function EDate(SDate,month)
{
    var newDate = moment(SDate);
    var AddMonth = newDate.add(month, 'month');
    var EDate = AddMonth.add(-1, 'day');
    return EDate.format('YYYY/MM/DD');
}

/* 
    NOTIFICATION SETTINGS FOR GOOGLE CHROME BROWSER 
    IMPLEMENT BY W3C NOTIFICATION API
*/
//Require Notification Permission
document.addEventListener('DOMContentLoaded', function () {   
    if (CheckDesktopNotificationSupported()) {
        if (Notification.permission !== 'granted')
            Notification.requestPermission();
    }    
});

//Check notification function if browser supported
function CheckDesktopNotificationSupported() {
    if (!("Notification" in window)) {
        return false;
    }
}

function ShowDesktopNotification(message) {
    if (CheckDesktopNotificationSupported()) {
        if (Notification) {
            var notification = new Notification('RMS Notification', {
                body: message
            });
        }
    }    
}

/* -- END -- */