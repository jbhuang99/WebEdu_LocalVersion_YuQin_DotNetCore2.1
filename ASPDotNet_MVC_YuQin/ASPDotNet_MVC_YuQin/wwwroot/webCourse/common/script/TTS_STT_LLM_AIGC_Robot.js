function fnOnLoad() {
window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
//try{window.recognitionSystemInternal.stop();} catch(e){;}// 停止系统内部的语音识别
//try{window.recognitionSystemExternal.stop();} catch(e){;}// 停止系统外部的语音识别
window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；
//document.getElementById('btnSystemInternal').addEventListener("click", fnBtnSystemInternalOnClick, false);
//document.getElementById('btnSystemExternal').addEventListener("click", fnBtnSystemExternalOnClick, false);
document.getElementById('startBtnSystemInternal').addEventListener('click',fnStartBtnSystemInternalOnClick,false);          
document.getElementById('stopBtnSystemInternal').addEventListener('click',fnStopBtnSystemInternalOnClick, false); 
document.getElementById('startBtnSystemExternal').addEventListener('click',fnStartBtnSystemExternalOnClick,false);
document.getElementById('stopBtnSystemExternal').addEventListener('click',fnStopBtnSystemExternalOnClick,false);

document.getElementById('idQwenAPIKeyConfirm').addEventListener('click',fnidQwenAPIKeyConfirmOnClickSystemExternal,false);

//document.getElementById('btnSystemInternal').disabled=true;
//document.getElementById('btnSystemExternal').disabled=false;
//document.getElementById('startBtnSystemInternal').click(); // 页面加载后自动点击开始系统内部的录音按钮
window.sHref=window.location.href.substring(0,window.location.href.indexOf("/webCourse/"));
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
   // alert();
    var eventSoureElementColor=window.event.target.style.color;
    
    if(eventSoureElementColor==""||eventSoureElementColor==null){
        window.event.target.style.color="brown";
    }
    else{
        window.event.target.style.color="";
    }
}

function fnAccuracySimilarityForPixelImage() {
  //  if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("免费")>=0)    {alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom());}

fnToggleEventSoureElementColor();
document.getElementById('sPixelImageFromAIGC').value=window.sHref+"/webCourse/options/Accuracy-Similarity/Cone-of-Experience-FromAIGC-willTransformToFiveLayeredMVC.jpg";
document.getElementById('sShowPixelImageFromAIGC').src=window.sHref+"/webCourse/options/Accuracy-Similarity/Cone-of-Experience-FromAIGC-willTransformToFiveLayeredMVC.jpg";
document.getElementById('sPixelImageFromKnowledgebase').value=window.sHref+"/webCourse/options/Accuracy-Similarity/Cone-of-Experience-FromKnowledgebase-willTransformToFiveLayeredMVC.jpg";
document.getElementById('sShowPixelImageFromKnowledgebase').src=window.sHref+"/webCourse/options/Accuracy-Similarity/Cone-of-Experience-FromKnowledgebase-willTransformToFiveLayeredMVC.jpg";

if(document.getElementById("AIGCPixelImage").style.display=="none"){
    document.getElementById("AIGCPixelImage").style.display="block";
    } 
else{document.getElementById("AIGCPixelImage").style.display="none"} 
}

function fnAccuracySimilarityForVideo() {
//if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("免费")>=0) {alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom());}
fnToggleEventSoureElementColor();
document.getElementById('sVideoFromAIGC').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.mp4";
document.getElementById('sShowVideoFromAIGC').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.mp4";
document.getElementById('sVideoFromKnowledgebase').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.mp4";
document.getElementById('sShowVideoFromKnowledgebase').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.mp4";

if(document.getElementById("AIGCVideo").style.display=="none"){document.getElementById("AIGCVideo").style.display="block"} else{document.getElementById("AIGCVideo").style.display="none"}
}

function fnAccuracySimilarityFor2D() {
//if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("免费")>=0) {alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom());}
fnToggleEventSoureElementColor();
document.getElementById('s2DFromAIGC').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.svg";
document.getElementById('sShow2DFromAIGC').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.svg";
document.getElementById('s2DFromKnowledgebase').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.svg";
document.getElementById('sShow2DFromKnowledgebase').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.svg";

if(document.getElementById("AIGC2D").style.display=="none"){document.getElementById("AIGC2D").style.display="block"} else{document.getElementById("AIGC2D").style.display="none"}
}

function fnAccuracySimilarityFor3D() {
//if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("免费")>=0) {alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom());}
fnToggleEventSoureElementColor();
document.getElementById('s3DFromAIGC').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.x3dv";
document.getElementById('sShow3DFromAIGC').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.x3dv";
document.getElementById('s3DFromKnowledgebase').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.x3dv";
document.getElementById('sShow3DFromKnowledgebase').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.x3dv";

if(document.getElementById("AIGC3D").style.display=="none"){document.getElementById("AIGC3D").style.display="block"} else{document.getElementById("AIGC3D").style.display="none"}
}

