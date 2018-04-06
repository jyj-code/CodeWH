var fgm = {
    on: function (element, type, handler) {
        return element.addEventListener ? element.addEventListener(type, handler, false) : element.attachEvent("on" + type, handler)
    },
    un: function (element, type, handler) {
        return element.removeEventListener ? element.removeEventListener(type, handler, false) : element.detachEvent("on" + type, handler)
    },
    bind: function (object, handler) {
        return function () {
            return handler.apply(object, arguments)
        }
    },
    randomRange: function (lower, upper) {
        return Math.floor(Math.random() * (upper - lower + 1) + lower)
    },
    getRanColor: function () {
        var str = this.randomRange(0, 0xFFFFFF).toString(16);
        while (str.length < 6) str = "0" + str;
        return "#" + str
    }
};
//初始化对象
function FireWorks() {
    this.type = 0;
    this.timer = null;
    this.fnManual = fgm.bind(this, this.manual)
}
FireWorks.prototype = {
    initialize: function () {
        clearTimeout(this.timer);
        fgm.un(document, "click", this.fnManual);
        switch (this.type) {
            case 1:
                fgm.on(document, "click", this.fnManual);
                break;
            case 2:
                this.auto();
                break;
        };
    },
    manual: function (event) {
        event = event || window.event;
        this.__create__({
            x: event.clientX,
            y: event.clientY
        });
    },
    auto: function () {
        var that = this;
        that.timer = setTimeout(function () {
            that.__create__({
                x: fgm.randomRange(50, document.documentElement.clientWidth - 50),
                y: fgm.randomRange(50, document.documentElement.clientHeight - 150)
            })
            that.auto();
        }, fgm.randomRange(900, 1100))
    },
    __create__: function (param) {
        var that = this;
        var oEntity = null;
        var oChip = null;
        var aChip = [];
        var timer = null;
        var oFrag = document.createDocumentFragment();
        oEntity = document.createElement("div");
        with (oEntity.style) {
            position = "absolute";
            top = document.documentElement.clientHeight + "px";
            left = param.x + "px";
            width = "4px";
            height = "30px";
            borderRadius = "4px";
            background = fgm.getRanColor();
        };
        document.body.appendChild(oEntity);
        oEntity.timer = setInterval(function () {
            oEntity.style.top = oEntity.offsetTop - 20 + "px";
            if (oEntity.offsetTop <= param.y) {
                clearInterval(oEntity.timer);
                document.body.removeChild(oEntity);
                (function () {
                    //在50-100之间随机生成碎片
                    //由于IE浏览器处理效率低, 随机范围缩小至20-30
                    //自动放烟花时, 随机范围缩小至20-30
                    var len = (/msie/i.test(navigator.userAgent) || that.type == 2) ? fgm.randomRange(20, 30) : fgm.randomRange(50, 100)
                    for (i = 0; i < len; i++) {
                        oChip = document.createElement("div");
                        with (oChip.style) {
                            position = "absolute";
                            top = param.y + "px";
                            left = param.x + "px";
                            width = "4px";
                            height = "4px";
                            overflow = "hidden";
                            borderRadius = "4px";
                            background = fgm.getRanColor();
                        };
                        oChip.speedX = fgm.randomRange(-20, 20);
                        oChip.speedY = fgm.randomRange(-20, 20);
                        oFrag.appendChild(oChip);
                        aChip[i] = oChip
                    };
                    document.body.appendChild(oFrag);
                    timer = setInterval(function () {
                        for (i = 0; i < aChip.length; i++) {
                            var obj = aChip[i];
                            with (obj.style) {
                                top = obj.offsetTop + obj.speedY + "px";
                                left = obj.offsetLeft + obj.speedX + "px";
                            };
                            obj.speedY++;
                            (obj.offsetTop < 0 || obj.offsetLeft < 0 || obj.offsetTop > document.documentElement.clientHeight || obj.offsetLeft > document.documentElement.clientWidth) && (document.body.removeChild(obj), aChip.splice(i, 1))
                        };
                        !aChip[0] && clearInterval(timer);
                    }, 30)
                })()
            }
        }, 30)
    }
};
fgm.on(window, "load", function () {
    var oTips = document.getElementById("tips");
    var aBtn = oTips.getElementsByTagName("a");
    var oFireWorks = new FireWorks();
    fgm.on(oTips, "click", function (event) {
        var oEvent = event || window.event;
        var oTarget = oEvent.target || oEvent.srcElement;
        var i = 0;
        if (oTarget.tagName.toUpperCase() == "A") {
            for (i = 0; i < aBtn.length; i++) aBtn[i].className = "";
            switch (oTarget.id) {
                case "manual":
                    oFireWorks.type = 1;
                    break;
                case "auto":
                    oFireWorks.type = 2;
                    break;
                case "stop":
                    oFireWorks.type = 0;
                    break;
            }
            oFireWorks.initialize();
            oTarget.className = "active";
            oEvent.stopPropagation ? oEvent.stopPropagation() : oEvent.cancelBubble = true
        }
    });

});
fgm.on(document, "contextmenu", function (event) {
    var oEvent = event || window.event;
    oEvent.preventDefault ? oEvent.preventDefault() : oEvent.returnValue = false
});