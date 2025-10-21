function fnToggleEventSoureElementColor(){
   // alert();
    var eventSoureElementColor=window.event.target.style.color;
    
    if(eventSoureElementColor==""||eventSoureElementColor==null){
        window.event.target.style.color="green";
    }
    else{
        window.event.target.style.color="";
    }
}
function fnToggleContentEditable(i) {
    
    var oTable = document.getElementsByTagName("table").item(0);
    var cTr = Array.from(oTable.rows);

    var oChildren=cTr[i].querySelectorAll('*');
    var iChildrenLength=oChildren.length;
  // alert(iChildrenLength);
    for(m=5;m<iChildrenLength;m++)
    {
    if(oChildren[m].contentEditable == "true")
    { 
        oChildren[m].contentEditable =window.oArrayForTr1ContentEditable[m];
         event.target.style.color=""; 
    }
    else{ 
      window.oArrayForTr1ContentEditable[m]=oChildren[m].contentEditable;
      oChildren[m].contentEditable = "true";
      event.target.style.color="green"; 
    }
    }
    
    for (j=i;j<i+8;j++){

    if (cTr[j+1].contentEditable == "true") 
    {
        cTr[j+1].contentEditable = window.oArrayForTr2ToEndContentEditable[j+1]; 
        event.target.style.color=""; 
        }
    else {
        window.oArrayForTr2ToEndContentEditable[j+1]=cTr[j+1].contentEditable;
        //alert(oArray[j+1]);     
        cTr[j+1].contentEditable = "true";
         event.target.style.color="green"; 
        }     
    }           
    }
function fnToggleScreen() {
    try {
        window.event.returnValue = false;//去除双击时的默认选定文本效果
    }
    catch (e) {//考虑目录框架的右键菜单中的命令。
        return;
    }
    finally {
        //if (parent.document.getElementById("sFramesetMiddle").cols == "1022,*") {
        
        if (parent.document.getElementById("sFramesetBook").rows == "100,*,20") {
            parent.sFramesetMiddleColsTemp = parent.document.getElementById("sFramesetMiddle").cols;           
            parent.document.getElementById("sFramesetBook").rows = "0,*,20" 
            parent.document.getElementById("sFramesetMiddle").cols = "0%,*";
        }
        else {                       
            parent.document.getElementById("sFramesetBook").rows = "100,*,20" 
            parent.document.getElementById("sFramesetMiddle").cols = parent.sFramesetMiddleColsTemp;  
            //parent.document.getElementById("sFramesetMiddle").cols = "1022,*";
            //parent.document.getElementById("sFramesetMiddle").cols = "0%,*";           
        }
    }
}
function fnToggleItemDisplay(i) {
   
    var oTable = document.getElementsByTagName("table").item(0);
    var cTr = Array.from(oTable.rows);

    var oChildren=cTr[i].querySelectorAll('*');
    var iChildrenLength=oChildren.length;
  // alert(iChildrenLength);
    for(m=5;m<iChildrenLength;m++)
    {
    if(oChildren[m].style.display == "none")
    { 
        oChildren[m].style.display=window.oArrayForTr1Display[m];
         event.target.style.color=""; 
    //oChildren[m].style.display = "block";
    }
    else{ 
      window.oArrayForTr1Display[m]=oChildren[m].style.display;
      oChildren[m].style.display = "none";
      event.target.style.color="green"; 
    }
    }

    
    for (j=i;j<i+8;j++){

    if (cTr[j+1].style.display == "none") 
    {
        cTr[j+1].style.display = window.oArrayForTr2ToEndDisplay[j+1]; 
        event.target.style.color=""; 
        }
    else {
        window.oArrayForTr2ToEndDisplay[j+1]=cTr[j+1].style.display;
        //alert(oArray[j+1]);     
        cTr[j+1].style.display = "none";
         event.target.style.color="green"; 
        }     
    }
    }

