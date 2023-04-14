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
    public struct Pay
    {
        public string TargetId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public class DatabasePlayer
    {
        public string _id { get; set; }
        public float Money { get; set; }
        
        public string Nickname { get; set; }

        public List<Pay> PayHistory { get; set; }
        
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
                udbplayer.PayHistory ??= new List<Pay>();
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

        public static bool CanPay(Player sender, float amount)
        {
            if (sender == null) return false;
            if (sender.DoNotTrack) return false;
            var senderid = sender.RawUserId.Split('@')[0];
            var players = db.GetCollection<DatabasePlayer>("players");
            var dbsender = players.FindOne(x => x._id != null && x._id == senderid);

            float iAmount = amount;
            if(dbsender.PayHistory == null) return !(iAmount > GameStorePlugin.Instance.Config.MaxDailyPayAmount);
            iAmount += dbsender.PayHistory.Where(pay => pay.Date.Date == DateTime.Today).Sum(pay => pay.Amount);
            return !(iAmount > GameStorePlugin.Instance.Config.MaxDailyPayAmount);
        }
        public static void PayToPlayer(Player sender, Player reciver, float amount)
        {
            if (sender == null || reciver == null) return;
            if (sender.DoNotTrack || reciver.DoNotTrack) return;
            if(!CanRemoveMoneyFromPlayer(sender, amount)) return;
            var senderid = sender.RawUserId.Split('@')[0];
            var reciverid = reciver.RawUserId.Split('@')[0];

            var players = db.GetCollection<DatabasePlayer>("players");

            var dbsender = players.FindOne(x => x._id != null && x._id == senderid);
            var dbreciver = players.FindOne(x => x._id != null && x._id == reciverid);
            if (dbsender == null || dbreciver == null) return;
            reciver.SendHintWhenNone
            (
                GameStorePlugin.Instance.Translation.PayMoneyHintText.Replace(
                    "(money)", 
                    amount.ToString(CultureInfo.InvariantCulture)).Replace("(player)", sender.Nickname), 
                2
            );
            dbsender.Money -= amount;
            dbreciver.Money += amount;
            if (dbsender.PayHistory == null)
            {
                dbsender.PayHistory = new List<Pay> { new(){Date = DateTime.Now, Amount = amount, TargetId = reciverid} };
            }
            else
            {
                dbsender.PayHistory.Add(new Pay(){Date = DateTime.Now, Amount = amount, TargetId = reciverid});
            }

            if (dbsender.Money < 0) dbsender.Money = 0;
            players.Update(dbsender);
            players.Update(dbreciver);

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

            player.Broadcast(3, GameStorePlugin.Instance.Translation.BoughtItemBroadcast.Replace("(item)", item.Name));
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
                GameStorePlugin.Instance.Translation.AddMoneyHintText.Replace(
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
                dbplayer.Money += reward.Money[player.Role.Type] * GameStorePlugin.MoneyMuliplier;
                OnGainingMoney(player,reward.Money[player.Role.Type] * GameStorePlugin.MoneyMuliplier);
                player.SendHintWhenNone
                    (GameStorePlugin.Instance.Translation.AddMoneyHintText.Replace("(money)", (reward.Money[player.Role.Type] * GameStorePlugin.MoneyMuliplier).ToString(CultureInfo.InvariantCulture)), 2);
                
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
                dbplayer.Money += reward.Money[RoleTypeId.None] * GameStorePlugin.MoneyMuliplier;
                OnGainingMoney(player,reward.Money[RoleTypeId.None] * GameStorePlugin.MoneyMuliplier);
                player.SendHintWhenNone
                (
                    GameStorePlugin.Instance.Translation.AddMoneyHintText.Replace(
                        "(money)", 
                        (reward.Money[RoleTypeId.None] * GameStorePlugin.MoneyMuliplier).ToString(CultureInfo.InvariantCulture)), 
                    2
                );
            }
            else
            {
                return;
            }
            if (dbplayer.Money < 0) dbplayer.Money = 0;
            if (dbplayer.Money > GameStorePlugin.Instance.Config.MoneyLimit && GameStorePlugin.Instance.Config.EnableLimit) 
                dbplayer.Money = GameStorePlugin.Instance.Config.MoneyLimit;
        
            players.Update(dbplayer);
        }

        public static string GetLeaderboard(int amount = 10)
        {
            var players = db.GetCollection<DatabasePlayer>("players");
            var e = players.FindAll().OrderByDescending(p => p.Money).Take(amount).ToList();
            int i = 1;
            var str = "\n";
            foreach (var player in e)
            {
                if (player.Nickname == null)
                {
                    str += $"\n[{i}] {player._id}: {player.Money} {GameStorePlugin.Instance.Translation.CurrencyName}";
                }
                else
                {
                    str += $"\n[{i}] {player.Nickname}: {player.Money} {GameStorePlugin.Instance.Translation.CurrencyName}";
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

    }
    private static void OnSpendingMoney(Player player ,float amount)
    {
        if (!player.GameObject.TryGetComponent<GameStoreComponent>(out var gameStoreComponent)) return;
        
        gameStoreComponent.LifeSpentMoney += amount;
        gameStoreComponent.RoundSpentMoney += amount;
    }
}