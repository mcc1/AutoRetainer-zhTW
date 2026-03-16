using ECommons.ExcelServices;

namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries;
public class FastAddition : InventoryManagemenrBase
{
    public override string Name { get; } = "背包清理/快速添加和移除";

    private FastAddition()
    {
        Builder = InventoryCleanupCommon.CreateCleanupHeaderBuilder()
        .Section(Name)
        .Widget(() =>
        {
            var selectedSettings = InventoryCleanupCommon.SelectedPlan;
            ImGuiEx.TextWrapped(GradientColor.Get(EColor.RedBright, EColor.YellowBright), $"當此文字可見時，將滑鼠懸停在物品上並按住按鍵:");
            ImGuiEx.Text(!ImGui.GetIO().KeyShift ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, $"Shift - 添加至快速僱員販售清單");
            ImGuiEx.Text($"* 已在無條件出售清單中的物品不會再被加入快速探險出售清單");
            ImGuiEx.Text(!ImGui.GetIO().KeyCtrl ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, $"Ctrl - 添加至無條件出售清單");
            ImGuiEx.Text($"* 已在快速探險出售清單中的物品將會被移動到無條件出售清單");
            ImGuiEx.Text(!ImGui.GetIO().KeyAlt ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, $"Alt - 從任一清單刪除");
            ImGuiEx.Text("受保護的物品不受此操作影響");
            if(Svc.GameGui.HoveredItem > 0)
            {
                var id = (uint)(Svc.GameGui.HoveredItem % 1000000);
                if(ImGui.GetIO().KeyShift)
                {
                    if(!selectedSettings.IMProtectList.Contains(id) && !selectedSettings.IMAutoVendorSoft.Contains(id) && !selectedSettings.IMAutoVendorHard.Contains(id))
                    {
                        selectedSettings.IMAutoVendorSoft.Add(id);
                        Notify.Success($"已將 {ExcelItemHelper.GetName(id)} 添加至快速僱員販售清單");
                        selectedSettings.IMAutoVendorHard.Remove(id);
                    }
                }
                if(ImGui.GetIO().KeyCtrl)
                {
                    if(!selectedSettings.IMProtectList.Contains(id) && !selectedSettings.IMAutoVendorHard.Contains(id) && !selectedSettings.IMAutoVendorSoft.Contains(id))
                    {
                        selectedSettings.IMAutoVendorHard.Add(id);
                        Notify.Success($"已將 {ExcelItemHelper.GetName(id)} 添加至無條件出售清單");
                        selectedSettings.IMAutoVendorSoft.Remove(id);
                    }
                }
                if(ImGui.GetIO().KeyAlt)
                {
                    if(selectedSettings.IMAutoVendorSoft.Remove(id)) Notify.Info($"移除 {ExcelItemHelper.GetName(id)} 從快速僱員販售清單");
                    if(selectedSettings.IMAutoVendorHard.Remove(id)) Notify.Info($"移除 {ExcelItemHelper.GetName(id)} 從無條件出售清單");
                }
            }
        });
        DisplayPriority = -10;
    }
}
