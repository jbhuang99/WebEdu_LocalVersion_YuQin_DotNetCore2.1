/**
function fnSearchQueryString(){
            alert("window.location");
            alert(window.location.href);
            alert(window.location.search);
         var sSearch =new URLSearchParams(window.location.search);
            if(sSearch.has("text")){
         alert(document.getElementById("sIframeBook").src);
        document.getElementById("sIframeBook").src=document.getElementById("sIframeBook").src+"?text="+sSearch.get("text");
        alert(document.getElementById("sIframeBook").src);
}
else{
//document.getElementById("sIframeBook").src="initial.html";
alert();
alert(document.getElementById("sIframeBook").src);
    }
    }
**/
function getPort() {
            var sUrl = document.location.href;
            var sPartUrl = sUrl.substring(sUrl.lastIndexOf(":"));
            return sUrl.substring(sUrl.lastIndexOf(":") + 1, sUrl.lastIndexOf(":") + sPartUrl.indexOf("/"));
        }

function fnOnload() {

            if (screen.height / screen.width == 0.625) {

                window.iNewZoom = screen.width / 1280 * 100;
                document.body.style.zoom = iNewZoom + "%";

            }
            else {

                window.iNewZoom = screen.width / 1024 * 100;
                document.body.style.zoom = iNewZoom + "%";

            }
            document.title = document.title + ":" + getPort();
            document.body.scroll = "no";
        
            //alert(window.location.href.substring(window.location.href.lastIndexOf("?"),window.location.href.length-window.location.href.indexOf("?")));

            //var sQueryString=window.location.href.substring(window.location.href.lastIndexOf("?"),window.location.href.length-window.location.href.lastIndexOf("?"));
            //fnSearchQueryString();//难道是后续init.html加载时机的问题？(含frameset),无法调用函数？
  
            var sSearch =new URLSearchParams(window.location.search);
            if(sSearch.has("text")){
           // document.getElementById("sIframeBook").src="initial.html"+"?text="+sSearch.get("text"); //本机可以运行，github pages不行
            document.getElementById("sIframeBook").contentWindow.location.href="initial.html"+"?text="+sSearch.get("text");
            //alert(document.getElementById("sIframeBook").src);
        }
        else{
            //document.getElementById("sIframeBook").src="initial.html";//本机可以运行，github pages不行
             document.getElementById("sIframeBook").contentWindow.location.href="initial.html";
            //alert(document.getElementById("sIframeBook").src);
         }
}

        function RequestNotificationSubscriptionAsync() {
            var subscription = blazorPushNotifications.requestSubscription;
            console.log(subscription);
            if (subscription != null) {
                /*
                var request = new HttpRequestMessage(HttpMethod.Put, "notifications/subscribe");
                request.Content = JsonContent.Create(subscription);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
                await HttpClient.SendAsync(request);
                */
            }
        }