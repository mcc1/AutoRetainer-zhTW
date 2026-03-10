namespace AutoRetainer.UI.NeoUI.MultiModeEntries;
public class MultiModeDeployables : NeoUIEntry
{
    public override string Path => "多角色模式/遠航探索";

    public override NuiBuilder Builder { get; init; } = new NuiBuilder()
        .Section("多角色模式 - 潛艇/飛空艇")
        .Checkbox("等待航行完成", () => ref C.MultiModeWorkshopConfiguration.MultiWaitForAll, "啟用時，AutoRetainer 會等到所有探險潛艇回歸後才登入該角色。若你因其他原因已在線上，它仍會重新派遣已完成的潛艇——除非\"即使已登入也等待\"的全局設定也被開啟。")
        .Indent()
        .Checkbox("即使已登入也等待", () => ref C.MultiModeWorkshopConfiguration.WaitForAllLoggedIn, "更改\"等待航行完成\"的行為（包括全局與單一角色設定），使 AutoRetainer 在已登入時不再單獨派遣個別回歸的潛艇，而是等到\"全部\"潛艇都回歸後才一併處理。")
        .InputInt(120f, "最大等待時間（分鐘）", () => ref C.MultiModeWorkshopConfiguration.MaxMinutesOfWaiting.ValidateRange(0, 9999), 10, 60, "如果等待其餘潛艇回歸的時間超過此分鐘數，AutoRetainer 將忽略\"等待航行完成\"與\"即使已登入也等待\"的設定。")
        .Unindent()
        .DragInt(60f, "提前登入閾值（秒）", () => ref C.MultiModeWorkshopConfiguration.AdvanceTimer.ValidateRange(0, 300), 0.1f, 0, 300, "The number of seconds AutoRetainer should log in early before submarines on this character are ready to be resent.")
        .DragInt(120f, "Retainer venture processing cutoff, minutes", () => ref C.DisableRetainerVesselReturn.ValidateRange(0, 60), "If set to a value greater than 0, AutoRetainer will stop processing any retainers this number of minutes before any character is scheduled to redeploy submarines, taking all previous settings into account.")
        .Checkbox("進入部隊工作坊時，定期檢查部隊箱中的金幣", () => ref C.FCChestGilCheck, "在進入工作坊時定期檢查部隊箱，以保持金幣計數為最新狀態。")
        .Indent()
        .SliderInt(150f, "Check frequency, hours", () => ref C.FCChestGilCheckCd, 0, 24 * 5)
        .Widget("重設冷卻時間", (x) =>
        {
            if(ImGuiEx.Button(x, C.FCChestGilCheckTimes.Count > 0)) C.FCChestGilCheckTimes.Clear();
        })
        .Unindent();
}
