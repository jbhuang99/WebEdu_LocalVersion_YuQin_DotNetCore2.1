using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{
    public class Teacher : Object
    {
        public String ID = "";
        public String Name = "";
        public Double Height = 1.7;
        public Boolean IsMale = true;
        public Double Weight = 100;
    }

    public class TryObjectController : Controller
    {
        // GET: TryObjectController
        public ActionResult Index()
        {
            //return this.Content("黄景碧");
            Object TryObject = new Object();
            Object TryObject2 = new Object();
            Teacher HJB = new Teacher();
            HJB.ID = "0001";
            HJB.Name = "黄景碧";            
            Teacher ChenLi = new Teacher();
            ChenLi.ID = "0002";
            ChenLi.Name = "温善毅";
            // return this.Content(TryObject.ToString()+"是否就是1:"+ TryObject.Equals(1).ToString());
            return this.Content(HJB.ID + HJB.Name+ HJB.IsMale+ ChenLi.ID+ ChenLi.Name);
        }
    }
}
