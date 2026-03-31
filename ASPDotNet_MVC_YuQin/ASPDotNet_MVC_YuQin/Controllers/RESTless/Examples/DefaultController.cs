using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{

    public class DefaultController : Controller//:有点像“像”的单重继承又要功能强大，
    {
        // GET: Default
        public Int32 ID { set; get; }//3621.....
        public Int32 ID2 { set; get; }//3621.....
        public ActionResult Index()
        {
            String myString = "public";
            ID = 321;
            ID2 = 1;
            return this.Content(myString+(ID+ID2).ToString());
            // return Content("Hello world");
        }

        // GET: Default/Details/5

    }
}