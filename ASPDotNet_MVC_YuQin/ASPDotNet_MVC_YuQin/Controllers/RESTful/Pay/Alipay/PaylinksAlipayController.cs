using Azure;
using Essensoft.Paylinks.Alipay.Client;
using Essensoft.Paylinks.Alipay.Payments.Model;
using Essensoft.Paylinks.Alipay.Payments.Request;
using Essensoft.Paylinks.Alipay.Payments.Response;
using Essensoft.Paylinks.Razor.Pages.Samples.Web;
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

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Alipay
{
    public class PaylinksAlipayController : ControllerBase
    {
        private readonly IAlipayClient _alipayClient;
        private readonly PaylinksOptions _paylinksOptions;

        public PaylinksAlipayController(IAlipayClient alipayClient, IOptions<PaylinksOptions> paylinksOptions)
        {
            _alipayClient = alipayClient;
            _paylinksOptions = paylinksOptions.Value;
        }

        public async Task<String> Index()
        {
            //创建支付订单,构建请求模型，并发送支付请求
            AlipayTradePreCreateBodyModel alipayTradePreCreateBodyModel = new AlipayTradePreCreateBodyModel
            {
                OutTradeNo = DateTimeOffset.Now.ToString("yyyyMMddHHmmssfff"),
                TotalAmount = "0.01",
                Subject = "扫码支付测试",
                NotifyUrl = "https://www.domain.com/Alipay/Payments/Notify/TradeResult"
            };

            AlipayTradePreCreateRequest alipayTradePreCreateRequest = new AlipayTradePreCreateRequest();
            alipayTradePreCreateRequest.SetBodyModel(alipayTradePreCreateBodyModel);
            ///**
            AlipayTradePreCreateResponse response = await _alipayClient.ExecuteAsync(alipayTradePreCreateRequest, _paylinksOptions.Alipay, "");
            if (response.IsSuccessful)
            {
                // 获取二维码链接或支付信息
                String qrCode = response.QrCode;//该流程完成统一订单创建并获取支付凭证，可用于前端展示二维码或跳转支付。
                return qrCode;
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
