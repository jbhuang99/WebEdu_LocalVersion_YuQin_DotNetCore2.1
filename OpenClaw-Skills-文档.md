# OpenClaw Skills 完整文档

> 生成时间：2026-03-25  
> 版本：v1.0

---

## 📚 目录

1. [Skills 概述](#skills-概述)
2. [已安装 Skills](#已安装-skills)
3. [Skills 详细文档](#skills-详细文档)
4. [如何安装新 Skills](#如何安装新-skills)
5. [如何创建自定义 Skills](#如何创建自定义-skills)

---

## Skills 概述

Skills 是 OpenClaw 的核心扩展机制，每个 Skill 都是一个独立的功能模块，提供特定的能力。

### Skills 位置

- **系统 Skills**: `~/.nvm/versions/node/v24.14.0/lib/node_modules/openclaw/skills/`
- **用户 Skills**: `~/.openclaw/skills/`

### Skills 结构

```
skill-name/
├── SKILL.md          # 技能说明文档（必需）
├── src/              # 源代码（可选）
├── assets/           # 资源文件（可选）
└── package.json      # 依赖配置（可选）
```

---

## 已安装 Skills

| 序号 | Skill 名称 | 描述 | 触发关键词 |
|------|-----------|------|-----------|
| 1 | agent-browser | 浏览器自动化 | 打开网站、填写表单、点击按钮、截图、爬虫 |
| 2 | agent-reach | 全网内容访问 | 小红书、知乎、Twitter、YouTube、B 站等 |
| 3 | copywriting | 营销文案写作 | 文案、landing page、营销内容 |
| 4 | docx | Word 文档处理 | .docx 文件创建/编辑 |
| 5 | feishu-doc | 飞书文档获取 | 飞书 Wiki、Docs、Sheets |
| 6 | finance-data | 金融数据查询 | 股票、财经新闻、经济数据 |
| 7 | find-skills | 技能搜索 | 找技能、有没有技能可以... |
| 8 | ontology | 知识图谱 | 记忆、实体管理、数据关联 |
| 9 | pdf | PDF 文件处理 | PDF 创建/编辑/OCR |
| 10 | pptx | PPT 演示文稿 | 生成 PPT、演示文稿 |
| 11 | remotion-best-practices | Remotion 视频 | React 视频制作 |
| 12 | systematic-debugging | 系统化调试 | 调试、bug 修复 |
| 13 | tailwind-design-system | Tailwind 设计系统 | 组件库、设计系统 |
| 14 | travel-planner | 旅行规划 | 旅行行程、旅游规划 |
| 15 | using-superpowers | 技能使用指南 | 如何使用技能 |
| 16 | xlsx | Excel 表格处理 | .xlsx/.csv 文件处理 |

---

## Skills 详细文档

### 1. agent-browser

**位置**: `~/.openclaw/skills/agent-browser/`

**功能**: 浏览器自动化 CLI，用于网页交互

**触发场景**:
- 打开网站
- 填写表单
- 点击按钮
- 截图
- 数据抓取
- 测试 Web 应用
- 登录网站
- 自动化浏览器任务

**示例命令**:
```bash
# 打开网页
browser open https://example.com

# 截图
browser screenshot --fullPage

# 点击元素
browser act --kind click --ref e123

# 输入文本
browser act --kind type --ref e456 --text "Hello"
```

---

### 2. agent-reach

**位置**: `~/.openclaw/skills/agent-reach/`

**功能**: 全网内容访问，支持多个平台

**支持平台**:
- 📕 小红书 (XiaoHongShu)
- 📘 知乎 (Zhihu)
- 🐦 Twitter/X
- 🤖 Reddit
- 📺 YouTube
- 🐙 GitHub
- 📹 Bilibili (B 站)
- 🎵 抖音 (Douyin)
- 💼 LinkedIn
- 💼 Boss 直聘
- 📰 RSS

**触发关键词**:
- "登录"
- "帮我配"
- "帮我添加"
- "帮我安装"
- "agent reach"
- "install channels"
- "小红书"
- "configure twitter"
- "enable reddit"

---

### 3. copywriting

**位置**: `~/.openclaw/skills/copywriting/`

**功能**: 营销文案写作和优化

**适用场景**:
- 首页文案
- 落地页 (Landing Page)
- 价格页
- 功能介绍页
- 关于我们
- 产品页
- 营销文案优化
- 标题优化
- CTA 文案

**触发词**:
- "write copy for"
- "improve this copy"
- "rewrite this page"
- "marketing copy"
- "headline help"
- "CTA copy"

---

### 4. docx

**位置**: `~/.openclaw/skills/docx/`

**功能**: Word 文档 (.docx) 处理

**支持操作**:
- 创建新文档
- 读取/编辑现有文档
- 添加目录
- 设置标题样式
- 页码
- 信头模板
- 图片插入/替换
- 查找替换
- 修订模式/批注
- 转换为 Word 文档

**触发词**:
- "Word doc"
- "word document"
- ".docx"
- "报告"
- "备忘录"
- "信函"
- "模板"

---

### 5. feishu-doc

**位置**: `~/.openclaw/skills/feishu-doc/`

**功能**: 飞书 (Lark) 内容获取

**支持类型**:
- Wiki 文档
- Docs 文档
- Sheets 表格
- Bitable 多维表格

**特性**:
- 自动解析 Wiki URL 为实体
- 转换为 Markdown 格式

---

### 6. finance-data

**位置**: `~/.openclaw/skills/finance-data/`

**功能**: 金融数据查询

**支持数据**:
- A 股/港股股价
- 财务指标 (PE, PB, ROE 等)
- 市场新闻/动态
- 中国宏观经济数据 (GDP, CPI, PMI)
- 基金数据
- 财报分析

**触发词**:
- "股价"、"股票价格"
- "市场消息"、"财经新闻"
- "GDP"、"CPI"、"PMI"
- "财报"、"财务数据"
- "市场分析"、"行情"

**数据源**:
- MCP (A 股)
- akshare (A/HK/基金/宏观)

**注意**: 数据延迟 15 分钟，不构成投资建议

---

### 7. find-skills

**位置**: `~/.openclaw/skills/find-skills/`

**功能**: 技能搜索和发现

**使用场景**:
- "如何做到 X"
- "找一个技能可以..."
- "有没有技能可以..."
- 扩展能力需求

**搜索渠道**:
1. 本地已安装技能
2. ClawHub 社区 (https://clawhub.ai/)

---

### 8. ontology

**位置**: `~/.openclaw/skills/ontology/`

**功能**: 类型化知识图谱

**核心能力**:
- 创建/查询实体 (Person, Project, Task, Event, Document)
- 链接相关对象
- 约束执行
- 多步骤动作规划（作为图转换）
- 跨技能数据共享

**触发词**:
- "记住"
- "我知道关于..."
- "链接 X 到 Y"
- "显示依赖关系"
- 实体 CRUD
- 跨技能数据访问

---

### 9. pdf

**位置**: `~/.openclaw/skills/pdf/`

**功能**: PDF 文件处理

**支持操作**:
- 创建 PDF
- 读取/提取文本/表格
- 合并多个 PDF
- 分割 PDF
- 旋转页面
- 添加水印
- 填充表单
- 加密/解密
- 提取图片
- OCR 扫描 PDF

**触发词**:
- 任何提到".pdf"的请求
- 生成 PDF 文件

---

### 10. pptx (qoderwork-ppt)

**位置**: `~/.openclaw/skills/pptx/`

**功能**: 生成 QoderWork 风格演示文稿

**特性**:
- 自动匹配 14 种模板
- 根据主题选择模板
- 输出可编辑的.pptx 文件

**触发词**:
- "生成 PPT"
- "演示文稿"
- "presentation"

---

### 11. remotion-best-practices

**位置**: `~/.openclaw/skills/remotion-best-practices/`

**功能**: Remotion 视频制作最佳实践

**适用**: React 视频创建项目

---

### 12. systematic-debugging

**位置**: `~/.openclaw/skills/systematic-debugging/`

**功能**: 系统化调试方法

**使用场景**:
- 遇到任何 bug
- 测试失败
- 意外行为
- 在提出修复方案之前

---

### 13. tailwind-design-system

**位置**: `~/.openclaw/skills/tailwind-design-system/`

**功能**: 使用 Tailwind CSS 构建设计系统

**支持**:
- 组件库创建
- 设计系统实现
- UI 标准化
- 设计令牌 (Design Tokens)
- 响应式模式

---

### 14. travel-planner

**位置**: `~/.openclaw/skills/travel-planner/`

**功能**: 旅行行程规划

**触发词**:
- "我想去 X 旅行，帮我规划行程"
- "旅行计划"
- "旅游攻略"

---

### 15. using-superpowers

**位置**: `~/.openclaw/skills/using-superpowers/`

**功能**: 技能使用指南

**触发**: 任何对话开始时
**要求**: 在回复前必须调用 Skill 工具

---

### 16. xlsx

**位置**: `~/.openclaw/skills/xlsx/`

**功能**: Excel 表格处理

**支持操作**:
- 打开/读取/编辑 .xlsx, .xlsm, .csv, .tsv
- 添加列
- 计算公式
- 格式化
- 图表
- 数据清洗
- 格式转换

**触发词**:
- 任何提到 spreadsheet/xlsx/csv 的请求
- 创建新表格
- 清洗表格数据

**交付物**: 必须是电子表格文件

---

## 如何安装新 Skills

### 方法 1: 使用 find-skills

```bash
# 搜索技能
find-skills search "技能名称或功能"
```

### 方法 2: 使用 ClawHub CLI

```bash
# 搜索技能
clawhub search <关键词>

# 安装技能
clawhub install <技能名>

# 更新技能
clawhub update <技能名>

# 发布技能
clawhub publish <技能文件夹>
```

### 方法 3: 手动安装

```bash
# 克隆或下载技能到用户目录
git clone <repo> ~/.openclaw/skills/<skill-name>
```

---

## 如何创建自定义 Skills

使用 `skill-creator` 技能来创建新技能：

1. **触发 skill-creator**: 提到"创建技能"或"create skill"
2. **遵循 SKILL.md 规范**: 确保技能结构正确
3. **包含必要文件**:
   - SKILL.md (必需)
   - 源代码
   - 资源文件

---

## 技能优先级规则

执行任务时的优先级：

| 优先级 | 方式 | 说明 |
|--------|------|------|
| 1️⃣ | API 直接调用 | 最高效 |
| 2️⃣ | 已安装的 Skill | 检查 available_skills |
| 3️⃣ | find-skills 搜索 | 社区解决方案 |
| 4️⃣ | 浏览器自动化 | 最后手段 |

---

## 注意事项

### 安全提醒

1. **不要以明文存储密码** - 使用密码管理器
2. **谨慎授权** - 只给必要的权限
3. **定期检查** - 审查已安装的技能

### 性能优化

1. **并行执行** - 独立任务同时执行
2. **避免轮询** - 不要循环检查状态
3. **合理使用子代理** - 复杂任务 spawn sub-agent

---

## 资源链接

- **OpenClaw 文档**: https://docs.openclaw.ai
- **ClawHub**: https://clawhub.ai/
- **GitHub**: https://github.com/openclaw/openclaw
- **Discord 社区**: https://discord.com/invite/clawd

---

*文档结束*
