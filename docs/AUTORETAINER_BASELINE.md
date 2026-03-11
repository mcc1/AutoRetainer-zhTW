# AutoRetainer Baseline

This file records the current known-good AutoRetainer inputs before template extraction.

## Source Pin

- AutoRetainer source commit: `4f658f35a89341f78d5de412482dd7183824cb90`
- Human-readable revision: `AutoRetainer 4.5.1.13`

## Build Environment Pin

- Dalamud asset URL: `https://github.com/yanmucorp/Dalamud/releases/download/25-12-26-01/latest.7z`
- Dalamud assets JSON: `https://raw.githubusercontent.com/yanmucorp/DalamudAssets/master/assetCN.json`
- API generation: `API12`
- UI binding style: `ImGui.NET`

## Localizer Inputs

- Repo directory: `AutoRetainer`
- Source paths: `AutoRetainer`
- Dictionary file: `zh-TW.json`

## Build Target

- Project path: `AutoRetainer/AutoRetainer/AutoRetainer.csproj`
- Build command:

```bash
dotnet build AutoRetainer/AutoRetainer/AutoRetainer.csproj -c Release -p:CustomCS=true -p:EnableWindowsTargeting=true
```

## Expected Packaging Output

- Final artifact name: `AutoRetainer.zip`
- Expected contents:
  - root-level `*.dll`
  - root-level `*.json`
  - root-level `*.pdb`
  - `res/*.png`

## Verification Status

Verified locally on 2026-03-10:

- Source checkout: passed
- Submodule sync/update: passed
- Dalamud dependency preparation: passed
- Build: passed with warnings only
- Packaging: workflow packaging logic remains the reference for expected zip structure

Current localizer coverage notes:

- The scanner was widened from `AutoRetainer/UI` to `AutoRetainer`
- This is required because command help and other translatable strings exist outside the UI folder
