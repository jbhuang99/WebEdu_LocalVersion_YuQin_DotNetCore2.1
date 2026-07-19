function fnOnLoad() {
window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
//try{window.recognitionSystemInternal.stop();} catch(e){;}// 停止系统内部的语音识别
//try{window.recognitionSystemExternal.stop();} catch(e){;}// 停止系统外部的语音识别
window.sHref=window.location.href.substring(0,window.location.href.indexOf("/webCourse/"));

window.systemInternalRecognizingResultFlag =""; // 系统内部语音识别结果标志
window.systemExternalRecognizingResultFlag =""; // 系统内部语音识别结果标志

window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；

window.RunningLocalLLMID=""; 
window.RunningLocalLLMURL="";
window.DeletedLocalLLMID=""; 
window.DeletedLocalLLMURL="";
window.DownloadedLocalLLMID=""; 
window.DownloadedLocalLLMURL="";
window.LocalLLMDirPath="";
window.LocalLLMURLRoot=""; 
window.LocalLLMDirPathForDeleteTemp="";


document.getElementById('startBtnSystemInternal').addEventListener('click',fnStartBtnSystemInternalOnClick,false);          
document.getElementById('stopBtnSystemInternal').addEventListener('click',fnStopBtnSystemInternalOnClick, false); 
document.getElementById('startBtnSystemExternal').addEventListener('click',fnStartBtnSystemExternalOnClick,false);
document.getElementById('stopBtnSystemExternal').addEventListener('click',fnStopBtnSystemExternalOnClick,false);
//window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；//目前无法实现相关功能。
//document.getElementById('idQwenAPIKeyConfirm').addEventListener('click',fnidQwenAPIKeyConfirmOnClickSystemExternal,false);//目前无法实现相关功能。
//document.getElementById('startBtnSystemInternal').click(); // 页面加载后自动点击开始系统内部的录音按钮

if(document.getElementById("id_RadioSystemExternal").checked == true){
var sTextContent = document.getElementById("transcriptSystemExternal").textContent;
document.getElementById("id_CharNumber").textContent=sTextContent.length;
}
else{
var sTextContent = document.getElementById("transcriptSystemInternal").textContent;
document.getElementById("id_CharNumber").textContent=sTextContent.length;
}
document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value="“"+document.getElementById("idPrompt").value+"定义”";
document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value="“"+document.getElementById("idPrompt").value+"定义”的一道四个选项的单选题，适合用于考试测验。";
}

function fnStartFoundryLocal() {
    var sURL="/StartServiceFoundryLocal";      
       var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                      window.LocalLLMURLRoot=xmlHttpRequest.responseText;

                    // alert(window.LocalLLMURLRoot);
                     document.getElementById("idIsStarted").textContent="已经启动";
                     //window.DeletedLocalLLMDirPath=JSON.parse(xmlHttpRequest.responseText).modelDirPathxml; 
                     //alert(indow.DeletedLocalLLMDirPath);
                    }
                    else {
                        alert('出错了,模型启动可能需要等候一定时间，请稍后再试单击本按钮！Err：' + xmlHttpRequest.status);
                    }
                }
                }
    }
function fnRadioListDownloadLocalLLM() {
if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("本机网站发布")>=0)
{
     var oTempDownloadingLocalLLM=document.getElementById("id_DownloadingLocalLLM");
    //var oTemp=event.srcElement.nextElementSibling;
    var oTempParentElement=event.srcElement.parentElement;
    if(document.getElementById("idRunableLocalLLM").innerHTML.indexOf(oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量：")))>=0) {alert("当前本机LLM已经下载，无需重复下载！");return;}//如果已经下载过了，就不再下载了。
            //oTemp.textContent="正在下载LLM，LLM文件容量较大，请耐心等待，可在服务端Console视图查看下载进度...";
            oTempDownloadingLocalLLM.textContent="正在下载LLM，LLM文件容量较大，请耐心等待，可在服务端Console视图查看下载进度...请不要关闭服务端Console视图!!!";
            var sURL = "/ListDownloadLocalLLM?Model="+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量："));
            //var sURL = "/RunLocalLLM?Model=" + oTemppreviousElementSibling.textContent;           
            var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                        window.DownloadedLocalLLMURL=xmlHttpRequest.responseText.split("|||")[1];
                       // oTemp.textContent="当前本机LLM是"+xmlHttpRequest.responseText.split("|||")[0]+"已下载，如果不再使用，可以删除..."; 
                         oTempDownloadingLocalLLM.textContent="当前本机LLM是"+xmlHttpRequest.responseText.split("|||")[0]+"已下载，如果不再使用，可以删除..."; 
                        window.DownloadedLocalLLMID = xmlHttpRequest.responseText.split("|||")[0]; 
                        fnAjaxRunableLocalLLM();
                        fnStartFoundryLocal();
                    }
                    else {
                        alert('出错了,Err：' + xmlHttpRequest.status);
                    }
                }
                }
}
else{
alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom()+"此操作属于本机运行功能，已被禁止！！！请下载本软件的整体VS解决方案本机运行，实现本功能。");
return;
}
}

function fnLocalLLMDirPath() {
    var sURL=window.LocalLLMURLRoot+"openai/status";      
       var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                     window.LocalLLMDirPath=JSON.parse(xmlHttpRequest.responseText).modelDirPath; 
                     window.LocalLLMDirPathForDeleteTemp=window.LocalLLMDirPath;
                     //alert(window.LocalLLMDirPath+"|||"+window.LocalLLMDirPathForDeleteTemp);
                    }
                    else {
                        alert('出错了,模型启动可能需要等候一定时间，请稍后再试单击本按钮！,Err：' + xmlHttpRequest.status);
                    }
                }
                }
    }
function fnUnloadallLocalLLM() {
    var sURL=window.LocalLLMURLRoot+"openai/unloadall";      
       var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                     return;
                    }
                    else {
                        alert('出错了,卸载其他正在运行的模型出错！,Err：' + xmlHttpRequest.status);
                    }
                }
                }
    }
  function fnLoadLocalLLM(){
      var sURL=window.RunningLocalLLMURL+ "openai/load/" + window.RunningLocalLLMID
       var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                     return;
                    }
                    else {
                        alert('出错了,装载模型“'+window.RunningLocalLLMID+'”出错！,Err：' + xmlHttpRequest.status);
                    }
                }
                }
    }
