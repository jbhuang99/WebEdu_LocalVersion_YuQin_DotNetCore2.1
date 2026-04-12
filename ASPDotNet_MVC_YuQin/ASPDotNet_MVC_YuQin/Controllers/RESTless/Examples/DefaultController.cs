using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{

    public class DefaultController : Controller//:有点像“像”的单重继承又要功能强大，
    {
        public ActionResult Index()
        {
            //int b = (int)'人'; // 显式转为字节（安全，因 'A' ∈ 0–127）
            // string binary = Convert.ToString(b, 2).PadLeft(8, '0'); // → "01000001"
            String a = "我上课的班级";
            // 生成一个语句基于"我上课的班级"的"的"截成两段，并且返回浏览器端             

            return Content("我上课的班级"); // 返回字符串 "01000001"
            //return this.Content('A'.ToString());
            // return Content("Hello world");
        }

        // GET: Default
        //声明定义一个属性类型，名称教ID2。可以赋值整数的实例

        //声明定义一个属性类型，名称教ID
        public Int32 ID { set; get; }//3621.....
        //声明定义一个方法类型，名称教ID2


        // GET: Default/Details/5
        public String ID2 { set; get; }//3621.....

    }
}