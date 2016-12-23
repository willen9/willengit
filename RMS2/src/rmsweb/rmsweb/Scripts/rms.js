/*
    刪除數據提示信息
*/
function deleteInfo(url, infor) {
    $("#information").html("是否確定刪除" + infor + "？")
    $("#del").attr('href', url);
    $('.delete-modal-lg').modal();
};

/*
    只能輸入數字
*/
function onlyNumber() {
    if (!(event.keyCode == 46) && !(event.keyCode == 8) && !(event.keyCode == 37) && !(event.keyCode == 39))
        if (!((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode==9))
            event.returnValue = false;
};

/*
    前補0
*/
function addZero(str, length) {
    return new Array(length - str.length + 1).join("0") + str;
}

/*
    判斷數組是否有重複
*/
function unique(a) {
    for (var i = 0, len = a.length; i < len; i++) {
        for (var j = i + 1; j < len; j++) {
            if (a[i] === a[j])
                return false;
        }
    }
    return true;
}