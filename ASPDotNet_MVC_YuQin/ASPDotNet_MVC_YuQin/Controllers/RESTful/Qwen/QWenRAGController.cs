using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OpenXmlPowerTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using WebEdu_LocalVersion_YuQin_DotNetCore21.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers
{
    //[Authorize()]
    public class QWenRAGController : Controller
    {
        private IHostingEnvironment _hostingEnvironment { get; }
        //private IFileProvider _fileProvider { get; }//ASP.NET Core 2.0提供了继承自接口IFileProvider的PhysicalFileProvider类型，用于受限地访问本地文件系统，它是对System.IO.File的一个封装。只能读文件系统？


        public QWenRAGController(IHostingEnvironment hostingEnvironment)//通过注入 IHostingEnvironment 服务对象来取得Web根目录和内容根目录的物理路径
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/value
        // private String CreatedHTML{ get; set; }
        [HttpGet]
        public IActionResult Get()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
          //  string contentRootPath = _hostingEnvironment.ContentRootPath;
            String sSearchedKeyWords = this.Request.Query["SearchedKeywords"];//从浏览器端最好再传一个当前目录的含有j课文的总条目数，如果查询返回的文件总数等于当前条目的总数，意味着用户键入的Prompt意义不大，可以提示用户重新输入更有意义的Prompt。
            Console.WriteLine("sFolderPathOfTextbookResources" + sSearchedKeyWords);
            String sFolderPathOfTextbookResources = webRootPath + "\\webCourse\\lessons\\content\\book";
            
            //IEnumerable<String> sEnumDirectoriesOfTextbookResources = Directory.EnumerateDirectories(sFolderPathOfTextbookResources);//返回文件夹的绝对路径
             IEnumerable<String> sEnumerateFilesOfTextbookResources = Directory.EnumerateFiles(sFolderPathOfTextbookResources,"",System.IO.SearchOption.AllDirectories);//返回文件夹下所有文件的绝对路径
             // IEnumerable<String> sEnumFilesOfTextbookResources = Directory.EnumerateFiles(sFolderPathOfTextbookResources,System.IO.EnumerationOptions);//返回文件夹的绝对路径
            String sSearedFileNames = "";   
            String sPrompt = "";
            String sPromptTemp="";
            foreach (String sFileName in sEnumerateFilesOfTextbookResources)
            {
                //sTemp += sDirectory.Substring(sDirectory.LastIndexOf("\\")+1) + sSeperation;
                if (sFileName.Substring(sFileName.LastIndexOf(".")+ 1)=="htm")
                {
                    Boolean isSearched = false;
                    StreamReader streamReader = new StreamReader(sFileName);// 创建文本的读取流(会检查字节码标记确定编码格式)
                    String stringFromFile = streamReader.ReadToEnd(); //读取文本中的所有字符                                                                     
                    if (stringFromFile.IndexOf(sSearchedKeyWords) > 0){ 
                        isSearched=true; 

                    }
                    streamReader.Close();
                    if (isSearched) {
                        String sTemp = sFileName.Substring(sFileName.LastIndexOf("\\") + 1);
                        sSearedFileNames += sTemp.Substring(0, sTemp.LastIndexOf(".")) + "|";//在此特殊情况，文件名等于文件夹名。以“|”分割以便浏览器端裁切还原。
                       // sSearedFileNames += sTemp + "|";//在此特殊情况，文件名等于文件夹名。以“|”分割以便浏览器端裁切还原。
                    }
                }
            }
            if (false)
            {
                //拟增加代码，sSearedFileNames分割“|”，如果查询返回的文件总数等于当前条目的总数，意味着用户键入的Prompt意义不大，可以提示用户重新输入更有意义的Prompt。
                return this.Content("您当前输入的Prompt提问可能意义不大，请重新输入更有意义的Prompt提问！");
            }
            else {
                //查找所有可能的Prompt提问，组成复合Prompt提问，发送给AIGC模型进行回答，即本系统选用的（未选用矢量数据库技术的）融合数智思维知识库的RAG。
                foreach (String sFileName in sEnumerateFilesOfTextbookResources)
                {
                    if (sFileName.Substring(sFileName.LastIndexOf(".") + 1) == "htm")
                    {
                        Boolean isSearched = false;
                        StreamReader streamReader = new StreamReader(sFileName);// 创建文本的读取流(会检查字节码标记确定编码格式)
                        String stringFromFile = streamReader.ReadToEnd(); 
                        String sStringTempForLoop=stringFromFile;
                        while (sStringTempForLoop.IndexOf(sSearchedKeyWords) >= 0)
                        {
                            //是的，这个文件内容包含用户输入的关键词，可以从中抽取可能的Prompt提问。
                            String sFront = sStringTempForLoop.Substring(0, sStringTempForLoop.IndexOf(sSearchedKeyWords));//前面部分
                            //Console.WriteLine(sFront+"\n"+"\r");

                            String sBack = sStringTempForLoop.Substring(sStringTempForLoop.IndexOf(sSearchedKeyWords) + sSearchedKeyWords.Length, sStringTempForLoop.Length - sFront.Length - sSearchedKeyWords.Length);//后面部分
                             // Console.WriteLine(sBack + "\n" + "\r");
                            //从sFront向前查找最近的</p>标签，从sBack向后查找最近的<p>标签，抽取出完整的段落内容作为可能的Prompt提问。

                            String sFrontPart = sFront.Substring(sFront.LastIndexOf("</p>") + "</p>".Length, sFront.Length - sFront.LastIndexOf("</p>") - "</p>".Length);
                            //Console.WriteLine(sFrontPart + "\n" + "\r");
                            String sBackFrontPart = sBack.Substring(0, sBack.IndexOf("<p"));
                            //Console.WriteLine(sBackFrontPart + "\n" + "\r");

                            // Console.WriteLine(sFrontPart + sSearchedKeyWords + sBackFrontPart+"\n" + "\r");
                           
                            
                            //Console.WriteLine(sPromptTemp + "\n" + "\r");
                            String sBackBackPart = sBack.Substring(sBack.IndexOf("</p>")+ "</p>".Length, sBack.Length- sBack.IndexOf("</p>") -"</p>".Length);
                                                       
                            sStringTempForLoop = sBackBackPart;
                           // Console.WriteLine("Prompt:"+sPromptTemp + "  " + StripHtmlTags(sFrontPart) + "  "+(sPromptTemp.IndexOf(StripHtmlTags(sFrontPart)) >= 0).ToString() + "\n" + "\r");
                           // Console.WriteLine("Prompt:" + sPromptTemp + "  " + StripHtmlTags(sBackFrontPart) + "  " + (sPromptTemp.IndexOf(StripHtmlTags(sBackFrontPart)) >= 0).ToString() + "\n" + "\r");
                            //想去重，但是失败
                            if (sPromptTemp.IndexOf(StripHtmlTags(sFrontPart)) >= 0) { 
                               // Console.WriteLine((sPromptTemp.IndexOf(StripHtmlTags(sFrontPart)) >= 0).ToString() + "\n" + "\r"); 
                            }
                            else if (sPromptTemp.IndexOf(StripHtmlTags(sBackFrontPart)) >= 0) { 
                                //Console.WriteLine((sPromptTemp.IndexOf(StripHtmlTags(sBackFrontPart)) >= 0).ToString() + "\n" + "\r");
                                }
                            else
                            {
                                sPromptTemp = StripHtmlTags(sFrontPart + sSearchedKeyWords + sBackFrontPart);
                            }
                        }

                        isSearched = true;
                        streamReader.Close();
                        if (isSearched)
                        {
                            sPrompt=sPrompt+sPromptTemp + "\n";                            
                        }
                    }
                }

                
                //Console.WriteLine(sPrompt);
                String sPromptRemoveDuplicateParagraphs = TextUtils.RemoveDuplicateParagraphs(sPrompt, true, true);
                
                Console.WriteLine(sPromptRemoveDuplicateParagraphs);
                //return  RedirectToAction("Index", "QWen", new { queryString = sPrompt });
                //return this.Content("您当前输入的Prompt提问，正在等待AIGC回答！");
                return this.Content(sPromptRemoveDuplicateParagraphs);
            }
        }
        public static String StripHtmlTags(String html) //•	正则适合简单场景，但无法完美处理损坏或非常复杂的 HTML（比如嵌套不规范标签、CDATA 等）。•	生产环境建议使用 HTML 解析器（例如 HtmlAgilityPack）解析后取 InnerText，更加稳健且安全。
        {
            if (String.IsNullOrEmpty(html)) return String.Empty;

            // 1. 删除 <script> 和 <style> 块（包括内容）
            html = Regex.Replace(html, @"(?is)<script.*?>.*?</script>", String.Empty);
            html = Regex.Replace(html, @"(?is)<style.*?>.*?</style>", String.Empty);

            // 2. 删除注释
            html = Regex.Replace(html, @"(?s)<!--.*?-->", String.Empty);

            // 3. 删除所有 HTML 标签
            html = Regex.Replace(html, @"<[^>]+>", String.Empty);

            // 4. HTML 解码（把 &amp; &nbsp; 等实体转换回字符）
            html = WebUtility.HtmlDecode(html);

            // 5. 清理多余空白、换行
            html = Regex.Replace(html, @"\s{2,}", " ").Trim();
            return html;
        }
    }

    public static class TextUtils
    {
        /// <summary>
        /// 按 "\n" 切分，去除空段落，按首次出现顺序去重，返回以 Environment.NewLine 连接的结果。
        /// </summary>
        public static string RemoveDuplicateParagraphs(string input, bool ignoreCase = true, bool trimWhitespace = true)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            var comparer = ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
            var seen = new HashSet<string>(comparer);
            var result = new List<string>();

            // 统一换行并按 '\n' 切分；保留段内换行需要更复杂的逻辑
            var parts = input.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            foreach (var raw in parts)
            {
                var p = trimWhitespace ? raw.Trim() : raw;
                if (string.IsNullOrEmpty(p)) continue;

                // 可选：规范化连续空白为单个空格，去掉多余空格
                p = Regex.Replace(p, @"\s{2,}", " ");

                if (seen.Add(p))
                    result.Add(p);
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}
