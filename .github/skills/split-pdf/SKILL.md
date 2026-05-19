---
name: split-pdf
description: Split PDF files into separate single-page documents or extract specific page ranges. Use when you need to divide a PDF into multiple files, extract particular pages, or process PDF pages individually. Works with multi-page PDF documents.
license: MIT
---

# Split PDF

将 PDF 文件拆分为多个单页文件或提取指定页面范围。

## 使用场景

- 将多页 PDF 拆分为独立的单页文件
- 提取 PDF 的特定页面范围
- 需要单独处理 PDF 各个页面时

## 使用方法

使用 `scripts/split-pdf.cs` 脚本进行 PDF 拆分：

### 拆分页面
# 拆分所有页面
dotnet run --file scripts/split-pdf.cs input.pdf output-dir/
   ```bash
     dotnet run --file scripts/split-pdf.cs d://temp/Try.pdf d://temp/
   ```
# 拆分第 1-5 页
dotnet run --file scripts/split-pdf.cs input.pdf output-dir/ 1-5
   ```bash
     dotnet run --file scripts/split-pdf.cs d://temp/Try.pdf d://temp/ 1-5
   ```
## 输出格式

拆分后的文件命名格式：`{原文件名}_page_{页码}.pdf`

## 依赖项

- PdfSharpCore 1.3.65 - PDF 操作核心库
- Spectre.Console 0.49.1 - 美化的控制台输出



**注意**：
- `name` 必须与目录名 `split-pdf` 完全一致
- `description` 包含关键词 "split", "PDF", "pages" 帮助 Agent 识别场景