function fnRadioDeleteLocalLLM() {
 if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("本机网站发布")>=0)
 {
     //fnLocalLLMDirPath();
     //window.LocalLLMDirPath="C:\\Users\\1\\.foundry\\cache\\models";
     window.LocalLLMDirPath=window.LocalLLMDirPathForDeleteTemp.replace(/\\/g, "\\\\");
     if(window.LocalLLMDirPathForDeleteTemp==null || window.LocalLLMDirPathForDeleteTemp==""){alert("请当击上方按钮，重新启动服务！");return;}
     alert(window.LocalLLMDirPathForDeleteTemp+"|||"+window.LocalLLMDirPath);
   //var oUnloadAllWin = window.open(window.RunningLocalLLMURL+ "openai/unloadall","TempWin");oUnloadAllWin.close();//服务端未能实现自动关闭本机LLM的网页界面，故此处使用window.open()方法打开本机LLM的网页界面，并立即关闭。
   //prompt("当前本机LLM是"+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量："))+"将删除，如果需要，请重新下载...");
   if (confirm("请确认您的操作")) {
// 用户点击了确定
    var oTemp=event.srcElement.nextElementSibling;
    var oTempParentElement=event.srcElement.parentElement;
            oTemp.textContent="正在删除LLM...";
            var sURL = "/DeleteLocalLLM?Model="+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量：")).replace(/:/g, "-")+"&localLLMDirPath="+window.LocalLLMDirPath;
            //var sURL = "/DeleteLocalLLM?Model="+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量："))+"&localLLMDirPath="+window.LocalLLMDirPath;
            alert(sURL);
            //var sURL = "/RunLocalLLM?Model=" + oTemppreviousElementSibling.textContent;           
            var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                        alert(xmlHttpRequest.responseText);
                        oTemp.textContent="当前本机LLM是"+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量："))+"已删除，如果需要，请重新下载..."; 
                        fnAjaxRunableLocalLLM();
                        fnStartFoundryLocal();
                    }
                    else {
                        alert('出错了,Err：' + xmlHttpRequest.status);
                    }
                }
                }
                } else {
event.srcElement.checked = false; // 取消选中状态
}
}
else{
    alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom()+"此操作属于本机运行功能，已被禁止！！！请下载本软件的整体VS解决方案本机运行，实现本功能。");
    return;
}
}
function fnRadioRunLocalLLM() {
    var oTempRunningLocalLLM=document.getElementById("id_RunningLocalLLM");
    //var oTemp=event.srcElement.nextElementSibling;
    var oTempParentElement=event.srcElement.parentElement;
           // oTemp.textContent="正在启动运行LLM...";
             oTempRunningLocalLLM.textContent="正在启动运行LLM...";
            var sURL = "/RunLocalLLM?Model="+oTempParentElement.innerHTML.substring(0, oTempParentElement.innerHTML.indexOf("（文件容量："));
            //var sURL = "/RunLocalLLM?Model=" + oTemppreviousElementSibling.textContent;           
            var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行 
                        window.RunningLocalLLMURL=xmlHttpRequest.responseText.split("|||")[1];
                       // oTemp.textContent="当前本机LLM是"+xmlHttpRequest.responseText.split("|||")[0]+"已运行，等候客户端的Prompt..."; 
                        oTempRunningLocalLLM.textContent="当前本机LLM是"+xmlHttpRequest.responseText.split("|||")[0]+"已运行，等候客户端的Prompt...";
                        window.RunningLocalLLMID = xmlHttpRequest.responseText.split("|||")[0]; 
                        //var oUnloadAllWin = window.open(window.RunningLocalLLMURL+ "openai/unloadall","TempWin");oUnloadAllWin.close();//服务端未能实现自动关闭本机LLM的网页界面，故此处使用window.open()方法打开本机LLM的网页界面，并立即关闭。
                        fnUnloadallLocalLLM();
                        //var oLoadWin =window.open(window.RunningLocalLLMURL+ "openai/load/" + window.RunningLocalLLMID,"TempWin");oLoadWin.close();//服务端未能实现自动打开本机LLM的网页界面，故此处使用window.open()方法打开本机LLM的网页界面。
                         fnLoadLocalLLM();
                        //window.winTTS_STT_LLM_AIGC_Robot_RAG_Agent_Copilot.focus();//当前窗口聚焦，但是没能实现
                       // window.focus();
                    }
                    else {
                        alert('出错了,Err：' + xmlHttpRequest.status);
                    }
                }
                }
            }

  function fnBlurRadioRunLocalLLM(){      
      var oTemp=event.srcElement.nextElementSibling;
            oTemp.textContent="请单选运行LLM...";
            }

   function fnBlurRadioDeleteLocalLLM() {
      var oTemp=event.srcElement.nextElementSibling;
            oTemp.textContent="请单选删除LLM...";
            }            
   function fnBlurRadioListDownloadLocalLLM() {
      var oTemp=event.srcElement.nextElementSibling;
            oTemp.textContent="请单选下载LLM...";
            }  
 function fnAjaxListableLocalLLM() {
     var oTempDownloadingLocalLLM=document.getElementById("id_DownloadingLocalLLM");
      oTempDownloadingLocalLLM.textContent="正在查询返回可供下载的本机LLM，请耐心等候...";
           window.sReturnedListLocalLLM= "";
           document.getElementById("idListableLocalLLM").innerHTML = "";
            var sURL = "/ListableLocalLLM";
            var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var sTemp = xmlHttpRequest.responseText;
                        window.sReturnedListLocalLLM=sTemp;                        
                        if (sTemp == "") {
                            alert("未能列出本机LLM的相关内容！");
                        }
                        else {
                            document.getElementById("idListableLocalLLM").innerHTML ="<ol><li>"+sTemp.slice(0, -"|||".length).replace(/(\|\|\|)+/g, '<input type="radio" name="raio_ListableLocalLLM" onclick="fnRadioListDownloadLocalLLM()" onblur="fnBlurRadioListDownloadLocalLLM()"/><span>请单选下载LLM...</span></li><li>')+'<input type="radio" name="raio_ListableLocalLLM" onclick="fnRadioListDownloadLocalLLM()" onblur="fnBlurRadioListDownloadLocalLLM()"/><span>请单选下载LLM...</span></li></ol>';//将约定的"|||"替换为HTML的换行标签
                           oTempDownloadingLocalLLM.textContent="已查询到可供下载的本机LLM如下，请单选下载...";
                        }

                    }
                    else {
                        alert('出错了,Err：' + xmlHttpRequest.status);
                    }
                }
            }
        }
        
         function fnAjaxRunableLocalLLM() {
             var oTempRunningLocalLLM=document.getElementById("id_RunningLocalLLM");
             oTempRunningLocalLLM.textContent="正在查询返回可供运行的本机LLM，请耐心等候...";
           window.sReturnedRunLocalLLM= "";
           document.getElementById("idRunableLocalLLM").innerHTML = "";
            var sURL = "/RunableLocalLLM";
            var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
            xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var sTemp = xmlHttpRequest.responseText;
                        window.sReturnedRunLocalLLM=sTemp;                       
                        if (sTemp == "") {
                            alert("未能列出本机LLM的相关内容！");
                        }
                        else {
                            document.getElementById("idRunableLocalLLM").innerHTML ="<ol><li>"+sTemp.slice(0, -"|||".length).replace(/(\|\|\|)+/g, '<input type="radio" name="raio_RunableLocalLLM" onclick="fnRadioRunLocalLLM()" onblur="fnBlurRadioRunLocalLLM()"/><span>请单选运行LLM...</span><input type="radio" name="raio_DeletableLocalLLM" onclick="fnRadioDeleteLocalLLM()" onblur="fnBlurRadioDeleteLocalLLM()"/><span>请单选删除LLM...</span></li><li>')+'<input type="radio" name="raio_RunableLocalLLM" onclick="fnRadioRunLocalLLM()" onblur="fnBlurRadioRunLocalLLM()"/><span>请单选运行LLM...</span><input type="radio" name="raio_DeletableLocalLLM" onclick="fnRadioDeleteLocalLLM()" onblur="fnBlurRadioDeleteLocalLLM()"/><span>请单选删除LLM...</span></li></ol>';//将约定的"|||"替换为HTML的换行标签
                              var TempString=xmlHttpRequest.responseText.split("|||")[0];
                            window.LocalLLMURLRoot=TempString.substring(TempString.indexOf("；URL根：")+"；URL根：".length, TempString.lastIndexOf("）"));
                            oTempRunningLocalLLM.textContent="已查询到可供运行的本机LLM如下，请单选运行...";
                        }

                    }
                    else {
                        alert('出错了,Err：' + xmlHttpRequest.status);
                    }
                }
            }
        }

function fnAjaxServerSideCallBCI(){
    var xmlHttpRequest = new XMLHttpRequest();
           xmlHttpRequest.open('GET', '/BCI', true);
           xmlHttpRequest.send();
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        /** 
                        var oTemp=JSON.parse(xmlHttpRequest.responseText);
                         document.getElementById("idAttention").value=oTemp.output.text; 
                          document.getElementById("idMeditation").value=oTemp.output.text;  
                        document.getElementById("idRawSignal").value=oTemp.output.text;  
                        **/
                      // var oTemp=JSON.parse(xmlHttpRequest.responseText);
                         document.getElementById("idAttention").value=xmlHttpRequest.responseText; 
                          document.getElementById("idMeditation").value=xmlHttpRequest.responseText;  
                        document.getElementById("idRawSignal").value=xmlHttpRequest.responseText;  
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);                        
                    }
                }
                }
}
/**
 * 使用 Intl.Segmenter 实现中文字符串语义相似度估算（0.0 ~ 1.0）
 * 原理：分词 → 去停用词 → 词频统计 → Jaccard 集合相似度
 * 纯前端、无依赖、兼容 Chrome 93+/Edge 93+/Safari 16.4+/Firefox 111+
 **/
function fnStringSemanticSimilarity(str1, str2, options = {}) {
  const { language = 'zh', minLen = 2, stopwords = new Set(['的', '了', '在', '是', '我', '有', '和', '就', '不', '人', '都', '一', '一个', '上', '也', '很', '到', '说', '要', '去', '你', '会', '着', '没有', '看', '好', '自己', '这']) } = options;

  // 1. 初始化分词器（自动检测语言，中文推荐显式指定 'zh'）
  const segmenter = new Intl.Segmenter(language, { granularity: 'word' });

  // 2. 分词并过滤（保留长度≥minLen的非停用词）
  const tokenize = (s) => {
    return Array.from(segmenter.segment(s))
      .filter(seg => seg.isWordLike && seg.segment.length >= minLen)
      .map(seg => seg.segment.trim())
      .filter(word => word && !stopwords.has(word));
  };

  const words1 = new Set(tokenize(str1));
  const words2 = new Set(tokenize(str2));

  // 3. Jaccard 相似度：交集 / 并集
  const intersection = [...words1].filter(w => words2.has(w)).length;
  const union = new Set([...words1, ...words2]).size;

  return union > 0 ? intersection / union : 0.0;
  //console.log(fnStringSemanticSimilarity("采购笔记本电脑", "购买手提电脑")); // → ≈ 0.50（分词：['采购','笔记本','电脑'] ∩ ['购买','手提','电脑'] → 交集{'电脑'} → 1/5=0.2？等等）
}

function fnGetCurrentContentsItem(){
    document.getElementById("idPromptInternalLLM").value=opener.parent.document.getElementById("sIframeContents").contentWindow.oSrcElement.childNodes.item(0).nodeValue;
    document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value="“"+opener.parent.document.getElementById("sIframeContents").contentWindow.oSrcElement.childNodes.item(0).nodeValue+"”";
     document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value="“"+opener.parent.document.getElementById("sIframeContents").contentWindow.oSrcElement.childNodes.item(0).nodeValue+"”的一道四个选项的单选题，适合用于考试测验。";
    }

function fnOpenLocalhostDingTalkAIGC(){
try{
   //window.open("dingtalk://", "_blank");
    alert("请在您的本机操作系统中，打开运行您本机安装的免费AIGC客户端（例如，免费的钉钉AIGC助理）！如果没有安装，请Web搜索下载安装！");
}
catch(e){
   // alert("请手动打开您本机安装的钉钉AIGC助理！");
    }
finally{
   // alert("请手动打开您本机安装的钉钉AIGC助理！");
}    
}

function fnToggleEventSoureElementColor(){
   var eventSoureElementColor=window.event.target.style.color;
    
    if(eventSoureElementColor==""||eventSoureElementColor==null){
        window.event.target.style.color="brown";
    }
    else{
        window.event.target.style.color="";
    }
}

