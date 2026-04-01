using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;


namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{

    public class DefaultController : Controller//:有点像“像”的单重继承又要功能强大，
    {
        public ActionResult Index()
        {
            Char myChar = 'A';
            String myString = "public";
            this.ID = 321;
            this.ID2 = new string("计算机124");

            ;
            return this.Content(myString + (ID + ID2).ToString());
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