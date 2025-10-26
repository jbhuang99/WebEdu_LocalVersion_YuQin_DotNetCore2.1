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
    //document.getElementById('idQwenAPIKeyInput').value="";
    document.getElementById('idQwenAPIKeyInput').placeholder="您已键入Qwen千问的API Key";
    //alert(window.QwenAPIKey);
    alert(document.getElementById('idQwenAPIKeyInput').placeholder+"："+window.QwenAPIKey);
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


function fnAjaxServerSideCallAIGCAnswerCharactor() {
    fnToggleEventSoureElementColor();
            var sPrompt = document.getElementById("idPrompt").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceInternalPrompt = new SpeechSynthesisUtterance("您的Prompt是"+sPrompt+"对吗？"); 
             window.speechSynthesis.speak(utteranceInternalPrompt); 
            alert("您的Prompt是：" + sPrompt);
            var sURL = "/QWen/index?queryString=" + sPrompt;
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
                        document.getElementById("idShowServerSidePromptAnswer").innerHTML ="语音对话机器人的回答Answer是："+oTemp.output.text;
                        window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer是"+oTemp.output.text); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswer); 
                    }
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer是"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }
        }

   function fnAjaxClientBrowserSideCallAIGCAnswerCharactor() {
     if(window.QwenAPIKey==null||window.QwenAPIKey==""){
        alert("您可能还没输入并确认您的Qwen千问APIKey！");
    }
    /**
    else{
    fnToggleEventSoureElementColor();
            var sPrompt = document.getElementById("idPromptForClientBrowserSide").value;
             window.speechSynthesis.cancel();
                     //TTS
             const utteranceInternalPrompt = new SpeechSynthesisUtterance("您的Prompt是"+sPrompt+"对吗？"); 
             window.speechSynthesis.speak(utteranceInternalPrompt); 
            alert("您的Prompt是：" + sPrompt);
            var sURL = "/QWen/index?queryString=" + sPrompt;
            // var sURL = "https://localhost:5001/QWen/index?queryString=" + sSearchedKeywords;
           // open(sURL, "ServerSideCallAIGCAnswerCharactor");
           var xmlHttpRequest = new XMLHttpRequest();
            xmlHttpRequest.open('GET', sURL, true);//如果是post：xmlHttpRequest.open('POST',sURL , true);
           xmlHttpRequest.send();////如果是post：xmlHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');  //设置请求头说明文档类型   xhr.send(data);  //send里传递数据
            xmlHttpRequest.onreadystatechange = function () {  //如果readyState发生变化的时候执行的函数

                if (xmlHttpRequest.readyState == 4) {  //ajax为4说明执行完了

                    if (xmlHttpRequest.status == 200) { //如果是200说明成功
                        //如果函数存在的话执行
                        var sTemp = xmlHttpRequest.responseText;
                        document.getElementById("idShowClientBrowserSidePromptAnswer").innerHTML ="语音对话机器人的回答Answer是："+sTemp;
                        window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswer = new SpeechSynthesisUtterance("语音对话机器人的回答Answer是"+sTemp); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswer); 
                    }
                    else {
                        var sTempErr ='出错了,错误编号是：'+xmlHttpRequest.status+xmlHttpRequest.responseText;
                        alert(sTempErr);
                         window.speechSynthesis.cancel();
                     //TTS
                     const utteranceInternalAIGCAnswerOnError = new SpeechSynthesisUtterance("语音对话机器人的回答Answer是"+sTempErr); 
                     window.speechSynthesis.speak(utteranceInternalAIGCAnswerOnError); 
                    }
                }
        }        
        }
        **/
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
