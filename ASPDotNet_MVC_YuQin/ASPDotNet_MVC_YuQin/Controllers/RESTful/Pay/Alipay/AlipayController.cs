using AlipayIntegrationDemo.Options;
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
///**
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using AlipayDemo.Models;
//using global::AlipayDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/**
四、核心 Controller（含 APP/Web/扫码三合一）
💡 设计亮点：

使用 IAliPayClient 接口注入，便于单元测试 & Mock
所有方法均为 async，适配 .NET 8 异步最佳实践
关键参数校验（空值、金额格式）前置防御
返回 IActionResult 统一处理，支持 JSON/HTML/Redirect
**/
namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Alipay
{
    [Authorize()]
    public class AlipayController : Controller
    {
        private readonly IAopClient _client;
        private readonly IOptions<AlipayOptions> _options;

        public AlipayController(IAopClient client, IOptions<AlipayOptions> options)
        {
            _client = client;
            _options = options;
        }

        // 🌐 网页支付（PC 端跳转收银台）
        [HttpPost]
        public async Task<IActionResult> PagePay([FromBody] PayRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.OutTradeNo) ||
                string.IsNullOrWhiteSpace(req.Subject) ||
                !decimal.TryParse(req.TotalAmount, out _))
                return BadRequest("参数错误");

            try
            {
                var request = new AlipayTradePagePayRequest();
               // request.SetReturnUrl("https://yourdomain.com/return");
              //  request.SetNotifyUrl("https://yourdomain.com/notify");
                request.BizContent = @"{
""out_trade_no"": ""20250218001"",
""product_code"": ""FAST_INSTANT_TRADE_PAY"",
""total_amount"": 0.01,
""subject"": ""测试商品""
}";
                /**
                AlipayTradePagePayResponse response = client.pageExecute(request);
                var result = await _client
                    .Payment
                    .Page()
                    .CreateAsync(new PagePayRequest
                    {
                        OutTradeNo = req.OutTradeNo,
                        Subject = req.Subject,
                        TotalAmount = req.TotalAmount,
                        Body = req.Body ?? "",
                        ProductCode = "FAST_INSTANT_TRADE_PAY",
                        TimeoutExpress = "30m",
                        ReturnUrl = _options.Value.ReturnUrl,
                        NotifyUrl = _options.Value.NotifyUrl
                    });
                **/
                // ✅ 直接返回支付宝生成的 HTML 表单（浏览器自动 submit）
                AlipayTradePagePayResponse response = _client.pageExecute(request);
                return Content(response.Body, "text/html");
            }
            catch (Exception ex)
            {
                // 记录日志（建议接入 Serilog）
                Console.WriteLine($"PagePay error: {ex.Message}");
                return StatusCode(500, "支付请求失败");
            }
        }

        // 📱 APP 支付（返回 orderString 供客户端唤起）
        ///**
        [Authorize()]
        [HttpPost]
        public async Task<IActionResult> AppPay([FromBody] PayRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.OutTradeNo) ||
                string.IsNullOrWhiteSpace(req.Subject) ||
                !decimal.TryParse(req.TotalAmount, out _))
                return BadRequest("参数错误");

            try
            {
                var request = new AlipayTradePagePayRequest();
              //  request.SetReturnUrl("https://yourdomain.com/return");
              //  request.SetNotifyUrl("https://yourdomain.com/notify");
                request.BizContent = @"{
""out_trade_no"": ""20250218002"",
""product_code"": ""QUICK_MSECURITY_PAY"",
""total_amount"": 0.01,
""subject"": ""测试商品""
}";
                /**
                try
                {
                    var result = await _client
                        .Payment
                        .App()
                        .CreateAsync(new AppPayRequest
                        {
                            OutTradeNo = req.OutTradeNo,
                            Subject = req.Subject,
                            TotalAmount = req.TotalAmount,
                            Body = req.Body ?? "",
                            ProductCode = "QUICK_MSECURITY_PAY",
                            TimeoutExpress = "30m",
                            NotifyUrl = _options.Value.NotifyUrl
                        });
                **/
                AlipayTradePagePayResponse response = _client.pageExecute(request);

                return Json(new { success = true, orderString = response.Body });
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"AppPay error: {ex.Message}");
                return StatusCode(500, "APP 支付请求失败");
            }
        }
        //**/
        // 🔍 查询订单（用于对账/补单）
        /**
        [HttpGet]
        public async Task<IActionResult> Query(string outTradeNo, string tradeNo)
        {
            try
            {
                var result = await _client.
                    .Payment
                    .Common()
                    .QueryAsync(new CommonQueryRequest
                    {
                        OutTradeNo = outTradeNo,
                        TradeNo = tradeNo
                    });
                return Json(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Query error: {ex.Message}");
                return StatusCode(500, "查询失败");
            }
        }
    }
        **/

        // ✅ 请求 DTO（强类型约束）
        public class PayRequest
        {
            public string OutTradeNo { get; set; } = null!;
            public string Subject { get; set; } = null!;
            public string TotalAmount { get; set; } = null!;
            public string Body { get; set; }
        }

        // 返回当前登录用户的 email（前端页面通过 fetch 调用）
        [HttpGet]
        public IActionResult CurrentUserEmail()
        {
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            // 尝试读取 ClaimTypes.Email，否则回退到 Identity.Name
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? User.Identity.Name;
            return Json(new { email });
        }
    }
}
//**/
/**
using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Payment.Common;
using Alipay.EasySDK.Payment.Page;
using Alipay.EasySDK.Payment.Wap;
using AlipayDemo.Models;
//using AlipayDemo.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlipayDemo.Controllers
{
    public class AlipayController : Controller
    {
        private readonly AlipayOptions _alipayOptions;
        private readonly ILogger<AlipayController> _logger;

        public AlipayController(
            IOptions<AlipayOptions> alipayOptions,
            ILogger<AlipayController> logger)
        {
            _alipayOptions = alipayOptions.Value;
            _logger = logger;
        }

        /// <summary>
        /// 支付页面
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 发起电脑网站支付
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Pay(PayRequestModel model)
        {
            try
            {
                // 生成商户订单号（确保唯一）
                string outTradeNo = string.IsNullOrWhiteSpace(model.OrderId)
                    ? $"ORD{DateTime.Now:yyyyMMddHHmmssfff}{new Random().Next(1000, 9999)}"
                    : model.OrderId;

                // 创建支付请求
                var response = await Factory.Page().PayAsync(
                    model.Subject,
                    outTradeNo,
                    model.Amount.ToString("F2"),
                    _alipayOptions.ReturnUrl
                );

                // 返回表单 HTML，自动提交到支付宝
                return Content(response.Body, "text/html");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "支付发起失败");
                return Json(new { success = false, message = $"支付发起失败：{ex.Message}" });
            }
        }

        /// <summary>
        /// 手机网站支付（WAP）
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> WapPay(PayRequestModel model)
        {
            try
            {
                string outTradeNo = string.IsNullOrWhiteSpace(model.OrderId)
                    ? $"ORD{DateTime.Now:yyyyMMddHHmmssfff}{new Random().Next(1000, 9999)}"
                    : model.OrderId;

                var response = await Factory.Wap().PayAsync(
                    model.Subject,
                    outTradeNo,
                    model.Amount.ToString("F2"),
                    model.Body ?? "商品描述",
                    _alipayOptions.ReturnUrl
                );

                return Content(response.Body, "text/html");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WAP支付失败");
                return Json(new { success = false, message = $"支付发起失败：{ex.Message}" });
            }
        }

        /// <summary>
        /// 异步通知处理（支付宝服务器回调）
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Notify()
        {
            try
            {
                // 获取 POST 参数
                var formDict = new Dictionary<string, string>();
                foreach (var key in Request.Form.Keys)
                {
                    formDict[key.ToString()] = Request.Form[key].ToString()!;
                }

                // 验签
                bool verifyResult = await Factory.Util().VerifyNotifySignAsync(formDict);

                if (verifyResult)
                {
                    string outTradeNo = formDict["out_trade_no"];
                    string tradeNo = formDict["trade_no"];
                    string tradeStatus = formDict["trade_status"];
                    string totalAmount = formDict["total_amount"];

                    _logger.LogInformation($"异步通知 - 订单号：{outTradeNo}, 状态：{tradeStatus}, 金额：{totalAmount}");

                    // 判断交易状态
                    if (tradeStatus == "TRADE_SUCCESS" || tradeStatus == "TRADE_FINISHED")
                    {
                        // TODO: 更新订单状态（注意幂等性）
                        await UpdateOrderStatusAsync(outTradeNo, tradeNo, totalAmount);
                    }

                    // 返回成功给支付宝
                    return Content("success");
                }
                else
                {
                    _logger.LogWarning("支付宝异步通知验签失败");
                    return Content("fail");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "异步通知处理异常");
                return Content("fail");
            }
        }

        /// <summary>
        /// 同步返回处理（用户支付后跳转）
        /// </summary>
        public async Task<IActionResult> Return()
        {
            try
            {
                // 获取 GET 参数
                var paramDict = new Dictionary<string, string>();
                foreach (var key in Request.Query.Keys)
                {
                    paramDict[key.ToString()] = Request.Query[key].ToString()!;
                }

                // 验签
                bool verifyResult = await Factory.Util().VerifyNotifySignAsync(paramDict);

                if (verifyResult)
                {
                    string outTradeNo = paramDict["out_trade_no"];
                    string tradeNo = paramDict["trade_no"];

                    // 建议再次查询订单状态确认
                    var queryResult = await Factory.Payment().QueryAsync(outTradeNo, "");

                    ViewBag.Message = "支付成功！";
                    ViewBag.OrderNo = outTradeNo;
                    ViewBag.TradeNo = tradeNo;
                    ViewBag.TradeStatus = queryResult.TradeStatus;

                    return View("PaySuccess");
                }
                else
                {
                    ViewBag.Message = "支付验证失败！";
                    return View("PayError");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "同步返回处理异常");
                ViewBag.Message = "支付处理异常！";
                return View("PayError");
            }
        }

        /// <summary>
        /// 查询订单状态
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> QueryOrder(string outTradeNo)
        {
            try
            {
                var response = await Factory.Payment().QueryAsync(outTradeNo, "");

                if (response.Code == "10000")
                {
                    return Json(new
                    {
                        success = true,
                        tradeStatus = response.TradeStatus,
                        tradeNo = response.TradeNo,
                        totalAmount = response.TotalAmount
                    });
                }
                else
                {
                    return Json(new { success = false, message = response.Msg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 退款
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Refund(RefundRequestModel model)
        {
            try
            {
                var response = await Factory.Payment().RefundAsync(
                    model.TradeNo,
                    model.RefundAmount.ToString("F2"),
                    model.RefundReason ?? "退款",
                    model.OutRequestNo ?? DateTime.Now.ToString("yyyyMMddHHmmssfff")
                );

                if (response.Code == "10000")
                {
                    return Json(new { success = true, refundNo = response.FundChange });
                }
                else
                {
                    return Json(new { success = false, message = response.Msg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 更新订单状态（示例）
        /// </summary>
        private async Task UpdateOrderStatusAsync(string outTradeNo, string tradeNo, string amount)
        {
            // TODO: 实现业务逻辑
            // 1. 检查订单是否已处理（防止重复通知）
            // 2. 更新数据库订单状态
            // 3. 记录支付日志
            // 4. 发送通知等

            await Task.CompletedTask; // 占位
        }
    }
}
**/
/**
using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Payment.Common;
using Alipay.EasySDK.Payment.Page;
//using Alipay.EasySDK.Payment.Refund;
using Alipay.EasySDK.Payment.Wap;
//using AlipayDemo.Models;
using AlipayIntegrationDemo.Models;
using AlipayIntegrationDemo.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlipayIntegrationDemo.Controllers;

public class AlipayController : Controller
{
    private readonly AlipayOptions _options;
    private readonly ILogger<AlipayController> _logger;

    public AlipayController(IOptionsMonitor<AlipayOptions> optionsMonitor, ILogger<AlipayController> logger)
    {
        _options = optionsMonitor.CurrentValue;
        _logger = logger;
    }

    public IActionResult Index()
    {
        // 示例数据
        var model = new PaymentRequestModel
        {
            OutTradeNo = $"ORDER_{DateTime.Now:yyyyMMddHHmmss}",
            Subject = "测试商品",
            TotalAmount = 0.01m // 沙箱测试用0.01元
        };
        return View(model);
    }

    /// <summary>
    /// 发起电脑网站支付
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(PaymentRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            // 调用支付宝API
            var response = await Factory.Page().PayAsync(
                model.Subject,
                model.OutTradeNo,
                model.TotalAmount.ToString("F2"), // 保留两位小数
                _options.ReturnUrl // 可选，同步返回地址
            );

            // 返回表单HTML，浏览器会自动提交到支付宝
            return Content(response.Body, "text/html");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "发起支付失败，订单号：{OutTradeNo}", model.OutTradeNo);
            TempData["ErrorMessage"] = $"发起支付失败：{ex.Message}";
            return RedirectToAction("Index");
        }
    }

    /// <summary>
    /// 发起手机网站支付
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> WapPay(PaymentRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            var response = await Factory.Wap().PayAsync(
                model.Subject,
                model.OutTradeNo,
                model.TotalAmount.ToString("F2"),
                model.Body,
                _options.ReturnUrl // 同步返回地址
            );

            return Content(response.Body, "text/html");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "发起WAP支付失败，订单号：{OutTradeNo}", model.OutTradeNo);
            TempData["ErrorMessage"] = $"发起WAP支付失败：{ex.Message}";
            return RedirectToAction("Index");
        }
    }

    /// <summary>
    /// 支付宝异步通知接收接口
    /// </summary>
    [HttpPost]
    [IgnoreAntiforgeryToken] // 异步通知不需要CSRF Token
    [ApiExplorerSettings(IgnoreApi = true)] // 不在Swagger中显示
    public async Task<IActionResult> Notify()
    {
        try
        {
            _logger.LogInformation("收到支付宝异步通知");

            // 将请求Form转换为字典
            var notifyParams = new Dictionary<string, string>();
            foreach (string key in Request.Form.Keys)
            {
                notifyParams[key] = Request.Form[key];
            }

            // 验签
            bool verifyResult = await Factory.Util().VerifyNotifySignAsync(notifyParams);

            if (!verifyResult)
            {
                _logger.LogWarning("支付宝异步通知验签失败");
                return Content("failure"); // 必须返回 failure
            }

            // 获取关键参数
            string outTradeNo = notifyParams["out_trade_no"]!;
            string tradeNo = notifyParams["trade_no"]!;
            string tradeStatus = notifyParams["trade_status"]!;
            string totalAmount = notifyParams["total_amount"]!;

            _logger.LogInformation("验签成功 - 订单号：{OutTradeNo}, 交易号：{TradeNo}, 状态：{TradeStatus}",
                outTradeNo, tradeNo, tradeStatus);

            // 根据交易状态处理业务逻辑
            switch (tradeStatus)
            {
                case "TRADE_SUCCESS":
                case "TRADE_FINISHED":
                    // 支付成功，更新订单状态
                    // 注意：这里需要实现具体的订单更新逻辑，并做好幂等性处理
                    // 例如：await _orderService.MarkAsPaidAsync(outTradeNo, tradeNo, decimal.Parse(totalAmount));
                    _logger.LogInformation("订单支付成功，订单号：{OutTradeNo}", outTradeNo);
                    break;
                case "TRADE_CLOSED":
                    // 交易关闭
                    // await _orderService.CloseOrderAsync(outTradeNo);
                    _logger.LogInformation("订单已关闭，订单号：{OutTradeNo}", outTradeNo);
                    break;
                default:
                    _logger.LogInformation("收到其他状态通知，订单号：{OutTradeNo}, 状态：{TradeStatus}", outTradeNo, tradeStatus);
                    break;
            }

            // 必须返回 success，否则支付宝会持续发送通知
            return Content("success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理支付宝异步通知时发生错误");
            // 出错时也返回 success，避免支付宝重复通知
            // 或者根据实际情况返回 failure
            return Content("success");
        }
    }

    /// <summary>
    /// 支付宝同步返回处理接口
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Return()
    {
        try
        {
            // 将请求Query转换为字典
            var returnParams = new Dictionary<string, string>();
            foreach (string key in Request.Query.Keys)
            {
                returnParams[key] = Request.Query[key];
            }

            // 验签
            bool verifyResult = await Factory.Util().VerifyNotifySignAsync(returnParams);

            if (!verifyResult)
            {
                ViewBag.Message = "验证签名失败，请检查支付结果！";
                return View("Result");
            }

            string outTradeNo = returnParams["out_trade_no"]!;
            string tradeNo = returnParams["trade_no"]!;
            string tradeStatus = returnParams["trade_status"]!;

            // 同步返回的数据仅供参考，最终状态应以异步通知为准
            // 再次查询订单状态是最稳妥的做法
            var queryResponse = await Factory.Payment().QueryAsync(outTradeNo, tradeNo);
            if (queryResponse.Code == "10000") // 成功
            {
                ViewBag.Message = "支付成功！";
                ViewBag.OutTradeNo = outTradeNo;
                ViewBag.TradeNo = tradeNo;
                ViewBag.TradeStatus = queryResponse.TradeStatus;
                ViewBag.TotalAmount = queryResponse.TotalAmount;
            }
            else
            {
                ViewBag.Message = $"查询订单状态失败：{queryResponse.Msg}";
            }

            return View("Result");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理支付宝同步返回时发生错误");
            ViewBag.Message = $"处理返回结果时出错：{ex.Message}";
            return View("Result");
        }
    }

    /// <summary>
    /// 查询订单
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] QueryRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await Factory.Payment().QueryAsync(model.OutTradeNo, "");

            if (response.Code == "10000") // API调用成功
            {
                return Json(new
                {
                    Success = true,
                    Data = new
                    {
                        TradeNo = response.TradeNo,
                        TradeStatus = response.TradeStatus,
                        TotalAmount = response.TotalAmount,
                        BuyerLogonId = response.BuyerLogonId
                    }
                });
            }
            else
            {
                return Json(new { Success = false, Message = response.Msg, SubMessage = response.SubMsg });
            }
        }
        catch (Exception ex)
        {
            return Json(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// 退款
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Refund(RefundRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var outRequestNo = model.OutRequestNo ?? Guid.NewGuid().ToString("N")[..32]; // 生成退款请求号

            var response = await Factory.Refund().Async(
                model.TradeNo,
                model.RefundAmount.ToString("F2"),
                model.RefundReason,
                outRequestNo
            );

            if (response.Code == "10000") // API调用成功
            {
                return Json(new
                {
                    Success = true,
                    Data = new
                    {
                        TradeNo = response.TradeNo,
                        RefundAmount = response.RefundAmount,
                        RefundNo = response.FundChange // 退款单号
                    }
                });
            }
            else
            {
                return Json(new { Success = false, Message = response.Msg, SubMessage = response.SubMsg });
            }
        }
        catch (Exception ex)
        {
            return Json(new { Success = false, Message = ex.Message });
        }
    }
}
**/