/**
function fnBtnSystemInternalOnClick() {
                window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
                //window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
                window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
               // window.isRecognizingSystemInternal = true;//不知是否需要设置；
               // window.isRecognizingSystemExternal = false;//不知是否需要设置；
                //window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；暂时无法实现相关功能。
                document.getElementById('btnSystemInternal').disabled=true;
                document.getElementById('btnSystemExternal').disabled=false;
                document.getElementById('startBtnSystemInternal').disabled=false;
                document.getElementById('stopBtnSystemInternal').disabled=true; 
                document.getElementById('startBtnSystemExternal').disabled=true; 
                document.getElementById('stopBtnSystemExternal').disabled=true; 
                document.getElementById('startBtnSystemInternal').click();                
}

function fnBtnSystemExternalOnClick() {
               window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
                window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
               // window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
               // window.isRecognizingSystemInternal = false;//不知是否需要清空；
               // window.isRecognizingSystemExternal = true;//不知是否需要设置；
                //window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；暂时无法实现相关功能。
                document.getElementById('btnSystemExternal').disabled=true;
                document.getElementById('btnSystemInternal').disabled=false;
                document.getElementById('startBtnSystemInternal').disabled=true;
                document.getElementById('stopBtnSystemInternal').disabled=true; 
                document.getElementById('startBtnSystemExternal').disabled=false; 
                document.getElementById('stopBtnSystemExternal').disabled=true; 
                document.getElementById('startBtnSystemExternal').click();
 }
 **/

 function fnTTSOnEndSystemInternal(){
window.speechSynthesis.cancel();
const utteranceInternal ="您的Prompt指令是“"+document.getElementById("idPromptDirective").value+"”对吗？请关注系统内部主界面视图中的结果！"; //在此可以无需朗读。
document.getElementById('transcriptSystemInternal').textContent =utteranceInternal;
//window.isRecognizingSystemInternal = false;
//window.recognitionSystemInternal = null; 为了配合语音朗读TTS，所以fnSTTOnResultSystemInternal中的停止语音识别STT，但是因为还未能实现打断语音朗读，所以暂时放弃。
switch (true) {
case window.systemInternalRecognizingResultFlag =="外部": {
   document.getElementById('startBtnSystemExternal').click(); //切换到系统外部语音对话，即，开始系统外部录音Prompt指令。已经退出系统内部录音Prompt子系统。
   //语音提示用户已经切换到系统外部语音对话，已经退出系统内部录音Prompt子系统；
    break;
}

case window.systemExternalRecognizingResultFlag =="停止录音": {
   document.getElementById('stopBtnSystemInternal').click(); 
   //语音提示用户停止系统内部录音Prompt指令；   
    break;
}
case window.systemInternalRecognizingResultFlag =="向上": {
    //opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").style.display="none";//为了配合语音朗读TTS，所以fnSTTOnResultSystemInternal中的停止语音识别STT，但是因为还未能实现打断语音朗读，所以暂时放弃。
   //opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").style.display="none";  
  opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").disabled = true;
   opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("previous").click();    
   //增加内容框架的朗读功能。
    break;
}
case window.systemInternalRecognizingResultFlag =="向下": {
  // opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").style.display="none";//为了配合语音朗读TTS，所以fnSTTOnResultSystemInternal中的停止语音识别STT，但是因为还未能实现打断语音朗读，所以暂时放弃。
  // opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").style.display="none";
  opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("id_TTS").disabled = true;
   opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.getElementById("next").click();
   //增加内容框架的朗读功能。
    break;
    }
default:{
    alert("语音识别没有准确匹配的，默认打开滚动消息，以供了解最新咨询！");//修改成为TTS语音提示
    opener.fnMarquee();
    }
}
 document.getElementById('stopBtnSystemInternal').click();//如果包含语音朗读TTS，需要放置在onend回调中，否则会导致无法朗读AIGC回答Answer。
 //document.getElementById('startBtnSystemInternal').click();//如果包含语音朗读TTS，需要放置在onend回调中，否则会导致无法朗读AIGC回答Answer。
    }


 function fnTTSOnEndSystemExternal(){
 window.speechSynthesis.cancel();
//window.isRecognizingSystemExternal = false;
//window.recognitionSystemExternal = null; 为了配合语音朗读TTS，所以fnSTTOnResultSystemExternal中的停止语音识别STT，但是因为还未能实现打断语音朗读，所以暂时放弃。
 switch (true) {
case window.systemExternalRecognizingResultFlag =="内部": {
   document.getElementById('startBtnSystemInternal').click(); //切换到系统内部语音对话，即，开始系统内部录音Prompt指令。已经退出系统外部录音Prompt子系统。
   //语音提示这是一个内部Prompt指令，而非Prompt提问，用户已经切换到系统内部语音对话，并且准备就绪录音Prompt指令；   
    break;
}
case window.systemExternalRecognizingResultFlag =="停止录音": {
   document.getElementById('stopBtnSystemExternal').click(); 
   //语音提示这是一个内部Prompt指令，而非Prompt提问，用户停止系统外部录音Prompt指令；   
    break;
}

case window.systemExternalRecognizingResultFlag =="开始": {
  // 语音提示这是一个内部Prompt指令，而非Prompt提问，语音AIGC回答Answer的TTS语音朗读的从头开始指令。
    break;
    }
case window.systemExternalRecognizingResultFlag =="暂停": {
   //语音提示这是一个内部Prompt指令，而非Prompt提问， AIGC回答Answer的TTS语音朗读的暂停指令。
    break;
    }
case window.systemExternalRecognizingResultFlag =="继续": {
  //语音提示这是一个内部Prompt指令，而非Prompt提问， AIGC回答Answer的TTS语音朗读的继续恢复指令。
    break;
    }
case window.systemExternalRecognizingResultFlag =="结束": {
  // 语音提示这是一个内部Prompt指令，而非Prompt提问，AIGC回答Answer的TTS语音朗读的结束指令。
    break;
    }
default:{
    //AIGC回答Answer的默认朗读的指令;  
    // document.getElementById('idPrompt').value=window.transcriptSystemExternal;
      document.getElementById('idButtonAjaxServerSideCallAIGCAnswerCharactor').click();//TTS/STT的时候，可以调用初创方函数，无法调用自创方函数，不知为什么。可能是需要使用onend回调。
    }
}
 document.getElementById('stopBtnSystemExternal').click();//如果包含语音朗读TTS，需要放置在onend回调中，否则会导致无法朗读AIGC回答Answer。
 //document.getElementById('startBtnSystemExternal').click();//如果包含语音朗读TTS，需要放置在onend回调中，否则会导致无法朗读AIGC回答Answer。
    }

function fnStartBtnSystemInternalOnClick() {
    window.speechSynthesis.cancel();
    try{window.recognitionSystemExternal.stop();}catch(e){;}// 停止系统内部的语音识别
    document.getElementById('startBtnSystemInternal').disabled=true; 
    document.getElementById('stopBtnSystemInternal').disabled=false; 
    document.getElementById('startBtnSystemExternal').disabled=false; 
    document.getElementById('stopBtnSystemExternal').disabled=true; 

    window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
    window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；

    window.recognitionSystemInternal = new window.SpeechRecognition();
    window.recognitionSystemInternal.lang = 'zh-CN'; // 设置语言为中文
    window.recognitionSystemInternal.interimResults = true; // 启用中间结果
    window.recognitionSystemInternal.continuous = true; // 启用连续识别

    window.recognitionSystemInternal.onstart =fnSTTOnStartSystemInternal; // 语音识别开始时的回调            
    window.recognitionSystemInternal.onerror=fnSTTOnErrorSystemInternal; //// 语音识别出错时的回调(event)参数不知是否正确传递了。            
    window.recognitionSystemInternal.onresult=fnSTTOnResultSystemInternal;//(event)参数不知是否正确传递了。
    window.recognitionSystemInternal.onend=fnSTTOnEndSystemInternal;// 语音识别结束时的回调

    document.getElementById('transcriptSystemInternal').innerText='"段落": []'; // 清空显示区域
    window.speechContentParagraphsSystemInternal = { paragraphs: [] }; // 重置识别内容
    window.speechContentSentencesSystemInternal = { sentences: [] }; // 重置当前段落
   
    if (window.recognitionSystemInternal && !window.isRecognizingSystemInternal) {
           window.recognitionSystemInternal.start(); // 启动语音识别
       }
    }

function fnStartBtnSystemExternalOnClick() {
    window.speechSynthesis.cancel();
    try{window.recognitionSystemInternal.stop();}catch(e){;}// 停止系统内部的语音识别
    if(window.QwenAPIKey==""){
        alert("您可能还没输入并确认您的Qwen千问APIKey！");//该部分欲实现客户端浏览器中密码登录方式提供AIGC的APIKey，调用AIGC的功能，当前AIGC声明因为APIKey容易泄露的原因，一般不支持，故此功能暂时无法实现。
                }
    else{
     document.getElementById('startBtnSystemInternal').disabled=false; 
     document.getElementById('stopBtnSystemInternal').disabled=true; 
     document.getElementById('startBtnSystemExternal').disabled=true; 
     document.getElementById('stopBtnSystemExternal').disabled=false; 

    window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
    window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
    

    window.recognitionSystemExternal = new window.SpeechRecognition();
    window.recognitionSystemExternal.lang = 'zh-CN'; // 设置语言为中文
    window.recognitionSystemExternal.interimResults = true; // 启用中间结果
    window.recognitionSystemExternal.continuous = true; // 启用连续识别

    window.recognitionSystemExternal.onstart =fnSTTOnStartSystemExternal; // 语音识别开始时的回调            
    window.recognitionSystemExternal.onerror=fnSTTOnErrorSystemExternal; // 语音识别出错时的回调(event)参数不知是否正确传递了。            
    window.recognitionSystemExternal.onresult=fnSTTOnResultSystemExternal;//(event)参数不知是否正确传递了。
    window.recognitionSystemExternal.onend=fnSTTOnEndSystemExternal;// 语音识别结束时的回调

    document.getElementById('transcriptSystemExternal').innerText='"段落": []'; // 清空显示区域
    window.speechContentParagraphsSystemExternal = { paragraphs: [] }; // 重置识别内容
    window.speechContentSentencesSystemExternal = { sentences: [] }; // 重置当前段落
   
    if (window.recognitionSystemExternal && !window.isRecognizingSystemExternal) {
           window.recognitionSystemExternal.start(); // 启动语音识别
       }            
    }
  }