function fnHomeworkAndTest() {
   // fnTooManyModelDialog(); 
  window.oArrayForTr1Display = [];
  window.oArrayForTr2ToEndDisplay = [];
  window.oArrayForTr1ContentEditable = [];
  window.oArrayForTr2ToEndContentEditable = [];
 /**
  window.speechSynthesis.cancel();
  const utterance = new SpeechSynthesisUtterance(document.body.textContent);
  window.speechSynthesis.speak(utterance);
  **/

    fnValidationHomeworkAndTest();
    var oDate = new Date();
    var sTimeStamp=oDate.getTime();
  //  var cTr = document.getElementsByTagName("table").item(0).getElementsByTagName("tr");//这样会包含嵌套表格的行。
  //alert(cTr[1].getElementsByTagName("td").item(0).innerHTML);
  var oTable = document.getElementsByTagName("table").item(0);var cTr = Array.from(oTable.rows); // 只包含主表的直接<tr>。解决上述包含嵌套表格的行。
   //alert(cTr[1].getElementsByTagName("td").item(0).innerHTML);
    var iTrLenth = cTr.length;
   var iProblemNum = iTrLenth / 9;
    //console.log(cTr[1].innerHTML);
   
    for (i = 0; i < iTrLenth; i++) {
        //for (j = 0; j < 6; j=i*j) {
        if ((i + 9) % 9 === 0) { cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span style=\"display:inline-block; padding-top: 12px;padding-bottom: 12px;color:red\">题目" + ((i + 9) / 9) + "：<input type=\"button\" style=\"white-space: normal; text-align: justify\" value=\"单击切换本题显示\" title='以便聚焦显示的题目，以便排除其他题目干扰！' onclick=\"fnToggleItemDisplay("+i+")\" /><input type=\"button\" style=\"white-space: normal; text-align: justify\" value=\"单击切换本题在线编辑\" title='当前只是临时应急排版错误，但是无法保存编辑结果，必须重新上传当前条目的作业测验的.doc/.docx文档（作业测验的模板建议单击“标题框架”的“关于条目资源模板”下载）。后续将开发保存编辑结果的功能！' onclick=\"fnToggleContentEditable("+i+")\" /><input type=\"button\" style=\"white-space: normal; text-align: justify\" value=\"单击登录服务端的作业测验的“四层平台”的思维语言生成的“五层MVC”的统计分析（正在迁移开发之中...）\" onclick=\"fnHomeworkAndTestFiveLayerMVC()\" /></span>" + cTr[i].getElementsByTagName("td").item(0).innerHTML; }

        if ((i + 9) % 9 === 1) {cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"radio\" name=\"options" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\" />(A)</span>" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span>"; }
        if ((i + 9) % 9 === 2) {cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"radio\" name=\"options" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\" />(B)</span>" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span>"; }
        if ((i + 9) % 9 === 3) {cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"radio\" name=\"options" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\" />(C)</span>" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span>"; }
        if ((i + 9) % 9 === 4) {cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"radio\" name=\"options" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\" />(D)</span>" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span>";}

        if ((i + 9) % 9 === 5) {cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"button\" style=\"white-space: normal; text-align: justify\" title='（本机端作业测验不支持成绩的“四层平台”思维语言生成的“五层MVC”的统计分析，服务端作业测验支持成绩的“四层平台”思维语言生成的“五层MVC”的统计分析！）（本机端作业测验←可以登录服务端同步→服务端作业测验！）' onclick=\"fnAnswerAndToggoleAnswerDisplay();\" value=\"查看本题答案 【注：（本机端作业测验不支持成绩的“四层平台”思维语言生成的“五层MVC”的统计分析，服务端作业测验支持成绩的“四层平台”思维语言生成的“五层MVC”的统计分析！）（本机端作业测验←可以登录服务端同步→服务端作业测验！）】\" /></span>" + "<span style=\"display:none\" id=\"idAnswer" + sTimeStamp + Math.floor(((i + 9) / 9))  +"\""+">" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span></span>";}
       if ((i + 9) % 9 === 6) { cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"button\" style=\"white-space: normal; text-align: justify\" onclick=\"fnToggoleAnswerExplainDisplay();\" value=\"查看本题答案的解释 【注：建议基于本题答案解释的具体需求，超链接合适的字符（例如.docx文件、.pptx文件）-图像视频（例如.mp4文件）-2D（例如.svg文件）-3D（例如.x3d文件）】\" /></span>" + "<span style=\"display:none\" id=\"idExplain" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\""+">" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span></span>"; }
       if ((i + 9) % 9 === 7) { cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"button\" style=\"white-space: normal; text-align: justify\" onclick=\"fnToggoleFourLayerLanguageDisplay();\" value=\"查看本题选用的“四层平台”的思维语言的解释【注：同一目录条目的作业测验，各个题目选用的思维语言，一般差别不大】\" /></span>" + "<span style=\"display:none\" id=\"idToggoleFourLayerLanguageDisplay" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\""+">" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span></span>"; }
        if ((i + 9) % 9 === 8) { cTr[i].getElementsByTagName("td").item(0).innerHTML = "<span><span style=\"color:red\"><input type=\"button\" style=\"white-space: normal; text-align: justify\" onclick=\"fnToggoleFiveLayerMVCExplainDisplay();\" value=\"查看本题选用的“四层平台”的思维语言生成的“五层MVC”面向的主要层次【注：A、实践-数据读写封装（例如：人物对象的内容方法←映射→数据库数据仓库）；B、技术-信息提取运用（例如：数据确定性描述统计。典型案例：数据总计平均等等多维分析）；C、科学-规律预测探究（例如：数据概率性推断统计。典型案例：数据挖掘）；D、人文-情感交流共鸣（例如：数据概率性人文推断统计。典型案例：数据人文挖掘）；E、哲学-智能建构生成（例如：数据概率性AIGC推断统计。典型案例：神经元多层网络的已经训练学习的概率性推断统计）】\" /></span>" + "<span style=\"display:none\" id=\"idFiveLayerMVCExplain" + sTimeStamp + Math.floor(((i + 9) / 9)) + "\""+">" + cTr[i].getElementsByTagName("td").item(0).innerHTML + "</span></span>"; }
       // }
    }
    //alert(cTr[9].getElementsByTagName("td").item(0).innerHTML + ";" + cTr[9].getElementsByTagName("td").item(0).innerHTML + ";" + cTr[10].getElementsByTagName("td").item(0).innerHTML);
    /**
     if(!("speechSynthesis" in window)) {throw alert("对不起，您的浏览器不支持");} 
     //alert(parent.document.getElementById("sFramesetContentAndHomeworkAndTest").rows=="0%,*");
   if(parent.document.getElementById("sFramesetContentAndHomeworkAndTest").rows=="0%,*")
	{
        window.speechSynthesis.cancel();
        const utterance = new SpeechSynthesisUtterance(document.body.textContent);
        window.speechSynthesis.speak(utterance);
   }
   **/
  document.body.ondblclick=fnToggleScreen;
}



function fnValidationHomeworkAndTest() {
   
    try {
       // var cTr = document.getElementsByTagName("table").item(0).getElementsByTagName("tr");//最外层表格嵌套了表格（如题干有表格）的问题没有解决
        var oTable = document.getElementsByTagName("table").item(0);var cTr = Array.from(oTable.rows); // 只包含主表的直接<tr>。解决上述包含嵌套表格的行。
        var iTrLenth = cTr.length;
       // alert(iTrLenth);
       // if (iTrLenth % 8 != 0) { alert("上传的作业与测试可能有问题，无法正常运行。请基于word模板文件，排版制作作业与测试，然后上传！每道题必须是9个表行。总表行必须是9的倍数。"); }
       
if (iTrLenth % 9 != 0) { alert("上传的作业与测试可能有问题，无法正常运行。请基于word模板文件，排版制作作业与测试，然后上传！每道题必须是9个表行。总表行必须是9的倍数。"); }
      //  if (false) { alert("上传的作业与测试可能有问题，每道题必须是9个表行。第6个表行是答案，必须是A\B\C\D中的一个字母，不能有其他字符、空格！请基于word模板文件，排版制作作业与测试，然后上传！"); }//
      if (false) { alert("上传的作业与测试可能有问题，每道题必须是9个表行。第6个表行是答案，必须是A\B\C\D中的一个字母，不能有其他字符、空格！。第8个表行是四层平台思维语言的解释。第9个表行是五层MVC的解释（A.实践-数据读写封装/B.技术-信息提取运用/C.科学-规律预测探究/D.人文-情感交流共鸣/E.哲学-智能建构生成），必须是A\B\C\D\E中的一个字母，不能有其他字符、空格！请基于word模板文件，排版制作作业与测试，然后上传！"); }//
    }
    catch (e) {
        alert("上传的作业与测试可能有问题，无法正常运行。请基于word模板文件，排版制作作业与测试，然后上传！");
    }
}

function fnHomeworkAndTestFiveLayerMVC(){
    fnToggleEventSoureElementColor();
    //var sUrl="https://aspdotnetmvcyuqin.azurewebsites.net/HomeworkAndTestFiveLayerMVC/Index";
    var sUrl="../../../../options/HomeworkAndTestFiveLayerMVC.htm";
    window.open(sUrl,"winHomeworkAndTestFiveLayerMVC", "scrollbars=,width=800,height=600,top=" + (screen.height - 600) / 2 + ",left=" + (screen.width - 800) / 2);
}
function fnAnswerAndToggoleAnswerDisplay() {
    fnToggleEventSoureElementColor();
    document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "none";
    //alert(event.srcElement.parentElement.parentElement.getElementsByTagName("span").item(event.srcElement.parentElement.parentElement.getElementsByTagName("span").length - 1).innerHTML);
    // alert(event.srcElement.parentElement.parentElement.getElementsByTagName("span").item(event.srcElement.parentElement.parentElement.getElementsByTagName("span").length - 1).id);
    // alert(event.srcElement.parentElement.parentElement.children[1].innerHTML);
    // alert(event.srcElement.parentElement.parentElement.children[1].id);
    //  alert(document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display);
    
   // var sAnswer = document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).childNodes.item(1).innerHTML.replace(/\s*/g, "")
    alert(document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).childNodes.item(1).childNodes.item(0).innerHTML);//可能和Word版本有关。Word2106测试可以。有些版本可能要改为document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).childNodes.item(1).innerHTML
     var sAnswer = document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).childNodes.item(1).childNodes.item(0).innerHTML.replace(/\s*/g, "")//可能和Word版本有关。Word2106测试可以。
    var sIdOfAnswer = event.srcElement.parentElement.parentElement.children[1].id;
    //var sAutoGenPartOfAnswer = sIdOfAnswer.subString("idAnswer".length-1);
    var sAutoGenPartOfIdOfAnswer = sIdOfAnswer.substring("idAnswer".length);
    var cInput = document.getElementsByTagName("input");
    var iLengthOfInput = cInput.length;
    var iTarget = -1;
    for (var i = 0; i < iLengthOfInput; i++) {
        //console.log(cInput[i].type);
        if (cInput[i].type == "radio" && (cInput[i].checked == true) &&(cInput[i].name.indexOf(sAutoGenPartOfIdOfAnswer) > 1)) {
      // if ((cInput[i].type == "radio" )&& (cInput[i]..name.indexOf(sAutoGenPartOfIdOfAnswer) > 1)) {
            iTarget=i; 
       }
    }
    if (iTarget == -1) { alert("本题目中，您可能还没有单击选择一个选项！请重新选择一个选项！");return; }
    else {
       
        var isCorrect = "错误！";
        
        if (cInput[iTarget].parentNode.textContent.indexOf(sAnswer) > 0) {
            isCorrect = "正确！";
            alert("您选择的是：" + cInput[iTarget].parentNode.textContent + "；" + "您的选择：" + isCorrect + "；" + "正确答案是：" + sAnswer);
        }
        else {
            var isReDo = confirm("您选择的是：" + cInput[iTarget].parentNode.textContent + "；" + "您的选择：" + isCorrect + "；" +"重做吗？");
            if (isReDo) { return; }
            else {
                //if (document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display == "none") {
                    document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "";
               // }
               // else {
                  //  document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "none";
                //}
            }
        }
    }
}

function fnToggoleAnswerExplainDisplay() {
    fnToggleEventSoureElementColor();
        if (document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display == "none") {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "";
    }
    else {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "none";
    }
}

function fnToggoleFourLayerLanguageDisplay() {
    fnToggleEventSoureElementColor();
        if (document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display == "none") {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "";
    }
    else {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "none";
    }
}
function fnToggoleFiveLayerMVCExplainDisplay() {
    fnToggleEventSoureElementColor();
        if (document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display == "none") {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "";
    }
    else {
        document.getElementById(event.srcElement.parentElement.parentElement.children[1].id).style.display = "none";
    }
}


