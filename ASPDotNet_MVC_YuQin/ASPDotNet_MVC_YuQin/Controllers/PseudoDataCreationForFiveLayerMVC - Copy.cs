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


namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers
{
    public class PseudoDataCreationForFiveLayerMVCController : Controller
    {
        public PseudoDataCreationForFiveLayerMVCController(IHostingEnvironment hostingEnvironment)//通过注入 IHostingEnvironment 服务对象来取得Web根目录和内容根目录的物理路径
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private IHostingEnvironment _hostingEnvironment { get; }
        //private IFileProvider _fileProvider { get; }//ASP.NET Core 2.0提供了继承自接口IFileProvider的PhysicalFileProvider类型，用于受限地访问本地文件系统，它是对System.IO.File的一个封装。只能读文件系统？
        // GET api/value
        // private String CreatedHTML{ get; set; }
        [HttpGet]
        public IActionResult Get(String sFileName)
        {
            // sFileName准备用作文件读取；
            return Content("<HTML><HEAD><TITLE>课文</TITLE><BODY><div contenteditable='true' style='width:100%;height:100%;text-align:center;vertical-align:middle;'><span style='line-height: 400px;'>该条目尚没有对应的课文，这是自动创建的文本，请编辑</span></div></BODY ></HTM>", "text/html", System.Text.Encoding.UTF8);//不知为什么，必须""嵌套''，否则提示“字符文本中字符太多”错误
        }

        public async Task<IActionResult> Post()//ASP.NET MVC 操作支持使用简单的模型绑定（针对较小文件）或流式处理（针对较大文件）上传一个或多个文件。(在此选择流式处理）)
        {
            // return (Content(Request.Query["FolderAndFileName"]));
            //判断是本机版还是服务器版，服务器不允许上传

            if (new LocalVersionOrServerVersion().IsLocalVersion(this.Request.Host.ToUriComponent()))
            {

                //return Ok(new { count = files.Count, size, filePathAll, host = this.Request.Host.ToUriComponent() });
               //重新写入数据库数据。
                return Ok("已成功仿制DBName！" + Request.Query["DBName"]
                    + "已成功仿制MVCCategory！" + Request.Query["MVCCategoryNum"] 
                    + "已成功仿制CurriculumNum！" + Request.Query["CurriculumNum"] 
                    + "已成功仿制SelectedCurriculumNum！" + Request.Query["SelectedCurriculumNum"] 
                    + "已成功仿制OrganizationNum！" + Request.Query["OrganizationNum"] 
                    + "已成功仿制LearnerNum！" + Request.Query["LearnerNum"] 
                    + "已成功仿制EducatorNum！" + Request.Query["EducatorNum"]
                    + "已成功仿制HomeworkAndTestNum！" + Request.Query["HomeworkAndTestNum"]
                    + "已成功仿制SelectedHomeworkAndTestNum！" + Request.Query["SelectedHomeworkAndTestNum"]);
            }
            else { return Ok("这是服务器版（或者是本机版发布为了服务器版的方式运行），不允许直接上传！请在本机版制作好后（本机版无需登录）,连接服务器版（课程资源管理员的账号登录连接服务器版），上传本机版中的资源到服务器版!" + this.Request.Host.ToUriComponent()); }

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