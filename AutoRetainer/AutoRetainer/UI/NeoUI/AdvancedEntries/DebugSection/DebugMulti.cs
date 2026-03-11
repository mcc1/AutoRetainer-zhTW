using AutoRetainer.Internal;
using AutoRetainer.Scheduler.Tasks;
using Dalamud.Utility;
using ECommons.Automation.NeoTaskManager.Tasks;
using ECommons.ExcelServices;
using ECommons.ExcelServices.TerritoryEnumeration;
using ECommons.GameHelpers;
using ECommons.Reflection;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Lumina.Excel.Sheets;

namespace AutoRetainer.UI.NeoUI.AdvancedEntries.DebugSection;

internal unsafe class DebugMulti : DebugSectionBase
{
    public override void Draw()
    {
        if(ImGui.CollapsingHeader("Sorted data"))
        {
            ImGuiEx.Text($"{MultiMode.GetRetainerSortedOfflineDatas(true).Where(x => !x.ExcludeRetainer).Select(x => $"{x.Name}@{x.World}").Print("\n")}");
        }
        if(ImGui.CollapsingHeader("NeoHET"))
        {
            if(ImGui.Button("Enqueue HET")) TaskNeoHET.Enqueue(null);
            if(ImGui.Button("Enqueue workshop")) TaskNeoHET.TryEnterWorkshop(() => DuoLog.Error("Fail"));
            ImGuiEx.Text($"""
                可進入工坊：{S.LifestreamIPC.CanMoveToWorkshop()}
                """);
        }
        if(ImGui.CollapsingHeader("Tasks"))
        {
            if(ImGui.Button("TestAutomoveTask")) P.TaskManager.EnqueueTask(NeoTasks.ApproachObjectViaAutomove(() => Svc.Targets.FocusTarget));
            if(ImGui.Button("TestInteractTask")) P.TaskManager.EnqueueTask(NeoTasks.InteractWithObject(() => Svc.Targets.FocusTarget));
            if(ImGui.Button("TestBoth"))
            {
                P.TaskManager.EnqueueTask(NeoTasks.ApproachObjectViaAutomove(() => Svc.Targets.FocusTarget));
                P.TaskManager.EnqueueTask(NeoTasks.InteractWithObject(() => Svc.Targets.FocusTarget));
            }
        }
        ImGui.Checkbox("不要登出", ref C.DontLogout);
        ImGui.Checkbox("啟用", ref MultiMode.Enabled);
        ImGuiEx.Text($"預期：{TaskChangeCharacter.Expected}");
        if(ImGui.Button("Force mismatch")) TaskChangeCharacter.Expected = ("AAAAAAAA", "BBBBBBB");
        if(ImGui.Button("Simulate nothing left"))
        {
            MultiMode.Relog(null, out var error, RelogReason.MultiMode);
        }
        if(ImGui.Button($"Simulate autostart"))
        {
            MultiMode.PerformAutoStart();
        }
        if(ImGui.Button("Delete was loaded data"))
        {
            DalamudReflector.DeleteSharedData("AutoRetainer.WasLoaded");
        }
        ImGuiEx.Text($"移動中：{AgentMap.Instance()->IsPlayerMoving}");
        ImGuiEx.Text($"已占用：{IsOccupied()}");
        ImGuiEx.Text($"Casting: {Player.Object?.IsCasting}");
        ImGuiEx.TextCopy($"CID: {Player.CID}");
        ImGuiEx.Text($"{Svc.Data.GetExcelSheet<Addon>()?.GetRow(115).Text.ToDalamudString().GetText()}");
        ImGuiEx.Text($"伺服器時間：{CSFramework.GetServerTime()}");
        ImGuiEx.Text($"電腦時間：{DateTimeOffset.Now.ToUnixTimeSeconds()}");
        if(ImGui.CollapsingHeader("HET"))
        {
            ImGuiEx.Text($"最近入口：{Utils.GetNearestEntrance(out var d)}，距離={d}");
            if(ImGui.Button("進入房屋"))
            {
                TaskNeoHET.Enqueue(null);
            }
        }
        if(ImGui.CollapsingHeader("住宅區領地"))
        {
            ImGuiEx.Text(ResidentalAreas.List.Select(x => GenericHelpers.GetTerritoryName(x)).Join("\n"));
            ImGuiEx.Text($"In residental area: {ResidentalAreas.List.Contains(Svc.ClientState.TerritoryType)}");
        }
        ImGuiEx.Text($"Is in sanctuary: {TerritoryInfo.Instance()->InSanctuary}");
        ImGuiEx.Text($"Is in sanctuary ExcelTerritoryHelper: {ExcelTerritoryHelper.IsSanctuary(Svc.ClientState.TerritoryType)}");
        ImGui.Checkbox($"略過安全區檢查", ref C.BypassSanctuaryCheck);
        if(Svc.ClientState.LocalPlayer != null && Svc.Targets.Target != null)
        {
            ImGuiEx.Text($"到目標的距離：{Vector3.Distance(Svc.ClientState.LocalPlayer.Position, Svc.Targets.Target.Position)}");
            ImGuiEx.Text($"Target hitbox: {Svc.Targets.Target.HitboxRadius}");
            ImGuiEx.Text($"到目標碰撞箱的距離：{Vector3.Distance(Svc.ClientState.LocalPlayer.Position, Svc.Targets.Target.Position) - Svc.Targets.Target.HitboxRadius}");
        }
        if(ImGui.CollapsingHeader("CharaSelect"))
        {
            foreach(var x in Utils.GetCharacterNames())
            {
                ImGuiEx.Text($"{x.Name}@{x.World}");
            }
        }
    }
}
