using Azure;
using Essensoft.Paylinks.Razor.Pages.Samples.Web;
using Essensoft.Paylinks.WeChatPay.Client;
using Essensoft.Paylinks.WeChatPay.Payments.Domain;
using Essensoft.Paylinks.WeChatPay.Payments.Model;
using Essensoft.Paylinks.WeChatPay.Payments.Request;
using Essensoft.Paylinks.WeChatPay.Payments.Response;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
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
//using QRCoder;

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.WeChatPay
{
    public class PaylinksWeChatPayController : ControllerBase
    {

        public PaylinksWeChatPayController(IWeChatPayClient weChatPayClient, IOptions<PaylinksOptions> paylinksOptions)
        {
            _weChatPayClient = weChatPayClient;
            _paylinksOptions= paylinksOptions.Value;
        }
        private readonly IWeChatPayClient _weChatPayClient;
        private readonly PaylinksOptions _paylinksOptions;
        public async Task<String> Index()
        {
            //创建支付订单,构建请求模型，并发送支付请求
            WeChatPayTransactionsNativeBodyModel weChatPayTransactionsNativeBodyModel = new WeChatPayTransactionsNativeBodyModel
            {
                AppId = "123456789",
                MchId = "123456789",
                Description = "Native下单测试",
                OutTradeNo = DateTimeOffset.Now.ToString("yyyyMMddHHmmssfff"),
                NotifyUrl = "https://www.domain.com/WeChatPay/Payments/Notify/TransactionSuccess",
                Amount = new CommReqAmountInfo { Total = 1 }
            };

            WeChatPayTransactionsNativeRequest weChatPayTransactionsNativeRequest = new WeChatPayTransactionsNativeRequest();
            weChatPayTransactionsNativeRequest.SetBodyModel(weChatPayTransactionsNativeBodyModel);
            ///**
            WeChatPayTransactionsNativeResponse response = await _weChatPayClient.ExecuteAsync(weChatPayTransactionsNativeRequest, _paylinksOptions.WeChatPay);
            if (response.IsSuccessful)
            {
                // 获取二维码链接或支付信息.获取 CodeUrl 后可用于生成二维码供用户扫二维码支付。
                String codeUrl = response.CodeUrl;
                return codeUrl;
            }
            else
            {
                // 处理错误
                return response.ErrorMessage;
            }
            //**/


        }

       // [HttpPost]
        //支付成功回调与通知处理.第三方支付平台在用户完成支付后会发送回调通知，开发者需要实现相应路由来处理这些通知信息：
        /**
        public async Task<IActionResult> AlipayNotify()
        {
            var notifyRequest = Request.Form.ToAlipayNotifyRequest();
            var result = await _alipayClient.ExecuteAsync(notifyRequest);
            if (result.IsSuccess)
            {
                // 处理订单状态更新
            }
            return Content("success");
        }
       **/
    } 
}
