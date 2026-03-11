# Localization Custom Changes

This file records changes made during zh-TW localization work that are not simple dictionary updates.
Use it as a checklist when rebasing onto a newer upstream AutoRetainer version or when debugging future localization regressions.

## Template Changes

These changes live in `mcc1/dalamud-mod-localizer` and affect all consumer repos that use the shared localizer workflow.

### `Program.cs`

- Added consumer-first path resolution for `LOCALIZER_DICT_PATH`.
  - Purpose: make the reusable workflow read `zh-TW.json` from the consumer repo root instead of `.template/zh-TW.json`.
  - Related template commit: `1fece81`
- Added translation support for plugin manifests (`*.json`) for these fields:
  - `Punchline`
  - `Description`
  - Purpose: translate `AutoRetainer.json` metadata shown by the plugin installer/UI.
  - Related template commit: `6fb6569`

### `TranslationRewriter.cs`

- Added support for translating display text stored in dictionary-like UI name maps.
  - Covers patterns like `...Names`, `...Labels`, `...Texts`, `...Tooltips`, `...Descriptions`.
  - Purpose: translate values in structures such as `FrozenDictionary<Enum, string>` used by UI combos.
  - Related template commit: `c355881`
- Expanded dictionary-like detection to also cover declared field/property types such as:
  - `Dictionary`
  - `ReadOnlyDictionary`
  - `FrozenDictionary`
  - Purpose: fix cases like `Lang.UnlockModeNames` that were missed by shape-only detection.
  - Related template commit: `04c2257`
- Expanded UI keyword detection to include SeString builder methods:
  - `AddUiForeground`
  - `AddText`
  - Purpose: allow translation of chat/notification strings built through `SeStringBuilder`.
  - Related template commit: `2564dfd`

### Reusable Workflow

- Added translation smoke test after localizer execution.
  - Purpose: fail CI if expected zh-TW strings do not appear in translated sources.
  - Related template commit: `c656a34`
- Added `grep` fallback when `rg` is unavailable on the runner.
  - Related template commit: `2daedff`

## Consumer Changes

These changes live in `mcc1/AutoRetainer-zhTW` and are specific to the AutoRetainer consumer repo.
Source-level customizations that must survive upstream sync are stored under `.consumer-patches/` and are applied by the reusable workflow after localization.

### `AutoRetainer/AutoRetainer/UI/NeoUI/RetainersTab.cs`

- Added a null guard for the mass entrust-plan action.
  - The `設定存放計畫` button is now disabled when no entrust plan is selected.
  - The action path also checks `SelectedEntrustPlan != null` before using `.Guid`.
  - Purpose: fix `NullReferenceException` in `MassConfigurationChangeWidget()`.
  - Related consumer commit: `dd2911c`
  - Patch file: `.consumer-patches/002-retainers-tab-null-guard.patch`

### `AutoRetainer/AutoRetainer/UI/NeoUI/AdvancedEntries/ExpertTab.cs`

- Added explicit zh-TW display-name mappings for enum-driven combo boxes instead of relying on `enum.ToString().Replace("_", " ")`.
  - `OpenBellBehavior`
  - `TaskCompletedBehavior`
  - Purpose: keep enum identifiers stable in code/config while showing localized labels in UI.
  - Related consumer commit: `5f6f821`
  - Patch file: `.consumer-patches/001-localize-expert-tab-enums.patch`

### `zh-TW.json`

- Path-like labels must preserve halfwidth `/`.
  - Example: `Multi Mode/Exclusions and Order`
  - Purpose: avoid breaking tree/path-like UI rendering.
  - Related consumer commit: `9106104`
- Added exact-key translations for strings that are built from code rather than plain UI labels.
  - Example: `[AutoRetainer] Some of the retainers have completed their ventures!`
  - Purpose: support translation when the code includes prefixes or formatting as part of the literal.

## Upgrade Checklist

When updating to a newer AutoRetainer upstream version:

1. Re-check that the shared template repo still contains all template changes listed above.
2. Re-check `RetainersTab.cs` for the null guard if the bulk configuration UI was refactored upstream.
3. Re-check `ExpertTab.cs` if enum combo rendering was changed upstream.
4. Re-check `AutoRetainer.json` after localizer runs to confirm `Punchline` and `Description` are still translated.
5. Re-check chat/notification messages that use `SeStringBuilder`.
6. Re-check path-like translated labels to ensure halfwidth `/` was preserved.
7. Re-check that `.consumer-patches/*.patch` still apply cleanly after upstream source changes.
