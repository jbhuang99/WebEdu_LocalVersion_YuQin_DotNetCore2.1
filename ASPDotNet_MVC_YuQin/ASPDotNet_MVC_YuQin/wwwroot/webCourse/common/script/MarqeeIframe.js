function fnOnload() {
    var sSearch =new URLSearchParams(window.location.search);
    document.getElementById("sFromMarquee").innerHTML=sSearch.get('Marquee');            
            }

function fnOnUnload() {
    alert("bye");
   open("https://www.baidu.com/s?tn=baidu&wd=%E6%95%99%E8%82%B2%E6%A1%86%E6%9E%B6%E4%B8%8E%E6%A1%88%E4%BE%8B%E2%80%94%E2%80%94%E6%95%B0%E5%AD%97%E5%8C%96%E8%AE%A1%E7%AE%97%E6%80%9D%E7%BB%B4%E4%B8%8E%E4%BA%BA%E5%B7%A5%E6%99%BA%E8%83%BD%E7%BB%9F%E4%B8%80%E7%9A%84%E8%A7%86%E8%A7%92","winSearch");
     }