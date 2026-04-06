<!--通过 llms.txt/llms-full.txt引导AIGC高效使用网站内容-->
- [关于llms.txt/llms-full.txt标准](https://llmstxt.org/)：llms.txt 是放置在网站根目录下的一个文件，它的核心目标是向 LLMs 提供结构化的、机器可读的信息，从而帮助它们在推理阶段更有效地利用网站内容。与面向搜索引擎的 robots.txt 和 sitemap.xml 不同，llms.txt 专为推理引擎优化，它的目标是通过以 AI 能够高效处理的格式提供内容结构，解决了 AI 相关的挑战。这种（还在提出阶段的）标准标志着一种向 AI 优先的文档和内容策略的转变。随着 AI 在网络内容消费中扮演越来越重要的角色，针对 AI 的理解进行优化将变得与针对人类用户和搜索引擎进行优化同样关键。网站所有者也能从实施 llms.txt 中获益匪浅：AI 聊天机器人更有可能在其回复中引用那些提供了 llms.txt 文件的网站，从而提高网站在 AI 平台上的可见性。优化后的 llms.txt 文件还有可能在 AI 驱动的搜索体验中带来更好的可见性、更高的排名和更强的可发现性。更清晰地理解 llms.txt 在网络标准中的定位，可以参考下表：

| 名称         | 目的                                 | 目标受众           | 格式     |
|--------------|--------------------------------------|--------------------|----------|
| `robots.txt` | 控制搜索引擎爬虫对网站的访问         | 搜索引擎           | 文本     |
| `sitemap.xml`| 列出网站上所有可索引的页面           | 搜索引擎           | XML      |
| `llms.txt`   | 为大型语言模型提供结构化的内容概述   | 大型语言模型       | Markdown |
- [llms.txt案例](https://onevcat.com/llms.txt)：llms.txt 提供了一个精简的网站文档导航视图，旨在帮助 AI 系统快速理解网站的结构，它通过链接提供了一个简洁、结构化的关键内容概述。
- [llms-full.txt案例](https://onevcat.com/llms-full.txt)：llms-full.txt 是一个包含所有文档内容的综合性文件，它将所有文档整合到一个单独的 Markdown 文件中，这个文件需要“包罗万象”，因此尺寸一般比较大。

# 创建和放置 llms.txt.
按照上述内容创建好文件后，就可以将它们放置在网站的根目录下，并公开给互联网访问了。llms.txt 的标准约定了文件的位置就是网站根目录，这类似于 robots.txt 和 sitemap.xml 的存放方式，简化了 LLMs 的发现过程。

另外，我们还可以在服务器配置中添加 X-Robots-Tag: llms-txt HTTP Header。这个值可以向 LLM 发出信号，表明网站提供了 llms.txt 文件。这并不是一个严格要求，但这个 header 值可以明确表明网站已采用 llms.txt 标准。

我们可以使用一些工具来验证某个网站是否实现了 llms.txt 标准，比如这个[extension](https://chromewebstore.google.com/detail/llmstxt-checker/) 。

目前该标准仍处于早期阶段，但随着标准的普及，我们可能会看到 AI 系统发展出自动发现和使用 llms.txt 文件的能力，这会类似于搜索引擎处理 robots.txt 和 sitemap.xml 的方式。

值得注意的是，一些工具和平台已经开始支持 llms.txt 的集成。例如，Cursor 等平台允许用户添加和索引第三方文档（包括 /llms-full.txt），并在聊天中使用它们作为上下文。也有一些类似于[lms.txt hub](https://llmstxthub.com/) 的网站为 llms.txt 提供了更方便的检索方式。

# 在 AI 中使用 llms.

和主动抓取网络的搜索引擎或者爬虫不同，目前 LLMs 还不会自动发现和索引 llms.txt 文件（毕竟 llms-txt 还没有成为被完全认可的标准）。因此，我们还需要手动将文件内容提供给 AI 系统。这可以通过以下方式完成：

| 序号 | 适用场景                         | 操作方式                                                                 |
|------|----------------------------------|--------------------------------------------------------------------------|
| 1    | 可访问互联网的 AI                | 提供 `llms.txt` 或 `llms-full.txt` 文件的公开 URL（如 `https://example.com/llms.txt`） |
| 2    | 不能访问互联网的 AI（如本地部署模型） | 将 `llms.txt` 文件的完整文本内容直接嵌入系统提示词（system prompt）中                 |
| 3    | 支持文件上传的 AI 工具            | 上传纯文本格式的 `llms.txt` 文件（UTF-8 编码，无 BOM，LF 换行）                      |
# llms.txt 工具与实际案例.

目前有多种工具可以帮助生成 llms.txt 文件，从而简化了创建过程，使得网站所有者更容易采用该标准。例如：

dotenv 开发的 llmstxt 是一个开源的命令行工具，可以基于网站的 sitemap.xml 文件生成 llms.txt。
Firecrawl 也提供了一个名为 llmstxt 的工具，它使用 Firecrawl 的爬虫来生成 llms.txt 文件。Firecrawl 还提供了一个功能齐全的 AI 爬虫，可以创建 llms.txt 文件，并为大型平台提供在线生成器。
Mintlify 是一个文档平台，内置了 llms.txt 的生成功能，可以为托管的文档自动生成 /llms.txt 和 /llms-full.txt。
Microsoft 的 MarkItDown、Jina AI 的 Reader API 等，可以把任意内容转换为 Markdown，适合用来从没有直接纯文字驱动的网站生成 llms-full.txt。
也有一些 WordPress 插件可以用来创建和管理 /llms.txt 文件。

下表列出了一些可用于生成 llms.txt 文件的工具：

| 工具名称                          | 描述                         | 生成方法                 | 参考链接                                      |
|-----------------------------------|------------------------------|--------------------------|---------------------------------------------|
| `llmstxt by dotenv`               | 开源命令行工具               | 基于 `sitemap.xml` 文件生成 | [https://github.com/dotenvx/llmstxt](https://github.com/dotenvx/llmstxt) |
| `llmstxt by Firecrawl`            | 使用 Firecrawl 爬虫生成      | 抓取网站内容             | [https://llmstxt.firecrawl.dev/](https://llmstxt.firecrawl.dev/)         |
| `Mintlify`                        | 文档平台                     | 自动生成                 | [https://mintlify.com/](https://mintlify.com/)                           |
| `MarkItDown by Microsoft`         | 内容转换为 Markdown          | 手动转换内容             | [https://github.com/microsoft/markitdown](https://github.com/microsoft/markitdown) |
| `SLM (Reader API) by Jina AI`     | 内容转换为 Markdown          | 手动转换内容             | [https://jina.ai/reader/](https://jina.ai/reader/)                         |
| `LLMs.txt Generator`              | WordPress 插件               | 自动创建和管理           | [https://wordpress.org/plugins/llms-txt-generator/](https://wordpress.org/plugins/llms-txt-generator/) |