<h1>🤖 AutoRetainer Localizer</h1>

可配置的 Dalamud mod C# 源碼翻譯與建置框架，目前以 AutoRetainer 為範例。

<h2>🌟 功能</h2>

自動同步：自動獲取官方最新原始碼。

一鍵更新：透過 GitHub Actions 自動完成 Clone -> 翻譯 -> 建置 -> 上傳 Artifact。

修改字典：編輯 zh-TW.json 加入新的翻譯對照。

可移植：透過環境變數切換 repo 目錄、source 路徑與字典，套用到其他 mod。


<h2>🛠️ 技術棧</h2>

.NET 8.0 / C#

Microsoft.CodeAnalysis (Roslyn)

GitHub Actions


<h2>📘 可重用指南</h2>

[Mod 翻譯與建置可重用手冊](docs/MOD_TRANSLATION_BUILD_PLAYBOOK.md)

[Template 抽取說明](docs/TEMPLATE_EXTRACTION_NOTES.md)
