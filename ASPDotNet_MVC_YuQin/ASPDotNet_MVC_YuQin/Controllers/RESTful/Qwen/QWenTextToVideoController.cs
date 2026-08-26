using AliImageGen.Models;
using AliImageGen.Services;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Qwen
{
    public class DashScopeOptions
    {
        public string ApiKey { get; set; } = "";
        public string BaseUrl { get; set; } = "https://dashscope.aliyuncs.com";
        public string Text2VideoModel { get; set; } = "wan2.6-t2v";
        public string Image2VideoModel { get; set; } = "wan2.6-i2v";
    }
public class SubmitTaskResponse
    {
        [JsonPropertyName("request_id")] public string RequestId { get; set; }
        [JsonPropertyName("output")] public SubmitOutput Output { get; set; }
        [JsonPropertyName("code")] public string Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
    }
    public class SubmitOutput
    {
        [JsonPropertyName("task_id")] public string TaskId { get; set; }
        [JsonPropertyName("task_status")] public string TaskStatus { get; set; }
    }

    public class TaskQueryResponse
    {
        [JsonPropertyName("request_id")] public string RequestId { get; set; }
        [JsonPropertyName("output")] public TaskOutput Output { get; set; }
    }
    public class TaskOutput
    {
        [JsonPropertyName("task_id")] public string TaskId { get; set; }
        [JsonPropertyName("task_status")] public string TaskStatus { get; set; }
        [JsonPropertyName("video_url")] public string VideoUrl { get; set; }
        [JsonPropertyName("code")] public string Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
    }

public class WanVideoService
    {
        private readonly HttpClient _http;
        private readonly DashScopeOptions _opt;

        public WanVideoService(HttpClient http, IOptions<DashScopeOptions> opt)
        {
            _http = http;
            _opt = opt.Value;
        }

        /// <summary>1. 提交文生视频任务，返回 taskId</summary>
        public Task<string> SubmitTextToVideoAsync(
            string prompt, string resolution = "1080P", int duration = 5,
            bool promptExtend = true, CancellationToken ct = default)
        {
            var body = new
            {
                model = _opt.Text2VideoModel,
                input = new { prompt },
                parameters = new
                {
                    resolution,          // 480P / 720P / 1080P
                    duration,            // 秒数，按模型支持范围
                    prompt_extend = promptExtend, // 智能改写提示词
                    watermark = false
                }
            };
            return SubmitAsync(body, ct);
        }

        /// <summary>2. 提交图生视频任务（首帧），返回 taskId</summary>
        public Task<string> SubmitImageToVideoAsync(
            string prompt, string imgUrl, string resolution = "1080P",
            int duration = 5, CancellationToken ct = default)
        {
            var body = new
            {
                model = _opt.Image2VideoModel,
                input = new { prompt, img_url = imgUrl },
                parameters = new { resolution, duration }
            };
            return SubmitAsync(body, ct);
        }

        private async Task<string> SubmitAsync(object body, CancellationToken ct)
        {
            using var req = new HttpRequestMessage(HttpMethod.Post,
                $"{_opt.BaseUrl}/api/v1/services/aigc/video-generation/video-synthesis");
            req.Headers.Add("Authorization", $"Bearer {_opt.ApiKey}");
            req.Headers.Add("X-DashScope-Async", "enable");   // 必须！否则报同步调用错误
            req.Content = JsonContent.Create(body);

            var resp = await _http.SendAsync(req, ct);
            var json = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"提交任务失败 [{(int)resp.StatusCode}]: {json}");

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitTaskResponse>(json);
            if (string.IsNullOrEmpty(result?.Output?.TaskId))
                throw new InvalidOperationException($"未返回 task_id: {json}");
            return result.Output.TaskId;
        }

        /// <summary>3. 查询任务状态</summary>
        public async Task<TaskOutput> QueryTaskAsync(string taskId, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{_opt.BaseUrl}/api/v1/tasks/{taskId}");
            req.Headers.Add("Authorization", $"Bearer {_opt.ApiKey}");

            var resp = await _http.SendAsync(req, ct);
            var json = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"查询任务失败 [{(int)resp.StatusCode}]: {json}");

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskQueryResponse>(json);
            return result!.Output;
        }

        /// <summary>4. 阻塞轮询直到完成（服务端同步场景用）</summary>
        public async Task<TaskOutput> WaitForCompletionAsync(
            string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
        {
            var deadline = DateTime.UtcNow + (timeout ?? TimeSpan.FromMinutes(10));
            while (DateTime.UtcNow < deadline)
            {
                ct.ThrowIfCancellationRequested();
                var output = await QueryTaskAsync(taskId, ct);
                if (output.TaskStatus is "SUCCEEDED" or "FAILED" or "CANCELED" or "UNKNOWN")
                    return output;
                await Task.Delay(TimeSpan.FromSeconds(10), ct); // 生成通常要几十秒到几分钟
            }
            throw new TimeoutException("视频生成等待超时");
        }

        /// <summary>5. 下载视频（URL 24小时过期）</summary>
        public Task<byte[]> DownloadVideoAsync(string videoUrl, CancellationToken ct = default)
            => _http.GetByteArrayAsync(videoUrl, ct);
    }

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class QWenTextToVideoController : ControllerBase
    {

        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public String Get()
        {
            return "Try HomeAPIAPIAPI";
        }
    }
    /**

    [ApiController]
    [Route("api/[controller]")]
    
    public class QWenTextToVideoController : ControllerBase
    {
        private readonly WanVideoService _svc;
        public QWenTextToVideoController(WanVideoService svc) => _svc = svc;

        public record GenRequest(string Prompt, string Resolution = "1080P", int Duration = 5);

        // ① 提交任务
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] GenRequest r, CancellationToken ct)
        {
            var taskId = await _svc.SubmitTextToVideoAsync(r.Prompt, r.Resolution, r.Duration, ct: ct);
            return Ok(new { taskId });
        }

        // ② 查询状态（前端每 10 秒轮询一次）
        [HttpGet("status/{taskId}")]
        public async Task<IActionResult> Status(string taskId, CancellationToken ct)
        {
            var o = await _svc.QueryTaskAsync(taskId, ct);
            return Ok(new
            {
                status = o.TaskStatus,
                videoUrl = o.TaskStatus == "SUCCEEDED" ? o.VideoUrl : null,
                error = o.Message
            });
        }

        // ③ 一站式同步接口（内部等待，注意网关/反代的超时设置）
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenRequest r, CancellationToken ct)
        {
            var taskId = await _svc.SubmitTextToVideoAsync(r.Prompt, r.Resolution, r.Duration, ct: ct);
            var o = await _svc.WaitForCompletionAsync(taskId, TimeSpan.FromMinutes(10), ct);

            if (o.TaskStatus != "SUCCEEDED")
                return StatusCode(500, new { status = o.TaskStatus, o.Message });

            return Ok(new { videoUrl = o.VideoUrl });
        }
    }
    **/
}