function fnStopBtnSystemInternalOnClick() {
    
    document.getElementById('transcriptSystemInternal').innerText= '"段落": []'; // 清空显示区域
    window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
    window.recognitionSystemInternal.stop();
    try{window.recognitionSystemExternal.stop();}catch(e){;}// 停止系统内部的语音识别
     /** 
   if (window.recognitionSystemInternal && window.isRecognizingSystemInternal) {
                    window.recognitionSystemInternal.stop(); // 停止语音识别
                }
                **/
     document.getElementById('startBtnSystemInternal').disabled=false;
     document.getElementById('stopBtnSystemInternal').disabled=true; 
     document.getElementById('startBtnSystemExternal').disabled=false; 
     document.getElementById('stopBtnSystemExternal').disabled=true; 

    
    window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
    window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
    }

function fnStopBtnSystemExternalOnClick() {
     //document.getElementById("idPrompt").value="";
     document.getElementById('transcriptSystemInternal').innerText= '"段落": []'; // 清空显示区域
    window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
    window.recognitionSystemExternal.stop();
    try{window.recognitionSystemExternal.stop();}catch(e){;}// 停止系统内部的语音识别
    /** 
    if (window.recognitionSystemExternal && window.isRecognizingSystemExternal) {
                    window.recognitionSystemExternal.stop(); // 停止语音识别
                } 
    **/
    document.getElementById('startBtnSystemInternal').disabled=false;
    document.getElementById('stopBtnSystemInternal').disabled=true; 
    document.getElementById('startBtnSystemExternal').disabled=false; 
    document.getElementById('stopBtnSystemExternal').disabled=true; 
    window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
     window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
    //window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；               
   }
 function fnSTTOnErrorSystemInternal(event) {
alert(`系统内部语音对话时候的语音识别错误: ${event.error}`);
 window.isRecognizingSystemInternal = false;
       }
function fnUnderstandKernelDialogueBtnSystemInternal() {
   fnToggleEventSoureElementColor();
    if(document.getElementById("kernelDialogueSystemInternal").style.display=="none"){
    document.getElementById("kernelDialogueSystemInternal").style.display="block";
    document.getElementById("idRightTriangleOfUnderstandKernelDialogueSystemInternal").textContent="▼";
    } 
else{
    document.getElementById("kernelDialogueSystemInternal").style.display="none";
    document.getElementById("idRightTriangleOfUnderstandKernelDialogueSystemInternal").textContent="▶"
    }
}
function fnUnderstandKernelDialogueBtnSystemExternal() {
   fnToggleEventSoureElementColor();
    if(document.getElementById("kernelDialogueSystemExternal").style.display=="none"){
    document.getElementById("kernelDialogueSystemExternal").style.display="block";
    document.getElementById("idRightTriangleOfUnderstandKernelDialogueSystemExternal").textContent="▼";
    } 
else{
    document.getElementById("kernelDialogueSystemExternal").style.display="none"
    document.getElementById("idRightTriangleOfUnderstandKernelDialogueSystemExternal").textContent="▶"
}
}

function fnSTTOnErrorSystemExternal(event) {
alert(`系统外部语音对话时候的语音识别错误: ${event.error}`);
 window.isRecognizingSystemExternal = false;
       }
function fnSTTOnStartSystemInternal() {
    window.isRecognizingSystemInternal = true;
       }
function fnSTTOnStartSystemExternal() {
    window.isRecognizingSystemExternal = true;
       }

function fnSTTOnEndSystemInternal() {
    window.isRecognizingSystemInternal = false;
    window.speechSynthesis.cancel();
    document.getElementById("idPromptDirective").value=window.transcriptSystemInternal;
    //TTS
    const utteranceInternal = new SpeechSynthesisUtterance("您的Prompt指令是“"+window.transcriptSystemInternal+"”对吗？请关注系统主界面视图中的结果！"); 
    document.getElementById('transcriptSystemInternal').textContent ="您的Prompt指令是“"+window.transcriptSystemInternal+"”对吗？请关注系统主界面视图中的结果！";
     window.speechSynthesis.speak(utteranceInternal); 
    switch (true) {
case window.transcriptSystemInternal.indexOf("外部")>=0: {
    window.systemInternalRecognizingResultFlag ="外部"; 
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    break;
}
case window.transcriptSystemInternal.indexOf("向上")>=0: {
    window.systemInternalRecognizingResultFlag ="向上"; 
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    break;
}
case window.transcriptSystemInternal.indexOf("向下")>=0: {
    window.systemInternalRecognizingResultFlag ="向下"; 
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    break;
    }
default:{
    window.systemInternalRecognizingResultFlag ="default"; 
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    }
}
}

function fnSTTOnEndSystemExternal() {
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
       //TTS  
    document.getElementById("idPrompt").value=window.transcriptSystemExternal;
    const utteranceExternal = new SpeechSynthesisUtterance("您的Prompt提问是“"+window.transcriptSystemExternal+"”对吗？"); 
    window.speechSynthesis.speak(utteranceExternal);  
     switch (true) {
    case window.transcriptSystemExternal.indexOf("内部")>=0: {
    window.systemExternalRecognizingResultFlag ="内部"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    break;
    }
    case window.transcriptSystemExternal.indexOf("开始")>=0: {
    window.systemExternalRecognizingResultFlag ="开始"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    break;
    }
    case window.transcriptSystemExternal.indexOf("暂停")>=0: {
    window.systemExternalRecognizingResultFlag ="暂停"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    break;
    }
    case window.transcriptSystemExternal.indexOf("继续")>=0: {
    window.systemExternalRecognizingResultFlag ="继续"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    break;
    }
    case window.transcriptSystemExternal.indexOf("停止")>=0: {
    window.systemExternalRecognizingResultFlag ="停止"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    break;
    }
    default:{
    window.systemInternalRecognizingResultFlag ="default"; 
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    }
}     
}

   function fnOnEndSTTOnEndUtteranceExternal() {
      window.isRecognizingSystemExternal = true;
     window.speechSynthesis.cancel(); 
       }

function fnSTTOnResultSystemInternal(event) {
    // 遍历所有识别结果
    for (var i = event.resultIndex; i < event.results.length; i++) {
    var result = event.results[i];//const result = event.results[i];
    window.transcriptSystemInternal = result[0].transcript.trim();//const transcriptSystemInternal = result[0].transcript.trim(); // 获取识别的文本
    var isFinal = result.isFinal;// const isFinal = result.isFinal; // 是否为最终结果
                    
     if (isFinal) {
      // 如果是最终结果，将句子添加到当前段落
      window.speechContentSentencesSystemInternal.sentences.push({ finalTranscript: window.transcriptSystemInternal});
     window.speechContentParagraphsSystemInternal.paragraphs.push(window.speechContentSentencesSystemInternal); // 将段落添加到speechContent。window.speechContent=window.speechContentSentencesSystemInternal
      window.speechContentSentencesSystemInternal = { sentences: [] }; // 重置当前段落。
                    } 
     else {
     // 如果是临时结果，更新当前段落的最后一个句子
         if (window.speechContentSentencesSystemInternal.sentences.length === 0) {
         // 如果当前段落没有句子，添加一个临时句子
         window.speechContentSentencesSystemInternal.sentences.push({ interimTranscript: window.transcriptSystemInternal });
          } 
          else {
             // 更新最后一个句子的临时文本
             window.speechContentSentencesSystemInternal.sentences[window.speechContentSentencesSystemInternal.length - 1].interimTranscript = window.transcriptSystemInternal;
               }
            }
         }
           
     document.getElementById('transcriptSystemInternal').textContent = JSON.stringify(window.speechContentParagraphsSystemInternal, null, 2);
     window.recognitionSystemInternal.stop(); // 停止语音识别，进行TTS         
}

function fnSTTOnResultSystemExternal(event) {
    // 遍历所有识别结果
    for (var i = event.resultIndex; i < event.results.length; i++) {
    var result = event.results[i];//const result = event.results[i];
    window.transcriptSystemExternal = result[0].transcript.trim();//const transcriptSystemInternal = result[0].transcript.trim(); // 获取识别的文本
    //window.transcriptForTTSSystemInternal=transcript;
    var isFinal = result.isFinal;// const isFinal = result.isFinal; // 是否为最终结果
                    
     if (isFinal) {
      // 如果是最终结果，将句子添加到当前段落
      window.speechContentSentencesSystemExternal.sentences.push({ finalTranscript: window.transcriptSystemExternal});//
     window.speechContentParagraphsSystemExternal.paragraphs.push(window.speechContentSentencesSystemExternal); // 将段落添加到speechContent。window.speechContent=window.speechContentSentencesSystemInternal
      window.speechContentSentencesSystemExternal = { sentences: [] }; // 重置当前段落。
                    } 
     else {
     // 如果是临时结果，更新当前段落的最后一个句子
         if (window.speechContentSentencesSystemExternal.sentences.length === 0) {
         // 如果当前段落没有句子，添加一个临时句子
         window.speechContentSentencesSystemExternal.sentences.push({ interimTranscript: window.transcriptSystemExternal });
          } 
          else {
             // 更新最后一个句子的临时文本
             window.speechContentSentencesSystemExternal.sentences[window.speechContentSentencesSystemExternal.length - 1].interimTranscript = transcriptSystemExternal;
               }
            }
         }      
     document.getElementById('transcriptSystemExternal').textContent = JSON.stringify(window.speechContentParagraphsSystemExternal, null, 2);    
     window.recognitionSystemExternal.stop(); // 停止语音识别，进行TTS    
}



function fnUpdateTranscriptSystemInternal() {
document.getElementById('transcriptSystemInternal').textContent = JSON.stringify(speechContentParagraphsSystemInternal, null, 2); // 将speechContentParagraphsSystemInternal对象格式化为JSON字符串，并显示在页面上
            }

