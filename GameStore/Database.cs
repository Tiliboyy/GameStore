using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Exiled.API.Features;
using JetBrains.Annotations;
using LiteDB;
using MEC;
using PlayerRoles;

namespace GameStore;

public static class GameStoreDatabase
{
    
    public static IEnumerator<float> SentHint(Player player, string message, float duration)
    {
        yield return Timing.WaitUntilFalse(() => player.HasHint && player.CurrentHint != null && !player.CurrentHint.Content.Contains("<size=69></size>"));
        player.ShowHint(message, duration);
        yield return 0;
    }

    public static LiteDatabase db = new(Path.Combine(Paths.Configs, "Gamestore/GameStore.db"));

    public class DatabasePlayer
    {
        public string _id { get; set; }
        public float Money { get; set; }
        
        public string Nickname { get; set; }
    }

    public static class Database
    {
        public static void CreatePlayers()
        {
            var players = db.GetCollection<DatabasePlayer>("players");

            if (!players.Exists(x => true)) players.EnsureIndex(x => x._id);
        }
        
        public static void AddPlayer(Player player)
        {
            if (player == null) return;
            if (player.DoNotTrack) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            
            if (players.FindOne(x => x._id == playerID) != null)
            {
                var udbplayer = players.FindOne(x => x._id == playerID);
                udbplayer.Nickname = player.Nickname;
                players.Update(udbplayer);
                return;
            }

            players.Insert(new DatabasePlayer
            {
                _id = playerID,
                Money = 0,
                Nickname = player.Nickname
            });
            var dbplayer = players.FindOne(x => x._id == playerID);
            players.Update(dbplayer);
        }
        
        public static void BuyItem(Player player, Structs.ItemPrice item)
        {
            if (player.DoNotTrack) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer == null) return;
            dbplayer.Money -= item.Price;
            OnSpendingMoney(player, item.Price);
            if (item.IsAmmo)
                foreach (var items in item.AmmoTypes)
                    player.AddAmmo(items.Key, items.Value);
            else
                foreach (var items in item.ItemTypes)
                    player.AddItem(items);

            player.Broadcast(3, Plugin.Instance.Translation.BoughtItemBroadcast.Replace("(item)", item.Name));
            players.Update(dbplayer);
        }

        public static bool CanRemoveMoneyFromPlayer(Player player, float reward)
        {
            if (player == null) return false;
            if (player.DoNotTrack) return false;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null) return dbplayer.Money >= reward;

