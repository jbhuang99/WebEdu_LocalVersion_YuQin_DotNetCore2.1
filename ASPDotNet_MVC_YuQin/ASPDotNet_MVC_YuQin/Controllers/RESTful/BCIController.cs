using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TGAMParser;

namespace WebAPPMVC_WebAPIMC.Controllers.RESTful
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class BCIController : ControllerBase
    {
        
        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public String Get()
        {            
            TGAMDevice device = new TGAMDevice();
            device.Connect("COM4");
            TGAMData tgamData= new TGAMData();
            device.Dispose();
            return tgamData.RawData.ToString();            
           
           // return "tgamData.RawData.ToString()";
        }
    }
}