function fnUpdateTranscriptSystemExternal() {
document.getElementById('transcriptSystemExternal').textContent = JSON.stringify(speechContentParagraphsSystemExternal, null, 2); // 将speechContentParagraphsSystemExternal对象格式化为JSON字符串，并显示在页面上
            }

function fnAjaxServerSideCallAIGCAnswerCharactor(isProxy) {
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是"+sPrompt+"对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
             document.getElementById("transcriptSystemExternal").innerHTML ="这里将呈现本系统的服务端访问外部的他创方的LLM："+document.getElementById("idSelectedExternalLLM").value+"，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
            var sURL ="";           
            if(isProxy=="Proxy"){
            const utteranceExternalPrompt2 = new SpeechSynthesisUtterance("请注意：您当前选择的是登录后Prompt提问，请在打开的页面中登录，否则无法Prompt提问！如果已经登录，无需重复登录！"); 
             window.speechSynthesis.speak(utteranceExternalPrompt2); 
           open("/ProxyQWen/index?queryString=" + sPrompt,"LogInProxy");
           sURL = "/ProxyQWen/index?queryString=" + sPrompt;
            }
            else{
            sURL = "/QWen/index?queryString=" + sPrompt;
}
 
            // var sURL = "https://localhost:5001/QWen/index?queryString=" + sSearchedKeywords;
           // open(sURL, "ServerSideCallAIGCAnswerCharactor");
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var oTemp=JSON.parse(xmlHttpRequest.responseText);
                        document.getElementById("transcriptSystemExternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+oTemp.output.text;
                      
                        window.speechSynthesis.cancel();
                          /**
                     //TTS
                     const utteranceExternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+oTemp.output.text); 
                     if(document.getElementById("id_TTS").disabled==false){
                     window.speechSynthesis.speak(utteranceExternalAIGCAnswer);
                     utteranceExternalAIGCAnswer.onend=fnTTSOnEndSystemExternalAIGCAnswer;
                     **/
                   // document.getElementById("id_TTS_Play").click();
                   fnTTS_Play(0);
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
}
function fnAjaxServerSideCallAIGCAnswerCharactorInternal(isProxy) {
        if(window.RunningLocalLLMID==null || window.RunningLocalLLMID=="") {
        alert("请先选择本地LLM模型，否则Prompt提问可能出错！");
        return;
        }
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是"+sPrompt+"对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
             document.getElementById("transcriptSystemInternal").innerHTML ="这里将呈现本系统的服务端访问内部的LLM："+window.RunningLocalLLMID+"，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
            var sURL ="";           
            if(isProxy=="Proxy"){
            const utteranceExternalPrompt2 = new SpeechSynthesisUtterance("请注意：您当前选择的是登录后Prompt提问，请在打开的页面中登录，否则无法Prompt提问！如果已经登录，无需重复登录！"); 
             window.speechSynthesis.speak(utteranceExternalPrompt2); 
          // sURL =  "/QWen/index?queryString="  + sPrompt;//无需登录的Qwen的URL;//曾经Get请求实现。
          sURL= window.RunningLocalLLMURL+"v1/chat/completions";//无需登录的本机LLM的URL;必须是POST请求.
            }
            else{
           open("/ProxyQWen/index?queryString=" + sPrompt,"LogInProxy");//暂时使用了强制登录Prompt提问云端LLM的功能，没有强制登录后Prompt提问本机LLM的功能，后续会增加该功能。
           sURL = "/ProxyQWen/index?queryString=" + sPrompt;

}
 
           var xmlHttpRequest = new XMLHttpRequest();
           const data = JSON.stringify({
    "model": window.RunningLocalLLMID,
    "messages": [
      {
        "role": "user",
        "content": sPrompt
      }
    ]});
    alert(sURL);
    alert(data);

           xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.setRequestHeader('Content-Type', 'application/json');////如果是HTTP的Form元素的post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型
           xmlHttpRequest.send(data); 
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                       // var oTemp=JSON.parse(fnDecodeUnicodeEscape(xmlHttpRequest.responseText));//JSON.parse自动将Unicode转为中文，故不需要再使用fnDecodeUnicodeEscape函数。但是失败，没办法临时使用fnDecodeUnicodeEscape函数，后续再研究。
                        var oTemp=fnDecodeUnicodeEscape(xmlHttpRequest.responseText);
                      // document.getElementById("transcriptSystemInternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+oTemp.output.text;//oTemp.output.text失败，后续修改
                       document.getElementById("transcriptSystemInternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+oTemp;
                        window.speechSynthesis.cancel();
                   fnTTS_Play(0);
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
        alert();
}
function fnDecodeUnicodeEscape(str) {
  // 匹配 \uXXXX 格式（不区分大小写）
  return str.replace(/\\u([0-9a-fA-F]{4})/g, (match, hex) => {
    // 将十六进制字符串转为十进制码点，再转换为字符
    return String.fromCodePoint(parseInt(hex, 16));
  });
}
function fnAjaxServerSideCallAIGCAnswerCharactorInternal_Old(isProxy) {
        if(window.RunningLocalLLMID==null || window.RunningLocalLLMID=="") {
        alert("请先选择本地LLM模型，否则Prompt提问可能出错！");
        return;
        }
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是"+sPrompt+"对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
             document.getElementById("transcriptSystemInternal").innerHTML ="这里将呈现本系统的服务端访问外部的他创方的AIGC，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
            var sURL ="";           
            if(isProxy=="Proxy"){
            const utteranceExternalPrompt2 = new SpeechSynthesisUtterance("请注意：您当前选择的是登录后Prompt提问，请在打开的页面中登录，否则无法Prompt提问！如果已经登录，无需重复登录！"); 
             window.speechSynthesis.speak(utteranceExternalPrompt2); 
           open("/ProxyLocalLLM/index?queryString=" + sPrompt,"LogInProxy");//+ "&localLLMID=" + window.RunningLocalLLMID,"LogInProxy");
           sURL = "/ProxyLocalLLM/index?queryString=" + sPrompt;//+ "&localLLMID=" + window.RunningLocalLLMID;
            }
            else{
            sURL = "/LocalLLM/index?queryString=" + sPrompt;//+ "&localLLMID=" + window.RunningLocalLLMID;
}
 
            // var sURL = "https://localhost:5001/QWen/index?queryString=" + sSearchedKeywords;
           // open(sURL, "ServerSideCallAIGCAnswerCharactor");
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var oTemp=JSON.parse(xmlHttpRequest.responseText);
                        document.getElementById("transcriptSystemInternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+oTemp.output.text;
                      
                        window.speechSynthesis.cancel();
                          /**
                     //TTS
                     const utteranceExternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+oTemp.output.text); 
                     if(document.getElementById("id_TTS").disabled==false){
                     window.speechSynthesis.speak(utteranceExternalAIGCAnswer);
                     utteranceExternalAIGCAnswer.onend=fnTTSOnEndSystemExternalAIGCAnswer;
                     **/
                   // document.getElementById("id_TTS_Play").click();
                   fnTTS_Play(0);
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
}

function fnToggleDisplayOfAIGCAnswerHomeworkAndTest(){
    if(document.getElementById("id_Answer").style.display=="none"){
        document.getElementById("id_Answer").style.display="block";
    }
    else{
    document.getElementById("id_Answer").style.display="none";
    }
}

function fnToggleDisplayOfFiveLayMVCFromAIGCAnswerHomeworkAndTest(){
    if(document.getElementById("id_FiveLayMVCFromAIGCAnswerHomeworkAndTest").style.display=="none"){
        document.getElementById("id_FiveLayMVCFromAIGCAnswerHomeworkAndTest").style.display="block";
    }
    else{
    document.getElementById("id_FiveLayMVCFromAIGCAnswerHomeworkAndTest").style.display="none";
    }
}

function fnHTMLEditorForAIGCHomeworkAndTest(){
    var sTemp="(HTML源码编辑)";
    if(document.getElementById("id_HTMLEditorForAIGCHomeworkAndTest").textContent.indexOf(sTemp)>=0){
        document.getElementById("id_HTMLEditorForAIGCHomeworkAndTest").textContent=document.getElementById("id_HTMLEditorForAIGCHomeworkAndTest").textContent.replace(sTemp,"");
         document.getElementById("id_ForHTMLEditor").innerHTML=document.getElementById("id_ForHTMLEditor").textContent;
         document.getElementById("id_ForHTMLEditor").contentEditable="false";
    }
    else{
    document.getElementById("id_HTMLEditorForAIGCHomeworkAndTest").textContent=document.getElementById("id_HTMLEditorForAIGCHomeworkAndTest").textContent+sTemp;
     document.getElementById("id_ForHTMLEditor").textContent=document.getElementById("id_ForHTMLEditor").innerHTML;
     document.getElementById("id_ForHTMLEditor").contentEditable="true";
    }  
    }

function fnAjaxServerSideCallAIGCAnswerHomeworkAndTest(isProxy) {
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是"+sPrompt+"对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
            document.getElementById("transcriptSystemExternal").innerHTML ="这里将呈现本系统的服务端访问外部的他创方的LLM："+document.getElementById("idSelectedExternalLLM").value+"，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
           // var sURL = "/QWen/index?queryString=" + sPrompt;
           var sURL ="";           
            if(isProxy=="Proxy"){
            const utteranceExternalPrompt11 = new SpeechSynthesisUtterance("请注意：您当前选择的是登录后Prompt提问，请在打开的页面中登录，否则无法Prompt提问！如果已经登录，无需重复登录！"); 
             window.speechSynthesis.speak(utteranceExternalPrompt11); 
           open("/ProxyQWen/index?queryString=" + sPrompt,"LogInProxy");
           sURL = "/ProxyQWen/index?queryString=" + sPrompt;
            }
            else{
            sURL = "/QWen/index?queryString=" + sPrompt;
}
            // var sURL = "https://localhost:5001/QWen/index?queryString=" + sSearchedKeywords;
           // open(sURL, "ServerSideCallAIGCAnswerCharactor");
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var oTemp=JSON.parse(xmlHttpRequest.responseText);
                         var sString=oTemp.output.text;
     
        var sString1=sString.replace("A.", '<p/><input type="radio" name="raio_Four"/>A.');
        var sString2=sString1.replace("B.", '<p/><input type="radio" name="raio_Four"/>B.');
        var sString3=sString2.replace("C.", '<p/><input type="radio" name="raio_Four"/>C.');
        var sString4=sString3.replace("D.", '<p/><input type="radio" name="raio_Four"/>D.');
        // var sStringMayBe5=sString4.replace("E.", '<p/><input type="radio" name="raio_Four"/>E.');
        var sString5=sString4.substring(sString4.indexOf("正确答案"), sString4.length);
        var sString6=sString4.substring(0,sString4.indexOf("正确答案"))+'<p/>'+'<button title="单击可以切换答案显示" onclick="fnToggleDisplayOfAIGCAnswerHomeworkAndTest()">'+'**正确答案：**'+'</button>'+'<span id="id_Answer" style="display:none">'+sString5.substring(sString5.indexOf("**正确答案：**")+"**正确答案：**".length,sString5.length)+'</span>';
        
        document.getElementById("transcriptSystemExternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+"<p/>"+'<div id="id_ForHTMLEditor" contenteditable="false" >'+sString6+'<div><button id="id_FiveLayerMVC" title="因为AIGC生成作业测验的灵活度很大，所以本功能暂时不太稳定！）" onclick="fnToggleDisplayOfFiveLayMVCFromAIGCAnswerHomeworkAndTest()">本题选用的“四层平台”的思维语言生成的“五层MVC”面向的主要层次【注：A、实践-数据读写封装（例如：人物对象的内容方法←映射→数据库数据仓库）；B、技术-信息提取运用（例如：数据确定性描述统计。典型案例：数据总计平均等等多维分析）；C、科学-规律预测探究（例如：数据概率性推断统计。典型案例：数据挖掘）；D、人文-情感交流共鸣（例如：数据概率性人文推断统计。典型案例：数据人文挖掘）；E、哲学-智能建构生成（例如：数据概率性AIGC推断统计。典型案例：神经元多层网络的已经训练学习的概率性推断统计）】</button><div id="id_FiveLayMVCFromAIGCAnswerHomeworkAndTest" style="display:none">A/B/C/D/E之一（当前AIGC回答尚不稳定）</div></div>'+"</div>"+'<div><button id="id_HTMLEditorForAIGCHomeworkAndTest" title="单击可以切换HTML源码编辑。因为AIGC生成作业测验的灵活度很大，所以特意提供本功能，以便用户即时在线修改AIGC生成的作业测验（注意必须遵守法律修改AIGC生成的内容！！！）" style="width:100%" onclick="fnHTMLEditorForAIGCHomeworkAndTest()">“作业测验”的HTML帮助器（单击可以切换HTML源码编辑）</button></div>';//oTemp.output.text;
        //document.getElementById("transcriptSystemExternal").style.color="";              
        window.speechSynthesis.cancel();
                          /**
                     //TTS
                     const utteranceExternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+oTemp.output.text); 
                     if(document.getElementById("id_TTS").disabled==false){
                     window.speechSynthesis.speak(utteranceExternalAIGCAnswer);
                     utteranceExternalAIGCAnswer.onend=fnTTSOnEndSystemExternalAIGCAnswer;
                     **/
                   // document.getElementById("id_TTS_Play").click();
                   fnTTS_Play(0);
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
    /**
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idPrompt").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是“"+sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
             document.getElementById("transcriptSystemExternal").innerHTML ="这里将呈现本系统的服务端访问外部的他创方的AIGC，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
            var sURL = "/QWen/index?queryString=" + sPrompt;
            // var sURL = "https://localhost:5001/QWen/index?queryString=" + sSearchedKeywords;
           // open(sURL, "ServerSideCallAIGCAnswerCharactor");
           //以下作为测试无法实现调用外部AIGC的案例呈现而已。
    var oIframe = document.createElement('iframe');
    oIframe.src = "../options/NotFindHomeworkAndTest.htm";
    oIframe.frameBorder = '0';
    oIframe.allowFullscreen = true;    
    oIframe.style.width = '100%';
    oIframe.style.height = '100%';
    oIframe.style.display = 'block';
    oIframe.style.border = '0';
    document.getElementById("transcriptSystemExternal").appendChild(oIframe);  
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var oTemp=JSON.parse(xmlHttpRequest.responseText);
                        document.getElementById("transcriptSystemExternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）：";
                      
                        window.speechSynthesis.cancel();
                         
                   fnTTS_Play(0);
                   // open("../options/NotFindHomeworkAndTest.htm","_blank");
                   //以下作为测试可以实现调用外部AIGC的案例呈现而已。
    var oIframe = document.createElement('iframe');
    oIframe.src = "../options/NotFindHomeworkAndTest.htm";
    oIframe.frameBorder = '0';
    oIframe.allowFullscreen = true;    
    oIframe.style.width = '100%';
    oIframe.style.height = '100%';
    oIframe.style.display = 'block';
    oIframe.style.border = '0';
    document.getElementById("transcriptSystemExternal").appendChild(oIframe);  
    
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
        **/
}

function fnAjaxServerSideCallAIGCAnswerHomeworkAndTestInternal(isProxy) {
    if(window.RunningLocalLLMID==null || window.RunningLocalLLMID=="") {
        alert("请先选择本地LLM模型，否则Prompt提问可能出错！");
        return;
        }
    fnToggleEventSoureElementColor();
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceExternalPrompt = new SpeechSynthesisUtterance("您的Prompt提问是"+sPrompt+"对吗？语音对话机器人正在思考回答Answer，请耐心等候..."); 
             window.speechSynthesis.speak(utteranceExternalPrompt); 
            alert("您的Prompt提问是：“" + sPrompt+"”对吗？语音对话机器人正在思考回答Answer，请耐心等候...");
             document.getElementById("transcriptSystemInternal").innerHTML ="这里将呈现本系统的服务端访问内部的LLM："+window.RunningLocalLLMID+"，实现语音对话机器人的回答Answer并且TTS朗读。语音对话机器人正在思考回答Answer，请耐心等候...";
           // var sURL = "/QWen/index?queryString=" + sPrompt;
           var sURL ="";           
            if(isProxy=="Proxy"){
            const utteranceExternalPrompt11 = new SpeechSynthesisUtterance("请注意：您当前选择的是登录后Prompt提问，请在打开的页面中登录，否则无法Prompt提问！如果已经登录，无需重复登录！"); 
             window.speechSynthesis.speak(utteranceExternalPrompt11); 
          // sURL =  "/QWen/index?queryString="  + sPrompt;//无需登录的Qwen的URL;//曾经Get请求实现。
          sURL= window.RunningLocalLLMURL+"v1/chat/completions";//无需登录的本机LLM的URL;必须是POST请求.
            }
            else{
           open("/ProxyQWen/index?queryString=" + sPrompt,"LogInProxy");//暂时使用了强制登录Prompt提问云端LLM的功能，没有强制登录后Prompt提问本机LLM的功能，后续会增加该功能。
           sURL = "/ProxyQWen/index?queryString=" + sPrompt;

}
 
           var xmlHttpRequest = new XMLHttpRequest();
           const data = JSON.stringify({
   //  "model": window.RunningLocalLLMID.substring(0, window.RunningLocalLLMID.lastIndexOf(":")),
    "model": window.RunningLocalLLMID,
    "messages": [
      {
        "role": "user",
        "content": sPrompt
      }
    ]});
    alert(data);

           xmlHttpRequest.open('POST',sURL , true);
           //xmlHttpRequest.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');////如果是HTTP的Form元素的post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型
            xmlHttpRequest.setRequestHeader('Content-Type', 'application/json');////如果是HTTP的Form元素的post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型
           xmlHttpRequest.send(data); 
           xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                       // var oTemp=JSON.parse(xmlHttpRequest.responseText);
                        // var oTemp=JSON.parse(fnDecodeUnicodeEscape(xmlHttpRequest.responseText));//JSON.parse自动将Unicode转为中文，故不需要再使用fnDecodeUnicodeEscape函数。但是失败，没办法临时使用fnDecodeUnicodeEscape函数，后续再研究。
                        var oTemp=fnDecodeUnicodeEscape(xmlHttpRequest.responseText);
                        
                      // document.getElementById("transcriptSystemInternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+oTemp.output.text;//oTemp.output.text失败，后续修改\
                      // var sString=oTemp.output.text;失败，后续修改
                      var sString=oTemp;
     
        var sString1=sString.replace("A.", '<p/><input type="radio" name="raio_Four"/>A.');
        var sString2=sString1.replace("B.", '<p/><input type="radio" name="raio_Four"/>B.');
        var sString3=sString2.replace("C.", '<p/><input type="radio" name="raio_Four"/>C.');
        var sString4=sString3.replace("D.", '<p/><input type="radio" name="raio_Four"/>D.');
        // var sStringMayBe5=sString4.replace("E.", '<p/><input type="radio" name="raio_Four"/>E.');
        var sString5=sString4.substring(sString4.indexOf("正确答案"), sString4.length);
        var sString6=sString4.substring(0,sString4.indexOf("正确答案"))+'<p/>'+'<button title="单击可以切换答案显示" onclick="fnToggleDisplayOfAIGCAnswerHomeworkAndTest()">'+'**正确答案：**'+'</button>'+'<span id="id_Answer" style="display:none">'+sString5.substring(sString5.indexOf("**正确答案：**")+"**正确答案：**".length,sString5.length)+'</span>';
        
        document.getElementById("transcriptSystemInternal").innerHTML ="语音对话机器人的回答Answer如下（请注意思辨准确性）："+"<p/>"+'<div id="id_ForHTMLEditor" contenteditable="false" >'+sString6+'<div><button id="id_FiveLayerMVC" title="因为AIGC生成作业测验的灵活度很大，所以本功能暂时不太稳定！）" onclick="fnToggleDisplayOfFiveLayMVCFromAIGCAnswerHomeworkAndTest()">本题选用的“四层平台”的思维语言生成的“五层MVC”面向的主要层次【注：A、实践-数据读写封装（例如：人物对象的内容方法←映射→数据库数据仓库）；B、技术-信息提取运用（例如：数据确定性描述统计。典型案例：数据总计平均等等多维分析）；C、科学-规律预测探究（例如：数据概率性推断统计。典型案例：数据挖掘）；D、人文-情感交流共鸣（例如：数据概率性人文推断统计。典型案例：数据人文挖掘）；E、哲学-智能建构生成（例如：数据概率性AIGC推断统计。典型案例：神经元多层网络的已经训练学习的概率性推断统计）】</button><div id="id_FiveLayMVCFromAIGCAnswerHomeworkAndTest" style="display:none">A/B/C/D/E之一（当前AIGC回答尚不稳定）</div></div>'+"</div>"+'<div><button id="id_HTMLEditorForAIGCHomeworkAndTest" title="单击可以切换HTML源码编辑。因为AIGC生成作业测验的灵活度很大，所以特意提供本功能，以便用户即时在线修改AIGC生成的作业测验（注意必须遵守法律修改AIGC生成的内容！！！）" style="width:100%" onclick="fnHTMLEditorForAIGCHomeworkAndTest()">“作业测验”的HTML帮助器（单击可以切换HTML源码编辑）</button></div>';//oTemp.output.text;
        //document.getElementById("transcriptSystemExternal").style.color="";              
        window.speechSynthesis.cancel();
                          /**
                     //TTS
                     const utteranceExternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+oTemp.output.text); 
                     if(document.getElementById("id_TTS").disabled==false){
                     window.speechSynthesis.speak(utteranceExternalAIGCAnswer);
                     utteranceExternalAIGCAnswer.onend=fnTTSOnEndSystemExternalAIGCAnswer;
                     **/
                   // document.getElementById("id_TTS_Play").click();
                   fnTTS_Play(0);
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer如下（请注意思辨准确性）"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
   alert();
}

function fnTTSOnEndSystemExternalAIGCAnswer(){
            //alert("语音对话机器人的回答Answer已经结束朗读，请您继续对话！");
            window.speechSynthesis.cancel();
            const utteranceTTSOnEndSystemExternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer已经结束朗读，请您继续对话！"); 
            window.speechSynthesis.speak(utteranceTTSOnEndSystemExternalAIGCAnswer); 
            utteranceTTSOnEndSystemExternalAIGCAnswer.onend=fnTTSOnEndSystemExternalUtteranceTTSOnEndSystemExternalAIGCAnswer;
            //window.speechSynthesis.cancel();
        }
function fnTTSOnEndSystemExternalUtteranceTTSOnEndSystemExternalAIGCAnswer(){
            document.getElementById('stopBtnSystemExternal').click(); 
            document.getElementById('startBtnSystemExternal').click(); 
            //window.speechSynthesis.cancel();
        }

function fnTTS_Play(intCharBeginningNumber) {
    //document.getElementById("transcriptSystemExternal").style.color="green";
    document.getElementById('stopBtnSystemExternal').click(); 
    document.getElementById("id_RadioSystemExternal").checked=true;
  //  if(document.getElementById("id_RadioSystemExternal").checked == true) {
        
    document.getElementById("id_TTS_GoToText").value=intCharBeginningNumber;
    
   var sTemp="当前AIGC回答Answer朗读已结束，请继续Prompt提问AIGC！";
    if(!("speechSynthesis" in window)) {
		throw alert("对不起，您的浏览器不支持");
		}
        window.speechSynthesis.cancel();
       //var sTextContent = JSON.stringify(document.getElementById("transcriptSystemExternal").textContent);
       var sTextContent = document.getElementById("transcriptSystemExternal").textContent;
       document.getElementById("id_CharNumber").textContent=sTextContent.length;
       
              var utterance="";              
             if(sTextContent==null||sTextContent==""){
            utterance ="您好，当前AIGC回答Answer，没有字符自动朗诵！"+sTemp;            
                 }
            else{
            sTextContentBeginningNumber =sTextContent.substring(intCharBeginningNumber,sTextContent.length);
             utterance = new SpeechSynthesisUtterance(sTextContentBeginningNumber+sTemp);
            }
             window.speechSynthesis.speak(utterance);
             //utterance.onend=fnTTSOnEnd;// 语音朗读结束时的回调;
             utterance.onend=fnTTSOnEndSystemExternalAIGCAnswer;
           // }
    
}

function fnTTS_Pause() {
    window.speechSynthesis.pause();
}

function fnTTS_Resume() {
    window.speechSynthesis.resume();
}

function fnTTS_Cancel() {
    window.speechSynthesis.cancel();
}

function fnTTS_GoTo() {
    window.speechSynthesis.cancel();
    fnTTS_Play(document.getElementById("id_TTS_GoToText").value);
}

function fnTTSOnEnd(){
window.speechSynthesis.cancel();
//window.isRecognizingSystemInternal = false;
//window.recognitionSystemInternal = null; 为了配合语音朗读TTS，所以fnSTTOnResultSystemInternal中的停止语音识别STT，但是因为还未能实现打断语音朗读，所以暂时放弃。
    }

 function  fnToggleInternalWidth(){
    if(document.getElementById("tdSystemInternal").style.width=="50%"){
      document.getElementById("tdSystemExternal").style.display="none";
      document.getElementById("tdSystemExternal").style.width="0%";
      document.getElementById("tdSystemInternal").style.width="100%";
     }    
     else{
     document.getElementById("tdSystemInternal").style.display=null;
     document.getElementById("tdSystemExternal").style.display=null;
     document.getElementById("tdSystemInternal").style.width="50%";
     document.getElementById("tdSystemExternal").style.width="50%";
     }
}
 function  fnToggleExternalWidth(){
    if(document.getElementById("tdSystemInternal").style.width=="50%"){
      document.getElementById("tdSystemInternal").style.display="none";
      document.getElementById("tdSystemInternal").style.width="0%";
      document.getElementById("tdSystemExternal").style.width="100%";
     }    
     else{
     document.getElementById("tdSystemInternal").style.display=null;
     document.getElementById("tdSystemExternal").style.display=null;
     document.getElementById("tdSystemInternal").style.width="50%";
     document.getElementById("tdSystemExternal").style.width="50%";
     }
}
 function  fnPromptForAIGCOnBlur(){
     if(document.getElementById("id_SystemExternal_NonRAG").checked){
     document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value="“"+document.getElementById("idPrompt").value+"定义”";
     document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value="“"+document.getElementById("idPrompt").value+"定义”的一道四个选项的单选题，适合用于考试测验。";
     document.getElementById("idRadioRAGClickedForWaiting").textContent ="";
     }
     else{
         document.getElementById("idRadioRAGClickedForWaiting").textContent ="(正在字符媒体检索知识库...)";
     //window.isRecognizingSystemExternal = false;
     //window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idPrompt").value;
             
            var sURL = "/QWenRAG/Get?SearchedKeywords=" + sPrompt;
           
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        
                        document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value ="基于下述，生成数字思维视角、也称数字人工智能思维视角的、也称数智思维视角的"+document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value+"："+xmlHttpRequest.responseText;    
                        document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value ="基于下述，生成数字思维视角、也称数字人工智能思维视角的、也称数智思维视角的"+document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value+"      "+xmlHttpRequest.responseText; 
                        document.getElementById("idRadioRAGClickedForWaiting").textContent ="";
                        if(document.getElementById("id_SystemExternal_NonRAG").checked){
     document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerCharactor").value="“"+document.getElementById("idPrompt").value+"定义”";
     document.getElementById("idTextAreaAjaxServerSideCallAIGCAnswerHomeworkAndTest").value="“"+document.getElementById("idPrompt").value+"定义”的一道四个选项的单选题，适合用于考试测验。";
     document.getElementById("idRadioRAGClickedForWaiting").textContent ="";

                        }
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);                        
                    }
                }
        }
     }
  }
   function  fnPromptForInternalAIGCOnBlur(){
     if(document.getElementById("id_SystemInternal_NonRAG").checked){
     document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value="“"+document.getElementById("idPromptInternalLLM").value+"”";
     document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value="“"+document.getElementById("idPromptInternalLLM").value+"”的一道四个选项的单选题，适合用于考试测验。";
     document.getElementById("idRadioRAGClickedForWaitingInternal").textContent ="";
     }
     else{
         document.getElementById("idRadioRAGClickedForWaitingInternal").textContent ="(正在字符媒体检索知识库...)";
     //window.isRecognizingSystemExternal = false;
     //window.speechSynthesis.cancel(); 
            var sPrompt = document.getElementById("idPromptInternalLLM").value;
             
            var sURL = "/QWenRAG/Get?SearchedKeywords=" + sPrompt;
           
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        
                        document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value ="基于下述，生成数字思维视角、也称数字人工智能思维视角的、也称数智思维视角的"+document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value+"："+xmlHttpRequest.responseText;    
                        document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value ="基于下述，生成数字思维视角、也称数字人工智能思维视角的、也称数智思维视角的"+document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value+"      "+xmlHttpRequest.responseText; 
                        document.getElementById("idRadioRAGClickedForWaitingInternal").textContent ="";
                        if(document.getElementById("id_SystemInternal_NonRAG").checked){
     document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerCharactor").value="“"+document.getElementById("idPromptInternalLLM").value+"定义”";
     document.getElementById("idTextAreaAjaxInternalSideCallAIGCAnswerHomeworkAndTest").value="“"+document.getElementById("idPromptInternalLLM").value+"定义”的一道四个选项的单选题，适合用于考试测验。";
     document.getElementById("idRadioRAGClickedForWaitingInternal").textContent ="";

                        }
                     }
                    
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);                        
                    }
                }
        }
     }
  }
  function fnToggleSystemInternalExplainSystemExternalExplain(){
      if(document.getElementById("idSystemInternalExplain").style.display=="none"){
            document.getElementById("idSystemInternalExplain").style.display="block";
            document.getElementById("idSystemExternalExplain").style.display="block";
            document.getElementById("idRightTriangleOfSystemInternal").textContent="▼";
            document.getElementById("idRightTriangleOfSystemExternal").textContent="▼";
            }
      else{
            document.getElementById("idSystemInternalExplain").style.display="none";
            document.getElementById("idSystemExternalExplain").style.display="none";
            document.getElementById("idRightTriangleOfSystemInternal").textContent="▶";
            document.getElementById("idRightTriangleOfSystemExternal").textContent="▶";
          }
  }

  function fnAjaxServerSideCallAIGCLearningCommunity(isProxy) {
    fnToggleEventSoureElementColor();
    if(isProxy=="Proxy"){           
           open("/QWenSkillsLearningCommunity/index?queryString=sChecedName");
            }
}
/**AIGC官方声明：因为API Key容易泄露等等安全问题，所以当前不支持JS访问千问AIGC。
 *
 function fnidQwenAPIKeyConfirmOnClickSystemExternal(){
    window.QwenAPIKey=document.getElementById('idQwenAPIKeyInput').value;
    //document.getElementById('idQwenAPIKeyInput').value="";
    document.getElementById('idQwenAPIKeyInput').placeholder="您已键入Qwen千问的API Key";
    //alert(window.QwenAPIKey);
    alert(document.getElementById('idQwenAPIKeyInput').placeholder+"："+window.QwenAPIKey);
}

  function fnAjaxClientBrowserSideCallAIGCAnswerCharactor() {
     if(window.QwenAPIKey==null||window.QwenAPIKey==""){
        alert("您可能还没输入并确认您的Qwen千问APIKey！");
    }
    else{
    fnToggleEventSoureElementColor();
    var sPrompt = document.getElementById("idPromptForClientBrowserSide").value;
     window.speechSynthesis.cancel();
                     //TTS
    const utteranceInternalPrompt = new SpeechSynthesisUtterance("您的Prompt是"+sPrompt+"对吗？"); 
     window.speechSynthesis.speak(utteranceInternalPrompt); 
     alert("您的Prompt是：" + sPrompt);
    fnCallAIGCQwen(sPrompt);
    }
    }
   
 async function fnCallAIGCQwen(sPrompt) {
  const apiKey = window.QwenAPIKey; // 替换为你的实际 API Key
  const url = 'https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation';
    //alert(apiKey+sPrompt);
    alert(JSON.stringify({
        model: 'qwen-max', // 或 qwen-plus, qwen-turbo 等
        input: {
          prompt: sPrompt
        },
        parameters: {
          result_format: 'text'
        }
      }));
      alert(JSON.stringify({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${apiKey}` 
      }));
    try {
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
       // 'Authorization': `Bearer ${apiKey}`
       'Authorization': 'Bearer ${apiKey}'
       //  'Authorization': `Bearer apiKey`
      // 'Authorization': `Bearer sk-************`
     // max_tokens: 100,
   // temperature: 0.7,
    //stream: true // 如果启用流式响应
      },
      body: JSON.stringify({
        model: 'qwen-max', // 或 qwen-plus, qwen-turbo 等
        input: {
          prompt: sPrompt
        },
        parameters: {
          result_format: 'text'
        }
      })
    });
    alert(apiKey+"?????????????"+sPrompt);
    const data = await response.json();
    if (data.output &&data.output.text) {
      alert('千问回复:', data.output.text);
      return data.output.text;
    } 
    else {
      alert('API 错误:', data);
      return '抱歉，调用失败。';
    }
    }
    catch (error) {    
    if (error.name === 'AbortError') {
      alert('请求超时：'+error.name+error.message);
      return { success: false, error: '请求超时', message: 'Request timed out' };
    }

    if (!navigator.onLine) {
        alert(error.name+error.message);
      return { success: false, error: '网络不可用', message: 'Network is offline' };
    }

    alert('请求失败：'+'\r'+'error.name是：'+error.name+'\r'+'error.message：'+error.message);
    return { success: false, error: error.message };
  }
}
 **/
