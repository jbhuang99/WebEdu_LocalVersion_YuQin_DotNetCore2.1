# ============================================================
# （1）当前解决方案中的Skills路径: 例如，WebEdu_LocalVersion_YuQin\.github\skills（VS默认自动读写）；（2）当前操作系统OS用户的的Skills路径: 例如，C:\Users\YourName\.copilot\skills（VS默认自动读写）；（3）当前解决方案/项目交付发布的Skills路径: 例如，WebEdu_LocalVersion_YuQin\ASPDotNet_MVC_YuQin\ASPDotNet_MVC_YuQin\wwwroot\skills（VS无法默认自动读写）。期望通过符号链接（Symbolic Link）将（1）与（2）与（3)解耦，形成“一处维护，多处实时生效”的机制。
# 将“技能文件”与“AI工具的读取路径”解耦，通过操作系统层面的软链接（Symbolic Link）建立一个动态的“快捷方式”，从而实现“一处维护，多处实时生效”。
# 下面为你详细拆解这个机制的原理、操作步骤和核心优势。
# 🔗 核心原理：软链接是什么？
# 你可以将软链接理解为一个“实时指向牌”。
# 传统方式（复制）：你将 Skills 仓库复制到 ~/.copilot/skills/。当你修改仓库中的文件时，Copilot 读取的仍是旧副本，你必须手动再次复制才能同步。这会导致版本分裂和管理混乱。
# 软链接方式：你删除 ~/.copilot/skills/ 文件夹，然后创建一个指向你 Skills 仓库的软链接。此时，~/.copilot/skills/ 不再是一个真实的文件夹，而是一个“指向牌”。当 Copilot 访问这个路径时，操作系统会无缝地将它引导至你的 Skills 仓库。
# 因此，你在仓库中的任何修改，Copilot 都能实时、无延迟地读取到，且完全不占用额外的磁盘空间。
# ============================================================
# GitHub Copilot Skills 符号链接/软链接自动配置脚本 (Windows 10)
# 脚本会修改系统文件：脚本会重命名/删除 ~/.copilot/skills 目录，请确保已理解操作后果。
# 请以【管理员身份】运行此脚本
# 首次运行后验证：配置完成后，务必在 Visual Studio 中打开 Copilot Chat，输入一个你仓库中技能对应的指令，确认能被正确识别和调用。
# 回滚方案已内置：脚本末尾已提供回滚命令，如果配置后出现问题，可直接复制执行恢复原状。
# 路径包含空格：如果你的仓库路径包含空格，脚本已使用 Join-Path 和引号处理，无需额外转义。
# 路径长度限制：Windows 10 默认有 260 字符的路径长度限制。如果你的 Skills 仓库嵌套较深，可能触发此限制。
# 部分 Windows 下的 Git GUI 工具（如 TortoiseGit、SourceTree 的某些版本）可能无法正确识别符号链接，会将其当作普通文本文件读取，导致内容显示为路径字符串而非实际文件。解决方案：在 Git 仓库根目录创建 .gitattributes 文件，添加以下配置，强制 Git 将 skills 目录作为二进制文件处理，避免内容被错误解析：.copilot/skills/* -text
# ============================================================

# ⚠️ 【必须修改】替换为你的 Skills 仓库绝对路径
$SkillsRepoPath = "C:\Users\YourName\MySkillsRepo"

# 自动获取用户主目录，无需修改
$UserHome = $env:USERPROFILE
$CopilotSkillsPath = Join-Path $UserHome ".copilot\skills"
$BackupPath = Join-Path $UserHome ".copilot\skills_backup_$(Get-Date -Format 'yyyyMMdd_HHmmss')"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host " GitHub Copilot Skills 软链接配置工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. 校验仓库路径是否存在
if (-not (Test-Path $SkillsRepoPath)) {
    Write-Host "[错误] Skills 仓库路径不存在: $SkillsRepoPath" -ForegroundColor Red
    Write-Host "请修改脚本中的 `$SkillsRepoPath 变量后重试。" -ForegroundColor Yellow
    exit 1
}

