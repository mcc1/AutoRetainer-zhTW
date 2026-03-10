namespace AutoRetainer.Modules.GcHandin;

internal static class AutoGCHandinUI
{
    internal static void Draw()
    {
        ImGui.Checkbox("籌備稀有品完成時發送托盤通知（需要NotificationMaster插件）", ref C.GCHandinNotify);
    }
}
