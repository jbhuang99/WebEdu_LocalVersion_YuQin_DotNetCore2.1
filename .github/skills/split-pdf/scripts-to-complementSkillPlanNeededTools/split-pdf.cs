#!/usr/bin/env dotnet
#:package PdfSharpCore@1.3.65
#:package Spectre.Console@0.49.1
#:property PublishAot=true

// This is a C# script that can be run with `dotnet run split-pdf.cs <PDF文件> <输出目录> [页面范围]`。下面是上述预处理指令的解释（参见：https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps）（dotnet 也在 .NET 10 中引入了一个 dnx 的命令，我们可以通过 dnx 来执行一个 dotnet tool 而无需将其安装为 global tool 或者 local tool，这样可以执行 dotnet tool 同时不在 global tool 或者 local tool 中引入额外的工具。我们可以通过 dnx --help 来查看使用说明，从使用帮助可以看出 dnx 等同于 dotnet dnx，而它又是 dotnet tool exec/dotnet tool execute 的一个别名。dnx 的出现一方面是为了简化 dotnet tool execuete 的使用，同时方便基于标准输入输出的 MCP tool，这样比较方便地运行 MCP Server。同时在 CI 时使用这一命令可以避免影响整体环境。The dnx script provides a streamlined way to execute tools. It forwards all arguments to the dotnet CLI for processing, making tool usage as simple as possible:https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk?WT.mc_id=DT-MVP-5004222#the-new-dnx-tool-execution-script、A .NET tool is a special NuGet package that contains a console application：https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools）：
//运行环境声明：linux操作系统可用#!/usr/bin/env dotnet
// 依赖他创方的PDF处理的Nuget包
// 依赖他创方的控制台美化的Nuget包
// 发布为AOT编译，提升性能

using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using Spectre.Console;
using System;
using System.IO;

// ==================== 参数校验 ====================
if (args.Length < 2)
{
    AnsiConsole.MarkupLine("[red]错误: 参数不足[/]");
    AnsiConsole.MarkupLine("[yellow]用法: dotnet split-pdf.cs <PDF文件> <输出目录> [页面范围][/]");
    return 1;
}

var pdfPath = args[0];
var outputDir = args[1];
var pageRange = args.Length >= 3 ? args[2] : null;

// 验证文件
if (!File.Exists(pdfPath))
{
    AnsiConsole.MarkupLine($"[red]错误: 文件不存在: {pdfPath}[/]");
    return 1;
}

// 创建输出目录
Directory.CreateDirectory(outputDir);

// ==================== 拆分 PDF ====================
try
{
    using var inputDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import);
    var totalPages = inputDocument.PageCount;
    
    // 解析页面范围
    int startPage = 1, endPage = totalPages;
    if (!string.IsNullOrEmpty(pageRange))
    {
        var parts = pageRange.Split('-');
        if (parts.Length == 2 && 
            int.TryParse(parts[0], out startPage) && 
            int.TryParse(parts[1], out endPage))
        {
            startPage = Math.Max(1, Math.Min(startPage, totalPages));
            endPage = Math.Max(startPage, Math.Min(endPage, totalPages));
        }
    }

    var baseName = Path.GetFileNameWithoutExtension(pdfPath);
    
    await AnsiConsole.Progress()
        .StartAsync(async ctx =>
        {
            var task = ctx.AddTask("拆分 PDF 页面", maxValue: endPage - startPage + 1);

            for (int i = startPage; i <= endPage; i++)
            {
                using var outputDocument = new PdfDocument();
                outputDocument.AddPage(inputDocument.Pages[i - 1]);
                
                var outputPath = Path.Combine(outputDir, $"{baseName}_page_{i:D3}.pdf");
                outputDocument.Save(outputPath);
                
                task.Increment(1);
                await Task.CompletedTask;
            }
        });

    AnsiConsole.MarkupLine($"[green]✅ 拆分完成！已生成 {endPage - startPage + 1} 个文件[/]");
    return 0;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]❌ 错误: {ex.Message}[/]");
    return 1;
}