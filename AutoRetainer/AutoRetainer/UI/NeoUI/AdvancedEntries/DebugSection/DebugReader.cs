using AutoRetainer.Internal;
using AutoRetainer.Modules.Voyage.Readers;
using AutoRetainer.Scheduler.Tasks;
using AutoRetainer.UiHelpers;
using ECommons.UIHelpers.AtkReaderImplementations;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace AutoRetainer.UI.NeoUI.AdvancedEntries.DebugSection;

internal unsafe class DebugReader : DebugSectionBase
{
    public override void Draw()
    {
        {
            if(TryGetAddonByName<AtkUnitBase>("部隊軍票商店", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderFreeCompanyCreditShop(a);
                ImGuiEx.Text($"""
                    Rank: {reader.FCRank}
                    Credits: {reader.Credits}
                    Count: {reader.Count}
                    """);
                for(var i = 0; i < reader.Count; i++)
                {
                    var x = reader.Listings[i];
                    ImGuiEx.Text($"{x}");
                    if(ImGuiEx.HoveredAndClicked()) new FreeCompanyCreditShop(a).Buy(0);
                    var amount = Math.Floor((float)reader.Credits / (float)(x.Price));
                }

                if(ImGui.Button("Run task")) TaskRecursivelyBuyFuel.Enqueue();
            }
        }

        {
            if(TryGetAddonByName<AtkUnitBase>("僱員名單", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderRetainerList(a);
                foreach(var x in reader.Retainers)
                {
                    ImGuiEx.Text($"{x.Name}/act {x.IsActive}/gil {x.Gil}/lvl {x.Level}/inv {x.Inventory}");
                }
            }
        }
        {
            if(TryGetAddonByName<AtkUnitBase>("RetainerItemTransferList", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderRetainerItemTransferList(a);
                foreach(var r in reader.Items)
                {
                    ImGuiEx.Text($"物品 {r.ItemID}，高品質 = {r.IsHQ}");
                }
            }
        }
        {
            if(TryGetAddonByName<AtkUnitBase>("AirShipExploration", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderAirShipExploration(a);
                ImGuiEx.Text($"距離：{reader.Distance}");
                ImGuiEx.Text($"燃料：{reader.Fuel}");
                foreach(var r in reader.Destinations)
                {
                    ImGuiEx.Text($"Destination {r.NameFull}, rank={r.RequiredRank}, status={r.StatusFlag}, canBeSelected={r.CanBeSelected}");
                }
            }
        }
        {
            if(TryGetAddonByName<AtkUnitBase>("SubmarineExplorationMapSelect", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderSubmarineExplorationMapSelect(a);
                ImGuiEx.Text($"目前階級：{reader.SubmarineRank}");
                foreach(var r in reader.Maps)
                {
                    ImGuiEx.Text($"地圖 {r.Name}，階級={r.RequiredRank}");
                }
            }
        }
        {
            if(TryGetAddonByName<AtkUnitBase>("SelectString", out var a) && IsAddonReady(a))
            {
                var reader = new ReaderSelectString(a);
                foreach(var r in reader.Entries)
                {
                    ImGuiEx.Text($"{r.Text}");
                }
            }
        }
    }
}
