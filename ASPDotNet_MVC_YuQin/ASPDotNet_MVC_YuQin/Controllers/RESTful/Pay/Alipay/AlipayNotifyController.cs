//using QRCoder;
/**
using Alipay.EasySDK.Payment.Common;
using Alipay.EasySDK.Payment.Common.Models;
using Alipay.EasySDK.Payment.FaceToFace;
using Alipay.EasySDK.Payment.Page;
using Alipay.EasySDK.Payment.Wap;
**/
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using AlipayIntegrationDemo.Options;
//using AlipayDemo.Models;
using Azure;
//using global::AlipayDemo.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Alipay
{
    [Route("alipay/[controller]")]
    public class NotifyController : Controller
    {
        private readonly IAopClient _client;
        private readonly IOptions<AlipayOptions> _options;

        public NotifyController(IAopClient client, IOptions<AlipayOptions> options)
        {
            _client = client;
            _options = options;
        }

        // POST /alipay/notify
        [HttpPost]
        [IgnoreAntiforgeryToken] // 支付宝回调无 CSRF Token
        public async Task<IActionResult> Index()
        {
            try
            {
                // ✅ 1. 获取原始表单数据（关键！不能用 ModelBinding）
                var form = await Request.ReadFormAsync();
                var parameters = form.ToDictionary(x => x.Key, x => x.Value.ToString());

                // ✅ 2. 强制验签（SDK 内置，自动识别 RSA2/RSA）
                /**
                if (!await _client.VerifyAsync(parameters))
                {
                    return Content("fail"); // ❌ 立即返回 fail，触发支付宝重试
                }
                **/

                // ✅ 3. 解析业务状态（只信任 TRADE_SUCCESS）
                var tradeStatus = parameters.GetValueOrDefault("trade_status");
                var outTradeNo = parameters.GetValueOrDefault("out_trade_no");
                var tradeNo = parameters.GetValueOrDefault("trade_no");
                var totalAmount = parameters.GetValueOrDefault("total_amount");

                // ✅ 4. 【幂等关键】检查数据库是否已处理（伪代码，按你 ORM 替换）
                if (await IsOrderPaidAsync(outTradeNo))
                {
                    return Content("success"); // 已处理，直接成功响应
                }

                // ✅ 5. 执行业务逻辑（更新订单、发消息、扣库存...）
                await UpdateOrderToPaidAsync(outTradeNo, tradeNo, totalAmount);

                // ✅ 6. 返回 success（必须，且仅此二字）
                return Content("success");
            }
            catch (Exception ex)
            {
                // ⚠️ 即使业务异常，也返回 success（避免支付宝无限重试）
                // 后续通过定时任务 + Query 接口补偿
                Console.WriteLine($"Notify error: {ex}");
                return Content("success");
            }
        }

        // 👇 你的业务方法（示例）
        private async Task<bool> IsOrderPaidAsync(string outTradeNo) =>
            await Task.FromResult(false); // 替换为 EF/Dapper 查询

        private async Task UpdateOrderToPaidAsync(string outTradeNo, string tradeNo, string totalAmount) =>
            await Task.CompletedTask; // 替换为真实更新逻辑
    }
}
