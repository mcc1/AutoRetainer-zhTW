# Template Extraction Notes

## Current Direction

This repository now contains two layers:

1. A reusable workflow contract in `.github/workflows/reusable-build-mod.yml`
2. An AutoRetainer-specific wrapper in `.github/workflows/main.yml`

The reusable layer is the piece intended to move into the future template/framework
repository. The wrapper remains mod-specific and only passes pins and paths.

## Configuration Surface

Consumer repos must provide:

- `mod_name`
- `mod_version`
- `mod_repo_url`
- `mod_ref`
- `mod_repo_dir`
- `localizer_source_subpaths`
- `localizer_dict_path`
- `build_project_path`
- `package_build_dir`
- `artifact_basename`
- `release_tag_prefix`
- `package_include_patterns`
- `dalamud_asset_url`
- `dalamud_assets_json_url`
- `dalamud_required_files`

Optional behavior flags:

- `commit_changes`
- `publish_release`

## Extraction Plan

When the separate template repo is created, move these files first:

- `Program.cs`
- `TranslationRewriter.cs`
- `.github/workflows/reusable-build-mod.yml`
- `docs/MOD_TRANSLATION_BUILD_PLAYBOOK.md`
- `docs/TEMPLATE_EXTRACTION_NOTES.md`

Then keep only AutoRetainer-specific files in this repo:

- `zh-TW.json`
- `.github/workflows/main.yml`
- AutoRetainer version pins and package rules
