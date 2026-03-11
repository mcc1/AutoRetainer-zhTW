namespace AutoRetainer.UI.NeoUI.AdvancedEntries.DebugSection;

internal class DebugNMAPI : DebugSectionBase
{
    private static float vol;
    private static bool repeat;
    private static bool stopOnFocus;
    private static string path = "";
    public override void Draw()
    {
        ImGuiEx.Text($"啟用中：{P.NotificationMasterApi.IsIPCReady()}");
        ImGui.InputText("path", ref path, 500);
        ImGui.InputFloat("vol", ref vol);
        ImGui.Checkbox("repeat", ref repeat);
        ImGui.Checkbox("stopOnFocus", ref stopOnFocus);
        if(ImGui.Button("Flash")) new TickScheduler(() => P.NotificationMasterApi.FlashTaskbarIcon(), 1000);
        if(ImGui.Button("msg")) new TickScheduler(() => P.NotificationMasterApi.DisplayTrayNotification("Title", "Text"), 1000);
        if(ImGui.Button("訊息無標題")) new TickScheduler(() => P.NotificationMasterApi.DisplayTrayNotification("Text"), 1000);
        if(ImGui.Button("播放音效")) new TickScheduler(() => P.NotificationMasterApi.PlaySound(path, vol, repeat, stopOnFocus), 1000);
        if(ImGui.Button("stop sound")) P.NotificationMasterApi.StopSound();
    }
}
