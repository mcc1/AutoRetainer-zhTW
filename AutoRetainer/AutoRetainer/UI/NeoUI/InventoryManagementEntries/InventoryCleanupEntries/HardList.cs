namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries;
public class HardList : InventoryManagemenrBase
{
    public override string Name => "背包清理/快速出售清單";

    private HardList()
    {
        var s = InventoryCleanupCommon.SelectedPlan;
        Builder = InventoryCleanupCommon.CreateCleanupHeaderBuilder()
            .Section(Name)
            .TextWrapped("這些物品將始終被出售，不論其來源，只要堆疊數量不超過下方設定的數值。此外，僅這些物品會被出售給 NPC。")
            .InputInt(150f, $"可出售的最大堆疊數量", () => ref s.IMAutoVendorHardStackLimit)
            .Widget(() => InventoryManagementCommon.DrawListNew(s.IMAutoVendorHard, (x) =>
            {
                ImGui.SameLine();
                ImGui.PushFont(UiBuilder.IconFont);
                ImGuiEx.CollectionButtonCheckbox(FontAwesomeIcon.Database.ToIconString(), x, s.IMAutoVendorHardIgnoreStack);
                ImGui.PopFont();
                ImGuiEx.Tooltip($"忽略此物品的堆疊設定");
            }))
            .Separator()
            .Widget(() =>
            {
                InventoryManagementCommon.ImportFromArDiscard(s.IMAutoVendorHard);
            });
    }
}