function fnAccuracySimilarityForAudio() {
//if(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom().indexOf("免费")>=0) {alert(opener.parent.document.getElementById("sIframeContents").contentWindow.fnRunningFrom());}
fnToggleEventSoureElementColor();
document.getElementById('sAudioFromAIGC').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.m4a";
document.getElementById('sShowAudioFromAIGC').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromAIGC.m4a";
document.getElementById('sAudioFromKnowledgebase').value=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.m4a";
document.getElementById('sShowAudioFromKnowledgebase').src=window.sHref+"/webCourse/options/Accuracy-Similarity/EducationalTechnologyFromKnowledgebase.m4a";
if(document.getElementById("AIGCAudio").style.display=="none"){document.getElementById("AIGCAudio").style.display="block"} else{document.getElementById("AIGCAudio").style.display="none"}
}


/**
function fnOpenSystemExternalAIGC(){
    open("https://chat.baidu.com/search","SystemExternalAIGC");
}
**/
function fnBtnSystemInternalOnClick() {
                window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
                //window.recognitionSystemInternal = null; // STT的系统内部实例声明或清空；
                window.recognitionSystemExternal = null; // STT的系统外部实例声明或清空；
                //window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；
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
                //window.QwenAPIKey=""; // Qwen千问APIKey声明或清空；
                document.getElementById('btnSystemExternal').disabled=true;
                document.getElementById('btnSystemInternal').disabled=false;
                document.getElementById('startBtnSystemInternal').disabled=true;
                document.getElementById('stopBtnSystemInternal').disabled=true; 
                document.getElementById('startBtnSystemExternal').disabled=false; 
                document.getElementById('stopBtnSystemExternal').disabled=true; 
                document.getElementById('startBtnSystemExternal').click();
 }

 function fnTTSOnEndSystemInternal(){
 opener.parent.document.getElementById("sIFrameContents").contentWindow.fnMargee();
 document.getElementById('stopBtnSystemInternal').click();
 document.getElementById('startBtnSystemInternal').click();
    }

 function fnTTSOnEndSystemExternal(){
 opener.parent.document.getElementById("sIFrameContents").contentWindow.fnMargee();
 document.getElementById('stopBtnSystemExternal').click();
 document.getElementById('startBtnSystemExternal').click();
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
    window.recognitionSystemInternal.onend=fnSTTOnEndSystemInternal;// 语音识别结束时的回调
    window.recognitionSystemInternal.onresult=fnSTTOnResultSystemInternal;//(event)参数不知是否正确传递了。

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
        alert("您可能还没输入并确认您的Qwen千问APIKey！");
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
    window.recognitionSystemExternal.onend=fnSTTOnEndSystemExternal;// 语音识别结束时的回调
    window.recognitionSystemExternal.onresult=fnSTTOnResultSystemExternal;//(event)参数不知是否正确传递了。

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
     document.getElementById('transcriptSystemInternal').innerText= '"段落": []'; // 清空显示区域
    window.speechSynthesis.cancel();  // 取消任何正在进行的TTS;
    window.recognitionSystemExternal.stop();
    try{window.recognitionSystemInternal.stop();}catch(e){;}// 停止系统内部的语音识别
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
    } 
else{document.getElementById("kernelDialogueSystemInternal").style.display="none"}
}
function fnUnderstandKernelDialogueBtnSystemExternal() {
   fnToggleEventSoureElementColor();
    if(document.getElementById("kernelDialogueSystemExternal").style.display=="none"){
    document.getElementById("kernelDialogueSystemExternal").style.display="block";
    } 
else{document.getElementById("kernelDialogueSystemExternal").style.display="none"}
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
    //TTS
    const utteranceInternal = new SpeechSynthesisUtterance("您需要"+window.transcriptSystemInternal+"对吗？"); 
     window.speechSynthesis.speak(utteranceInternal); 
    switch (true) {
case window.transcriptSystemInternal.indexOf("向上")>0: {
    alert("1");
    alert(opener.parent.name);
    alert(opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.body.textContent);
    opener.parent.document.getElementById("sIFrameTitle").contentWindow.fnBackword();
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    break;
}
case window.transcriptSystemInternal.indexOf("向下")>0: {
    alert("2");
    alert(opener.parent.name);
   alert(opener.parent.document.getElementById("sIFrameTitle").contentWindow.document.body.textContent);
    opener.parent.document.getElementById("sIFrameTitle").contentWindow.fnNext();
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    break;
    }
default:{
    alert("default");
    alert(opener.parent.name);
    //alert(opener.parent.document.getElementById("sIFrameContents").contentWindow.document.body.textContent);
    opener.parent.document.getElementById("sIFrameContents").contentWindow.fnMargee();
    utteranceInternal.onend=fnTTSOnEndSystemInternal;// 语音朗读结束时的回调;
    }
}
 }