/**
 XMLHttpRequest 与 Fetch 的异同(Promise 在其中的作用)
•设计年代：XMLHttpRequest（XHR）是1999年引入的旧式API，而Fetch API是2015年随着ES6标准引入的现代API⁠⁣ ⁠⁣5 。
•异步处理机制：XHR基于事件和回调函数来处理异步请求，容易导致“回调地狱”问题；Fetch API则基于Promise，使用Promise对象来处理异步请求，使得代码更具可读性和可维护性⁠⁣ 。这使得Fetch在处理多个异步操作时更加简洁、直观，并且支持链式调用和async/await语法⁠⁣。
•语法简洁度：相较于XHR，Fetch API的语法更加简单明了，通常只需要几行代码就能完成一个请求⁠⁣  。此外，Fetch采用模块化设计，其API分散在多个对象上如Response, Request, Headers等，相比之下，XHR的API设计并不是很好，输入、输出及状态都在同一个接口管理⁠⁣。
•跨域请求：虽然两者都支持跨域请求，但Fetch默认不发送cookies，需要通过设置credentials: 'include'来允许携带cookie进行跨域请求；而XHR则默认会发送cookies⁠⁣。
•其他特性：Fetch API还提供了对流的支持，可以更高效地处理大文件或实时数据流；并且它可以在Service Worker中使用，这是XHR所不具备的功能⁠⁣ ⁠ 。
Promise 在其中的作用
•Promise是一种用于处理异步操作的对象，它代表了一个异步操作的最终完成（或失败）及其结果值⁠⁣  。
•Fetch API完全基于Promise实现，这意味着每次发起请求后返回的是一个Promise对象，开发者可以通过.then()方法链式调用来处理响应数据，或者使用async/await关键字以同步方式编写异步代码⁠⁣ 。
•XMLHttpRequest本身并不直接支持Promise，但是可以通过封装将其转换为Promise形式，从而利用Promise的优势简化回调逻辑⁠⁣ 。
综上所述，尽管XMLHttpRequest和Fetch都可以用来发起网络请求，但后者凭借其现代化的设计理念，在易用性、功能丰富度等方面展现出了明显优势。同时，Promise作为一种强大的异步编程工具，在Fetch API中得到了广泛应用，极大地提升了开发效率与代码质量。
 */