            return false;
        }

        public static void AddMoneyToPlayer(Player player, float money)
        {
            if (player == null) return ;
            if (player.DoNotTrack || money == 0) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

            if (dbplayer == null) return;
            player.SendHintWhenNone
            (
                Plugin.Instance.Translation.AddMoneyHintText.Replace(
                    "(money)", 
                    money.ToString(CultureInfo.InvariantCulture)), 
                2
            );
            dbplayer.Money += money;
            if (dbplayer.Money < 0) dbplayer.Money = 0;
            players.Update(dbplayer);
        }
        public static void AddRewardToPlayer(Player player, Structs.Reward reward)
        {
            if (player == null) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id != null && x._id == playerID);

            if (dbplayer == null) return;
            
            if (reward.Money.ContainsKey(player.Role.Type))
            {
                if (player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.ContainsKey(reward.Name))
                {
                    int amount = player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name];
                    if (amount >= reward.MaxPerRound  && reward.MaxPerRound != -1)
                    {
                        return;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.Add(reward.Name, 1);
                }
                if(reward.Money[player.Role.Type] == 0) return;
                dbplayer.Money += reward.Money[player.Role.Type] * Plugin.MoneyMuliplier;
                OnGainingMoney(player,reward.Money[player.Role.Type] * Plugin.MoneyMuliplier);
                player.SendHintWhenNone
                    (Plugin.Instance.Translation.AddMoneyHintText.Replace("(money)", (reward.Money[player.Role.Type] * Plugin.MoneyMuliplier).ToString(CultureInfo.InvariantCulture)), 2);
                
            }else if (reward.Money.ContainsKey(RoleTypeId.None))
            {
                
                if (player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.ContainsKey(reward.Name))
                {
                    int amount = player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name];
                    if (amount >= reward.MaxPerRound  && reward.MaxPerRound != -1)
                    {
                        return;
                    }

                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit[reward.Name]++;
                }
                else
                {
                    player.GameObject.GetComponent<GameStoreComponent>().rewardlimit.Add(reward.Name, 1);
                }
                dbplayer.Money += reward.Money[RoleTypeId.None] * Plugin.MoneyMuliplier;
                OnGainingMoney(player,reward.Money[RoleTypeId.None] * Plugin.MoneyMuliplier);
                player.SendHintWhenNone
                (
                    Plugin.Instance.Translation.AddMoneyHintText.Replace(
                        "(money)", 
                        (reward.Money[RoleTypeId.None] * Plugin.MoneyMuliplier).ToString(CultureInfo.InvariantCulture)), 
                    2
                );
            }
            else
            {
                return;
            }
            if (dbplayer.Money < 0) dbplayer.Money = 0;
            if (dbplayer.Money > Plugin.Instance.Config.MoneyLimit && Plugin.Instance.Config.EnableLimit) 
                dbplayer.Money = Plugin.Instance.Config.MoneyLimit;
        
            players.Update(dbplayer);
        }

        public static string GetLeaderboard()
        {
            var players = db.GetCollection<DatabasePlayer>("players");
            var e = players.FindAll().OrderByDescending(p => p.Money).Take(10).ToList();
            int i = 1;
            var str = "\n";
            foreach (var player in e)
            {
                if (player.Nickname == null)
                {
                    str += $"\n[{i}] {player._id}: {player.Money} {Plugin.Instance.Translation.CurrencyName}";
                }
                else
                {
                    str += $"\n[{i}] {player.Nickname}: {player.Money} {Plugin.Instance.Translation.CurrencyName}";
                }

                i++;
            }
            return str;
        }

        public static float GetPlayerMoney(Player player)
        {
            if (player.DoNotTrack) return 0;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null)
                return dbplayer.Money;
            return 0;
        }
        [UsedImplicitly]
        public static float GetMoneyFromSteam64ID(string steam64id)
        {
            var playerID = steam64id.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null)
                return dbplayer.Money;
            return 0;
        }
        [UsedImplicitly]
        public static string GetNicknameFromSteam64ID(string steam64id)
        {
            var playerID = steam64id.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");

            var dbplayer = players.FindOne(x => x._id == playerID);
            
            return dbplayer != null ? dbplayer.Nickname : "None";
        }

        public static void RemovePlayer(Player player)
        {
            if (player == null) return;
            var playerID = player.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbplayer = players.FindOne(x => x._id == playerID);

            if (dbplayer != null) players.Delete(dbplayer._id);
        }
    }

    private static void OnGainingMoney(Player player ,float amount)
    {
        if (!player.GameObject.TryGetComponent<GameStoreComponent>(out var gameStoreComponent)) return;
        
        gameStoreComponent.LifeGainedMoney += amount;
        gameStoreComponent.RoundGainedMoney += amount;
        Log.Info(gameStoreComponent.LifeGainedMoney);
        Log.Info(gameStoreComponent.RoundGainedMoney);

    }
    private static void OnSpendingMoney(Player player ,float amount)
    {
        if (!player.GameObject.TryGetComponent<GameStoreComponent>(out var gameStoreComponent)) return;
        
        gameStoreComponent.LifeSpentMoney += amount;
        gameStoreComponent.RoundSpentMoney += amount;
        Log.Info(gameStoreComponent.LifeSpentMoney);
        Log.Info(gameStoreComponent.RoundSpentMoney);
    }
}