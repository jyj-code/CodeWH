function backCall(data, ip) {
    var modelType;
    if (browser.versions.mobile || browser.versions.ios || browser.versions.android ||
        browser.versions.iPhone || browser.versions.iPad) {
        modelType = "mb";
    } else
        modelType = "pc";
    $.post("../Home/UserInfo", { data: data, ip: ip, modelType: modelType }, function (result) {

    });
}
$(document).ready(function () {
    jQuery(function ($) {
        var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
        $.getJSON(url, function (data) {
            backCall(data, returnCitySN["cip"]);
        });
    });
});