Write-Host "[✓] Skills 仓库路径已确认: $SkillsRepoPath" -ForegroundColor Green

# 2. 安全确认
Write-Host ""
Write-Host "即将执行以下操作:" -ForegroundColor Yellow
Write-Host "  1. 备份现有 $CopilotSkillsPath 目录" -ForegroundColor Yellow
Write-Host "  2. 删除原有 $CopilotSkillsPath 目录" -ForegroundColor Yellow
Write-Host "  3. 创建指向仓库的符号链接" -ForegroundColor Yellow
Write-Host "  4. 配置 .gitattributes 防止 Git 误解析" -ForegroundColor Yellow
Write-Host ""
$confirm = Read-Host "确认继续? (输入 Y 继续, 其他键取消)"

if ($confirm -ne "Y" -and $confirm -ne "y") {
    Write-Host "已取消操作。" -ForegroundColor Gray
    exit 0
}

# 3. 备份并移除原有目录
if (Test-Path $CopilotSkillsPath) {
    Write-Host "[→] 备份现有 skills 目录到: $BackupPath" -ForegroundColor Cyan
    Rename-Item -Path $CopilotSkillsPath -NewName (Split-Path $BackupPath -Leaf) -ErrorAction Stop
    Write-Host "[✓] 备份完成" -ForegroundColor Green
} else {
    Write-Host "[→] 原有 skills 目录不存在，跳过备份" -ForegroundColor Gray
}

# 4. 创建符号链接
Write-Host "[→] 创建符号链接: $CopilotSkillsPath -> $SkillsRepoPath" -ForegroundColor Cyan
try {
    New-Item -ItemType SymbolicLink -Path $CopilotSkillsPath -Target $SkillsRepoPath -ErrorAction Stop | Out-Null
    Write-Host "[✓] 符号链接创建成功" -ForegroundColor Green
} catch {
    Write-Host "[错误] 符号链接创建失败: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "请确认: 1) 以管理员身份运行 2) 目标路径未被占用" -ForegroundColor Yellow
    exit 1
}

# 5. 验证链接
$linkInfo = Get-Item $CopilotSkillsPath
if ($linkInfo.LinkType -eq "SymbolicLink" -and $linkInfo.Target -eq $SkillsRepoPath) {
    Write-Host "[✓] 符号链接验证通过" -ForegroundColor Green
} else {
    Write-Host "[警告] 符号链接验证异常，请手动检查" -ForegroundColor Yellow
}

# 6. 配置 .gitattributes (防止 Git GUI 误解析符号链接)
$gitAttributesPath = Join-Path $SkillsRepoPath ".gitattributes"
$gitAttributesContent = ".copilot/skills/* -text`n"

if (Test-Path $gitAttributesPath) {
    $existingContent = Get-Content $gitAttributesPath -Raw
    if ($existingContent -notmatch "\.copilot/skills/\* -text") {
        Add-Content -Path $gitAttributesPath -Value $gitAttributesContent
        Write-Host "[✓] .gitattributes 已追加配置" -ForegroundColor Green
    } else {
        Write-Host "[→] .gitattributes 已存在相关配置，跳过" -ForegroundColor Gray
    }
} else {
    Set-Content -Path $gitAttributesPath -Value $gitAttributesContent
    Write-Host "[✓] .gitattributes 已创建并配置" -ForegroundColor Green
}

# 7. 完成提示
Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host " 配置完成!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "下一步操作:" -ForegroundColor Cyan
Write-Host "  1. 重启 Visual Studio" -ForegroundColor White
Write-Host "  2. 在 Copilot Chat 中测试技能调用" -ForegroundColor White
Write-Host "  3. 如需回滚，删除符号链接并恢复备份:" -ForegroundColor White
Write-Host "     Remove-Item '$CopilotSkillsPath' -Force" -ForegroundColor Gray
Write-Host "     Rename-Item '$BackupPath' 'skills'" -ForegroundColor Gray
Write-Host ""