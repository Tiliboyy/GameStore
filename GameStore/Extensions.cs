using Core.Features.Data.Enums;
using Core.Features.Extensions;
using System;
using System.Globalization;
using System.Linq;
using Exiled.API.Features;
using GameStore.Components;
using GameStore.Configs;
using MEC;
using PlayerRoles;

namespace GameStore;

public static class Extensions
{
    
    public static bool SetMoney(this Player player, float money)
    {
        if (player == null) return false;
        if (player.DoNotTrack) return false;
        var playerID = player.RawUserId.Split('@')[0];
        var players = GameStoreDatabase.db.GetCollection<GameStoreDatabase.DatabasePlayer>("players");
        var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

        if (dbplayer == null) return false;
        dbplayer.Money = money;
        if (dbplayer.Money < 0) dbplayer.Money = 0;
        players.Update(dbplayer);
        return true;
    }

    public static float GetMoney(this Player player)
    {
        return GameStoreDatabase.Database.GetPlayerMoney(player);
    }
    
    public static void GiveReward(this Player player, Structs.Reward reward)
    {
        GameStoreDatabase.Database.AddRewardToPlayer(player, reward);
    }
    
    public static void GiveMoney(this Player player, int money)
    {
        GameStoreDatabase.Database.AddMoneyToPlayer(player, money);
    }
    
    public static string GetAvailableCategories(this Player player, bool ShowAll = false)
    {
        var list = GameStorePlugin.Instance.Config.Categorys.OrderBy(category => category.id).ToList();
        var categories = list;
        if (GameStorePlugin.Instance.Config.ShowOnlyAvalibleItems && !ShowAll)
            categories = list.Where(VARIABLE => VARIABLE.AllowedRoles.Contains(player.Role.Type) || VARIABLE.AllowedRoles.Contains(RoleTypeId.None) && !player.IsScp).ToList();
        var category = $"";;
        var i = 1;
        foreach (var categoryitem in categories)
        {
            category += $"\n[{i}] " + categoryitem.Name + "\n        " + categoryitem.Description;
            i++;
        }
        if (category == "")
            category = GameStorePlugin.Instance.Translation.NothingToBuy;
        return category + "";
    }
    
    public static string GetAvailableItems(this Player player,int category)
    {
        var items = $"";
        var e = GameStorePlugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
        var categories = e;
        if (GameStorePlugin.Instance.Config.ShowOnlyAvalibleItems)
            categories = e.Where(VARIABLE => VARIABLE.AllowedRoles.Contains(player.Role.Type) || VARIABLE.AllowedRoles.Contains(RoleTypeId.None) && !player.IsScp).ToList();
        if (category > categories.Count)
            return GameStorePlugin.Instance.Translation.ItemDoesNotExist;
        var i = 0;
        foreach (var categorynum in categories)
        {
            i++;
            if(i != category) continue;
            items = categorynum.Items.Aggregate(items, (current, item) => current + $"\n[{item.Id}] {item.Name} - {item.Price} {GameStorePlugin.Instance.Translation.CurrencyName}");
        }
        
        return items + "";
    }
    
