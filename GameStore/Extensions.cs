using System;
using System.Linq;
using Exiled.API.Features;
using JetBrains.Annotations;
using MEC;
using PlayerRoles;

namespace GameStore;

public static class Extensions
{
    public static void GameStoreRewardPlayer(this Player player, Structs.Reward reward)
    {
        GameStoreDatabase.Database.AddRewardToPlayer(player, reward);
    }
    public static void GameStoreMoneyPlayer(this Player player, float money)
    {
        GameStoreDatabase.Database.AddMoneyToPlayer(player, money);
    }
    public static void SendHintWhenNone(this Player player, string message, float duration)
    {
        if (player != null) Timing.RunCoroutine(GameStoreDatabase.HintWaitUntilFalse(player, message, duration));
    }
    public static string BuyItemFromName(this Player player, string Name)
    {
        var list = Plugin.Instance.Config.Categorys.OrderBy(category1 => category1.id);
                bool foundwithrole = false;
                bool notexisting = false;
                bool nomoney = false;
                bool notfullinventory = false;
                foreach (var category1 in list)
                {
                    var itemlist = category1.Items.OrderBy(price => price.Id);
                    foreach (var items in itemlist)
                    {
                        if(!string.Equals(items.Name, Name, StringComparison.CurrentCultureIgnoreCase)) continue;
                        notexisting = true;
                        string num = $"{category1.id}{items.Id}";
                        if (!int.TryParse(num, out var result))
                        {
                            continue;
                        }
                        if (!items.Roles.Contains(player.Role.Type) && !items.Roles.Contains(RoleTypeId.None) || player.IsScp)
                        {
                            continue;
                        }
                        foundwithrole = true;
                        if (player.Items.Count >= 8)
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
                        return Plugin.Instance.Translation.Boughtitem.Replace("(itemname)", items.Name)
                            .Replace("(itemprice)", items.Price.ToString());
                    }
                }

                if (!notexisting)
                {
                    return Plugin.Instance.Translation.Categorydoesnotexist;
                }
                if (!foundwithrole)
                {
                    return Plugin.Instance.Translation.WrongeRole;
                }
                

                if (!notfullinventory)
                {
                    return Plugin.Instance.Translation.Fullinventory;
                }

                return !nomoney ? Plugin.Instance.Translation.Cantafford : Plugin.Instance.Translation.Maxamountreached;

    }

}