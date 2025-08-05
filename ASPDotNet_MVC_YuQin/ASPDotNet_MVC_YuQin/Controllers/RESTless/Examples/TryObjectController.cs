
//using Microsoft.AspNetCore.Http;
using EDSSTemp.Controllers;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Word;
using System;
using System.Text;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{

    public class TryObject1Controller : Controller
    {
        // GET: TryObjectController
        // public String MypRO {set;get;} 
        public ActionResult MyCharToASCII2()
        {

            return this.Content("CopyAndRef案例" + ";");
        }
        public ActionResult CopyAndRef()
        {
            PersonNew person1 = new PersonNew { Name = "Alice" };
            PersonNew person2 = person1; // person2 和 person1 指向同一个对象

            person2.Name = "Bob";
            Int32 int1 = 1;
            Int32 int2 = 2;
            int2 = int1;
           // String string1 = new string("I AM HJB");
           // String string2 = "I AM HJB";
            return this.Content("CopyAndRef案例"+";"+ person1.Name + ";" + person2.Name + ";" + int1 + ";" + int2);
        }

        public ActionResult CharToASCII()
        {
            Char myChar = 'A';
            Int32 ascii = (Int32)myChar;
            return this.Content(Convert.ToString(ascii, 2));
        }


        public ActionResult Index()
        {
            
            //return this.Content("黄景碧");
          System.Object tryObject1 = new System.Object();
           
          System.Object tryObject2 = new Object();
            #region
            Teacher hJB = new Teacher();
            hJB.TeacherId = "0001";
            hJB.Name = "黄景";
            hJB.IsMale = false;
            Teacher wSY = new Teacher();
            wSY.TeacherId = "0002";
            wSY.Name = "温";
            Student student1 = new Student();
            student1.StudentId = "s0001";
            Student student2 = new Student();
            student1.StudentId = "s0002";
            Person person1 = new Person();
            #endregion
            
            return this.Content(tryObject2.ToString()+ ";   "+person1.ToString());
        }


        public ActionResult MyCharToASCII()
        {
            Char hjbChar = 'A'; // 示例字符
            Int32 hjbCharInt32 = (Int32)hjbChar;
            return this.Content(Convert.ToString(hjbCharInt32, 2));
        }
    }
    public class PersonNew:Object
    {
        public String Name { get; set; }
    }
    public class Person : Object
    {

        //public String PersonId = "";
        public String PersonId { get; set; }
        public String PassPort = "";
        public String Name = "";
       // public Single Height = 1.7f;
        public Boolean IsMale = true;
        public Double Weight = 100;
        private String Password = "";
        override public String ToString()
        {
            return "改写了System.Object的ToStirng方法返回的人";
        }
      


    }
    public class Teacher : Person
    {
        public String TeacherId = "";
        public String Ask(String a,String b)
        {
            return (a+b);
        }
        override public String ToString()
        {
            return "改写了System.Object的ToStirng方法返回的老师";
        }
    }

    public class Student


    {
        public String StudentId = "00001";
        public String Answer(String a, String b)
        {
            return (a + b);
        }
    }



}
