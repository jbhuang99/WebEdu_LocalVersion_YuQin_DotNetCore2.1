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
alert(document.getElementById("sIframeBook").src);
    }
    }