/** 
 * 封装一个通用的 fetch 请求函数可以提高代码的复用性、可维护性和健壮性。以下是一个完整的通用 fetch 封装示例，包含请求拦截、响应处理、错误处理和常用配置.封装要点说明：
 1. 默认配置：设置通用的 headers、method 和 credentials。
2. JSON 自动序列化：对对象类型的 body 自动调用 JSON.stringify。
3. 超时控制：使用 AbortController 模拟 timeout 功能。
4. 响应格式判断：根据 Content-Type 决定是 .json() 还是 .text()。
5. 错误分类处理：区分网络错误、HTTP 错误、超时、离线等场景。
6. 统一返回格式：便于调用方统一处理成功与失败。
可选增强功能：
•添加请求/响应拦截器（类似 Axios）
•自动重试机制
•请求缓存
•日志记录
•Token 自动刷新与注入
 **/
/**
 // 通用 fetch 请求封装函数.你可以将此函数封装成一个独立模块，在项目中全局使用，极大提升开发效率。
async function request(url, options = {}) {
  // 默认配置
  const defaultOptions = {
    method: 'GET', // 默认请求方法
    headers: {
      'Content-Type': 'application/json', // 默认内容类型
    },
    credentials: 'include', // 允许携带 cookies（用于跨域会话）
    timeout: 10000, // 自定义超时时间（fetch 原生不支持 timeout，需手动实现）
  };

  // 合并用户传入的 options
  const config = { ...defaultOptions, ...options };

  // 如果是 POST/PUT/PATCH 请求，自动将 body 转为 JSON 字符串
  if (['POST', 'PUT', 'PATCH'].includes(config.method) &&typeof config.body === 'object') {
    config.body = JSON.stringify(config.body);
  }

  // 使用 AbortController 实现超时控制
  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), config.timeout);

  try {
    const response = await fetch(url, {
      ...config,
      signal: controller.signal, // 绑定中断信号
    });

    clearTimeout(timeoutId); // 清除超时定时器

    if (!response.ok) {
      throw new Error(`HTTP Error! status: ${response.status}`);
    }

    // 尝试解析 JSON，失败则返回文本
    let data;
    const contentType = response.headers.get('content-type');
    if (contentType &&contentType.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    return {
      success: true,
      data,
      status: response.status,
      headers: response.headers,
    };
  } catch (error) {
    clearTimeout(timeoutId);

    // 区分不同类型的错误
    if (error.name === 'AbortError') {
      console.error('请求超时');
      return { success: false, error: '请求超时', message: 'Request timed out' };
    }

    if (!navigator.onLine) {
      return { success: false, error: '网络不可用', message: 'Network is offline' };
    }

    console.error('请求失败:', error.message);
    return { success: false, error: error.message };
  }
}

// 使用示例
request('/api/user', {
  method: 'POST',
  body: { name: 'Alice', age: 25 },
})
  .then((res) => {
    if (res.success) {
      console.log('数据:', res.data);
    } else {
      console.error('错误:', res.error);
    }
  });

 */
