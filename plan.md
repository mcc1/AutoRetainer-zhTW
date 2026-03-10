# Plan

## Goal

Stabilize the current AutoRetainer-TW build first, then extract the reusable translation/build framework into a separate template repository, and finally convert the AutoRetainer repository into a consumer/example repo built on that template.

## Phase 1: Stabilize Current AutoRetainer Repo

Purpose:
Lock down the current working AutoRetainer flow before any abstraction work so we do not lose known-good version pins or build behavior.

Tasks:

1. Verify the current repository can still:
   - run the localizer
   - resolve the pinned AutoRetainer source revision
   - prepare the pinned yanmucorp/Dalamud build environment
   - build successfully
   - package the expected `AutoRetainer.zip` artifact
2. Record the exact known-good inputs:
   - AutoRetainer source commit
   - Dalamud asset URL
   - assets JSON URL
   - source paths used by the localizer
   - expected artifact structure
3. Fix any regressions discovered during verification before doing template work.

Exit criteria:

- Current AutoRetainer workflow logic is verified end-to-end.
- Known-good pins and assumptions are documented in repo files.

## Phase 2: Extract Template/Framework Repo

Purpose:
Separate reusable logic from mod-specific configuration.

Tasks:

1. Move the reusable pieces into a standalone template/framework repository:
   - Roslyn localizer entrypoint
   - translation rewriter
   - reusable workflow logic
   - shared docs
2. Define the configuration surface the consumer repos must provide:
   - mod repo URL
   - mod ref
   - source paths
   - dictionary path
   - build project path
   - Dalamud asset pins
   - artifact name
3. Provide one reusable workflow interface:
   - either `workflow_call`
   - or shared script entrypoints invoked by thin per-mod workflows

Exit criteria:

- Template repo can be used without AutoRetainer-specific hardcoding.
- AutoRetainer-specific data is no longer required for the template to make sense.

## Phase 3: Convert AutoRetainer Repo into a Consumer/Example Repo

Purpose:
Turn the current repo into the first concrete example that consumes the template/framework.

Tasks:

1. Create an AutoRetainer-specific wrapper workflow that passes only AutoRetainer settings.
2. Keep AutoRetainer-specific files only:
   - dictionary
   - version pins
   - build paths
   - artifact naming
3. Reference the new template repo:
   - preferably via submodule for shared code
   - plus a thin workflow wrapper in this repo
4. Verify the converted repo still produces the same AutoRetainer zip output.

Exit criteria:

- AutoRetainer repo is a clean example consumer.
- Adding another mod such as Lifestream becomes a repeatable process.

## Working Rules

1. Do not start extracting the template until Phase 1 is green.
2. Preserve the current known-good AutoRetainer pins until a replacement is verified.
3. Prefer thin consumer workflows and reusable shared logic over duplicating full workflows per mod.
4. Treat AutoRetainer as the reference implementation while designing the generic framework.
