using System.Diagnostics;

namespace AutoRetainer.UI
{
    public static class CustomAboutTab
    {
        private static string GetImageURL()
        {
            return Svc.PluginInterface.Manifest.IconUrl ?? "";
        }

        public static void Draw()
        {
            ImGuiEx.LineCentered("About1", delegate
            {
                ImGuiEx.Text($"{Svc.PluginInterface.Manifest.Name} - {Svc.PluginInterface.Manifest.AssemblyVersion}");
            });

            ImGuiEx.LineCentered("About0", () =>
            {
                ImGuiEx.Text($"發布與開發者：");
                ImGui.PushFont(UiBuilder.IconFont);
                ImGui.SameLine(0, 0);
                ImGuiEx.Text(ImGuiColors.DalamudRed, FontAwesomeIcon.Heart.ToIconString());
                ImGui.PopFont();
                ImGui.SameLine(0, 0);
                ImGuiEx.Text($" 由 Puni.sh 與 NightmareXIV 開發");
            });

            ImGuiHelpers.ScaledDummy(10f);
            ImGuiEx.LineCentered("About2", delegate
            {
                if(ThreadLoadImageHandler.TryGetTextureWrap(GetImageURL(), out var texture))
                {
                    ImGui.Image(texture.ImGuiHandle, new(200f, 200f));
                }
            });
            ImGuiHelpers.ScaledDummy(10f);
            ImGuiEx.LineCentered("About3", delegate
            {
                ImGui.TextWrapped("加入我們的 Discord 社群，以獲取專案公告、更新與支援。");
            });
            ImGuiEx.LineCentered("About4", delegate
            {
                if(ImGui.Button("Discord"))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "https://discord.gg/Zzrcc8kmvy",
                        UseShellExecute = true
                    });
                }
                ImGui.SameLine();
                if(ImGui.Button("Repository"))
                {
                    ImGui.SetClipboardText("https://love.puni.sh/ment.json");
                    Notify.Success("連結已複製到剪貼簿");
                }
                ImGui.SameLine();
                if(ImGui.Button("原始碼"))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = Svc.PluginInterface.Manifest.RepoUrl,
                        UseShellExecute = true
                    });
                }
                ImGui.SameLine();
                if(ImGui.Button("贊助 Puni.sh 平台"))
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "https://ko-fi.com/spetsnaz",
                        UseShellExecute = true
                    });
                }
            });
        }
    }
}
