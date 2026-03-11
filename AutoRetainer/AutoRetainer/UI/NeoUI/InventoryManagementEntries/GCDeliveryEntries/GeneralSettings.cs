using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.GCDeliveryEntries;
public sealed unsafe class GeneralSettings : InventoryManagemenrBase
{
    public override string Name { get; } = "大國防聯軍 - 一般設定";

    public override void Draw()
    {
        ImGui.Checkbox("啟用自動籌備交換", ref C.AutoGCContinuation);
        ImGui.Indent();
        ImGuiEx.TextWrapped($"""
            啟用專家交付續行後：
            - 插件會自動使用可用的軍票，從已設定的兌換清單中購買物品。
            - 若兌換清單為空，則只會購買探險幣。
            
            當軍票花費完畢後：
            - 專家交付會自動繼續。
            - 這個流程會重複，直到沒有符合條件的物品可交付，或軍票耗盡為止。
            """);
        ImGui.Unindent();
    }
}