    public static string BuyItemFromName(this Player player, string Name)
    {
        if (!Round.IsStarted)
        {
            return GameStorePlugin.Instance.Translation.RoundNotStarted;
        }
        var list = GameStorePlugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
        bool foundwithrole = false;
        bool notexisting = false;
        bool nomoney = false;
        bool notfullinventory = false;
        foreach (var category1 in list)
        {
            var itemlist = category1.Items.OrderBy(price => price.Id);
            bool itembytype = Enum.TryParse(Name, out ItemType itemType);
            foreach (var items in itemlist)
            {
                if (itembytype)
                {
                    if (!items.ItemTypes.Contains(itemType)) continue;
                }
                else
                {
                    if(items.Name.Replace(" ", "").ToLower() != Name.Replace(" ", "").ToLower()) continue;
                }
                notexisting = true;
                string num = $"{category1.id}{items.Id}";
                if (!int.TryParse(num, out var result))
                {
                    continue;
                }
                if (!category1.AllowedRoles.Contains(player.Role.Type) && !category1.AllowedRoles.Contains(RoleTypeId.None) || player.IsScp)
                {
                    continue;
                }
                foundwithrole = true;
                if (player.Items.Count >= 8 && items.NoInventoryCheck == false)
                {
                    continue;
                }

                notfullinventory = true;


                if (!GameStoreDatabase.Database.CanRemoveMoneyFromPlayer(player, items.Price))
                {
                    continue;
                }

                nomoney = true;

                if (player.GameObject.GetComponent<GameStoreComponent>().BoughtItems.ContainsKey(result))
                {
                    if (player.GameObject.GetComponent<GameStoreComponent>().BoughtItems[result] >=
                        items.Maxbuys)
                    {
                        continue;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().BoughtItems[result]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().BoughtItems.Add(result, 1);
                }

                GameStoreDatabase.Database.BuyItem(player, items);
                return GameStorePlugin.Instance.Translation.BoughtItem.Replace("(itemname)", items.Name)
                    .Replace("(itemprice)", items.Price.ToString());
            }
        }

        if (!notexisting)
        {
            return GameStorePlugin.Instance.Translation.ItemDoesNotExist;
        }
        if (!foundwithrole)
        {
            return GameStorePlugin.Instance.Translation.WrongeRole;
        }
        if (!notfullinventory)
        {
            return GameStorePlugin.Instance.Translation.FullInventory;
        }

        return !nomoney ? GameStorePlugin.Instance.Translation.CantAfford : GameStorePlugin.Instance.Translation.MaxAmountReached;

    }
    
    public static string BuyItemFromId(this Player player, int category, int item, bool ShowAll = false)
    {
        if (!Round.IsStarted)
        {
            return GameStorePlugin.Instance.Translation.RoundNotStarted;
        }

        if (player == null)
        {
            return GameStorePlugin.Instance.Translation.ErrorMessage.Replace("(error)", "Player is Null");
        }

        var Categorys = GameStorePlugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
        if (GameStorePlugin.Instance.Config.ShowOnlyAvalibleItems)
        {
            Categorys = Categorys.Where(x => x.AllowedRoles.Contains(player.Role.Type) || x.AllowedRoles.Contains(RoleTypeId.None)).ToList();
        }
        if (category > Categorys.Count)
        {
            return GameStorePlugin.Instance.Translation.CategoryDoesNotExist;
        }

        var list = Categorys;
        var i = 0;
        foreach (var category1 in list)
        {
            i++;
            if (i != category) continue;
            var itemlist = category1.Items.OrderBy(price => price.Id).ToList();
            foreach (var items in itemlist.Where(items => item == items.Id))
            {
                var num = $"{category1.id}{items.Id}";
                if (!int.TryParse(num, out var result))
                {
                    Log.Warn($"\nCategory ID: {category1.id} \nItem ID:{items.Id} is invalid");
                    return GameStorePlugin.Instance.Translation.ErrorMessage.Replace("(error)", $"Category Id: {category1.id} or {items.Id} is Invalid");
                }

                if (category1.AllowedRoles == null)
                {
                    Log.Error("Allowed Roles is null!");
                    return GameStorePlugin.Instance.Translation.ErrorMessage.Replace("(error)", $"Allowed roles from {category1.Name} is null");
                }
                if (!category1.AllowedRoles.Contains(player.Role.Type) && !category1.AllowedRoles.Contains(RoleTypeId.None) || player.IsScp)
                {
                    return GameStorePlugin.Instance.Translation.WrongeRole;
                }
                if (player.IsInventoryFull && items.NoInventoryCheck == false)
                {
                    return GameStorePlugin.Instance.Translation.FullInventory;
                }

                if (!GameStoreDatabase.Database.CanRemoveMoneyFromPlayer(player, items.Price))
                {
                    return GameStorePlugin.Instance.Translation.CantAfford;
                }

                if (player.GameObject.GetComponent<GameStoreComponent>().BoughtItems.ContainsKey(result))
                {

                    if (player.GameObject.GetComponent<GameStoreComponent>().BoughtItems[result] >=
                        items.Maxbuys)
                    {
                        return GameStorePlugin.Instance.Translation.MaxAmountReached;
                    }
                    player.GameObject.GetComponent<GameStoreComponent>().BoughtItems[result]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().BoughtItems.Add(result, 1);
                }
                GameStoreDatabase.Database.BuyItem(player, items);
                return GameStorePlugin.Instance.Translation.BoughtItem.Replace("(itemname)", items.Name)
                    .Replace("(itemprice)", items.Price.ToString());
            }
            break;
        }

        return GameStorePlugin.Instance.Translation.ItemDoesNotExist;


    }

}