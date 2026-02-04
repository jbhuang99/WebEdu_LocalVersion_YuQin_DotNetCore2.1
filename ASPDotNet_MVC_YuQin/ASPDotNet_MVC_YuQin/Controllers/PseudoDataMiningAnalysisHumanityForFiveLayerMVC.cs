///** //不删除-客户端操作系统端可能可以使用Bundle.Microsoft.Office.Interop之类COM
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using WebEdu_LocalVersion_YuQin_DotNetCore21.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
//using Microsoft.Office.Core;//引用不了。本来说可以设置word另存为utf8格式的.htm
using System.Text;
using WebEdu_LocalVersion_YuQin_DotNetCore21.Common;
using Microsoft.EntityFrameworkCore;
using CurriculumSelection.Data;
using Microsoft.Extensions.Logging;


namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers
{
    public class PseudoDataMiningAnalysisHumanityForFiveLayerMVCController : Controller
    {
        public PseudoDataMiningAnalysisHumanityForFiveLayerMVCController(
             IHostingEnvironment hostingEnvironment,
             CurriculumSelectionDbContext curriculumSelectionDbContext,
             ILogger<PseudoDataCreationForFiveLayerMVCController> logger) // inject logger for diagnostics
        {
            _hostingEnvironment = hostingEnvironment;
            _curriculumDbContext = curriculumSelectionDbContext;
            _logger = logger;
        }

        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly CurriculumSelectionDbContext _curriculumDbContext;
        private readonly ILogger<PseudoDataCreationForFiveLayerMVCController> _logger;

        //private IFileProvider _fileProvider { get; }//ASP.NET Core 2.0提供了继承自接口IFileProvider的PhysicalFileProvider类型，用于受限地访问本地文件系统，它是对System.IO.File的一个封装。只能读文件系统？
        // GET api/value
        // private String CreatedHTML{ get; set; }
        [HttpGet]
        public IActionResult Get()
        {
            // sFileName准备用作文件读取；
            return Content("<HTML><HEAD><TITLE>课文</TITLE><BODY><div style='width:100%;height:100%;text-align:center;vertical-align:middle;'><span>(中层第4层MVC) 人文(情感交流共鸣)【默认标量的媒体字面顺序匹配，理解向量/矩阵/张量的媒体Token语义距离】：数据人文推断统计的案例（当前还需在VS/SSMS/SS中预实现后调用）\" id=\"id_ButtonPseudoDataMiningAnalysisHumanityForFiveLayerMVCForm\" style=\"white-space: normal\" title=\"单击将提交，开始人文推断统计海量数据仓库数据！请耐心等候...数据人文推断统计进度可以本机本系统服务端的Console界面中查看...</span></div></BODY ></HTM>", "text/html", System.Text.Encoding.UTF8);//不知为什么，必须""嵌套''，否则提示“字符文本中字符太多”错误
        }

        public async Task<IActionResult> Post()//ASP.NET MVC 操作支持使用简单的模型绑定（针对较小文件）或流式处理（针对较大文件）上传一个或多个文件。(在此选择流式处理）)
        {
            // return (Content(Request.Query["FolderAndFileName"]));
            //判断是本机版还是服务器版，服务器不允许上传
            return Content("服务器版不允许上传文件");

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}