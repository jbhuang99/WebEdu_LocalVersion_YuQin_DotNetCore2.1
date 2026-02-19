using System.ComponentModel.DataAnnotations;

namespace AlipayIntegrationDemo.Models;

public class PaymentRequestModel
{
    [Required(ErrorMessage = "订单号不能为空")]
    [StringLength(64, ErrorMessage = "订单号不能超过64位")]
    public string OutTradeNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "订单标题不能为空")]
    [StringLength(256, ErrorMessage = "订单标题不能超过256位")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "支付金额不能为空")]
    [Range(0.01, 9999999.99, ErrorMessage = "支付金额必须在 0.01 - 9999999.99 元之间")]
    public decimal TotalAmount { get; set; }

    public string? Body { get; set; }

    public string? ProductCode { get; set; } = "FAST_INSTANT_TRADE_PAY"; // 默认产品码
}

public class RefundRequestModel
{
    [Required]
    public string TradeNo { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "退款金额必须大于0")]
    public decimal RefundAmount { get; set; }

    public string? RefundReason { get; set; }
    public string? OutRequestNo { get; set; } // 退款请求号，用于防止重复退款
}

public class QueryRequestModel
{
    [Required]
    public string OutTradeNo { get; set; } = string.Empty;
}