using System;
using System.Globalization;
using System.Linq;
using Exiled.API.Features;
using MEC;
using Microsoft.CSharp.RuntimeBinder;
using PlayerRoles;

namespace GameStore;

public static class Extensions
{

    public static void SendHintWhenNone(this Player player, string message, float duration)
    {
        if (player != null) Timing.RunCoroutine(GameStoreDatabase.HintWaitUntilFalse(player, message, duration));
    }
    public static void SetMoney(this Player player, float money)
    {
        if (player == null) return ;
        if (player.DoNotTrack || money == 0) return;
        var playerID = player.RawUserId.Split('@')[0];
        var players = GameStoreDatabase.db.GetCollection<GameStoreDatabase.DatabasePlayer>("players");
        var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

        if (dbplayer == null) return;
        player.SendHintWhenNone
        (
            Plugin.Instance.Translation.SetMoneyHintText.Replace(
                "(money)", 
                money.ToString(CultureInfo.InvariantCulture)), 
            2
        );
        dbplayer.Money = money;
        if (dbplayer.Money < 0) dbplayer.Money = 0;
        players.Update(dbplayer);    
    }

    public static float GetMoney(this Player player)
    {
        return GameStoreDatabase.Database.GetPlayerMoney(player);
    }
    
    public static void GiveReward(this Player player, Structs.Reward reward)
    {
        GameStoreDatabase.Database.AddRewardToPlayer(player, reward);
    }
    
    public static void GiveMoney(this Player player, float money)
    {
        GameStoreDatabase.Database.AddMoneyToPlayer(player, money);
    }
    
    public static string GetAvailableCategories(this Player player)
    {
        var list = Plugin.Instance.Config.Categorys.OrderBy(category => category.id).ToList();
        var categories = list;
        if (Plugin.Instance.Config.ShowOnlyAvalibleItems)
            categories = list.Where(VARIABLE => VARIABLE.AllowedRoles.Contains(player.Role.Type) || VARIABLE.AllowedRoles.Contains(RoleTypeId.None) && !player.IsScp).ToList();
        var category = "";
        var i = 1;
        foreach (var categoryitem in categories)
        {
            category += $"\n[{i}] " + categoryitem.Name + "\n        " + categoryitem.Description;
            i++;
        }
        if (category == "")
            category = Plugin.Instance.Translation.NothingToBuy;
        return category;
    }
    
    public static string GetAvailableItems(this Player player,int category)
    {
        var items = "";
        var e = Plugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
        var categories = e;
        if (Plugin.Instance.Config.ShowOnlyAvalibleItems)
            categories = e.Where(VARIABLE => VARIABLE.AllowedRoles.Contains(player.Role.Type) || VARIABLE.AllowedRoles.Contains(RoleTypeId.None) && !player.IsScp).ToList();
        if (category > categories.Count)
            return Plugin.Instance.Translation.ItemDoesNotExist;
        var i = 0;
        foreach (var categorynum in categories)
        {
            i++;
            if(i != category) continue;
            items = categorynum.Items.Aggregate(items, (current, item) => current + $"\n[{item.Id}] {item.Name} - {item.Price} {Plugin.Instance.Translation.CurrencyName}");
        }

        return items;
    }
    
    public static string BuyItemFromName(this Player player, string Name)
    {
        if (!Round.IsStarted)
        {
            return Plugin.Instance.Translation.RoundNotStarted;
        }
        var list = Plugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
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

                if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(result))
                {
                    if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result] >=
                        items.Maxbuys)
                    {
                        continue;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems.Add(result, 1);
                }

                GameStoreDatabase.Database.BuyItem(player, items);
                return Plugin.Instance.Translation.BoughtItem.Replace("(itemname)", items.Name)
                    .Replace("(itemprice)", items.Price.ToString());
            }
        }

        if (!notexisting)
        {
            return Plugin.Instance.Translation.ItemDoesNotExist;
        }
        if (!foundwithrole)
        {
            return Plugin.Instance.Translation.WrongeRole;
        }
        if (!notfullinventory)
        {
            return Plugin.Instance.Translation.FullInventory;
        }

        return !nomoney ? Plugin.Instance.Translation.CantAfford : Plugin.Instance.Translation.MaxAmountReached;

    }
    
    public static string BuyItemFromId(this Player player, int category, int item)
    {
        if (!Round.IsStarted)
        {
            return Plugin.Instance.Translation.RoundNotStarted;
        }

        if (player == null)
        {
            return Plugin.Instance.Translation.ErrorMessage.Replace("(error)", "Player is Null");
        }

        var Categorys = Plugin.Instance.Config.Categorys.OrderBy(category1 => category1.id).ToList();
        if (Plugin.Instance.Config.ShowOnlyAvalibleItems)
        {
            Categorys = Categorys.Where(x => x.AllowedRoles.Contains(player.Role.Type) || x.AllowedRoles.Contains(RoleTypeId.None)).ToList();
        }
        if (category > Categorys.Count)
        {
            return Plugin.Instance.Translation.CategoryDoesNotExist;
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
                    Log.Error($"\nCategory ID: {category1.id} \nItem ID:{items.Id} is invalid");
                    return Plugin.Instance.Translation.ErrorMessage.Replace("(error)", $"Category Id: {category1.id} or {items.Id} is Invalid");
                }

                if (category1.AllowedRoles == null)
                {
                    Log.Error("Allowed Roles is null!");
                    return Plugin.Instance.Translation.ErrorMessage.Replace("(error)", $"Allowed roles from {category1.Name} is null");
                }
                if (!category1.AllowedRoles.Contains(player.Role.Type) && !category1.AllowedRoles.Contains(RoleTypeId.None) || player.IsScp)
                {
                    return Plugin.Instance.Translation.WrongeRole;
                }
                if (player.Items.Count >= 8 && items.NoInventoryCheck == false)
                {
                    return Plugin.Instance.Translation.FullInventory;
                }

                if (!GameStoreDatabase.Database.CanRemoveMoneyFromPlayer(player, items.Price))
                {
                    return Plugin.Instance.Translation.CantAfford;
                }

                if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems.ContainsKey(result))
                {

                    if (player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result] >=
                        items.Maxbuys)
                    {
                        return Plugin.Instance.Translation.MaxAmountReached;
                    }
                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems[result]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().boughtitems.Add(result, 1);
                }
                GameStoreDatabase.Database.BuyItem(player, items);
                return Plugin.Instance.Translation.BoughtItem.Replace("(itemname)", items.Name)
                    .Replace("(itemprice)", items.Price.ToString());
            }
            break;
        }

        return Plugin.Instance.Translation.ItemDoesNotExist;


    }

}