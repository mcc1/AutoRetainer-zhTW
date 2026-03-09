namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries;
public class SoftList : InventoryManagemenrBase
{
    public override string Name => "背包清理/快速僱員販售清單";
    private SoftList()
    {
        var s = InventoryCleanupCommon.SelectedPlan;
        Builder = InventoryCleanupCommon.CreateCleanupHeaderBuilder()
            .Section(Name)
            .TextWrapped("這些物品從快速任務（Quick Venture）獲得後會被出售，除非它們與相同物品堆疊。")
            .Widget(() => InventoryManagementCommon.DrawListNew(s.IMAutoVendorSoft))
            .Widget(() =>
            {
                InventoryManagementCommon.ImportFromArDiscard(s.IMAutoVendorSoft);
            });
    }
}
