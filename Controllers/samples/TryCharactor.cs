using System;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EDSS.Controllers
{
    public class TryCharactorController : Controller
    {
      
        public ContentResult Index()
        {


            char character = 'A'; // 需要转换的字符
            string binaryString = Convert.ToString(character, 2);
            return this.Content("The ASCII code of A: " + binaryString);
             
        }   
    }
}