function fnSTTOnEndSystemExternal() {
    window.isRecognizingSystemExternal = false;
     window.speechSynthesis.cancel(); 
       //TTS      
    const utteranceExternal = new SpeechSynthesisUtterance("您需要"+window.transcriptSystemExternal+"对吗？"); 
    /** 
    var sKeywordsForAIGC = window.transcriptSystemExternal;
    var url = "/QWen/index?queryString=" +sKeywordsForAIGC;
     open(url, "AIGCAnswerCharactor");
     **/
    utteranceExternal.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
     window.speechSynthesis.speak(utteranceExternal);  
    alert(fnCallAIGCQwen(window.transcriptSystemExternal));
    fnCallAIGCQwen(window.transcriptSystemExternal).then(answer => {
    // Do something with the answer
    alert(answer);
});
 window.speechSynthesis.cancel(); 
 var answerFromAIGCQwen=fnCallAIGCQwen(window.transcriptSystemExternal);
   const utteranceExternalAIGC = new SpeechSynthesisUtterance("千问AIGC的回答是："+answerFromAIGCQwen); 
   utteranceExternalAIGC.onend=fnTTSOnEndSystemExternal;// 语音朗读结束时的回调;
    window.speechSynthesis.speak(utteranceExternalAIGC);  
    alert("千问AIGC的回答是："+answerFromAIGCQwen);
    }

async function fnCallAIGCQwen(prompt) {
    const response = await fetch('https://qwen-api.aliyun.com/v1/completions', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': window.QwenAPIKey // Replace with your actual key, or use a backend proxy
        },
        body: JSON.stringify({
            model: 'qwen-turbo', // Use the correct model name per Qwen docs
            prompt: prompt,
            max_tokens: 100,
            temperature: 0.7
        })
    });

    if (!response.ok) {
        throw new Error('Qwen API call failed!');
    }

    const data = await response.json();
    console.log(data.choices[0].text); // Output the generated text
    return data.choices[0].text;
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

function fnidQwenAPIKeyConfirmOnClickSystemExternal(){
    window.QwenAPIKey=document.getElementById('idQwenAPIKeyInput').value;
    document.getElementById('idQwenAPIKeyInput').value="";
    document.getElementById('idQwenAPIKeyInput').placeholder="您已键入Qwen千问的API Key";
    //alert(window.QwenAPIKey);
    alert(document.getElementById('idQwenAPIKeyInput').placeholder);
}


/**
 function fnMatchCommandSystemInternal(){
 var trueorfalse=prompt("主人，您是需要查看当前条目的作业与测验，对吗？"); 
 if(trueorfalse){
     opener.parent.document.getElementById("sIFrameContents").contentWindow..fnMargee();
     opener.parent.document.getElementById("sIFrameContents").contentWindow.fnViewHomeworkAndTest();
     }
 else{
     window.speechSynthesis.cancel();
     const utterance = new SpeechSynthesisUtterance("好的，主人，请重新说出您的需求！");     
     window.speechSynthesis.speak(utterance);
     }
 }

async function fnFetchNunStreamDataSystemExternal(prompt){//浏览器Prompt AIGC后的非流式返回answer。系统外部的调用AIGC的语音对话注意事项：API 密钥安全性 不要将密钥直接暴露在前端代码中，建议通过后端代理转发请求。兼容性处理 不同大模型（例如，ChatGPT、Qwen）的 API 参数可能略有不同，请参考官方文档调整
    const response = await fetch('https://api.openai.com/v1/completions', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    'Authorization': window.QwenAPIKey
    },
    body: JSON.stringify({
    model: 'gpt-4',
    prompt: prompt,
    max_tokens: 100,
    temperature: 0.7
    //stream: true // 如果启用流式响应
    })
    });

    if (!response.ok) {
    throw new Error('未能收到AIGC的答复！');
    }

    const data = await response.json();
    console.log(data.choices[0].text); // 输出生成的文本
    return data;
   //案例：fnFetchNunStreamData('请翻译教育数字思维成为英文');
 }

 async function fnFetchStreamDataSystemExternal(prompt){ //浏览器Prompt AIGC后的流式返回answer  //系统外部的调用AIGC的语音对话注意事项：API 密钥安全性 不要将密钥直接暴露在前端代码中，建议通过后端代理转发请求。兼容性处理 不同大模型（例如，ChatGPT、Qwen）的 API 参数可能略有不同，请参考官方文档调整
    const response = await fetch('https://api.openai.com/v1/completions', {
    method: 'POST',
    headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer YOUR_API_KEY`
    },
    body: JSON.stringify({
    model: 'gpt-4',
    prompt: prompt,
    stream: true // 启用流式响应
    })
    });

    if (!response.ok) {
    throw new Error('未能收到AIGC的答复！');
    }

    const reader = response.body.getReader();
    const decoder = new TextDecoder('utf-8');
    let done = false;

    while (!done) {
    const { value, done: readerDone } = await reader.read();
    done = readerDone;
    const chunk = decoder.decode(value, { stream: true });
    console.log(chunk); // 实时输出每块数据
    return chunk;
    }
    //案例：fnFetchStreamData("请翻译教育数字思维成为英文");
}

**/
