//namespace AlipayDemo.Models;
namespace AlipayIntegrationDemo.Options;
public class AlipayOptions
{
    public string GatewayUrl { get; set; } = "https://openapi.alipay.com/gateway.do";
    public string AppId { get; set; } = null!;
    public string PrivateKey { get; set; } = null!;
    public string Format { get; set; } = "json"!;    
    public string Version { get; set; } = "1.0";
    public string SignType { get; set; } = "RSA2";
    public string AlipayPublicKey { get; set; } = null!;
    public string Charset { get; set; } = "utf-8";
    //public string NotifyUrl { get; set; } = null!;
    //public string ReturnUrl { get; set; } = null!;
    //public bool IsSandbox { get; set; } = false;
}