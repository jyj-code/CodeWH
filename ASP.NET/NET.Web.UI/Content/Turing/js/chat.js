var isF = false;
var Client_Ip;
var Client_IpAddress;
var srcround = Math.round(Math.random() * 9 + 1);
var ServerHub_chat = $.connection.serverHub;
$(function () {
    $("#discussions").css("height", window.screen.availHeight - 300);
    hidshow(1);
    jQuery(function ($) {
        $.getScript('http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js', function (_result) {
            if (remote_ip_info.ret == '1') {
                Client_IpAddress = remote_ip_info;
            }
        })
    })
    $("#valiCode").bind("click", function () {
        this.src = "/Home/GetValidateCode?time=" + (new Date()).getTime();
    });
    $("#btnvaliCode").bind("click", function () {
        if ($("#valiCodename").val().trim() == null || $("#valiCodename").val().trim() == "")
            return;
        $.get("../Home/ValidateCode?var=" + $("#valiCodename").val() + "", function (result) {
            if (result == "1" || result > 1) {
                hidshow(1);
                send();
            } else
                Alert("验证码错误");
            $("#valiCodename").val("");
        });
    });
    ServerHub_chat.client.sendMessage = function (message, count, htmlContext) {
        $('#countTol').html('');
        $('#countTol').text(count);
        if (htmlContext == null) return;
        $('#discussions').append('<li class="row"><div class="chat"><div class="chat-left col-xs-6"><div class="row"><div class="col-xs-3"><strong>科小智</strong><p><img src="../../Content/image/im.jpg" title="我是科布尔--科小智--机器人" width="35" height="35"></p></div><div class="col-xs-9"><div class="chat-context chat-left-context">' + message + '</div></div></div></div>' + htmlContext + '</div></li>');
        if ($('#ISScraper').val() == "刷屏关闭") {
            lct = document.getElementById('discussions');
            lct.scrollTop = Math.max(0, lct.scrollHeight - lct.offsetHeight);
        }
        $('#sendmessage').removeAttr("disabled");
        $('#message').removeAttr("disabled");
        $('#message').focus();
    };
    $('#message').focus();
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            send();
        });
        $('#message').keydown(function (e) {
            if (e.keyCode == 13) {
                send();
            }
        });
        $('#ISScraper').click(function () {
            if ($('#ISScraper').val() == "开启刷屏") {
                $('#ISScraper').removeClass("btn-success");
                $('#ISScraper').addClass("btn-default");
                $('#ISScraper').val("刷屏关闭");
            }
            else {
                $('#ISScraper').removeClass("btn-default");
                $('#ISScraper').addClass("btn-success");
                $('#ISScraper').val("开启刷屏");
            }
        });
        $('#NewKnow').click(function () {
            location.href = "/Home/NewKnow";
        });
    });
});
function send() {
    var flage = true;
    var id = $('#hidKey').val();
    var Contexxt = $('#message').val();
    if (Contexxt == null || Contexxt == undefined || Contexxt == '' || id == '')
        flage = false;
    if (flage) {
        var array = decodeURIComponent($("#hidFilterKey").val()).split(',');
        $.each(array, function (n, value) {
            if (Contexxt.indexOf(value) >= 0) {
                layer.alert("你键入的字符含有违反国家法律法规不允许的字符,如诺多次恶意将会限制进入，请自重", { skin: 'layui-layer-lan', closeBtn: 0, shift: 4 });
                flage = false;
            }
        });
    }
    if (flage) {
        $('#sendmessage').attr('disabled', "true");
        $('#message').attr('disabled', "true");
        var id = $('#hidKey').val();
        if (Client_Ip == undefined || Client_Ip == null || Client_Ip == '' || id == '') {
            jQuery(function ($) {
                var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&_=' + Math.random();
                $.getJSON(url, function (data) {
                    Client_Ip = data.Ip;
                    sendFunction(Contexxt, Client_Ip);
                });
            });
        } else {
            sendFunction(Contexxt, Client_Ip);
        }
    }
}
var ost = function () {
    var ua = navigator.userAgent;
    if (ua.indexOf("windows mobile") != -1) return "WindowsMobile";
    if (ua.indexOf("windows mobile") != -1) return "WindowsMobile";
    if (ua.indexOf("windows ce") != -1) return "WindowsCe";
    if (ua.indexOf("Windows") != -1) return "Windows";
    if (ua.indexOf("iPhone") != -1) return "iPhone";
    if (ua.indexOf("iPad") != -1) return "iPad";
    if (ua.indexOf("Linux") != -1) {
        var index = ua.indexOf("Android");
        if (index != -1) {
            var os = ua.slice(index, index + 13);
            var index2 = ua.indexOf("Build");
            var type = ua.slice(index1 + 1, index2);
            return type + os;
        } else {
            return "Linux";
        }
    }
}
function sendFunction(Contexxt, ip) {
    var dt = new Date();
    var dateTime = dt.getMonth() + 1 + '/' + dt.getDate() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds()
    var ad;
    if (Client_IpAddress != null)
        ad = Client_IpAddress.country + "" + Client_IpAddress.province + "" + Client_IpAddress.city + "" + Client_IpAddress.district + "" + Client_IpAddress.isp
    if ($('#ISScraper').val() == "刷屏关闭") {
        lct = document.getElementById('discussions');
        lct.scrollTop = Math.max(0, lct.scrollHeight - lct.offsetHeight);
    }
    $.post("../Home/ValidateRobot", { message: Contexxt, ip: ip, Client_IpAddress: ad, UserId: $('#hidKey').val() }, function (result) {
        if (isF == false && result == "1" || result == 1) {
            hidshow(0);
            return;
        } else if (result == "2" || result == 2) {
            return location.href = " http://kebue.com/";
        } else {
            if (srcround == null || parseInt(srcround) < 0 || parseInt(srcround) > 12)
                srcround = 9;
            ServerHub_chat.server.send(Contexxt, ip, ad, $('#hidKey').val(), '<div class="chat-right col-xs-6"><div class="row"><div class="col-xs-6"><div class="chat-context chat-right-context">' + htmlEncode(Contexxt) + '</div></div><div class="col-xs-6"><dl class="chat-right-title"><dt><span class="fll">' + ad + '</span><span class="flr"><img class="chat-right-title-img" src="../../Content/image/user/' + srcround + '.jpg" title="我是科布尔--科小智--机器人" width="35" height="35"></span></dt><dd><span class="fll">' + ip + '</span><span class="flr">' + htmlEncode(ost) + '</span></dd><dd><span class="fll">' + dateTime + '</span><span class="clear"></span></dd></dl></div></div></div>');
            $('#message').val('').focus();
        }
    });
}
// 为显示的消息进行Html编码
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
function Alert(result) {
    layer.alert(result, {
        skin: 'layui-layer-lan'
               , closeBtn: 0
               , shift: 4
    });
}
function hidshow(obj) {
    if (obj == 1) {
        $("#chat-send").show();
        $("#vsCode").hide();
        isF = false;
    } else {
        $("#chat-send").hide();
        $("#vsCode").show();
        isF = true;
    }
}