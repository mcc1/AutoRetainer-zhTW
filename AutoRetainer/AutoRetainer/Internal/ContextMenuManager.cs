using Dalamud.Game.Gui.ContextMenu;
using Dalamud.Game.Text.SeStringHandling;
using ECommons.ChatMethods;
using ECommons.ExcelServices;
using ECommons.EzContextMenu;
using ECommons.Interop;
using Lumina.Excel.Sheets;
using UIColor = ECommons.ChatMethods.UIColor;

namespace AutoRetainer.Internal;

internal unsafe class ContextMenuManager
{
    private SeString Prefix = new SeStringBuilder().AddUiForeground(" ", 539).Build();

    public ContextMenuManager()
    {
        ContextMenuPrefixRemover.Initialize();
        Svc.ContextMenu.OnMenuOpened += ContextMenu_OnMenuOpened;
    }

    private void ContextMenu_OnMenuOpened(IMenuOpenedArgs args)
    {
        if(Data == null) return;
        if(!Data.GetIMSettings().IMEnableContextMenu) return;
        if(args.MenuType == ContextMenuType.Inventory && args.Target is MenuTargetInventory inv && inv.TargetItem != null)
        {
            var id = inv.TargetItem.Value.ItemId % 1_000_000;
            if(id != 0 && inv.TargetItem.Value.ItemId < 2_000_000)
            {
                if(Data.GetIMSettings(true).IMProtectList.Contains(id))
                {
                    args.AddMenuItem(new MenuItem()
                    {
                        Name = new SeStringBuilder().Append(Prefix).AddText("= 物品已受到保護 =").Build(),
                        OnClicked = (a) =>
                        {
                            if(IsKeyPressed([LimitedKeys.LeftControlKey, LimitedKeys.RightControlKey]) && IsKeyPressed([LimitedKeys.RightShiftKey, LimitedKeys.LeftShiftKey]))
                            {
                                var t = $"物品 {ExcelItemHelper.GetName(id)} 已從保護清單移除";
                                Notify.Success(t);
                                ChatPrinter.Red("[AutoRetainer] " + t);
                                Data.GetIMSettings(true).IMProtectList.Remove(id);
                            }
                            else
                            {
                                Notify.Error($"點擊時按住 CTRL+SHIFT 以移除此物品的保護");
                            }
                        }
                    }.RemovePrefix());
                }
                else
                {
                    var data = Svc.Data.GetExcelSheet<Item>().GetRow(id);
                    if(Data.GetIMSettings(true).IMAutoVendorSoft.Contains(id))
                    {
                        args.AddMenuItem(new MenuItem()
                        {
                            Name = new SeStringBuilder().Append(Prefix).AddUiForeground("- Remove from Quick Venture sell list", (ushort)UIColor.Orange).Build(),
                            OnClicked = (a) =>
                            {
                                Data.GetIMSettings(true).IMAutoVendorSoft.Remove(id);
                                Notify.Info($"物品 {ExcelItemHelper.GetName(id)} 已從快速探險出售清單移除");
                            }
                        }.RemovePrefix());
                    }
                    else if(data.PriceLow > 0)
                    {
                        args.AddMenuItem(new MenuItem()
                        {
                            Name = new SeStringBuilder().Append(Prefix).AddUiForeground("+ Add to Quick Venture sell list", (ushort)UIColor.Yellow).Build(),
                            OnClicked = (a) =>
                            {
                                Data.GetIMSettings(true).IMAutoVendorHard.Remove(id);
                                Data.GetIMSettings(true).IMAutoVendorSoft.Add(id);
                                Notify.Success($"物品 {ExcelItemHelper.GetName(id)} 已加入快速探險出售清單");
                            }
                        }.RemovePrefix());
                    }

                    if(Data.GetIMSettings(true).IMAutoVendorHard.Contains(id))
                    {
                        args.AddMenuItem(new MenuItem()
                        {
                            Name = new SeStringBuilder().Append(Prefix).AddUiForeground("- Remove from Unconditional sell list", (ushort)UIColor.Orange).Build(),
                            OnClicked = (a) =>
                            {
                                Data.GetIMSettings(true).IMAutoVendorHard.Remove(id);
                                Notify.Success($"物品 {ExcelItemHelper.GetName(id)} 已從無條件出售清單移除");
                            }
                        }.RemovePrefix());
                    }
                    else if(data.PriceLow > 0)
                    {
                        args.AddMenuItem(new MenuItem()
                        {
                            Name = new SeStringBuilder().Append(Prefix).AddUiForeground("+ Add to Unconditional sell list", (ushort)UIColor.Yellow).Build(),
                            OnClicked = (a) =>
                            {
                                Data.GetIMSettings(true).IMAutoVendorSoft.Remove(id);
                                Data.GetIMSettings(true).IMAutoVendorHard.Add(id);
                                Notify.Success($"物品 {ExcelItemHelper.GetName(id)} 已加入無條件出售清單");
                            }
                        }.RemovePrefix());
                    }
                    args.AddMenuItem(new MenuItem()
                    {
                        Name = new SeStringBuilder().Append(Prefix).AddText("保護物品不受自動操作影響").Build(),
                        OnClicked = (a) =>
                        {
                            Data.GetIMSettings(true).IMAutoVendorHard.Remove(id);
                            Data.GetIMSettings(true).IMAutoVendorSoft.Remove(id);
                            Data.GetIMSettings(true).IMProtectList.Add(id);
                            Notify.Success($"{ExcelItemHelper.GetName(id)} 已加入保護清單");
                        }
                    }.RemovePrefix());
                }
            }
        }
    }

    public void Dispose()
    {
        Svc.ContextMenu.OnMenuOpened -= ContextMenu_OnMenuOpened;
    }
}
