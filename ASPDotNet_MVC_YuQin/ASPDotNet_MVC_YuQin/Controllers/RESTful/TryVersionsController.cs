using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPPMVC_WebAPIMC.Controllers.RESTful
{
    [ApiController]
    //[Route("[controller]")]
    [AllowAnonymous]
    [ApiVersion("1.0", Deprecated = true)] // Deprecated 表示废弃了, 会在返回的 header 表示, 虽然是废弃了, 但是依然会跑和 response 的哦 (自行处理)
    [ApiVersion("2.0")]
    [Route("[controller]/v{version:apiVersion}/")] // 声明 version in path
                                                     
    public class TryVersionsController : ControllerBase
    {
        //[HttpGet(Name = "GetWeatherForecast")]
        /**
        [HttpGet,ApiVersionNeutral] //[ApiVersion("1.0")] //不需要 API versioning 的就加上[ApiVersionNeutral]
        public String Get() // 名称是空
        {
            return "默认版本";
        }
        **/
        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet,MapToApiVersion("1.0")]
        public String Get_v1() // 名称是 v1
        {
            return "V1.0";
        }
    
        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet,MapToApiVersion("2.0")]
        public String Get_v2() // 名称是 v2
        {
            return "V2.0";
        }
    }
}
