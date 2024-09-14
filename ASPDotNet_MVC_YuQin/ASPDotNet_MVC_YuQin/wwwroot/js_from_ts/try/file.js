var TryNameSpace;
(function (TryNameSpace) {
    var TryClass = /** @class */ (function () {
        function TryClass() {
        }
        TryClass.prototype.TryMothod = function () {
            window.alert("TryNameSpace名称空间");
            document.getElementById('s1').innerHTML = "TryNameSpace名称空间，I'm comming....";
            return (2);
        };
        return TryClass;
    }());
    TryNameSpace.TryClass = TryClass;
})(TryNameSpace || (TryNameSpace = {}));
//var http = new HttpClient();
//var uriBuilder = new URIBuilder("http://www.sogou.com/web");
var tryClass = new TryNameSpace.TryClass();
var str = "";
tryClass.TryMothod();
//# sourceMappingURL=file.js.map