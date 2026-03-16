namespace AutoRetainer.UI.NeoUI.Experiments;

internal class NightMode : ExperimentUIEntry
{
    public override string Name => "夜間模式";
    public override void Draw()
    {
        ImGuiEx.TextWrapped($"夜間模式:\n- 將強制啟用登入介面等待選項\n- 將強制套用內建的FPS限制器\n- 當遊戲視窗失去焦點且處於等待狀態時，遊戲幀率將被限制在0.2 FPS\n- 這看起來可能像是遊戲卡住了，但重新啟動遊戲視窗後，請給予最多5秒的時間讓其恢復\n- 預設情況下，夜間模式僅啟用遠航探索\n- 停用夜間模式後，緊急管理器將啟動以將您重新登入回遊戲");
        if(ImGui.Checkbox("啟用夜間模式", ref C.NightMode)) MultiMode.BailoutNightMode();
        ImGui.Checkbox("顯示夜間模式勾選框", ref C.ShowNightMode);
        ImGui.Checkbox("在夜間模式下處理僱員", ref C.NightModeRetainers);
        ImGui.Checkbox("在夜間模式下處理派遣", ref C.NightModeDeployables);
        ImGui.Checkbox("使夜間模式狀態持久化", ref C.NightModePersistent);
        ImGui.Checkbox("使關機指令改為啟動夜間模式而非關閉遊戲", ref C.ShutdownMakesNightMode);
    }
}
