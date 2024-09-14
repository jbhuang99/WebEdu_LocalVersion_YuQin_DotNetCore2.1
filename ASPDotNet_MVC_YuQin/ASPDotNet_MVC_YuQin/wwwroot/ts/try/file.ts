namespace TryNameSpace {
    export class TryClass {
        public TryMothod(): Number {
            window.alert("TryNameSpace名称空间");
            document.getElementById('s1').innerHTML = "TryNameSpace名称空间，I'm comming...."
            return (2);
        }
        public TryProperty: String;
    }

}
//var http = new HttpClient();
//var uriBuilder = new URIBuilder("http://www.sogou.com/web");
var tryClass: TryNameSpace.TryClass = new TryNameSpace.TryClass();
var str: String = "";
tryClass.TryMothod();

