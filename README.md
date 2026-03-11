<h1>AutoRetainer zh-TW</h1>

這個 repo 是 AutoRetainer 的繁體中文 consumer repo。

它保留 AutoRetainer 專屬內容：

- `zh-TW.json`
- AutoRetainer wrapper workflow
- AutoRetainer 版本 pin
- AutoRetainer package 規則
- 已處理的 AutoRetainer 原始碼快照

共用翻譯與建置框架已移到：

- `mcc1/dalamud-mod-localizer`

<h2>Workflow</h2>

此 repo 的 GitHub Actions wrapper 會呼叫外部 reusable workflow：

- [main.yml](/h:/project/dalamud-mod-translater/.github/workflows/main.yml)

<h2>Docs</h2>

- [AutoRetainer baseline](/h:/project/dalamud-mod-translater/docs/AUTORETAINER_BASELINE.md)

<h2>Credits</h2>

- Original translation/localizer foundation and prior project history: Miaki
- Reusable workflow extraction, AutoRetainer baseline stabilization, and consumer split work: mcc
